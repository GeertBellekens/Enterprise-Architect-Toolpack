using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{

    class FunctionModule : SAPElement<UMLEA.Class>
    {
        public static string stereotype => "SAP_functionModule";
        public FunctionModule(string name, UML.Classes.Kernel.Package package)
            : base(name, package, stereotype)
        {

        }
        public FunctionModule(UMLEA.Class element) : base(element) { }
        public void addRole(Role role, string category)
        {
            // Check if the aggregation relationship exists already.
            if (!this.wrappedElement.relationships
                .OfType<UMLEA.ConnectorWrapper>()
                .Any(x => x.stereotypes.Any(y => y.name == "SAP_assignment")
                    && x.target.name == role.name
                    && x.name.Equals(category, StringComparison.InvariantCultureIgnoreCase)))
            {
                //If the relationship does not exist, create one
                var newRelation = this.wrappedElement.model.factory.createNewElement<UMLEA.Dependency>(this.wrappedElement, category);
                newRelation.addStereotype("SAP_assignment");
                newRelation.target = role.wrappedElement;
                newRelation.save();
            }

        }

        internal void addParameter(string parameterName, string parameterDatatypeType, string parameterDirection)
        {
            if (!this.wrappedElement.embeddedElementWrappers
                    .Any(x=> x.name.Equals(parameterName, StringComparison.InvariantCultureIgnoreCase)))
            {
                //create new embeddedElement of type Port
                var portElement = this.wrappedElement.addOwnedElement<UMLEA.ElementWrapper>(parameterName, "Port");
                //TODO: add datatype and direction. Currently not supported
                //For datatype we need a datatype object to set the classifier.
                //For direction maybe a tagged value? in that case we might need a new stereotype
                portElement.save();
            }
        }
    }
}
