using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using UMLEA = TSF.UmlToolingFramework.Wrappers.EA;

namespace SAP2EAImporter
{
    internal class BOPFDiagram
    {
        const string MDGType = "SAP::SAP BOPF";
        private Diagram wrappedDiagram { get; set; }
        private BOPFNodeOwner owner { get; set; }
        private BOPFBusinessObject ownerBusinessObject { get => this.owner as BOPFBusinessObject; }
        private BOPFNode ownerNode { get => this.owner as BOPFNode; }
        public BOPFDiagram(Diagram diagram)
        {
            this.wrappedDiagram = diagram;
        }
        public BOPFDiagram (BOPFNodeOwner owner)
        {
            this.owner = owner;
            //get diagram object
            this.wrappedDiagram = owner.elementWrapper.ownedDiagrams
                .OfType<Diagram>()
                .FirstOrDefault(x => x.metaType == MDGType
                                && x.name == owner.name);
            //if not found with the same name, then we take any diagram of the correct type
            if (this.wrappedDiagram == null)
            {
                this.wrappedDiagram = owner.elementWrapper.ownedDiagrams
                .OfType<Diagram>()
                .FirstOrDefault(x => x.metaType == MDGType);
            }
            //if still not found we create a new diagram
            if (this.wrappedDiagram == null)
            {
                this.wrappedDiagram = owner.elementWrapper.addOwnedDiagram<DeploymentDiagram>(owner.name);
                this.wrappedDiagram.metaType = MDGType;
                this.wrappedDiagram.save();
            }
        }
        public void complete()
        {
            //get the elements we need to add
            List<ISAPElement> elementsToAdd = new List<ISAPElement>();
            if (this.ownerBusinessObject != null)
            {
                //get all owned nodes recursively
                elementsToAdd.AddRange(ownerBusinessObject.allOwnedNodes);
            }
            else if (this.ownerNode != null)
            {
                //get all direct owned SAPElements
                //TODO:
            }
            //add them to the diagram
            foreach (var element in elementsToAdd)
            {
                this.wrappedDiagram.addToDiagram(element.elementWrapper);
            }
        }

        public void format()
        {

        }
    }
}
