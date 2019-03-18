using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace ERXImporter
{
    public class ERXImporter
    {
        private string fileName { get; set; }
        private string connectionString
        {
            get
            {
                var builder = new SqlConnectionStringBuilder();
                builder["Server"] = @"DESKTOP-LCVMLKT\SQLEXPRESS";
                builder["integrated Security"] = true;
                builder["Initial Catalog"] = "ERX";
                return builder.ConnectionString;
            }
        }
        public ERXImporter(string fileName)
        {
            this.fileName = fileName;
        }
        private List<ErxImportTable> parseErx()
        {
            var importTables = new List<ErxImportTable>();
            bool startdll = false;
            bool startImports = false;
            string ddl = string.Empty;
            var imports = new StringBuilder();
            string tableName = string.Empty;
            foreach (var line in File.ReadAllLines(this.fileName))
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
                        if (ddlLine.EndsWith("+"))
                        {
                            //fix syntax error 
                            ddlLine.Replace("+", ";");
                        }
                        ddl += line;
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

        public string import()
        {
            var errors = new StringBuilder();
            var importTables = this.parseErx();
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
                    catch(Exception e)
                    {
                        if (!e.Message.Contains("There is already an object named"))
                        {
                            //skip this one
                            errors.Append($"{Environment.NewLine}Error on ddl for table '{importTable.tableName}' {Environment.NewLine}{e.Message}"
                                + $"{Environment.NewLine}SQL Statement: {importTable.imports}");
                            continue;
                        }
                        //if the table already exists we don't care and we continue
                    }

                    //execute inserts
                    if (!string.IsNullOrEmpty(importTable.imports))
                    {
                        var importcommand = new SqlCommand(importTable.imports, connection);
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

    }
}
