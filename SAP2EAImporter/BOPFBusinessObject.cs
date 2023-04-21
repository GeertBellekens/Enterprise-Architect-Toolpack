using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class BOPFBusinessObject: SAPElement<UMLEA.Component>, BOPFNodeOwner
    {
        public static string stereotype => "BOPF_businessObject";

        public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
            : base(name, package, stereotype, key, true, true) { }
        public BOPFBusinessObject(UMLEA.Component component) : base(component) { }

        const string objectCategoryTagName = "Object Category";
        public string objectCategory
        {
            get => this.getStringProperty(objectCategoryTagName);
            set => this.setStringProperty(objectCategoryTagName, value);
        }

        public SAPElement<UMLEA.Component> element => this;

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key, true);
        }


    }
}
