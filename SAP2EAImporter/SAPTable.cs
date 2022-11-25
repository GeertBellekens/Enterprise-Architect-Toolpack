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
        const string stereotypeName = "EAUML::table";

        //public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
        //    : base(name, package, stereotypeName, key)
        //{
        //}
        public SAPTable(string name, UML.Classes.Kernel.Namespace owner)
            : base(name, owner, stereotypeName ) 
        {
            this.wrappedElement.genType = "Hana";
            this.save();
        }
        public SAPTable(UMLEA.Class classElement) : base(classElement) { }
    }
}
