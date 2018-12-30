using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EAValidator
{
    public class EAValidatorSettings : EAAddinFramework.Utilities.AddinSettings
    {
        #region EA Add-in Framework settings
        protected override string configSubPath => @"\Bellekens\EAValidator\";
        protected override string defaultConfigFilePath => Assembly.GetExecutingAssembly().Location;
        #endregion

        #region EA Validator settings

        public string outputName
        {
            get => this.getValue("outputName");
            set => this.setValue("outputName", value);
        }

        public string ValidationChecks_Directory
        {
            get => this.getValue("ValidationChecks_Directory");
            set => this.setValue("ValidationChecks_Directory", value);
        }

        public string SearchTermInQueryToFindElements
        {
            get => this.getValue("SearchTermInQueryToFindElements");
            set => this.setValue("SearchTermInQueryToFindElements", value);
        }

        public string ElementGuidsInQueryToCheckFoundElements
        {
            get => this.getValue("ElementGuidsInQueryToCheckFoundElements");
            set => this.setValue("ElementGuidsInQueryToCheckFoundElements", value);
        }

        public string PackageBranch
        {
            get => this.getValue("PackageBranch");
            set => this.setValue("PackageBranch", value);
        }

        public string ValidationChecks_Documenttype
        {
            get => this.getValue("ValidationChecks_Documenttype");
            set => this.setValue("ValidationChecks_Documenttype", value);
        }

        public string XML_CheckMainNode
        {
            get => this.getValue("XML_CheckMainNode");
            set => this.setValue("XML_CheckMainNode", value);
        }

        public string AllowedRepositoryTypes
        {
            get => this.getValue("AllowedRepositoryTypes");
            set => this.setValue("AllowedRepositoryTypes", value);
        }

        public string SearchElementTypes
        {
            get => this.getValue("SearchElementTypes");
            set => this.setValue("SearchElementTypes", value);
        }

        public string QueryExcludeArchivedPackages
        {
            get => this.getValue("QueryExcludeArchivedPackages");
            set => this.setValue("QueryExcludeArchivedPackages", value);
        }
        public bool logToSystemOutput
        {
            get => this.getBooleanValue("logToSystemOutput");
            set => this.setBooleanValue("logToSystemOutput", value);
        }
        public bool excludeArchivedPackages
        {
            get => this.getBooleanValue("excludeArchivedPackages");
            set => this.setBooleanValue("excludeArchivedPackages", value);
        }

        #endregion
    }
}
