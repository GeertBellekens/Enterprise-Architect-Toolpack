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
        const string resolvingNodeTagName= "Resolving Node";
        const string categoryTagName = "Category";
        const string associationClassTagName = "Association Class";
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
            get => this.getStringProperty(keyTagName);
            set => this.setStringProperty(keyTagName, value);
        }
        public string sourceMultiplicity
        {
            get => this.wrappedAssociation.sourceEnd.multiplicity.ToString();
            set => this.wrappedAssociation.sourceEnd.EAMultiplicity.EACardinality = value;
        }
        public string targetMultiplicity
        {
            get => this.wrappedAssociation.targetEnd.multiplicity.ToString();
            set => this.wrappedAssociation.targetEnd.EAMultiplicity.EACardinality = value;
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
        public string resolvingNode
        {
            get => this.getStringProperty(resolvingNodeTagName);
            set => this.setStringProperty(resolvingNodeTagName, value);
        }
        public string category
        {
            get => this.getStringProperty(categoryTagName);
            set => this.setStringProperty(categoryTagName, value);
        }
        public SAPClass associationClass
        {
            get => new SAPClass(this.getLinkProperty<UMLEA.Class>(associationClassTagName));
            set => this.setLinkProperty(associationClassTagName, value.wrappedElement);
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
        protected Q getLinkProperty<Q>(string tagName) where Q : class, UML.Extended.UMLItem
        {
            return this.wrappedAssociation.model.getItemFromGUID(this.getStringProperty(tagName)) as Q;
        }
        protected void setLinkProperty(string tagName, UML.Extended.UMLItem value)
        {
            this.setStringProperty(tagName, value.uniqueID);
        }
        protected string getStringProperty(string tagName)
        {
            return this.wrappedAssociation.taggedValues
                .FirstOrDefault(x => x.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                                ?.tagValue?.ToString();
        }
        protected void setStringProperty(string tagName, string value)
        {
            var taggedValue = this.wrappedAssociation.addTaggedValue(tagName, value);
            taggedValue.save();
        }
    }
}
