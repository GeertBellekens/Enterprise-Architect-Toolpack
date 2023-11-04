using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal abstract class SAPAttribute<T> where T : UMLEA.AttributeWrapper
    {
        const string keyTagName = "Key";
        internal T wrappedAttribute { get; private set; }
        public string name
        {
            get => this.wrappedAttribute.name;
            set => this.wrappedAttribute.name = value;
        }
        public string notes
        {
            get => this.wrappedAttribute.notes;
            set => this.wrappedAttribute.notes = value;
        }
        public string key
        {
            get => this.getStringProperty(keyTagName);
            set => this.setStringProperty(keyTagName, value);
        }
        protected SAPAttribute(ISAPElement owner , string name, string key, string stereotype)
        {
            //check if this attribute already exists
            this.wrappedAttribute = owner.elementWrapper.attributeWrappers.OfType<T>()
                                                        .Where(x => x.hasStereotype(stereotype)
                                                                    && x.taggedValues.Any(y => y.name == keyTagName
                                                                                            && y.tagValue.ToString() == key))
                                                        .FirstOrDefault();
            //create new attribute
            if (this.wrappedAttribute == null)
            {
                this.wrappedAttribute = owner.elementWrapper.addOwnedElement<T>(name);
                this.wrappedAttribute.addStereotype(stereotype);
                this.save();
            }

            bool dirty = false;
            //set the name if needed
            if (this.name != name)
            {
                this.name = name;
                dirty = true;
            }
            if (this.key != key)
            {
                this.key = key;
                dirty = true;
            }
            if (dirty)
            {
                this.save();
            }
        }
        internal SAPAttribute(T attribute)
        {
            this.wrappedAttribute = attribute;
        }

        public ISAPElement owner
        {
            get => SAPElementFactory.CreateSAPElement(this.wrappedAttribute.owner as UMLEA.ElementWrapper);
            set => this.wrappedAttribute.owner = value.elementWrapper;
        }

        public void save()
        {
            this.wrappedAttribute.save();
        }
        protected Q getLinkProperty<Q>(string tagName) where Q : class, UML.Extended.UMLItem
        {
            return this.wrappedAttribute.model.getItemFromGUID(this.getStringProperty(tagName)) as Q;
        }
        protected void setLinkProperty(string tagName, UML.Extended.UMLItem value)
        {
            this.setStringProperty(tagName, value?.uniqueID ?? string.Empty);
        }
        protected string getStringProperty(string tagName)
        {
            return this.wrappedAttribute.taggedValues
                .FirstOrDefault(x => x.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                                ?.tagValue?.ToString();
        }
        protected void setStringProperty(string tagName, string value)
        {
            var taggedValue = this.wrappedAttribute.addTaggedValue(tagName, value);
            taggedValue.save();
        }
        protected bool getBoolProperty(string tagName)
        {
            return "True".Equals(this.wrappedAttribute.taggedValues
                .FirstOrDefault(x => x.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                                ?.tagValue?.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
        protected void setBoolProperty(string tagName, bool value)
        {
            var boolString = value ? "True" : "False";
            this.setStringProperty(tagName, boolString);
        }
        public static T getExistingAttribute(ISAPElement owner,  string stereotype, string name)
        {
            if (owner == null) return null;
            var attributeToWrap = owner.elementWrapper.attributeWrappers.OfType<T>()
                                                            .FirstOrDefault(x => x.hasStereotype(stereotype)
                                                                        && x.name == name);
            return attributeToWrap;
        }
    }
}
