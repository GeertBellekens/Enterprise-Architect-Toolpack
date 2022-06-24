using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class CompositeRole : Role
    {
        const string stereotypeName = "SAP_compositeRole";

        public CompositeRole(string elementName, UML.Classes.Kernel.Package package)
            : base(elementName, package, stereotypeName)
        {
        }

        public void addSingleRole(SingleRole singleRole)
        {
            // Check if the aggregation relationship exists already.
            if (!this.wrappedElement.relationships
                .OfType<UMLEA.ConnectorWrapper>()
                .Any(x => x.stereotypes.Any(y => y.name == "SAP_aggregation")
                    && x.target.name == singleRole.name))
            {
                //If the relationship does not exist, create one
                var newRelation = this.wrappedElement.model.factory.createNewElement<UMLEA.Association>(this.wrappedElement, "");
                newRelation.addStereotype("SAP_aggregation");
                newRelation.target = singleRole.wrappedElement;
                newRelation.sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.shared;
                newRelation.save();
            }
            
        }
    }
}
