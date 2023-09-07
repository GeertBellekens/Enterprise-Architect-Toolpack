using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class BOPFTrigger : SAPConnector<UMLEA.Dependency>
    {

        const string createTagName = "Create";
        const string updateTagName = "Update";
        const string deleteTagName = "Delete";
        const string loadTagName = "Load";
        const string determineTagName = "Determine";
        public static string stereotype => "BOPF_determinationTriggeredBy";

        public BOPFTrigger(BOPFDetermination source, BOPFNode target)
        : base(source, target, "", "", stereotype)
        {

        }
        public bool triggersOnCreate
        {
            get => this.getBoolProperty(createTagName);
            set => this.setBoolProperty(createTagName, value);
        }
        public bool triggersOnUpdate
        {
            get => this.getBoolProperty(updateTagName);
            set => this.setBoolProperty(updateTagName, value);
        }
        public bool triggersOnDelete
        {
            get => this.getBoolProperty(deleteTagName);
            set => this.setBoolProperty(deleteTagName, value);
        }
        public bool triggersOnLoad
        {
            get => this.getBoolProperty(loadTagName);
            set => this.setBoolProperty(loadTagName, value);
        }
        public bool triggersOnDetermine
        {
            get => this.getBoolProperty(determineTagName);
            set => this.setBoolProperty(determineTagName, value);
        }

        internal BOPFTrigger(UMLEA.Dependency dependency) : base(dependency) { }

    }
}
