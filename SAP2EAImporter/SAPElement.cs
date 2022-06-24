using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    abstract class SAPElement<T>
        where T : UMLEA.ElementWrapper
    {
        const string keyTagName = "Key";
        internal T wrappedElement { get; set; }

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
        protected SAPElement(string name, UML.Classes.Kernel.Package package, string stereotypeName): this(name, package, stereotypeName, string.Empty){}
        protected SAPElement(string name, UML.Classes.Kernel.Namespace owner, string stereotypeName, string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                //get element based on key
                var sqlGetData = $@"select o.Object_ID from t_object o
                            inner join t_objectproperties tv on tv.Object_ID = o.Object_ID
								                            and tv.Property = '{keyTagName}'
                            where tv.Value = '{key}'";
                //get element with same name 
                var existingElements = ((UMLEA.Model)owner.model).getElementWrappersByQuery(sqlGetData).OfType<T>();
                //first get the one in the given package
                this.wrappedElement = existingElements.FirstOrDefault(x => x.stereotypes.Any(y => y.name == stereotypeName)
                                                                     || x.owningPackage.uniqueID == owner.uniqueID);
                //if not found here, search in other packages
                if (this.wrappedElement == null)
                {
                    this.wrappedElement = existingElements.FirstOrDefault(x => x.stereotypes.Any(y => y.name == stereotypeName));
                }
                if (this.wrappedElement != null)
                { 
                    //set package and name
                    this.wrappedElement.owner = owner;
                    this.name = name;
                }
            }
            if (this.wrappedElement == null)
            {
                // Does an element with given name and stereotype exist?
                this.wrappedElement = owner.ownedElements.ToList().
                                OfType<T>().
                                FirstOrDefault(x => x.name == name
                                                && x.stereotypes.Any(y => y.name == stereotypeName));
                if (this.wrappedElement == null)
                {
                    // Create the element in EA
                    this.wrappedElement = ((UMLEA.ElementWrapper)owner).addOwnedElement<T>(name);

                    // Add the stereotype to the element.
                    this.wrappedElement.setStereotype("SAP::" + stereotypeName);
                    this.save();
                }
                if (!string.IsNullOrEmpty(key))
                {
                    this.key = key;
                }
            }
        }
        public SAPElement()
        {
            //default empty constructor
        }

        public void save()
        {
            this.wrappedElement.save();
        }

    }
}
