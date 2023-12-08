using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPView : SAPTable
    {


        //public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
        //    : base(name, package, stereotypeName, key)
        //{
        //}
        public SAPView(string name, UML.Classes.Kernel.Namespace owner)
            : base(name, owner)
        {
            //set keyword to "view"
            this.wrappedElement.keywords = new List<string>() { "view"};
            this.save();
        }
        public SAPView(UMLEA.Class classElement) : base(classElement) { }
    }
}