using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;


namespace SAP2EAImporter
{
    internal class BOPFAuthorizationCheck : SAPConnector<UMLEA.Association>
    {
        public static string stereotype => "BOPF_authorizationCheck";

        const string constraintName = "bopf_auth_field_mapping";

        public BOPFAuthorizationCheck(BOPFNode source, SAPAuthorizationObject target)
        : base(source, target, "", "", stereotype) { }
        internal BOPFAuthorizationCheck(UMLEA.Association association) : base(association) { }
        public string constraint
        {
            get => this.wrappedConnector.constraints
                                    .OfType<UMLEA.ConnectorConstraint>()
                                    .FirstOrDefault(x => x.name == constraintName)?.notes;
            set
            {
                var wrappedConstraint = this.wrappedConnector.constraints
                            .OfType<UMLEA.ConnectorConstraint>()
                            .FirstOrDefault(x => x.name == constraintName);
                if (wrappedConstraint == null)
                {
                    //add the constraint
                    wrappedConstraint = this.wrappedConnector.addConnectorConstraint(constraintName);
                }
                wrappedConstraint.notes = value;
                wrappedConstraint.save();
            }
        }
        public SAPAuthorizationObject authorizationObject => this.target as SAPAuthorizationObject;
        public BOPFNode node => this.source as BOPFNode;

    }
}
