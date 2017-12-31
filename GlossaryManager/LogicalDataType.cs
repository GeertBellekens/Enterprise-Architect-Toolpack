using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

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
    }
}
