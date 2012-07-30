
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Configuration;

namespace TSF.UmlToolingFramework.EANavigator
{
    [Guid("29fe6e24-9486-4d55-ac16-0200c1b477f2")]
	[ComVisible(true)]
	/// <summary>
	/// Description of NavigatorControl.
	/// </summary>
	public partial class NavigatorControl : UserControl
	{
		private const string dummyName = "None";
		private int dummyIndex = 0;
		private int attributeIndex = 1;
		private int elementIndex = 2;
		private int operationIndex = 3;
		private int diagramIndex = 4;
		private int packageElementIndex = 5;
		private int primitiveIndex = 6;
		private int messagIndex = 7;
		private int actionIndex = 8;
		private int sequenceDiagramIndex = 9;
		private int classIndex = 10;
		private int stateMachineIndex = 11;
		private int interactionIndex = 12;
		private int activityIndex = 13;
		private int taggedValueIndex = 14;
		private int attributeTagIndex = 15;
		private int elementTagIndex = 16;
		private int operationTagIndex = 17;
		private int relationTagIndex = 18;
		private int parameterIndex = 19;
		private int packageIndex = 20;
		private int packageActionIndex = 21;
		private int packageAttributeIndex = 22;
		private int packageOperationIndex = 23;
		private int packageParameterIndex = 24;
		private int packageSequenceDiagramIndex = 25;
		private int packageTaggedValuesIndex = 26;
		private int parameterTagIndex = 27;
		private int rootPackageIndex = 28;
		private int communicationDiagramIndex = 29;
		private int enumerationIndex = 30;
		private int dataTypeIndex = 31;

			
		private int maxNodes = 50;
		
		public NavigatorSettings settings {get;set;}
		
		public NavigatorControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

		}
		/// <summary>
		/// clears all nodes
		/// </summary>
		public void clear()
		{
			this.NavigatorTree.Nodes.Clear();
		}
		/// <summary>
		/// set the main element to navigate
		/// </summary>
		/// <param name="newElement">the element to navigate</param>
		public void setElement(UML.UMLItem newElement)
		{
			this.addElementToTree(newElement,null);
			
		}
		private int getImageIndex(UML.UMLItem element)
		{
			int imageIndex;
			if (element is UML.Classes.Kernel.Property)
			{
				imageIndex = this.attributeIndex;
			}
			else if (element is UML.Classes.Kernel.Operation)
			{
				imageIndex = this.operationIndex;
			}
			else if (element is UML.Classes.Kernel.Package)
			{
				if (element.owner == null)
				{
					imageIndex = this.rootPackageIndex;
				}
				else
				{
					imageIndex = this.packageIndex;
				}
			}
			else if (element is UML.Diagrams.SequenceDiagram)
			{
				imageIndex = this.sequenceDiagramIndex;
			}
			else if (element is UML.Diagrams.CommunicationDiagram)
			{
				imageIndex = this.communicationDiagramIndex;
			}
			else if( element is UML.Diagrams.Diagram)
			{
				imageIndex = this.diagramIndex;
			}
			else if (element is UML.Interactions.BasicInteractions.Interaction)
			{
				imageIndex = this.interactionIndex;
			}
			else if (element is UML.StateMachines.BehaviorStateMachines.StateMachine)
			{
				imageIndex = this.stateMachineIndex;
			}
			else if (element is UML.Activities.FundamentalActivities.Activity)
			{
				imageIndex = this.activityIndex;
			}
			else if (element is UML.Classes.Kernel.PrimitiveType)
			{
				imageIndex = this.primitiveIndex;
			}
			else if (element is UML.Classes.Kernel.Relationship)
			{
				imageIndex = this.messagIndex;
			}
			else if (element is UML.Actions.BasicActions.Action)
			{
				imageIndex = this.actionIndex;
			}
			else if (element is UML.Classes.Kernel.Class)
			{
				imageIndex = this.classIndex;
			}
			else if (element is UML.Classes.Kernel.Parameter)
			{
				imageIndex = this.parameterIndex;
			}
			else if (element is UML.Profiles.TaggedValue)
			{
				UML.Profiles.TaggedValue taggedValue = (UML.Profiles.TaggedValue)element;
				if (taggedValue.owner is UML.Classes.Kernel.Property)
				{
					imageIndex = this.attributeTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Operation)
				{
					imageIndex = this.operationTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Parameter)
				{
					imageIndex = this.parameterTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Relationship)
				{
					imageIndex = this.relationTagIndex;
				}
				else if (taggedValue.owner is UML.Classes.Kernel.Element)
				{
					imageIndex = this.elementTagIndex;
				}
				else 
				{
					imageIndex = this.taggedValueIndex;
				}
			}
			else if (element is UML.Classes.Kernel.Enumeration)
			{
				imageIndex = this.enumerationIndex;
			}
			else if (element is UML.Classes.Kernel.DataType)
			{
				imageIndex = this.dataTypeIndex;
			}
			else
			{
				imageIndex = this.elementIndex;
			}
			return imageIndex;
		}
		private int getFolderImageIndex(string menuOptionName)
		{
			int imageIndex;
			switch (menuOptionName)
			{
				case EAAddin.menuActions:
					imageIndex = this.packageActionIndex;
					break;
				case EAAddin.menuAttributes:
					imageIndex = this.packageAttributeIndex;
					break;
				case EAAddin.menuDependentTaggedValues:
					imageIndex = this.packageTaggedValuesIndex;
					break;
				case EAAddin.menuDiagramOperations:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuDiagrams:
					imageIndex = this.packageSequenceDiagramIndex;
					break;
				case EAAddin.menuImplementation:
					imageIndex = this.packageSequenceDiagramIndex;
					break;
				case EAAddin.menuOperation:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuImplementedOperations:
					imageIndex = this.packageOperationIndex;
					break;
				case EAAddin.menuParameters:
					imageIndex = this.packageParameterIndex;
					break;
				case EAAddin.menuParameterTypes:
					imageIndex = this.packageElementIndex;
					break;
				default:
					if( menuOptionName.StartsWith(EAAddin.taggedValueMenuPrefix)
				   	&& menuOptionName.EndsWith(EAAddin.taggedValueMenuSuffix))
				  	{
				   		imageIndex = this.packageElementIndex;
				  	}
					else
					{
						//just in case we forgot a case				 
						imageIndex = this.packageElementIndex;
					}
					break;
			}
			return imageIndex;
		}
		/// <summary>
		/// returns the tooltiptext for the given element
		/// </summary>
		/// <param name="element">the element</param>
		/// <returns>the tooltip text</returns>
		private string getToolTipText(UML.UMLItem element)
		{
			return element.fqn;
		}
		/// <summary>
		/// returns the name to show as node name for this element
		/// </summary>
		/// <param name="element"></param>
		private string getNodeName(UML.UMLItem element)
		{
			
			string name = string.Empty;
			if (element != null)
			{
				name = element.name;
			}
			if (element is UML.Classes.Kernel.Parameter)
			{
				UML.Classes.Kernel.Parameter parameter = (UML.Classes.Kernel.Parameter)element;
				if (parameter.direction != UML.Classes.Kernel.ParameterDirectionKind._return)
				{
					name = parameter.name + " (" + this.getNodeName(parameter.operation) + ")";
				}
			}
			else if (element is UML.Classes.Kernel.Feature)
			{
				UML.Classes.Kernel.Feature feature = (UML.Classes.Kernel.Feature)element;
				name = feature.owner.name + "." + feature.name;
			}
			else if (element is UML.Profiles.TaggedValue)
			{
				UML.Profiles.TaggedValue taggedValue = (UML.Profiles.TaggedValue)element;
				if (taggedValue.owner.name.Length > 0)
				{
					name = taggedValue.owner.name + "." + taggedValue.name;
				}
			}
			return name;
			    
		}
		/// <summary>
		/// adds an elementNode to the tree
		/// </summary>
		/// <param name="element">the source element</param>
		/// <param name="parentNode">the parentNode. If null is passed then the node will be added as root node</param>
		private TreeNode addElementToTree(UML.UMLItem element,TreeNode parentNode,TreeNode nodeToReplace = null)
		{
			//create new node
			TreeNode elementNode;
			if (nodeToReplace != null)
			{
				elementNode = nodeToReplace;
				if (nodeToReplace.Text.StartsWith(EAAddin.ownerMenuPrefix.Replace("&",string.Empty)))
				{
					elementNode.Text = EAAddin.ownerMenuPrefix.Replace("&",string.Empty) + this.getNodeName(element);
				}
				//the type name is already ok
				//remove dummy node
				this.removeDummyNode(elementNode);
			}
			else
			{
				elementNode = new TreeNode(this.getNodeName(element));
			}
			elementNode.Tag = element;
			int imageIndex = this.getImageIndex(element);
			elementNode.ImageIndex = imageIndex;
			elementNode.SelectedImageIndex = imageIndex;
			elementNode.ToolTipText = this.getToolTipText(element);
			
			//get sub menu option
			List<string> subMenuOptions = EAAddin.getMenuOptions(element);
			if (subMenuOptions.Count > 0)
			{
				this.removeDummyNode(elementNode);
			}			
			//add subnodes
			foreach (string subNodeName in subMenuOptions) 
			{
				TreeNode subNode = new TreeNode(subNodeName.Replace("&",String.Empty));
				int subImageIndex = this.getFolderImageIndex(subNodeName);
				subNode.ImageIndex = subImageIndex;
				subNode.SelectedImageIndex = subImageIndex;
				subNode.Nodes.Add( new TreeNode(dummyName,this.dummyIndex,this.dummyIndex));
				elementNode.Nodes.Add(subNode);
				
			}
			
			if (parentNode != null)
			{
				if (nodeToReplace == null)
				{
					this.removeDummyNode(parentNode);
					parentNode.Nodes.Add(elementNode);
				}
			}
			else
			{
				
				//remove duplicate
				if (this.removeRootNode(element))
				{
					//no parentNode, add as new root node before any others
					this.NavigatorTree.Nodes.Insert(0,elementNode);
					//remove the excess nodes
					this.removeExcessNodes();
					//expand the node
					elementNode.Expand();
				}
				else 
				{
					//first node is already the one we need
					elementNode = this.NavigatorTree.Nodes[0];
				}

				// select the node
				NavigatorTree.SelectedNode = elementNode;
			}
			return elementNode;
		}

		/// <summary>
		/// removes the rootnode representig the given element
		/// unless it this node is the first rootnode.
		/// In that case it simply returns false
		/// </summary>
		/// <param name="sourceElement">the element</param>
		private bool removeRootNode(UML.UMLItem sourceElement)
		{
			for (int i = this.NavigatorTree.Nodes.Count -1;i >= 0;i--) 
			{
				TreeNode node = this.NavigatorTree.Nodes[i];
				if (node.Tag.Equals(sourceElement))
				{
					if( i > 0)
					{
						node.Remove();
						return true;
					}else
					{
						//if the node is the first root node then don't remove it but return false
						return false;
					}
				}
			}
			return true;
		}
		/// <summary>
		/// event launched just before a node is expanded
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			foreach (TreeNode subNode in e.Node.Nodes)
			{
				if (this.isOwnerNode(subNode))
				{
					UML.UMLItem owner = ((UML.UMLItem)e.Node.Tag).owner;
					if (owner != null)
					{
						this.addElementToTree(owner,e.Node,subNode);		
					}
				}
				else if (this.isTypeNode(subNode))
				{
					UML.Classes.Kernel.Property attribute  = e.Node.Tag as UML.Classes.Kernel.Property;
					if (attribute != null
					    && attribute.type != null)
					{
						this.addElementToTree(attribute.type,e.Node,subNode);
					}
				}
			}
			if (BeforeExpand != null)
				BeforeExpand(sender,e);
		}
		private bool isOwnerNode(TreeNode node)
		{
			return (!(node.Tag is UML.UMLItem)
			   && node.Parent.Tag is UML.UMLItem
			   && node.Text.StartsWith(EAAddin.ownerMenuPrefix.Replace("&",string.Empty)));

		}
		private bool isTypeNode(TreeNode node)
		{
			return (!(node.Tag is UML.UMLItem)
			   && node.Parent.Tag is UML.UMLItem
			   && node.Text.StartsWith(EAAddin.typeMenuPrefix.Replace("&",string.Empty)));
		}
		/// <summary>
		/// remove the dummy node(s) from the given node
		/// </summary>
		/// <param name="node">the parent ndoe</param>
		private void removeDummyNode(TreeNode node)
		{
			foreach ( TreeNode subNode in node.Nodes) 
			{
				if (subNode.Text == dummyName)
				{
					node.Nodes.Remove(subNode);
				}
			}
		}
		/// <summary>
		/// removes all rootnodes with index larger then maxnodes
		/// </summary>
		private void removeExcessNodes()
		{
			for (int i = this.NavigatorTree.Nodes.Count -1; i > this.maxNodes;i--)
			{
				this.NavigatorTree.Nodes.RemoveAt(i);
			}
		}
		public event TreeViewCancelEventHandler BeforeExpand;
		public void setSubNodes(TreeNode node, List<UML.UMLItem> elements)
		{
			if (elements.Count > 0)
			{
				//remove dummy node
				node.Nodes.Clear();
				//add nodes for each named element
				foreach (UML.UMLItem element in elements) 
				{
					this.addElementToTree(element,node);
				}
			}
		}
		public event TreeNodeMouseClickEventHandler NodeDoubleClick;
		
		void NavigatorTreeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			UML.UMLItem selectedElement = e.Node.Tag as UML.UMLItem;
			if (selectedElement != null)
			{
				if (this.settings.isProjectBrowserDefaultAction())
				{
					selectedElement.open();
				}
				else 
				{
					selectedElement.openProperties();
				}
			}
//			if (NodeDoubleClick != null)
//			{
//				NodeDoubleClick(sender,e);
//			}
		}
		
		void OpenPropertiesMenuItemClick(object sender, EventArgs e)
		{
			UML.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.UMLItem;
			if (selectedElement != null)
			{
				selectedElement.openProperties();
			}
		}
		
		void NavigatorTreeNodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
		{
			//set the selected node also with right mouse click
			if (e.Button == MouseButtons.Right)
			{
				this.NavigatorTree.SelectedNode = e.Node;
			}
			//show context menu
			UML.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.UMLItem;
			if (e.Button == MouseButtons.Right
			    && selectedElement != null)
			{
				//set enabled status
				this.enableDisableContexMenu(selectedElement);
				//then actually show the menu
				this.navigatorContextMenu.Show(this.NavigatorTree,e.Location);
			}
		}
		/// <summary>
		/// enables or disabled the context menu items based on the type of the selected element
		/// </summary>
		/// <param name="selectedElement">the selected element</param>
		private void enableDisableContexMenu(UML.UMLItem selectedElement)
		{
			//both options should be disabled for connectors, 
			if (selectedElement is UML.Classes.Kernel.Relationship)
			{
				this.setContextMenuItemsEnabled(false);
			}
			//or for tagged values on connectors because we can't open their properties dialog anyway.
			else if (selectedElement is UML.Profiles.TaggedValue
			         && ((UML.Profiles.TaggedValue)selectedElement).owner is UML.Classes.Kernel.Relationship)
			{
				this.setContextMenuItemsEnabled(false);
			}
			//standard should be enabled.
			else
			{
				this.setContextMenuItemsEnabled(true);
			}
			    
		}
		private void setContextMenuItemsEnabled(bool enabled)
		{
			foreach (ToolStripMenuItem menuItem in this.navigatorContextMenu.Items) 
			{
				menuItem.Enabled = enabled;	
			}
		}
		
		void SelectBrowserMenuItemClick(object sender, EventArgs e)
		{
			UML.UMLItem selectedElement = this.NavigatorTree.SelectedNode.Tag as UML.UMLItem;
			if (selectedElement != null)
			{
				selectedElement.open();
			}
		}
	}
}
