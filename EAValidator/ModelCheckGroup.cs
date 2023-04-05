using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace EAValidator
{
    internal class ModelCheckGroup : CheckGroup
    {
        private TSF_EA.Package package { get; set; }
        public ModelCheckGroup(TSF_EA.Package package, EAValidatorSettings settings, TSF_EA.Model model) : base(settings, model)
        {
            this.package = package;
        }
        public override string name => this.package.name;

        protected override List<CheckGroup> getSubGroups()
        {
            var tempSubGroups = new List<CheckGroup>();
            foreach (var subPackage in this.package.nestedPackages
                                    .OfType<TSF_EA.Package>().ToList())
            {
                var newSubGroup = new ModelCheckGroup(subPackage, this.settings, this.model);
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
            // Get artifacts from the package
            foreach (var artifact in this.package.ownedElementWrappers.Where(x => x.subType == "Artifact"))
            {
                // add new check
                try
                {
                    var check = new ModelCheck(artifact, this, this.settings, this.model);
                    tempChecks.Add(check);

                }
                catch (XmlSchemaValidationException e)
                {
                    EAAddinFramework.Utilities.EAOutputLogger.log(this.model, this.settings.outputName, e.Message, 0, EAAddinFramework.Utilities.LogTypeEnum.error);
                }
            }
            //order by checkID
            tempChecks = tempChecks.OrderBy(x => x.CheckId).ToList();
            return tempChecks;
        }
    }
}
