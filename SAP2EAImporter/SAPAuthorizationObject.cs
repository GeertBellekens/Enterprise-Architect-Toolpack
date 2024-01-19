using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TSF.UmlToolingFramework.UML.Classes.Kernel;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class SAPAuthorizationObject: SAPElement<UMLEA.Class>
    {
        public static string stereotype => "SAP_authorizationObject";
        const string authorizationClassTagName = "AuthorizationClass";

        public string authorizationClass
        {
            get => this.wrappedElement.taggedValues.FirstOrDefault(x => x.name == authorizationClassTagName)?.tagValue?.ToString();

            set
            {
                var taggedValue = this.wrappedElement.addTaggedValue(authorizationClassTagName, value);
                taggedValue.save();
            }
        }

        
        /// <summary>
        ///  
        /// </summary>
        /// <param name="name"> name of the authorization object</param>
        /// <param name="package"> the parent package of the authorization object</param>
        public SAPAuthorizationObject(string name, UML.Classes.Kernel.Package package)
            :base(name, package, stereotype,String.Empty, true, true){}
        public SAPAuthorizationObject(UMLEA.Class element) : base(element) { }
        internal void addAuthorizationField(string name)
        {
            if (!this.wrappedElement.attributes.Any(x => x.name == name))
            {
                var attribute = this.wrappedElement.addOwnedElement<UMLEA.Attribute>(name);
                attribute.save();
            }
        }

    }

}
