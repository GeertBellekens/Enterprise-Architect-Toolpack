using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Xml.Schema;

namespace EAValidator
{
    internal class FileCheckGroup : CheckGroup
    {
        private DirectoryInfo directory { get; set; }
        public FileCheckGroup(DirectoryInfo directory, EAValidatorSettings settings, TSF_EA.Model model):base(settings, model)
        {
            this.directory = directory;
        }
        public override string name => this.directory.Name;
        protected override List<CheckGroup> getSubGroups()
        {
            var tempSubGroups  = new List<CheckGroup>();
            foreach (var subdirectory in this.directory.GetDirectories())
            {
                var newSubGroup = new FileCheckGroup(subdirectory, this.settings, this.model);
                if (newSubGroup.subItems.Any())
                {
                    tempSubGroups.Add(newSubGroup);
                }
            }
            return tempSubGroups;
        }
        protected override List<Check> GetChecks()
        {
            var tempChecks = new List<Check>();
            // Get files from given directory                
            foreach (var file in this.directory.GetFiles("*.xml"))
            {
                // add new check
                try
                {
                    var check = new FileCheck (file.FullName, this, this.settings, this.model);
                    tempChecks.Add(check);

                }
                catch (XmlSchemaValidationException e)
                {
                    EAAddinFramework.Utilities.EAOutputLogger.log(this.model, this.settings.outputName, e.Message, 0, EAAddinFramework.Utilities.LogTypeEnum.error);
                }
            }
            //order by checkID
            tempChecks = tempChecks.OrderBy(x => x.CheckId).ToList();
            //return
            return tempChecks;
        }

    }
}
