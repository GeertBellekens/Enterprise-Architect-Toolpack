using EAAddinFramework.Databases;
using System.Collections.Generic;
using System.Linq;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{
    public class LogicalDatatype
    {
        internal UML.Classes.Kernel.DataType wrappedDatatype { get; set; }
        public LogicalDatatype(UML.Classes.Kernel.DataType wrappedDatatype)
        {
            this.wrappedDatatype = wrappedDatatype;
        }
        public static string stereoType => "EDD_LogicalDatatype";
        public string GUID => this.wrappedDatatype.uniqueID;
        public string name => this.wrappedDatatype.name;
        public int? defaultSize
        {
            get
            {
                var defaultSizeTag = ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("default size");
                int foundDefault;
                if (defaultSizeTag != null && int.TryParse(defaultSizeTag.eaStringValue, out foundDefault))
                {
                    return foundDefault;
                }
                else
                {
                    return null;
                }
            }
        }
        public int? defaultPrecision
        {
            get
            {
                var defaultPrecisionTag = ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("default precision");
                int foundDefault;
                if (defaultPrecisionTag != null && int.TryParse(defaultPrecisionTag.eaStringValue, out foundDefault))
                {
                    return foundDefault;
                }
                else
                {
                    return null;
                }
            }
        }
        public string defaultInitialValue => ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("default initial value")?.eaStringValue;
        public string defaultFormat => ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("default format")?.eaStringValue;

        public DatabaseFramework.BaseDataType getBaseDatatype(string databaseType)
        {
            //check if multiple technical mappings are defined.
            string technicalMapping = ((TSF_EA.ElementWrapper)this.wrappedDatatype).getTaggedValue("technical mapping")?.eaStringValue;
            string baseDataTypeName = EAAddinFramework.Utilities.KeyValuePairsHelper.getValueForKey(databaseType, technicalMapping);
            //technical mapping can be either "databasetype1=datatype;databasetype2=datatype" or simply "datatype" (in case this is valid for all database types)
            if (string.IsNullOrEmpty(baseDataTypeName))
            {
                baseDataTypeName = technicalMapping;
            }

            var baseDatatypes = DatabaseFactory.getBaseDataTypes(databaseType, (TSF_EA.Model)this.wrappedDatatype.model);
            if (baseDatatypes.ContainsKey(baseDataTypeName))
            {
                return baseDatatypes[baseDataTypeName];
            }
            else
            {
                return null;
            }
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
