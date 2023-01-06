using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class SAPAssociation
    {
        public static string stereotype =>  "SAP_Association";
        internal UMLEA.Association wrappedAssociation { get; private set; }
        public string name
        {
            get => this.wrappedAssociation.name;
            set => this.wrappedAssociation.name = value;
        }
        public string notes
        {
            get => this.wrappedAssociation.notes;
            set => this.wrappedAssociation.notes = value;
        }
        public BOPFNode source
        {
            get => new BOPFNode(this.wrappedAssociation.source as UMLEA.Class);
            set => this.wrappedAssociation.target = value.wrappedElement;
        }
        public BOPFNode target
        {
            get => new BOPFNode(this.wrappedAssociation.target as UMLEA.Class);
            set => this.wrappedAssociation.source = value.wrappedElement;
        }

        internal SAPAssociation(BOPFNode source, BOPFNode target, String name)
        {
            //check if this association already exists
            this.wrappedAssociation = source.wrappedElement.getRelationships<UMLEA.Association>(true, false)
                                                        .Where(x => x.hasStereotype(stereotype)
                                                                    && x.name == name
                                                                    && x.target == target.wrappedElement)
                                                        .FirstOrDefault();
            //create new association
            if (this.wrappedAssociation == null)
            {
                this.wrappedAssociation = source.wrappedElement.addOwnedElement<UMLEA.Association>(name);
                this.wrappedAssociation.target = target.wrappedElement;
                this.wrappedAssociation.addStereotype(stereotype);
                this.wrappedAssociation.save();
            }
        }
        internal SAPAssociation(UMLEA.Association association)
        {
            this.wrappedAssociation = association;
        }
        public void save()
        {
            this.wrappedAssociation.save();
        }
    }
}
