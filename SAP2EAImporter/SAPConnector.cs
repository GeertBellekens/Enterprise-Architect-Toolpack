using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal abstract class SAPConnector<T> where T: UMLEA.ConnectorWrapper
    {
        const string keyTagName = "Key";
        internal T wrappedConnector { get; private set; }
        public string name
        {
            get => this.wrappedConnector.name;
            set => this.wrappedConnector.name = value;
        }
        public string notes
        {
            get => this.wrappedConnector.notes;
            set => this.wrappedConnector.notes = value;
        }
        public string key
        {
            get => this.getStringProperty(keyTagName);
            set => this.setStringProperty(keyTagName, value);
        }
        protected SAPConnector(ISAPElement source, ISAPElement target, string name, string connectorKey,string stereotype )
        {
            //check if this association already exists
            this.wrappedConnector = source.elementWrapper.getRelationships<T>(true, false)
                                                        .Where(x => x.hasStereotype(stereotype)
                                                                    && x.taggedValues.Any(y => y.name == keyTagName
                                                                                            && y.tagValue.ToString() == connectorKey)
                                                                    && x.target.Equals(target.elementWrapper))
                                                        .FirstOrDefault();
            //create new association
            if (this.wrappedConnector == null)
            {
                this.wrappedConnector = source.elementWrapper.addOwnedElement<T>(name);
                this.wrappedConnector.target = target.elementWrapper;
                this.wrappedConnector.addStereotype(stereotype);
                this.save();
            }

            bool dirty = false;
            //set the name if needed
            if (this.name != name)
            {
                this.name = name;
                dirty = true;
            }
            if (this.key != connectorKey)
            {
                this.key = connectorKey;
                dirty = true;
            }
            if (dirty)
            {
                this.save();
            }
        }
        internal SAPConnector(T connector)
        {
            this.wrappedConnector = connector;
        }

        public ISAPElement source
        {
            get => SAPElementFactory.CreateSAPElement(this.wrappedConnector.source as UMLEA.ElementWrapper);
            set => this.wrappedConnector.source = value.elementWrapper;
        }
        public ISAPElement target
        {
            get => SAPElementFactory.CreateSAPElement(this.wrappedConnector.target as UMLEA.ElementWrapper);
            set => this.wrappedConnector.target = value.elementWrapper;
        }
        public string sourceMultiplicity
        {
            get => this.wrappedConnector.sourceEnd.multiplicity.ToString();
            set => this.wrappedConnector.sourceEnd.EAMultiplicity.EACardinality = value;
        }
        public string targetMultiplicity
        {
            get => this.wrappedConnector.targetEnd.multiplicity.ToString();
            set => this.wrappedConnector.targetEnd.EAMultiplicity.EACardinality = value;
        }

        public void save()
        {
            this.wrappedConnector.save();
        }
        protected Q getLinkProperty<Q>(string tagName) where Q : class, UML.Extended.UMLItem
        {
            return this.wrappedConnector.model.getItemFromGUID(this.getStringProperty(tagName)) as Q;
        }
        protected void setLinkProperty(string tagName, UML.Extended.UMLItem value)
        {
            this.setStringProperty(tagName, value.uniqueID);
        }
        protected string getStringProperty(string tagName)
        {
            return this.wrappedConnector.taggedValues
                .FirstOrDefault(x => x.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                                ?.tagValue?.ToString();
        }
        protected void setStringProperty(string tagName, string value)
        {
            var taggedValue = this.wrappedConnector.addTaggedValue(tagName, value);
            taggedValue.save();
        }
        public static T getExistingConnector(ISAPElement source, ISAPElement target, string stereotype, string name)
        {
            if (source == null || target == null) return null;
            var connectorToWrap = source.elementWrapper.getRelationships<T>(true, false)
                                    .FirstOrDefault(x => x.hasStereotype(stereotype)
                                                    && x.name == name
                                                    && x.source.uniqueID == target.elementWrapper.uniqueID);
            return connectorToWrap;
        }
    }
}
