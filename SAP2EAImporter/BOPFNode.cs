using EAAddinFramework.Utilities;
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
        public static string stereotype => "BOPF_node";
        

        public BOPFNode(string name, BOPFNodeOwner owner, string key, bool relocate)
            : this(name, owner, key)
        {
            if (relocate)
            {
                //set the owner
                this.owner = owner;
            }

        }
        public BOPFNode(string name, BOPFNodeOwner owner, string key)
            : base(name, (UML.Classes.Kernel.Namespace)owner.elementWrapper, stereotype, key)
        {
        }
        public BOPFNode(UMLEA.Class eaClass) : base(eaClass) { }

        public static List<BOPFNode> getOwnedNodes(BOPFNodeOwner owner)
        {
            List<BOPFNode> nodes = new List<BOPFNode>();
            foreach (var wrapper in owner.elementWrapper.ownedElementWrappers
                                    .OfType<Class>()
                                    .Where(x => x.fqStereotype == $"{profileName}::{stereotype}" ))
            {
                var element = SAPElementFactory.CreateSAPElement(wrapper) as BOPFNode;
                if (element != null)
                {
                    nodes.Add(element);
                }
            }
            return nodes;
        }
        public static List<ISAPElement> getOwnedNonNodes(BOPFNodeOwner owner)
        {
            List<ISAPElement> nodes = new List<ISAPElement>();
            foreach (var wrapper in owner.elementWrapper.ownedElementWrappers
                                    .Where(x => x.fqStereotype.Substring(0,profileName.Length + 2) == $"{profileName}::"
                                    && x.fqStereotype != $"{profileName}::{stereotype}"))
            {
                var element = SAPElementFactory.CreateSAPElement(wrapper);
                if (element != null)
                {
                    nodes.Add(element);
                }
            }
            return nodes;
        }
        public static List<BOPFNode> getAllOwnedNodes(BOPFNodeOwner owner)
        {
            List<BOPFNode> ownedNodes = new List<BOPFNode>(owner.ownedNodes);
            foreach (BOPFNode node in owner.ownedNodes)
            {
                ownedNodes.AddRange(getAllOwnedNodes(node));
            }
            return ownedNodes;
        }

        BOPFNodeOwner _owner;
        public BOPFNodeOwner owner
        {
            get => this._owner;
            set
            {
                //get the existing composition before setting the owner
                var composition = this.compositionToOwner;
                this._owner = value;
                //check if there is a composition to the new owner
                if (composition == null)
                {
                    composition = this.compositionToOwner;
                }
                //ceate new composition if needed
                if (composition == null)
                {
                    composition = new SAPComposition(value, this);
                }
                composition.source = value;
                composition.save();

                //set ownership of wrapped element 
                this.wrappedElement.owner = value.elementWrapper;
                this.save();
            }
        }
        public SAPComposition compositionToOwner
        {
            get => SAPComposition.getExisitingComposition(this.owner, this);
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
            get
            {
                var linkProperty = this.getLinkProperty<DataType>(combinedStructureTagName);
                return linkProperty != null 
                        ? new SAPDatatype(linkProperty) 
                        : null;
            }
            set => this.setLinkProperty(combinedStructureTagName, value.wrappedElement);
        }
        const string dataStructureTagName = "Data Structure";
        public SAPDatatype dataStructure
        {
            get
            {
                var linkProperty = this.getLinkProperty<DataType>(dataStructureTagName);
                return linkProperty != null
                        ? new SAPDatatype(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(dataStructureTagName, value.wrappedElement);
        }

        const string transientStructureTagName = "Transient Structure";
        public SAPDatatype transientStructure
        {
            get
            {
                var linkProperty = this.getLinkProperty<DataType>(transientStructureTagName);
                return linkProperty != null
                        ? new SAPDatatype(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(transientStructureTagName, value.wrappedElement);
        }
        const string combinedTableTypeTagName = "Combined Table Type";
        public SAPDatatype combinedTableType
        {
            get
            {
                var linkProperty = this.getLinkProperty<DataType>(combinedTableTypeTagName);
                return linkProperty != null
                        ? new SAPDatatype(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(combinedTableTypeTagName, value.wrappedElement);
        }
        const string nodeClassTagName = "Node Class";
        public SAPClass nodeClass
        {
            get
            {
                var linkProperty = this.getLinkProperty<Class>(nodeClassTagName);
                return linkProperty != null
                        ? new SAPClass(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(nodeClassTagName, value.wrappedElement);
        }
        const string checkClassTagName = "Check Class";
        public SAPClass checkClass
        {
            get
            {
                var linkProperty = this.getLinkProperty<Class>(checkClassTagName);
                return linkProperty != null
                        ? new SAPClass(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(checkClassTagName, value.wrappedElement);
        }
        const string databaseTableTagName = "Database Table";
        public SAPTable databaseTable
        {
            get
            {
                var linkProperty = this.getLinkProperty<Class>(databaseTableTagName);
                return linkProperty != null
                        ? new SAPTable(linkProperty)
                        : null;
            }
            set => this.setLinkProperty(databaseTableTagName, value.wrappedElement);
        }
        private List<SAPAssociation> _associations;
        public IEnumerable<SAPAssociation> associations
        {
            get
            {
                if (this._associations == null)
                {
                    this._associations = new List<SAPAssociation>();
                    foreach (var eaAssociation in this.wrappedElement.getRelationships<Association>(true, false)
                                                        .Where(x => x.hasStereotype(SAPAssociation.stereotype)))
                    {
                        this._associations.Add(new SAPAssociation(eaAssociation));
                    }
                }
                return this._associations;

            }
        }

        private List<SAPAuthorizationObject> _authorizationObjects;
        public List<SAPAuthorizationObject> authorizationObjects
        {
            get
            {
                if (this._authorizationObjects == null)
                {
                    this._authorizationObjects = new List<SAPAuthorizationObject>();
                    foreach (var eaAssociation in this.wrappedElement.getRelationships<Association>(true, false)
                                                        .Where(x => x.hasStereotype(BOPFAuthorizationCheck.stereotype)))
                    {
                        var authorizationCheck = new BOPFAuthorizationCheck(eaAssociation);
                        this._authorizationObjects.Add(authorizationCheck.authorizationObject);
                    }
                }
                return this._authorizationObjects;
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

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key, true);
        }
        public List<BOPFNode> ownedNodes => getOwnedNodes(this);
        public List<BOPFNode> allOwnedNodes => getAllOwnedNodes(this);
        public List<ISAPElement> ownedNonNodes => getOwnedNonNodes(this);

        const string outputName = "SAP2EAImporter"; //TODO move to settings
        public override void formatDiagrams()
        {
            EAOutputLogger.log(this.wrappedElement.EAModel, outputName
                          , $"Formatting diagrams for '{this.name}'"
                          , this.wrappedElement.id
                         , LogTypeEnum.log);
            //make sure all nodes and subnodes are on the diagram
            this.diagram.complete();
            //format the diagram
            this.diagram.format();
            //close the diagram
            this.diagram.close();
        }
        private BOPFDiagram _diagram;
        private BOPFDiagram diagram
        {
            get
            {
                if (this._diagram == null)
                {
                    this._diagram = new BOPFDiagram(this);
                }
                return this._diagram;
            }
        }

    }
}
