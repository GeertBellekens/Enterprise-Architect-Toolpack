using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    abstract class SAPElement<T> : ISAPElement
        where T : UMLEA.ElementWrapper
    {
        protected const string keyTagName = "Key";
        protected const string profileName = "SAP";
        private T _wrappedElement;
        internal T wrappedElement 
        { get => this._wrappedElement;
            set
            {
                this._wrappedElement = value;
                //add myself to the registry of SAPElements
                SAPElement<ElementWrapper>.elementRegistry[value.uniqueID] = this;
            }
        }
        public ElementWrapper elementWrapper 
        { 
            get => this.wrappedElement;
            set => this.wrappedElement = value as T;
        }

        public string notes
        {
            get => this.wrappedElement.notes;
            set => this.wrappedElement.notes = value;
        }

        public string name
        {
            get => this.wrappedElement.name;
            set => this.wrappedElement.name = value;
        }
        public string key
        {
            get => this.wrappedElement.taggedValues.FirstOrDefault(x => x.name == keyTagName)?.tagValue?.ToString();
            set
            {
                var taggedValue = this.wrappedElement.addTaggedValue(keyTagName, value);
                taggedValue.save();
            }
        }

        

        /// <summary>
        ///  
        /// </summary>
        /// <param name="name"> name</param>
        /// <param name="package"> the parent package</param>
        protected SAPElement(string name, UML.Classes.Kernel.Namespace package) : this(name, package, string.Empty) { }
        protected SAPElement(string name, UML.Classes.Kernel.Namespace package, string stereotypeName): this(name, package, stereotypeName, string.Empty){}
        protected SAPElement(string name, UML.Classes.Kernel.Namespace owner, string stereotypeName, string key, bool searchGlobal = false, bool searchPackage = false): this()
        {
            var fqStereo = string.Empty;
            if (!string.IsNullOrEmpty(stereotypeName))
            {
                //check if the stereotype is already fully qualified
                if (stereotypeName?.Contains("::") == true)
                {
                    fqStereo = stereotypeName;
                }
                else
                {
                    fqStereo = $"{profileName}::{stereotypeName}";
                }
            }
                        
            this.wrappedElement = this.getElement<T>(owner as UMLEA.ElementWrapper, name, key, fqStereo, searchGlobal, searchPackage);
            if (this.wrappedElement != null)
            {
                this.name = name;
                if (!string.IsNullOrEmpty(key))
                {
                    this.key = key;
                }
                if (!string.IsNullOrEmpty(fqStereo))
                {
                    this.wrappedElement.fqStereotype = fqStereo;
                }
                this.save();
            }
        }
        protected SAPElement(T wrappedElement) : this()
        {
            this.wrappedElement = wrappedElement;
        }
        public SAPElement()
        {        

        }
        public static Dictionary<string,ISAPElement> elementRegistry = new Dictionary<string, ISAPElement>();
        public static void clearRegistry()
        {
            SAPElement<ElementWrapper>.elementRegistry.Clear();
        }

        internal Q getElement<Q>(UMLEA.ElementWrapper owner, string name, string key, string fqStereo, bool searchGlobal = false, bool searchPackage = false  ) where Q : UMLEA.ElementWrapper
        {
            Q element = null;
            string stereotypeName = string.Empty;
            if (fqStereo.Contains("::"))
            {
                var splittedStereo = fqStereo.Split(new string[] { "::" }, StringSplitOptions.None);
                if (splittedStereo.Count() == 2)
                {
                    stereotypeName = splittedStereo[1];
                }
            }
            if (!string.IsNullOrEmpty(key))
            {
                //get element based on key
                var sqlGetData = $@"select o.Object_ID from t_object o
                            inner join t_objectproperties tv on tv.Object_ID = o.Object_ID
								                            and tv.Property = '{keyTagName}'
                            where tv.Value = '{key}'";
                var existingElements = ((UMLEA.Model)owner.model).getElementWrappersByQuery(sqlGetData).OfType<Q>();
                //first get the one in the given package
                element = existingElements.FirstOrDefault(x => (string.IsNullOrEmpty(fqStereo)
                                                                || x.fqStereotype.Equals(fqStereo, StringComparison.InvariantCultureIgnoreCase))
                                                                 && x.owningPackage.uniqueID == owner.uniqueID);
                //if not found here, search in other packages
                if (element == null)
                {
                    element = existingElements.FirstOrDefault(x => string.IsNullOrEmpty(fqStereo)
                                                                || x.fqStereotype.Equals(fqStereo, StringComparison.InvariantCultureIgnoreCase));
                                                                
                }
            }
            if (element == null)
            {
                // Does an element with given name and stereotype exist?
                if (owner != null)
                {
                    element = owner.ownedElements.ToList().
                                OfType<Q>().
                                FirstOrDefault(x => x.name == name
                                                && (string.IsNullOrEmpty(fqStereo)
                                                    || x.fqStereotype.Equals(fqStereo, StringComparison.InvariantCultureIgnoreCase)));
                }

                var sqlGetData = $@"select o.Object_ID from t_object o
                                        where o.Stereotype = '{stereotypeName}'
                                        and o.Name = '{name}'";

                //Do we find an element with that name, type and stereotype 
                if (element == null 
                      && owner != null 
                      && searchPackage)
                {
                    var package = owner as UMLEA.Package;
                    if (package == null)
                    {
                        package = owner.owningPackage as UMLEA.Package;
                    }
                    var sqlGetDataWithPackage = sqlGetData + $"{Environment.NewLine} and o.Package_ID in ({package.packageTreeIDString}) ";
                    element = ((UMLEA.Model)owner.model).getElementWrappersByQuery(sqlGetDataWithPackage).OfType<Q>().FirstOrDefault();
                }
                if (element == null && searchGlobal)
                {
                    element = ((UMLEA.Model)owner.model).getElementWrappersByQuery(sqlGetData).OfType<Q>().FirstOrDefault();
                }

                if (element == null)
                {
                    // Create the element in EA
                    element = ((UMLEA.ElementWrapper)owner).addOwnedElement<Q>(name);

                    // Add the stereotype to the element.
                    if (!string.IsNullOrEmpty(fqStereo))
                    {
                        element.fqStereotype = fqStereo;
                    }
                    element.save();
                }
            }
            return element;
        }


        public void save()
        {
            this.wrappedElement.save();
        }
        //property getters and setters
        protected string getStringProperty(string tagName)
        {
            return this.wrappedElement.taggedValues
                .FirstOrDefault(x => x.name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase))
                                ?.tagValue?.ToString();
        }
        protected void setStringProperty(string tagName, string value)
        {
            var taggedValue = this.wrappedElement.addTaggedValue(tagName, value ?? "");
            taggedValue.save();
        }
        protected bool getBoolProperty(string tagName)
        {
            if (bool.TryParse(this.getStringProperty(tagName), out bool boolValue))
                {
                return boolValue;
            }
            return false;

        }
        protected void setBoolProperty(string tagName, bool value)
        {
            this.setStringProperty(tagName, value.ToString());
        }
        protected Q getLinkProperty<Q>(string tagName) where Q : class, UML.Extended.UMLItem
        {
           return   this.wrappedElement.model.getItemFromGUID(this.getStringProperty(tagName)) as Q;
        }
        protected void setLinkProperty(string tagName, UML.Extended.UMLItem value)
        {
            this.setStringProperty(tagName, value?.uniqueID);
        }

        internal UMLEA.Attribute addOrUpdateAttribute(string attributeName, string key, string fqStereo, string notes, string datatype, int attributePos )
        {
            //check if this attribute exists
            var sqlGetData = $@"select a.ea_guid from t_attribute a
               left join t_attributetag tv on tv.ElementID = a.ID
                       and tv.Property = 'Key'
               where a.Object_ID = {this.wrappedElement.id}
                ";
            if (!string.IsNullOrEmpty(key))
            {
                sqlGetData += $" and tv.Value = '{key}' ";
            }
            else
            {
                sqlGetData += $" and a.name = '{attributeName}' ";
            }


            var attribute = this.wrappedElement.EAModel.getAttributesByQuery(sqlGetData).FirstOrDefault();
            //create new
            if (attribute == null)
            {
                attribute = this.wrappedElement.addOwnedElement<UMLEA.Attribute>(attributeName);
            }
            //set properties
            attribute.notes = notes;
            if (!string.IsNullOrEmpty(fqStereo))
            {
                attribute.setStereotype(fqStereo);
            }
            attribute.type = attribute.EAModel.factory.createPrimitiveType(datatype);
            attribute.position = attributePos;
            attribute.save();
            return attribute;
        }

        public virtual void formatDiagrams()
        {
            //default empty implementation
        }
    }
}
