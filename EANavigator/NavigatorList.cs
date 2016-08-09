using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;

namespace TSF.UmlToolingFramework.EANavigator
{
    public partial class NavigatorList : Form
    {
    	private UML.Extended.UMLItem context;
    	/// <summary>
    	/// createas a new navigatorList based on the given list of UML Items
    	/// </summary>
    	/// <param name="items">the items to show</param>
    	public NavigatorList(List<UML.Extended.UMLItem> items, UML.Extended.UMLItem context):base()
        {
    		if (items.Count > 0)
    		{
    			this.context = context;
    			if (items[0] is UML.Diagrams.Diagram)
    			{
    				this.InitDiagrams(items.Cast<UML.Diagrams.Diagram>().ToList());
    			}
    			else
    			{
    				this.InitNamedElements(items);
    			}
    		}
    			
        }
    	/// <summary>
    	/// initialise based on diagrams
    	/// </summary>
    	/// <param name="diagrams">the diagrams</param>
        private void InitDiagrams(List<UML.Diagrams.Diagram> diagrams)
        {
            InitializeComponent();

            //fill the diagramlist
            //create the content list from the diagrams
            foreach (UML.Diagrams.Diagram diagram in diagrams)
            {
                //add the diagram
                ListViewItem item = new ListViewItem(diagram.name);
                item.Tag = diagram;

                string ownerName = string.Empty;

                UML.Classes.Kernel.NamedElement owner = diagram.owner as UML.Classes.Kernel.NamedElement;
                if (null != owner)
                {
                    ownerName = owner.name;
                }
                item.SubItems.Add(ownerName);
                this.navigateListView.Items.Add(item);
            }

        }
        /// <summary>
        /// initialise based on named elements
        /// </summary>
        /// <param name="namedElements"></param>
        private void InitNamedElements(List<UML.Extended.UMLItem> namedElements)
        {
        	InitializeComponent();
        	this.Text = "Select Elements";
        	this.ItemHeader.Text = "Element";
        	this.openButton.Text = "Select";
    		//fill the diagramList
			foreach (UML.Extended.UMLItem element in namedElements)
            {
                //add the element
                ListViewItem item = new ListViewItem(element.name);
                item.Tag = element;

                string ownerName = string.Empty;

                UML.Extended.UMLItem owner = element.owner as UML.Extended.UMLItem;
                if (null != owner)
                {
                    ownerName = owner.name;
                }
                item.SubItems.Add(ownerName);
                if (ownerName == string.Empty)
                {
                	item.ForeColor = SystemColors.GrayText;
                }
                item.UseItemStyleForSubItems = true;
                this.navigateListView.Items.Add(item);
            }
        }
        
		/// <summary>
		/// close the window
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
		/// <summary>
		/// open the selected items
		/// </summary>
		/// <param name="sender">sedder</param>
		/// <param name="e">params</param>
        private void openButton_Click(object sender, EventArgs e)
        {
        	this.openSelectedElements();
        }
        
        private void NavigateListViewMouseDoubleClick(object sender, MouseEventArgs e)
        {
        	this.openSelectedElements();
        }
        private void openSelectedElements()
        {
        	foreach (ListViewItem item in this.navigateListView.SelectedItems)
            {
        		UML.Extended.UMLItem element = item.Tag as UML.Extended.UMLItem;
        		if (null != element)
        		{
                	element.open();
        		}
        		//if the element is a diagram then also select the context
        		if (element is UML.Diagrams.Diagram)
        		{
        			((UML.Diagrams.Diagram)element).selectItem(context);
        		}
            }
        }
    }
}
