using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.DiagramLayout;


namespace SAP2EAImporter
{
    internal class BOPFDiagram
    {
        const int verticalPadding = 10;
        const int horizontalPadding = 100;
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
                this.wrappedDiagram.hideConnectorStereotypes = true;
                this.wrappedDiagram.disableFullyScopedObjectNames = true;
                this.wrappedDiagram.save();
            }
            //make sure this diagram is the composite diagram
            this.owner.elementWrapper.compositeDiagram = this.wrappedDiagram;
        }
        public void complete()
        {
            //get the elements we need to add
            List<ISAPElement> elementsToAdd = new List<ISAPElement>();
            if (this.ownerBusinessObject != null)
            {
                //get all owned nodes recursively
                elementsToAdd.Add(this.ownerBusinessObject);
                elementsToAdd.AddRange(ownerBusinessObject.allOwnedNodes);
                //add the autorization object(s)
                elementsToAdd.AddRange(this.ownerBusinessObject.allOwnedNodes.SelectMany(x => x.authorizationObjects));
            }
            else if (this.ownerNode != null)
            {
                //get all direct owned SAPElements that are not nodes
                elementsToAdd.Add(this.ownerNode);
                elementsToAdd.AddRange(ownerNode.ownedNonNodes);
                //add the autorization object(s)
                elementsToAdd.AddRange(this.ownerNode.authorizationObjects);
                //add combined structure
                if (this.ownerNode.combinedStructure != null)
                {
                    elementsToAdd.Add(this.ownerNode.combinedStructure);
                }
            }
            //add them to the diagram
            foreach (var element in elementsToAdd)
            {
                this.wrappedDiagram.addToDiagram(element.elementWrapper);
            }
        }

        public void format()
        {
            //layout elements
            if (this.ownerBusinessObject != null)
            {
                layoutBusinessObjectDiagram();
            }
            if (this.ownerNode != null)
            {
                layoutNodeDiagram();
            }
            //layout links
            layoutDiagramLinks();
        }
        private void layoutBusinessObjectDiagram()
        {
            //auto layout to make sure all sizes are correct
            this.wrappedDiagram.autoLayout();
            //reload
            this.wrappedDiagram = this.wrappedDiagram.model.getDiagramByID(wrappedDiagram.DiagramID);
            //get a dictionary of all diagramObjects with the elementID as the key
            var diagramObjects = this.wrappedDiagram.getDiagramObjectWrappersDictionary();
            //get the diagramObject of the businessObject, and set position that in the top left corner
            DiagramObjectWrapper businessObjectDiagramObject;
            if (!diagramObjects.TryGetValue(this.ownerBusinessObject.wrappedElement.id, out businessObjectDiagramObject))
            {
                return;//for some reason the BusinessObject did not end up on the diagram. Quit formatting
            }
            businessObjectDiagramObject.xPosition = 10;
            businessObjectDiagramObject.yPosition = 10;
            businessObjectDiagramObject.save();
            //processSubElements
            processSubElementsForBusinessObjectDiagram(businessObjectDiagramObject, this.ownerBusinessObject, diagramObjects);
        }
        private int processSubElementsForBusinessObjectDiagram(DiagramObjectWrapper parentDiagramObject, BOPFNodeOwner nodeOwner , Dictionary<int, DiagramObjectWrapper> diagramObjects)
        {
            //get current x position
            var xPos = parentDiagramObject.xPosition + parentDiagramObject.width + horizontalPadding;
            var yPos = parentDiagramObject.yPosition + parentDiagramObject.height + verticalPadding;
            //check if any ownedNodes
            if (nodeOwner.ownedNodes.Count == 0) 
            {
                return yPos;
            }
            //get diagramObjects for ownedNodes
            var subDiagramObjects = diagramObjects.Where(x => nodeOwner.ownedNodes.Any(y => y.elementWrapper.id == x.Key)).Select(v => v.Value);
            //get max width
            var maxWidth = subDiagramObjects.OrderByDescending(x => x.width).FirstOrDefault().width;
            //loop all subDiagramObjects
            foreach (var diagramObject in subDiagramObjects)
            {
                
                diagramObject.xPosition = xPos;
                diagramObject.yPosition = yPos;
                diagramObject.width = maxWidth;
                diagramObject.save();
                //go one level deeper
                yPos = this.processSubElementsForBusinessObjectDiagram(diagramObject, nodeOwner.ownedNodes.First(x => x.elementWrapper.uniqueID == diagramObject.element.uniqueID), diagramObjects);
            }
            return yPos;
        }
        private void layoutNodeDiagram()
        {
            //auto layout to make sure all sizes are correct
            this.wrappedDiagram.autoLayout();
            //reload
            this.wrappedDiagram = this.wrappedDiagram.model.getDiagramByID(wrappedDiagram.DiagramID);
            //get a dictionary of all diagramObjects with the elementID as the key
            var diagramObjects = this.wrappedDiagram.getDiagramObjectWrappersDictionary();
            //get the diagramObject of the businessObject, and set position that in the top left corner
            DiagramObjectWrapper nodeDiagramObject;
            if (!diagramObjects.TryGetValue(this.ownerNode.wrappedElement.id, out nodeDiagramObject))
            {
                return;//for some reason the BusinessObject did not end up on the diagram. Quit formatting
            }
            nodeDiagramObject.xPosition = 600;
            nodeDiagramObject.yPosition = 10;
            nodeDiagramObject.save();
            //Process all subElements of this node
            var horizontalPadding = 150;
            var verticalPadding = 10;
            //get ordered list of DiagramObjects
            var orderedDiagramObjects = getNodeDiagramObjectsInOrder(diagramObjects);
            //get center position
            var centerX = nodeDiagramObject.xPosition + (nodeDiagramObject.width / 2);
            int position = 0;
            int yPos = nodeDiagramObject.yPosition + nodeDiagramObject.height + verticalPadding *2 ;
            List<int> xPositions = new List<int>
                                    {
                                        centerX - horizontalPadding/2,
                                        centerX + horizontalPadding/2,
                                        centerX - (horizontalPadding * 2),
                                        centerX + (horizontalPadding * 2)
                                    };
            foreach (var diagramObject in orderedDiagramObjects)
            {

                diagramObject.xPosition = xPositions[position];
                if (diagramObject.xPosition < centerX)
                {
                    diagramObject.xPosition = diagramObject.xPosition - diagramObject.width;
                }
                diagramObject.yPosition = yPos;
                diagramObject.save();
                //calculate next position
                if (position == 1 || position == 3)
                {
                    //move yPos down
                    yPos = diagramObject.yPosition + diagramObject.height + verticalPadding;
                }
                //reset position after 3
                position = position == 3 ? 0 : position + 1;
            }
        }
        private void layoutDiagramLinks()
        {
            //set all compositions links to lateral horizontal
            new LateralHorizontalLayout(this.wrappedDiagram)
                    .layout(this.wrappedDiagram.diagramLinkWrappers.Where(x => x.relation.hasStereotype(SAPComposition.stereotype)));
            //other relations orthogonal rounded and spread out
            new SpreadRelationsEvenlyLayout(this.wrappedDiagram)
                    .layout(this.wrappedDiagram.diagramLinkWrappers.Where(x => !x.relation.hasStereotype(SAPComposition.stereotype)));
            
        }

        private List<DiagramObjectWrapper> getNodeDiagramObjectsInOrder(Dictionary<int, DiagramObjectWrapper> allDiagramObjects)
        {
            //get all owned non nodes that are also in the diagramObjects list
            var ownedNonNodes = this.ownerNode.ownedNonNodes.Where(x => allDiagramObjects.ContainsKey(x.elementWrapper.id));
            var sortedNonNodes = new List<ISAPElement>();
            //first get all determinations sorted by name
            sortedNonNodes.AddRange(ownedNonNodes.OfType<BOPFDetermination>().ToList().OrderBy(x => x.name));
            //then get the BOPFValidations
            sortedNonNodes.AddRange(ownedNonNodes.OfType<BOPFValidation>().ToList().OrderBy(x => x.name));
            //then get the BOPFActions
            sortedNonNodes.AddRange(ownedNonNodes.OfType<BOPFAction>().ToList().OrderBy(x => x.name));
            //then add the rest
            sortedNonNodes.AddRange(ownedNonNodes.Where(x => ! sortedNonNodes.Contains(x)).ToList().OrderBy(x => x.GetType().Name).ThenBy(x => x.name));
            //add each DiagramObject to the sorted list
            var sortedDiagramObjectList = new List<DiagramObjectWrapper>();
            foreach (var nonNode in sortedNonNodes)
            {
                if (allDiagramObjects.ContainsKey(nonNode.elementWrapper.id))
                {
                    sortedDiagramObjectList.Add(allDiagramObjects[nonNode.elementWrapper.id]);
                }
            }
            return sortedDiagramObjectList;
        }

        internal void close()
        {
            this.wrappedDiagram?.close();
        }
    }
}
