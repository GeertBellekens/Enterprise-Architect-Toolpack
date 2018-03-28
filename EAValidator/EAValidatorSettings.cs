using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace EAValidator
{
    public class EAValidatorSettings : EAAddinFramework.Utilities.AddinSettings
    {
        #region EA Add-in Framework settings
        protected override string configSubPath
        {
            get
            {
                return @"\Bellekens\EAValidator\";
            }
        }
        protected override string defaultConfigFilePath
        {
            get
            {
                return Assembly.GetExecutingAssembly().Location;
            }
        }
        #endregion

        #region EA Validator settings

        public string outputName
        {
            get
            {
                return this.getValue("outputName");
            }
            set
            {
                this.setValue("outputName", value);
            }
        }

        public string ValidationChecks_Directory
        {
            get
            {
                return this.getValue("ValidationChecks_Directory");
            }
            set
            {
                this.setValue("ValidationChecks_Directory", value);
            }
        }

        public string SearchTermInQueryToFindElements
        {
            get
            {
                return this.getValue("SearchTermInQueryToFindElements");
            }
            set
            {
                this.setValue("SearchTermInQueryToFindElements", value);
            }
        }

        public string ElementGuidsInQueryToCheckFoundElements
        {
            get
            {
                return this.getValue("ElementGuidsInQueryToCheckFoundElements");
            }
            set
            {
                this.setValue("ElementGuidsInQueryToCheckFoundElements", value);
            }
        }

        public string PackageBranch
        {
            get
            {
                return this.getValue("PackageBranch");
            }
            set
            {
                this.setValue("PackageBranch", value);
            }
        }

        public string ValidationChecks_Documenttype
        {
            get
            {
                return this.getValue("ValidationChecks_Documenttype");
            }
            set
            {
                this.setValue("ValidationChecks_Documenttype", value);
            }
        }

        public string XML_CheckMainNode
        {
            get
            {
                return this.getValue("XML_CheckMainNode");
            }
            set
            {
                this.setValue("XML_CheckMainNode", value);
            }
        }

        public string AllowedRepositoryTypes
        {
            get
            {
                return this.getValue("AllowedRepositoryTypes");
            }
            set
            {
                this.setValue("AllowedRepositoryTypes", value);
            }
        }

        public string SearchElementTypes
        {
            get
            {
                return this.getValue("SearchElementTypes");
            }
            set
            {
                this.setValue("SearchElementTypes", value);
            }
        }

        public string QueryExcludeArchivedPackages
        {
            get
            {
                return this.getValue("QueryExcludeArchivedPackages");
            }
            set
            {
                this.setValue("QueryExcludeArchivedPackages", value);
            }
        }

        #endregion
    }
}
