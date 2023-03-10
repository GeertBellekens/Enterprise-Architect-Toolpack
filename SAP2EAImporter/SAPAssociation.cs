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
        const string keyTagName = "Key";
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
        public string key
        {
            get => this.wrappedAssociation.taggedValues.FirstOrDefault(x => x.name == keyTagName)?.tagValue?.ToString();
            set
            {
                var taggedValue = this.wrappedAssociation.addTaggedValue(keyTagName, value);
                taggedValue.save();
            }
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

        internal SAPAssociation(BOPFNode source, BOPFNode target, String name, String associationKey)
        {
            //check if this association already exists
            this.wrappedAssociation = source.wrappedElement.getRelationships<UMLEA.Association>(true, false)
                                                        .Where(x => x.hasStereotype(stereotype)
                                                                    && x.taggedValues.Any(y => y.name == keyTagName
                                                                                            && y.tagValue.ToString() == associationKey)
                                                                    && x.target.Equals(target.wrappedElement))
                                                        .FirstOrDefault();
            //debug
            //foreach (var association in source.wrappedElement.getRelationships<UMLEA.Association>(true, false))
            //{
            //    if (association.hasStereotype(stereotype))
            //    {
            //        if (association.target == target.wrappedElement)
            //        {
            //            if (association.taggedValues.Any(y => y.name == keyTagName
            //                                             && y.tagValue.ToString() == associationKey))
            //            {
            //                this.wrappedAssociation = association;
            //            }
            //        }
            //    }
            //}
            //create new association
            if (this.wrappedAssociation == null)
            {
                this.wrappedAssociation = source.wrappedElement.addOwnedElement<UMLEA.Association>(name);
                this.wrappedAssociation.target = target.wrappedElement;
                this.wrappedAssociation.addStereotype(stereotype);
                this.save();
            }

            bool dirty = false;
            //set the name if needed
            if (this.name != name)
            {
                this.name = name;
                dirty = true;
            }
            if (this.key != associationKey)
            {
                this.key = associationKey;
                dirty = true;
            }
            if (dirty)
            {
                this.save();
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
