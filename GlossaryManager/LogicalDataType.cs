using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using EAAddinFramework.Databases.Strategy;
using EAAddinFramework.Databases;

namespace GlossaryManager
{
    /// <summary>
    /// Description of Class1.
    /// </summary>
    public class LogicalDatatype
    {
        internal UML.Classes.Kernel.DataType wrappedDatatype { get; set; }
        public LogicalDatatype(UML.Classes.Kernel.DataType wrappedDatatype)
        {
            this.wrappedDatatype = wrappedDatatype;
        }
        public static string stereoType { get { return "EDD_LogicalDatatype"; } }
        public string GUID
        {
            get { return this.wrappedDatatype.uniqueID; }
        }
        public string name
        {
            get { return this.wrappedDatatype.name; }
        }
        public DatabaseFramework.BaseDataType getBaseDatatype(string databaseType)
        {
            //check if multiple technical mappings are defined.
            string technicalMapping = ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("technical mapping")?.eaStringValue;
            string baseDataTypeName = EAAddinFramework.Utilities.KeyValuePairsHelper.getValueForKey(databaseType, technicalMapping);
            //technical mapping can be either "databasetype1=datatype;databasetype2=datatype" or simply "datatype" (in case this is valid for all database types)
            if (string.IsNullOrEmpty(baseDataTypeName))
                baseDataTypeName = technicalMapping;
            var baseDatatypes = DatabaseFactory.getBaseDataTypes(databaseType, (TSF_EA.Model)this.wrappedDatatype.model);
            if (baseDatatypes.ContainsKey(baseDataTypeName))
                return baseDatatypes[baseDataTypeName];
            else
                return null;
        }
        /// <summary>
        /// gets all the logical datatype in the model
        /// </summary>
        /// <param name="model">the model</param>
        /// <returns>a list of logical datatypes found in the model</returns>
        public static IEnumerable<LogicalDatatype> getAllLogicalDatatypes(TSF_EA.Model model)
        {
            var logicalDatatypes = new List<LogicalDatatype>();
            var sqlGetLogicalDatatypes = @"select o.[Object_ID] from t_object o
                                           where o.Stereotype = 'EDD_LogicalDatatype'
                                           order by o.name";
            foreach (var datatype in model.getElementWrappersByQuery(sqlGetLogicalDatatypes).OfType<UML.Classes.Kernel.DataType>())
            {
                logicalDatatypes.Add(new LogicalDatatype(datatype));
            }
            return logicalDatatypes;
        }
    }
}
