using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class BOPFActionValidationTrigger : SAPConnector<UMLEA.Dependency>
    {
        public static string stereotype => "BOPF_actionValidationTriggeredBy";

        const string nodeCategoryTagName = "NodeCategory";

        public BOPFActionValidationTrigger(BOPFValidation source, BOPFAction target)
        : base(source, target, "", "", stereotype){}
        internal BOPFActionValidationTrigger(UMLEA.Dependency dependency) : base(dependency) { }
        public string nodeCategory
        {
            get => this.getStringProperty(nodeCategoryTagName);
            set => this.setStringProperty(nodeCategoryTagName, value);
        }

    }
}
