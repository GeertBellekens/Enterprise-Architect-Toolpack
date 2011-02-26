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
        public NavigatorList(List<UML.Diagrams.Diagram> diagrams):base()
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
        public NavigatorList(List<UML.Classes.Kernel.NamedElement> namedElements):base()
        {
        	InitializeComponent();
        	this.Text = "Select Elements";
        	this.ItemHeader.Text = "Element";
        	this.openButton.Text = "Select";
    		//fill the diagramList
			foreach (UML.Classes.Kernel.NamedElement element in namedElements)
            {
                //add the element
                ListViewItem item = new ListViewItem(element.name);
                item.Tag = element;

                string ownerName = string.Empty;

                UML.Classes.Kernel.NamedElement owner = element.owner as UML.Classes.Kernel.NamedElement;
                if (null != owner)
                {
                    ownerName = owner.name;
                }
                item.SubItems.Add(ownerName);
                this.navigateListView.Items.Add(item);
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
        		UML.Diagrams.Diagram diagram = item.Tag as UML.Diagrams.Diagram;
        		if (null != diagram)
        		{
                	diagram.open();
        		}else
        		{
        			UML.Classes.Kernel.NamedElement element = item.Tag as UML.Classes.Kernel.NamedElement;
        			if (null != element)
        			{
        				((UTF_EA.Model) UML.UMLFactory.getInstance().model).selectElement(element);
        			}
        		}
            }
        }
    }
}
