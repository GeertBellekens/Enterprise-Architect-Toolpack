using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPTable : SAPElement<UMLEA.Class>
    {
        public static string stereotype => "table";
        public static string profile = "EAUML";
        public static string fqStereo = $"{profile}::{stereotype}";

        //public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
        //    : base(name, package, stereotypeName, key)
        //{
        //}
        public SAPTable(string name, UML.Classes.Kernel.Namespace owner)
            : base(name, owner, fqStereo) 
        {
            this.wrappedElement.genType = "Hana";
            this.save();
        }
        public SAPTable(UMLEA.Class classElement) : base(classElement) { }
    }
}
