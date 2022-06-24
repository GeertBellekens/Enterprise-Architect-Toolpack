using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class Authorization : SAPElement<UMLEA.InstanceSpecification>
    {
        const string stereotypeName = "SAP_authorization";
        public Authorization(string name, SingleRole owningRole, AuthorizationObject authorizationObject)
        {
            //first find out if an element the the given name, stereotype and classifiername exists under the owning role
            // Does an element with given name and stereotype exist?
            this.wrappedElement = owningRole.wrappedElement.ownedElements.ToList().
                            OfType<UMLEA.InstanceSpecification>().
                            FirstOrDefault(x => x.name == name
                                            && x.stereotypes.Any(y => y.name == stereotypeName)
                                            && x.classifier != null
                                            && x.classifier.name.Equals(authorizationObject.name, StringComparison.InvariantCultureIgnoreCase));
            if (this.wrappedElement == null)
            {
                // Create the element in EA
                this.wrappedElement = owningRole.wrappedElement.addOwnedElement<UMLEA.InstanceSpecification>(name);
                // Add the stereotype to the element.
                this.wrappedElement.setStereotype(stereotypeName);
                //set the authorizationObject
                this.authorizationObject = authorizationObject;
                //save 
                this.save();
            }
            
        }

        private AuthorizationObject _authorizationObject;
        public AuthorizationObject authorizationObject
        {
            get
            {
                if (this._authorizationObject == null)
                {
                    if (this.wrappedElement.classifier != null)
                    {
                        this._authorizationObject = new AuthorizationObject(this.wrappedElement.classifier.name, this.wrappedElement.classifier.owningPackage);
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
