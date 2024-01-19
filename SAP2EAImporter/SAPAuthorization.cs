using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class SAPAuthorization : SAPElement<UMLEA.InstanceSpecification>
    {
        public static string stereotype => "SAP_authorization";
        public SAPAuthorization(string name, SingleRole owningRole, SAPAuthorizationObject authorizationObject)
        {
            //first find out if an element the the given name, stereotype and classifiername exists under the owning role
            // Does an element with given name and stereotype exist?
            this.wrappedElement = owningRole.wrappedElement.ownedElements.ToList().
                            OfType<UMLEA.InstanceSpecification>().
                            FirstOrDefault(x => x.name == name
                                            && x.stereotypes.Any(y => y.name == stereotype)
                                            && x.classifier != null
                                            && x.classifier.name.Equals(authorizationObject.name, StringComparison.InvariantCultureIgnoreCase));
            if (this.wrappedElement == null)
            {
                // Create the element in EA
                this.wrappedElement = owningRole.wrappedElement.addOwnedElement<UMLEA.InstanceSpecification>(name);
                // Add the stereotype to the element.
                this.wrappedElement.fqStereotype = stereotype;
                //set the authorizationObject
                this.authorizationObject = authorizationObject;
                //save 
                this.save();
            }
            
        }
        public SAPAuthorization(UMLEA.InstanceSpecification instance) : base(instance) { }

        private SAPAuthorizationObject _authorizationObject;
        public SAPAuthorizationObject authorizationObject
        {
            get
            {
                if (this._authorizationObject == null)
                {
                    if (this.wrappedElement.classifier != null)
                    {
                        this._authorizationObject = new SAPAuthorizationObject(this.wrappedElement.classifier.name, this.wrappedElement.classifier.owningPackage);
                    }
                }
                return this._authorizationObject;
            }
            set
            {
                this._authorizationObject = value;
                this.wrappedElement.classifier = value.wrappedElement;
            }
        }

        public Dictionary<string, string> authorizationFields
        {
            //TODO get
            set
            {
                var runstateString = string.Empty;
                //build runstate string
                foreach(var key in value.Keys)
                {
                    runstateString += $"@VAR;Variable={key};Value={value[key]};Op==;@ENDVAR;";
                }

                // Add the runstate to the EA element wrappedElement
                this.wrappedElement.runState = runstateString; 
            }
        }
    }
}
