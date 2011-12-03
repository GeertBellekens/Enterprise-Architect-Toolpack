
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
		public NavigatorControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();

		}
		/// <summary>
		/// set the main element to navigate
		/// </summary>
		/// <param name="newElement">the element to navigate</param>
		public void setElement(UML.UMLItem newElement)
		{
			this.addElementToTree(newElement,null);
			
		}

		private void addElementToTree(UML.UMLItem element,TreeNode parentNode)
		{
			//create new node
			TreeNode elementNode = new TreeNode(element.name);
			elementNode.Tag = element;
			
			//add subnodes
			foreach (string subNodeName in EAAddin.getMenuOptions(element)) 
			{
				TreeNode subNode = new TreeNode(subNodeName.Replace("&",String.Empty));
				subNode.Nodes.Add( new TreeNode(dummyName));
				elementNode.Nodes.Add(subNode);
				
			}
			
			if (parentNode != null)
			{
				this.removeDummyNode(parentNode);
				parentNode.Nodes.Add(elementNode);
			}
			else
			{
				//no parentNode, add as new root node before any others
				this.NavigatorTree.Nodes.Insert(0,elementNode);
				elementNode.Expand();
				
			}
				
		}
		void NavigatorTreeBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (BeforeExpand != null)
				BeforeExpand(sender,e);
		}
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
