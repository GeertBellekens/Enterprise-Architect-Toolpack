using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    internal class ModelCheck : Check
    {
        protected override string xmlString => this.model.convertFromEANotes(this.element.notes, "TXT");

        protected override string checkName => this.element.name;

        private TSF_EA.ElementWrapper element { get; set; }

        public ModelCheck(TSF_EA.ElementWrapper element, CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model) : base(group, settings, model)
        {
            this.element = element;
            //load the contents from the the xml file
            this.loadXml();
        }
        public override void save()
        {
            using (StringWriter writer = new StringWriter())
            {
                this.xdoc.Save(writer);
                this.element.notes = writer.ToString();
                this.element.name = $"Rule {this.CheckId}: {this.CheckDescription}"; 
                this.element.save();
            }
        }
        public ModelCheck copy()
        {
            var newElementWrapper = ((TSF_EA.Package)this.element.owningPackage).addOwnedElement<TSF_EA.ElementWrapper>(this.name + "_Copy", "Artifact");
            //var newElementWrapper = this.model.factory.createNewElement<TSF_EA.ElementWrapper> (this.element.owner, this.name + "_Copy");
            if (newElementWrapper == null)
            {
                return null;
            }
            //copy notes
            newElementWrapper.notes = this.element.notes;
            //create new modelcheck
            return new ModelCheck(newElementWrapper, this.group, this.settings, this.model);
        }
    }
}
