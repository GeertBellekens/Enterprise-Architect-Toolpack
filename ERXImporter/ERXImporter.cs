using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace ERXImporter
{
    public class ERXImporter
    {
        private TSF_EA.Model _model;
        private TSF_EA.Model model
        {
            get
            {
                if (this._model == null)
                {
                    this._model = new TSF_EA.Model();
                }
                return _model;
            }
        }
        public TSF_EA.Package selectedPackage { get; private set; }
        public void selectPackage()
        {
            this.selectedPackage = this.model.getUserSelectedPackage() as TSF_EA.Package;
        }
        public List<Relation> relations { get; set; } = new List<Relation>();
        private string connectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder();
                builder["Server"] = Properties.Settings.Default.ERXServer;
                builder["integrated Security"] = true;
                builder["Initial Catalog"] = Properties.Settings.Default.ERXDatabase;
                return builder.ConnectionString;
            }
        }
        private string targetConnectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder();
                builder["Server"] = Properties.Settings.Default.CMSServer;
                builder["integrated Security"] = true;
                builder["Initial Catalog"] = Properties.Settings.Default.CMSDatabase; 
                return builder.ConnectionString;
            }
        }
        private List<ErxImportTable> parseErx(string fileName)
        {
            var importTables = new List<ErxImportTable>();
            bool startdll = false;
            bool startImports = false;
            string ddl = string.Empty;
            var imports = new StringBuilder();
            string tableName = string.Empty;
            foreach (var line in File.ReadAllLines(fileName))
            {
                if (line.StartsWith("/*"))
                {
                    if (!string.IsNullOrEmpty(ddl))
                    {
                        importTables.Add(new ErxImportTable(tableName, ddl, imports.ToString()));
                    }
                    startdll = true;
                    startImports = false;
                    tableName = string.Empty;
                    ddl = string.Empty;
                }
                else if (line.StartsWith("*/"))
                {
                    startImports = true;
                    startdll = false;
                    imports = new StringBuilder();
                }
                else
                {
                    if (startdll)
                    {
                        if (string.IsNullOrEmpty(tableName))
                        {
                            //get the table name from this string
                            var tableRegex = new Regex(@"(?:TABLE )(?<table>\w+)");
                            var tableMatch = tableRegex.Match(line);
                            if (tableMatch.Success)
                            {
                                tableName = tableMatch.Groups["table"].Value;
                            }
                        }
                        //it's a ddl line
                        if (!string.IsNullOrEmpty(ddl))
                        {
                            ddl += Environment.NewLine;
                        }
                        var ddlLine = line;
                        //fix BYTE datatype
                        if (tableName == "FONT" && ddlLine.Contains("BYTE"))
                        {
                            ddlLine = ddlLine.Replace("BYTE", "TINYINT");
                        }
                        //fix FK name column too smal
                        if (tableName == "REL_PHYS_PROP" && ddlLine.Contains("FOREIGN_KEY_NAME         VARCHAR(8)"))
                        {
                            ddlLine = ddlLine.Replace("FOREIGN_KEY_NAME         VARCHAR(8)", "FOREIGN_KEY_NAME         VARCHAR(255)");
                        }
                        //fix syntax error with "+"
                        if ((tableName == "DIAGRAM_OPTION" || tableName == "COLOR" || tableName == "DISPLAY")
                            && ddlLine.EndsWith("+"))
                        {
                            //fix syntax error 
                            ddlLine = ddlLine.Replace("+", ";");
                        }
                        ddl += ddlLine;
                    }
                    else if (startImports)
                    {
                        //it's an import line
                        if (imports.Length == 0)
                        {
                            //clear the table first
                            imports.Append($"truncate table {tableName}");
                        }
                        imports.Append(Environment.NewLine);
                        var importLine = line;
                        //escape single quotes
                        importLine = importLine.Replace("'", "''");
                        //replace double double quotes with a sing quote
                        importLine = importLine.Replace("int\"\"", "int'");
                        //replace double quotes with single quotes
                        importLine = importLine.Replace("\"", "'");
                        //check if importLine has a "," at the end. If so, cut it off
                        if (importLine.EndsWith(","))
                        {
                            importLine = importLine.Substring(0, importLine.Length - 1);
                        }
                        imports.Append($"insert into {tableName} values ({importLine})");
                    }
                }
            }
            //end of the lines. Check if we have to add the last table
            if (!string.IsNullOrEmpty(ddl))
            {
                importTables.Add(new ErxImportTable(tableName, ddl, imports.ToString()));
            }
            return importTables;
        }

        public void createRelations(string exportFileName)
        {
            //skip if selected package is not filled in
            if (this.selectedPackage == null) return;
            //first remove all dependencies between tables in the selected package
            this.deleteExistingDependencies();
            //then create them again
            foreach (var relation in this.relations)
            {
                //find source table
                var sourceTable = this.getTable(relation.fromTable);
                //find target table
                var targetTable = this.getTable(relation.toTable);
                //create relationship
                if (sourceTable != null &&  targetTable != null)
                {
                    var newDependency = this.model.factory.createNewElement<TSF_EA.Dependency>(sourceTable, relation.joinSpecification);
                    newDependency.target = targetTable;
                    newDependency.sourceEnd.name = relation.fromColumn;
                    newDependency.targetEnd.name = relation.toColumn;
                    newDependency.save();
                    relation.createdInEA = true;
                }
            }
            //write export content
            this.createOutputFile(exportFileName);
        }
        private void deleteExistingDependencies()
        {
            //skip if selected package is not filled in
            if (this.selectedPackage == null) return;
            var sqlGetDepedencies = "select c.Connector_ID from ((t_connector c                     "
                                    + " inner join t_object o on (c.Start_Object_ID = o.Object_ID   "
                                    + " 						and o.Stereotype = 'table'))        "
                                    + " inner join t_object o2 on (c.End_Object_ID = o2.Object_ID   "
                                    + " 						and o2.Stereotype = 'table'))       "
                                    + $" where c.Connector_Type = 'Dependency' " 
                                    + $" and o.Package_ID in ({this.selectedPackage.packageTreeIDString})   "
                                    + $" and o2.Package_ID in  ({this.selectedPackage.packageTreeIDString}) ";
            var dependencies = this.model.getRelationsByQuery(sqlGetDepedencies);
            foreach (var dependency in dependencies)
            {
                dependency.delete();
            }
        }
        private TSF_EA.ElementWrapper getTable(string TableName)
        {
            var sqlGetTable = "select o.Object_ID from t_object o " 
                            + " where o.Stereotype = 'table' "
                            + $" and o.Name = '{TableName}' "
                            + $" and  o.Package_ID in ({this.selectedPackage.packageTreeIDString})";
            return this.model.getElementWrappersByQuery(sqlGetTable).FirstOrDefault();
        }

        public string import(string fileName)
        {
            var errors = new StringBuilder();
            var importTables = this.parseErx(fileName);
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                foreach (var importTable in importTables)
                {
                    //execute create table
                    var ddlCommand = new SqlCommand(importTable.ddl, connection);
                    try
                    {
                        ddlCommand.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        if (!e.Message.Contains("There is already an object named"))
                        {
                            //skip this one
                            errors.Append($"{Environment.NewLine}Error on ddl for table '{importTable.tableName}' {Environment.NewLine}{e.Message}"
                                + $"{Environment.NewLine}SQL Statement: {importTable.ddl}");
                            continue;
                        }
                        //if the table already exists we don't care and we continue
                    }

                    //execute inserts
                    if (!string.IsNullOrEmpty(importTable.imports))
                    {
                       
                        var importcommand = new SqlCommand(importTable.imports, connection);
                        //set timeout to 3 minutes to avoid timeout issues
                        importcommand.CommandTimeout = 360;
                        try
                        {
                            importcommand.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            errors.Append($"{Environment.NewLine}Error on imports for table '{importTable.tableName}' {Environment.NewLine}{e.Message}"
                                + $"{Environment.NewLine}SQL Statement: {importTable.imports}");
                        }
                    }
                }
            }
            return errors.ToString();
        }
        public List<Relation> synchronizeForeignKeys(string exportFileName)
        {
            this.relations = new List<Relation>();
            var errors = new StringBuilder();
            var exportContent = new StringBuilder();
            using (var erxConnection = new SqlConnection(this.connectionString))
            {
                erxConnection.Open();
                var sqlGetRelations = @"select 
                                    ee.TABLE_NAME as FromTable, pn.DOMAINPV_STR_VAL as FromColumn , e2.TABLE_NAME as ToTable,pan.DOMAINPV_STR_VAL as ToColumn
                                     from ERWIN_ENTITY ee
                                    inner join ERWIN_ATTRIBUTE a on ee.ENTITY_ID = a.ENTITY_ID
                                    inner join DOMAIN_PROP_VALUE pn on pn.DOMAIN_ID = a.ATTRIBUTE_ID 
								                                    and pn.DOMAINPV_TYPE = 'CD_NAM'
                                    inner join DOMAIN_PROP_VALUE pnul on pnul.DOMAIN_ID = a.ATTRIBUTE_ID 
								                                    and pnul.DOMAINPV_TYPE = 'CD_NUL'
                                    inner join DOMAIN_PROP_VALUE pt on pt.DOMAIN_ID = a.ATTRIBUTE_ID 
								                                    and pt.DOMAINPV_TYPE = 'CD_TYP'
                                    inner join ERWIN_RELATIONSHIP r on a.RELATIONSHIP_ID = r.RELATIONSHIP_ID
                                    inner join ERWIN_ENTITY e2 on e2.ENTITY_ID = r.PARENT_ENTITY
                                    inner join ERWIN_ATTRIBUTE pa on a.PARENT_ATTRIBUTE = pa.ATTRIBUTE_ID
								                                    and pa.ENTITY_ID = e2.ENTITY_ID
                                    inner join DOMAIN_PROP_VALUE pan on pan.DOMAIN_ID = pa.ATTRIBUTE_ID 
								                                    and pan.DOMAINPV_TYPE = 'CD_NAM'
                                    where 1=1
                                    order by 1,2";

                //get the relations
                var getRelationsCommand = new SqlCommand(sqlGetRelations, erxConnection);
                var relationResults = getRelationsCommand.ExecuteReader();
                //connect to target database
                using (var targetConnection = new SqlConnection(this.targetConnectionString))
                {
                    targetConnection.Open();
                    //read the relations
                    while (relationResults.Read())
                    {
                        //get an applicable index
                        var relation = new Relation();
                        //add to list
                        relations.Add(relation);
                        //fill in details
                        relation.fromTable = relationResults["FromTable"].ToString();
                        relation.fromColumn = relationResults["FromColumn"].ToString();
                        relation.toTable = relationResults["ToTable"].ToString();
                        relation.toColumn = relationResults["ToColumn"].ToString();
                        relation.fromColumns = new List<string>();
                        relation.toColumns = new List<string>();

                        var indexName = string.Empty;
                        var indexesResult = this.getTargetIndexes(targetConnection, relation.toTable, relation.toColumn);
                        bool fKAdded = false;
                        SqlException lastException = null;
                        while (indexesResult.Read())
                        {
                            //find the appropriate columns in the source table
                            if (string.IsNullOrEmpty(indexName))
                            {
                                indexName = indexesResult["IndexName"].ToString();
                            }
                            var newIndex = indexesResult["IndexName"].ToString();
                            if (newIndex != indexName)
                            {
                                //try to add index
                                indexName = newIndex;
                                try
                                {
                                    this.addFK(relation, exportContent);
                                    fKAdded = true;
                                    break;
                                }
                                catch (SqlException e)
                                {
                                    lastException = e;
                                }
                                //star newt
                                relation.fromColumns = new List<string>();
                                relation.toColumns = new List<string>();
                            }
                            var indexColumn = indexesResult["ColumnName"].ToString();
                            relation.toColumns.Add(indexColumn);
                            if (indexColumn.Equals(relation.toColumn, StringComparison.InvariantCultureIgnoreCase))
                            {
                                //add the corresponding column
                                relation.fromColumns.Add(relation.fromColumn);
                            }
                            else
                            {
                                //add the columns with the same name
                                relation.fromColumns.Add(indexColumn);
                            }
                        }
                        //close datareader
                        indexesResult.Close();
                        if (!fKAdded
                            && !string.IsNullOrEmpty(indexName))
                        {
                            if (lastException != null)
                            {
                                this.processErrors(errors, exportContent,relation, lastException);
                            }
                            else
                            {
                                try
                                {
                                    //add the index
                                    this.addFK(relation, exportContent);

                                }
                                catch (SqlException e)
                                {
                                    this.processErrors(errors, exportContent, relation, e);
                                }
                            }
                        }
                        else
                        {
                            //add to errors
                            errors.AppendLine($"ERROR: no index found when adding relation {relation.fromTable}.{relation.fromColumn} => {relation.toTable}.{relation.toColumn}");
                            //add to export
                            relation.FKStatus = "Error: No index found when adding relation";
                        }
                    }
                }
            }
            //write export content
            this.createOutputFile(exportFileName);
            //return relations
            return relations;
        }

        private void processErrors(StringBuilder errors, StringBuilder exportContent, Relation relation, SqlException e)
        {
            //add to list of relations to be added "manually"
            if (e.Message.Contains("references invalid column"))
            {
                errors.AppendLine($"ERROR: Invalid column error when adding relation {relation.fromTable}.{relation.fromColumn} => {relation.toTable}.{relation.toColumn}:");
            }
            else
            {
                errors.AppendLine($"ERROR: Other error when adding relation {relation.fromTable}.{relation.fromColumn} => {relation.toTable}.{relation.toColumn}:");
            }
            //add the error
            errors.AppendLine(e.Message);
            //add to the export content
            relation.FKStatus = $"\"{e.Message}\"";
        }
        public void createOutputFile(string outputFileName)
        {
            var outputContent = new StringBuilder();
            //header line
            outputContent.AppendLine(Relation.CSVHeader);
            foreach (var relation in this.relations)
            {
                outputContent.AppendLine(relation.CSVLine);
            }
            //write export content
            var exportFile = new StreamWriter(outputFileName, false);
            exportFile.Write(outputContent.ToString());
            exportFile.Close();
        }

        private void addFK(Relation relation, StringBuilder exportContent)
        {
            using (var targetConnection = new SqlConnection(this.targetConnectionString))
            {
                targetConnection.Open();
                var sourceColumns = string.Join("], [", relation.fromColumns.ToArray());
                var targetColumns = string.Join("], [", relation.toColumns.ToArray());
                var sqlAddIndex = $"ALTER TABLE [{relation.fromTable}] ADD CONSTRAINT[FK_{relation.fromTable}_{relation.fromColumn}_{relation.toTable}] "
                                + $"FOREIGN KEY([{sourceColumns}]) REFERENCES [{relation.toTable}]([{targetColumns}]) ON DELETE No Action ON UPDATE No Action";
                var addIndexCommand = new SqlCommand(sqlAddIndex, targetConnection);
                addIndexCommand.ExecuteNonQuery();
                //add to log
                Logger.log($"Added FK between {relation.fromTable}.{relation.fromColumn} to {relation.toTable}.{relation.toColumn} with query {sqlAddIndex}" );
                //add to export content
                relation.FKStatus = "OK";
            }
        }
        private SqlDataReader getTargetIndexes(SqlConnection targetConnection, string tableName, string columnName)
        {
            var sqlGetIndexes = @"SELECT t.name as TableName, ind.name as IndexName, col.name as ColumnName
                                FROM sys.indexes ind 
                                INNER JOIN  sys.index_columns ic ON  ind.object_id = ic.object_id 
									                                and ind.index_id = ic.index_id 
                                INNER JOIN sys.columns col ON ic.object_id = col.object_id 
							                                and ic.column_id = col.column_id 
                                INNER JOIN sys.tables t ON ind.object_id = t.object_id 
                                inner join (select ind.object_id, ind.index_id 
			                                FROM sys.indexes ind 
			                                INNER JOIN  sys.index_columns ic ON  ind.object_id = ic.object_id 
												                                and ind.index_id = ic.index_id 
			                                INNER JOIN sys.columns col ON ic.object_id = col.object_id 
										                                and ic.column_id = col.column_id 
			                                INNER JOIN sys.tables t ON ind.object_id = t.object_id 
			                                where t.name = @tableName
			                                and col.name = @columnName
			                                and ind.is_unique = 1) idx on idx.object_id = ind.object_id
										                                and idx.index_id = ind.index_id
                                WHERE 1=1
                                ORDER BY 
                                t.name, ind.name, ic.key_ordinal";
            var targetCommand = new SqlCommand(sqlGetIndexes, targetConnection);
            targetCommand.Parameters.AddWithValue("@tableName", tableName);
            targetCommand.Parameters.AddWithValue("@columnName", columnName);
            return targetCommand.ExecuteReader();
        }
    }
}
