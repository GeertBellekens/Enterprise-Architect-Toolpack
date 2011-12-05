
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using System.Runtime.InteropServices;
using System.Collections.Generic;

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
		private int classIndex = 2;
		private int operationIndex = 3;
		private int diagramIndex = 4;
		private int folderIndex = 5;
		private int primitiveIndex = 6;
		private int messagIndex = 7;
		private int actionIndex = 8;
		
		private int maxNodes = 20;
		
		
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
			else if( element is UML.Diagrams.Diagram)
			{
				imageIndex = this.diagramIndex;
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
			else
			{
				imageIndex = this.classIndex;
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
			string tooltip;
			if (element is UML.Diagrams.Diagram)
			{
				tooltip = "Doubleclick to open diagram";
			}
			else if (element is UML.Classes.Kernel.PrimitiveType)
			{
				tooltip = "Primitives cannot be selected";
			}
			else
			{
				tooltip = "Doubleclick to select item in project browser";
			}
			return tooltip;
		}
		/// <summary>
		/// adds an elementNode to the tree
		/// </summary>
		/// <param name="element">the source element</param>
		/// <param name="parentNode">the parentNode. If null is passed then the node will be added as root node</param>
		private void addElementToTree(UML.UMLItem element,TreeNode parentNode)
		{
			//create new node
			TreeNode elementNode = new TreeNode(element.name);
			elementNode.Tag = element;
			elementNode.ImageIndex = this.getImageIndex(element);
			elementNode.SelectedImageIndex = this.getImageIndex(element);
			elementNode.ToolTipText = this.getToolTipText(element);
			
			//add subnodes
			foreach (string subNodeName in EAAddin.getMenuOptions(element)) 
			{
				TreeNode subNode = new TreeNode(subNodeName.Replace("&",String.Empty));
				subNode.ImageIndex = this.folderIndex;
				subNode.SelectedImageIndex = this.folderIndex;
				subNode.Nodes.Add( new TreeNode(dummyName,this.dummyIndex,this.dummyIndex));
				elementNode.Nodes.Add(subNode);
				
			}
			
			if (parentNode != null)
			{
				this.removeDummyNode(parentNode);
				parentNode.Nodes.Add(elementNode);
			}
			else
			{
				//remove duplicate
				this.removeRootNode(element);
				//no parentNode, add as new root node before any others
				this.NavigatorTree.Nodes.Insert(0,elementNode);
				//remove the excess nodes
				this.removeExcessNodes();
				//expand the node
				elementNode.Expand();
				// select the node
				NavigatorTree.SelectedNode = elementNode;
			}
				
		}
		/// <summary>
		/// removes the rootnode representig the given element
		/// </summary>
		/// <param name="sourceElement">the element</param>
		private void removeRootNode(UML.UMLItem sourceElement)
		{
			for (int i = this.NavigatorTree.Nodes.Count -1;i >= 0;i--) 
			{
				TreeNode node = this.NavigatorTree.Nodes[i];
				if (node.Tag.Equals(sourceElement))
				{
					node.Remove();
				}
			}
		}
		/// <summary>
		/// event launched just before a node is expanded
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (BeforeExpand != null)
				BeforeExpand(sender,e);
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
		
		void NavigatorTreeNodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			UML.UMLItem selectedElement = e.Node.Tag as UML.UMLItem;
			if (selectedElement != null)
			{
				selectedElement.open();
			}
		}
	}
}
