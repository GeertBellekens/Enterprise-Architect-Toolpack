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
        public BOPFNode(UMLEA.Class eaClass) : base(eaClass){ }

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
        public SAPDatatype combinedStructure
        {
            get => new SAPDatatype(this.getLinkProperty<DataType>(combinedStructureTagName));
            set => this.setLinkProperty(combinedStructureTagName, value.wrappedElement);
        }
        const string dataStructureTagName = "Data Structure";
        public SAPDatatype dataStructure
        {
            get => new SAPDatatype(this.getLinkProperty<DataType>(dataStructureTagName));
            set => this.setLinkProperty(dataStructureTagName, value.wrappedElement);
        }

        const string transientStructureTagName = "Transient Structure";
        public SAPDatatype transientStructure
        {
            get => new SAPDatatype(this.getLinkProperty<DataType>(transientStructureTagName));
            set => this.setLinkProperty(transientStructureTagName, value.wrappedElement);
        }
        const string combinedTableTypeTagName = "Combined Table Type";
        public SAPDatatype combinedTableType
        {
            get => new SAPDatatype(this.getLinkProperty<DataType>(combinedTableTypeTagName));
            set => this.setLinkProperty(combinedTableTypeTagName, value.wrappedElement);
        }
        const string nodeClassTagName = "Node Class";
        public SAPClass nodeClass
        {
            get => new SAPClass(this.getLinkProperty<Class>(nodeClassTagName));
            set => this.setLinkProperty(nodeClassTagName, value.wrappedElement);
        }
        const string checkClassTagName = "Check Class";
        public SAPClass checkClass
        {
            get => new SAPClass(this.getLinkProperty<Class>(checkClassTagName));
            set => this.setLinkProperty(checkClassTagName, value.wrappedElement);
        }
        const string databaseTableTagName = "Database Table";
        public SAPTable databaseTable
        {
            get => new SAPTable(this.getLinkProperty<Class>(databaseTableTagName));
            set => this.setLinkProperty(databaseTableTagName, value.wrappedElement);
        }
        private List<SAPAssociation> _associations;
        public IEnumerable<SAPAssociation> associations
        {
            get
            {
                if (this._associations == null)
                {
                    foreach (var eaAssociation in this.wrappedElement.getRelationships<Association>(true, false)
                                                        .Where(x => x.hasStereotype(SAPAssociation.stereotype)))
                    {
                        this._associations.Add(new SAPAssociation(eaAssociation));
                    }
                }
                return this._associations;
                
            }
        }
        
        //public SAPAssociation getAssociation(BOPFNode target, string Name)
        //{
        //    var association = this.associations.FirstOrDefault(x => x.target.key == target.key);
        //    if (association == null)
        //    {
        //        var wrappedAssociation = this.wrappedElement.addOwnedElement<UMLEA.Association>(Name)

        //    }
        //    return association;
        //}

        public ElementWrapper elementWrapper => this.wrappedElement;

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key);
        }

    }
}
