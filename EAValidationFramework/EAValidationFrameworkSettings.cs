using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAValidationFramework
{
    public class EAValidationFrameworkSettings : EAAddinFramework.Utilities.AddinSettings
    {
        protected override string configSubPath
        {
            get
            {
                return @"\Bellekens\EAValidationFramework\";
            }
        }
        protected override string defaultConfigFilePath
        {
            get
            {
                return System.Reflection.Assembly.GetExecutingAssembly().Location;
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

        public string SearchTermInQuery
        {
            get
            {
                return this.getValue("SearchTermInQuery");
            }
            set
            {
                this.setValue("SearchTermInQuery", value);
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
    }
}
