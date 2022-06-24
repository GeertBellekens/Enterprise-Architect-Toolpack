using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class BOPFBusinessObject: SAPElement<UMLEA.Component>
    {
        const string stereotypeName = "BOPF_businessObject";

        public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
            : base(name, package, stereotypeName, key)
        {
        }
        const string objectCategoryTagName = "Object Category";
        public string objectCategory
        {
            get => this.getStringProperty(objectCategoryTagName);
            set => this.setStringProperty(objectCategoryTagName, value);
        }

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key);
        }


    }
}
