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
    class BOPFBusinessObject: SAPElement<UMLEA.Component>, BOPFNodeOwner
    {
        public static string stereotype => "BOPF_businessObject";

        public BOPFBusinessObject(string name, UML.Classes.Kernel.Package package, string key)
            : base(name, package, stereotype, key, true, true) { }
        public BOPFBusinessObject(UMLEA.Component component) : base(component) { }

        const string objectCategoryTagName = "Object Category";
        public string objectCategory
        {
            get => this.getStringProperty(objectCategoryTagName);
            set => this.setStringProperty(objectCategoryTagName, value);
        }

        public SAPElement<UMLEA.Component> element => this;

        public BOPFNode addNode(string name, string key)
        {
            return new BOPFNode(name, this, key, true);
        }
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

        public List<BOPFNode> ownedNodes => BOPFNode.getOwnedNodes(this);
        public List<BOPFNode> allOwnedNodes => BOPFNode.getAllOwnedNodes(this);


    }
}
