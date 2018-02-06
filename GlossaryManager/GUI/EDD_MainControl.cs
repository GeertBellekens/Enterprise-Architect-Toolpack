using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace GlossaryManager.GUI
{
    /// <summary>
    /// Description of EDD_MainControl.
    /// </summary>
    public partial class EDD_MainControl : UserControl
    {
        public List<Domain> domains { get; set; }
        public List<String> statusses { get; set; }
        public EDD_MainControl()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            enableDisable();

        }
        private void enableDisable()
        {
            this.openPropertiesButton.Enabled = this.selectedBusinessItem != null;
            this.navigateProjectBrowserButton.Enabled = this.selectedBusinessItem != null;
        }
        public void setBusinessItems(IEnumerable<BusinessItem> businessItems, Domain domain)
        {
            this.BusinessItemsListView.Objects = businessItems;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            this.BusinessItemsListView.SelectedObject = businessItems.FirstOrDefault();
        }
        private KryptonBreadCrumbItem getBreadCrumbSubItem(KryptonBreadCrumbItem parentItem, Domain domain)
        {
            foreach (var subItem in parentItem.Items)
            {
                var foundDomain = (Domain)subItem.Tag;
                if (foundDomain.uniqueID == domain.uniqueID)
                    return subItem; //found it
                //not found, look deeper
                var foundItem = getBreadCrumbSubItem(subItem, domain);
                if (foundItem != null)
                    return foundItem;
            }
            //not found, return null
            return null;
        }
        public void setStatusses(List<String> statusses)
        {
            this.statusses = statusses;
            BU_StatusCombobox.DataSource = this.statusses;
        }
        public void setDomains(List<Domain> domains)
        {
            this.domains = domains;
            BU_DomainComboBox.DataSource = this.domains;
            BU_DomainComboBox.DisplayMember = "displayName";

            //set the domains breadcrumb
            foreach (var domain in domains)
            {
                if (domain.parentDomain == null) //only process top level domains
                {
                    domainBreadCrumb.RootItem.Items.Add(createDomainBreadCrumbItem(domain));
                }
            }
        }
        public KryptonBreadCrumbItem createDomainBreadCrumbItem(Domain domain)
        {
            var breadCrumbItem = new KryptonBreadCrumbItem(domain.name);
            breadCrumbItem.Tag = domain;
            foreach (var subDomain in domain.subDomains)
            {
                breadCrumbItem.Items.Add(createDomainBreadCrumbItem(subDomain));
            }
            return breadCrumbItem;
        }
        private BusinessItem previousBusinessItem { get; set; }
        private BusinessItem selectedBusinessItem
        {
            get
            {
                return this.BusinessItemsListView.SelectedObject as BusinessItem;
            }
        }

        private void BusinessItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if the previous item has been changed
            if (this.previousBusinessItem != null)
            {
                if (this.hasBusinessitemChanged(this.previousBusinessItem))
                {
                    var response = MessageBox.Show(this, string.Format("Save changes to {0}?", this.previousBusinessItem.Name)
                                                     , "Unsaved changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (response == DialogResult.Yes)
                    {
                        this.unloadBusinessItemData(this.previousBusinessItem);
                        this.saveBusinessItem(this.previousBusinessItem);
                    }
                }
            }
            //then load the next item
            loadSelectedItemData();
            enableDisable();
            //set the previous business item
            this.previousBusinessItem = this.selectedBusinessItem;
        }
        private bool hasBusinessitemChanged (BusinessItem businessItem)
        {
            return businessItem.Name != this.BU_NameTextBox.Text
                    || businessItem.domain != (Domain)this.BU_DomainComboBox.SelectedItem
                    || businessItem.Description != this.BU_DescriptionTextBox.Text
                    || businessItem.Status != this.BU_StatusCombobox.Text
                    || businessItem.Version != this.BU_VersionTextBox.Text
                    || string.Join(",", businessItem.Keywords) != this.BU_KeywordsTextBox.Text;
        }

        private void loadSelectedItemData()
        {
            if (selectedBusinessItem != null)
            {
                this.BU_NameTextBox.Text = selectedBusinessItem.Name;
                this.BU_DomainComboBox.SelectedItem = selectedBusinessItem.domain;
                this.BU_DescriptionTextBox.Text = selectedBusinessItem.Description;
                this.BU_StatusCombobox.Text = selectedBusinessItem.Status;
                this.BU_VersionTextBox.Text = selectedBusinessItem.Version;
                this.BU_KeywordsTextBox.Text = string.Join(",", selectedBusinessItem.Keywords);
                this.BU_CreatedTextBox.Text = selectedBusinessItem.CreateDate.ToString("G");
                this.BU_CreatedByTextBox.Text = selectedBusinessItem.Author;
                this.BU_ModifiedDateTextBox.Text = selectedBusinessItem.UpdateDate.ToString("G");
                this.BU_ModifiedByTextBox.Text = selectedBusinessItem.UpdatedBy;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (selectedBusinessItem != null)
            {
                saveBusinessItem(this.selectedBusinessItem);
                //refresh listview
                this.BusinessItemsListView.RefreshSelectedObjects();
            }
        }
        private void unloadBusinessItemData(BusinessItem item)
        {
            item.Name = this.BU_NameTextBox.Text;
            item.domain = (Domain)this.BU_DomainComboBox.SelectedItem;
            item.Description = this.BU_DescriptionTextBox.Text;
            item.Status = this.BU_StatusCombobox.Text;
            item.Version = this.BU_VersionTextBox.Text;
            item.Keywords = this.BU_KeywordsTextBox.Text.Split(',')
                                                .Select(x => x.Trim())
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        }
        private void saveBusinessItem(BusinessItem item)
        {
            unloadBusinessItemData(item);
            item.Save();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            loadSelectedItemData();
        }

        public event EventHandler selectedDomainChanged;
        private void domainBreadCrumb_SelectedItemChanged(object sender, EventArgs e)
        {
            var selectedDomain = domainBreadCrumb.SelectedItem.Tag as Domain;
            this.selectedDomainChanged?.Invoke(selectedDomain, e);
        }

        private void navigateProjectBrowserButton_Click(object sender, EventArgs e)
        {
            this.selectedBusinessItem?.selectInProjectBrowser();
        }

        private void openPropertiesButton_Click(object sender, EventArgs e)
        {
            this.selectedBusinessItem?.openProperties();
        }

        private void BusinessItemsListView_DoubleClick(object sender, EventArgs e)
        {
            this.selectedBusinessItem?.openProperties();
        }
    }
}
