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
        public List<Domain> domains { get; set; }
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
        public void SetDomains(List<Domain> domains)
        {
            this.domains = domains;
            BU_DomainComboBox.DataSource = this.domains;
            BU_DomainComboBox.DisplayMember = "displayName";
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
                this.BU_DomainComboBox.SelectedItem = selectedBusinessItem.domain;
                this.BU_DescriptionTextBox.Text = selectedBusinessItem.Description;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selectedBusinessItem != null)
            {
                selectedBusinessItem.Name = this.BU_NameTextBox.Text;
                selectedBusinessItem.domain = (Domain)this.BU_DomainComboBox.SelectedItem;
                selectedBusinessItem.Description = this.BU_DescriptionTextBox.Text;
                selectedBusinessItem.Save();
                //refresh listview
                this.BusinessItemsListView.RefreshSelectedObjects();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            loadSelectedItemData();
        }
    }
}
