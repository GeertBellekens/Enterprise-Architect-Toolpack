using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class RolePackage: SAPElement<UMLEA.Class>
    {
        public static string stereotype => "SAP_rolePackage";
        public RolePackage(string name, UML.Classes.Kernel.Package package)
            : base(name, package, stereotype){ }
        public RolePackage(UMLEA.Class element) : base(element) { }
        public void addRole(Role role)
        {
            // Check if the aggregation relationship exists already.
            if (!this.wrappedElement.relationships
                .OfType<UMLEA.ConnectorWrapper>()
                .Any(x => x.stereotypes.Any(y => y.name == "SAP_aggregation")
                    && x.target.name == role.name))
            {
                //If the relationship does not exist, create one
                var newRelation = this.wrappedElement.model.factory.createNewElement<UMLEA.Association>(this.wrappedElement, "");
                newRelation.addStereotype("SAP_aggregation");
                newRelation.target = role.wrappedElement;
                newRelation.sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.shared;
                newRelation.save();
            }

        }

    }
}
