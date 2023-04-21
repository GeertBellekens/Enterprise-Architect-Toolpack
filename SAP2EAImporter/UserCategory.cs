using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class UserCategory : SAPElement<UMLEA.Class>
    {
        public static string stereotype => "SAP_userCategory";
        public UserCategory(string name, UML.Classes.Kernel.Package package)
            : base(name, package, stereotype) { }

        public UserCategory(UMLEA.Class element) : base(element) { }
        public void addRolePackage(RolePackage rolePackage)
        {
            // Check if the aggregation relationship exists already.
            if (!this.wrappedElement.relationships
                .OfType<UML.Classes.Dependencies.Usage>()
                .OfType<UMLEA.ConnectorWrapper>()
                .Any(x => x.target.name == rolePackage.name
                       && x.target.stereotypes.Any(y => y.name == stereotype)))
            {
                //If the relationship does not exist, create one
                var newRelation = this.wrappedElement.model.factory.createNewElement<UML.Classes.Dependencies.Usage>(this.wrappedElement, "");
                newRelation.target = rolePackage.wrappedElement;
                newRelation.save();
            }

        }
    }
}
