using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    class BOPFNode : SAPElement<UMLEA.Class>, BOPFNodeOwner
    {
        const string stereotypeName = "BOPF_node";

        public BOPFNode(string name, BOPFNodeOwner owner, string key)
            : base(name, (UML.Classes.Kernel.Namespace) owner.elementWrapper, stereotypeName, key)
        {
            //set the owner
            this.owner = owner;
        }
        public SAPElement<UMLEA.Class> element => this;

        BOPFNodeOwner _owner;
        public BOPFNodeOwner owner
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
                    composition = this.owner.elementWrapper.addOwnedElement<UMLEA.Association>("");
                    composition.addStereotype("SAP_composition");
                }
                composition.sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.composite;
                composition.targetEnd.isNavigable = true;
                composition.target = this.wrappedElement;
                composition.source = value.elementWrapper;
                composition.save();

                //set ownership of wrapped element 
                this.wrappedElement.owner = value.elementWrapper;
            }
        }
        public UMLEA.Association compositionToOwner
        {
                 get => this.wrappedElement.getRelationships<UMLEA.Association>(false, true)
                                    .FirstOrDefault(x => x.hasStereotype("SAP_composition")
                                                    && x.source.uniqueID == this.owner?.elementWrapper.uniqueID);
        }
        const string codeTypeTagName = "Node Type";
        public string nodeType
        {
            get => this.getStringProperty(codeTypeTagName);
            set => this.setStringProperty(codeTypeTagName, value); 
        }
        const string isTransientTagName = "IsTransient";
        public bool isTransient
        {
            get => this.getBoolProperty(isTransientTagName);
            set => this.setBoolProperty(isTransientTagName, value);
        }
        const string combinedStructureTagName = "Combined Structure";
        public UML.Classes.Kernel.DataType combinedStructure
        {
            get => this.getLinkProperty<UML.Classes.Kernel.DataType>(combinedStructureTagName);
            set => this.setLinkProperty(combinedStructureTagName, value);
        }
        const string dataStructureTagName = "Data Structure";
        public UML.Classes.Kernel.DataType dataStructure
        {
            get => this.getLinkProperty<UML.Classes.Kernel.DataType>(dataStructureTagName);
            set => this.setLinkProperty(dataStructureTagName, value);
        }
        const string combinedTableTypeTagName = "Combined Table Type";
        public UML.Classes.Kernel.Class combinedTableType
        {
            get => this.getLinkProperty<UML.Classes.Kernel.Class>(combinedTableTypeTagName);
            set => this.setLinkProperty(combinedTableTypeTagName, value);
        }
        const string nodeClassTagName = "Combined Structure";
        public UML.Classes.Kernel.Class nodeClass
        {
            get => this.getLinkProperty<UML.Classes.Kernel.Class>(nodeClassTagName);
            set => this.setLinkProperty(nodeClassTagName, value);
        }

        public ElementWrapper elementWrapper => this.wrappedElement;

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key);
        }

    }
}
