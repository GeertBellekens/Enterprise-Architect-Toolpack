using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GlossaryManager.GUI
{
	/// <summary>
	/// Description of EDD_MainControl.
	/// </summary>
	public partial class EDD_MainControl : UserControl
	{
		public EDD_MainControl()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public void setBusinessItems(IEnumerable<BusinessItem> businessItems)
		{
			this.BusinessItemsListView.Objects = businessItems;
		}
		private BusinessItem selectedBusinessItem
		{
			get
			{
				return this.BusinessItemsListView.SelectedObject as BusinessItem;
			}
		}

        private void BusinessItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadSelectedItemData();
        }

        private void loadSelectedItemData()
        {
            if (selectedBusinessItem != null)
            {
                this.BU_NameTextBox.Text = selectedBusinessItem.Name;
                this.BU_DomainComboBox.Text = selectedBusinessItem.domainPath;
                this.BU_DescriptionTextBox.Text = selectedBusinessItem.Description;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selectedBusinessItem != null)
            {
                selectedBusinessItem.Name = this.BU_NameTextBox.Text;
                //TODO: fix set domain
                //selectedBusinessItem.domainPath = this.BU_DomainComboBox.Text
                selectedBusinessItem.Description = this.BU_DescriptionTextBox.Text;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            loadSelectedItemData();
        }
    }
}
