using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public class EAValidatorSettings : EAAddinFramework.Utilities.AddinSettings
    {
        #region EA Add-in Framework settings
        protected override string addinName => "EAValidator";
        protected override string configSubPath => @"\Bellekens\EAValidator\";
        protected override string defaultConfigAssemblyFilePath => Assembly.GetExecutingAssembly().Location;
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

        public IEnumerable<RepositoryType> AllowedRepositoryTypes
        {
            get
            {
                var repositoryTypes = new List<RepositoryType>();
                foreach(var stringValue in this.getListValue("AllowedRepositoryTypes"))
                {
                    RepositoryType repositoryType;
                    if (Enum.TryParse(stringValue, out repositoryType))
                    {
                        repositoryTypes.Add(repositoryType);
                    }
                }
                return repositoryTypes;
            }
            set => this.setListValue("AllowedRepositoryTypes", value.Select(x => x.ToString()).ToList());
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
        public List<string> scopeElementTypes
        {
            get => this.getListValue("scopeElementTypes");
            set => this.setListValue("scopeElementTypes", value);
        }
        public List<string> scopeDiagramTypes
        {
            get => this.getListValue("scopeDiagramTypes");
            set => this.setListValue("scopeDiagramTypes", value);
        }


        #endregion
    }
}
