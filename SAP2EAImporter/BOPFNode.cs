using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class BOPFNode : SAPElement<UMLEA.Class>
    {
        const string stereotypeName = "BOPF_node";

        public BOPFNode(string name,BOPFBusinessObject owner, string key)
            : base(name, owner.wrappedElement, stereotypeName, key)
        {
            //set the owner
            this.owner = owner;
        }
        BOPFBusinessObject _owner;
        public BOPFBusinessObject owner
        {
            get => this._owner;
            set
            {

                var composition = this.compositionToOwner;
                this._owner = value;
                if (composition == null)
                {
                    composition = this.compositionToOwner;
                }
                //ceate new composition if needed
                if (composition == null)
                {
                    composition = this.owner.wrappedElement.addOwnedElement<UMLEA.Association>("");
                    composition.addStereotype("SAP_composition");
                }
                composition.sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.composite;
                composition.targetEnd.isNavigable = true;
                composition.target = this.wrappedElement;
                composition.source = value.wrappedElement;
                composition.save();

                //set ownership of wrapped element 
                this.wrappedElement.owner = value.wrappedElement;
            }
        }
        public UMLEA.Association compositionToOwner
        {
                 get => this.wrappedElement.getRelationships<UMLEA.Association>(false, true)
                                    .FirstOrDefault(x => x.hasStereotype("SAP_composition")
                                                    && x.source.uniqueID == this.owner?.wrappedElement.uniqueID);
        }
    }
}
