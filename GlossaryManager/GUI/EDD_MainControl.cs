using BrightIdeasSoftware;
using ComponentFactory.Krypton.Toolkit;
using DatabaseFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GlossaryManager.GUI
{
    /// <summary>
    /// Description of EDD_MainControl.
    /// </summary>
    public partial class EDD_MainControl : UserControl
    {
        public List<Domain> domains { get; set; }
        public List<String> statusses { get; set; }
        const string nameFilterDefault = "Name";
        bool nameFilterDefaultActive;
        const string descriptionFilterDefault = "Description";
        bool descriptionFilterDefaultActive;
        bool businessItemsShowing = false;
        bool dataItemsShowing = false;
        public EDD_MainControl()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            this.InitializeComponent();
            this.enableDisable();
            this.setColumnsListViewDelegates();
            this.setFilterDefaults();
            this.dataItemsSplitContainer.Panel2Collapsed = true;

        }
        private bool settingDefaults { get; set; }
        /// <summary>
        /// set the default text and forecolor for the empty filter fields
        /// </summary>
        private void setFilterDefaults()
        {
            this.settingDefaults = true;
            if (string.IsNullOrEmpty(this.nameFilterTextBox.Text))
            {
                this.nameFilterTextBox.Text = nameFilterDefault;
                this.nameFilterTextBox.ForeColor = Color.Gray;
                this.nameFilterDefaultActive = true;
            }
            if (string.IsNullOrEmpty(this.descriptionFilterTextBox.Text))
            {
                this.descriptionFilterTextBox.Text = descriptionFilterDefault;
                this.descriptionFilterTextBox.ForeColor = Color.Gray;
                this.descriptionFilterDefaultActive = true;
            }
            this.settingDefaults = false;
        }
        private string nameFilter { get; set; }
        private string descriptionFilter { get; set; }
        public GlossaryItemSearchCriteria searchCriteria => new GlossaryItemSearchCriteria()
        {
            nameSearchTerm = this.nameFilter,
            descriptionSearchTerm = this.descriptionFilter,
            showAll = this.showAllCheckBox.Checked
        };
        private void setColumnsListViewDelegates()
        {
            //tell the control who can expand (only tables)
            TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object x)
               {
                   return (x is EDDTable);
               };
            this.columnsListView.CanExpandGetter = canExpandGetter;
            this.dColumnsListView.CanExpandGetter = canExpandGetter;
            //tell the control how to expand
            TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object x)
               {
                   var table = (EDDTable)x;
                   return table.columns;
               };
            this.columnsListView.ChildrenGetter = childrenGetter;
            this.dColumnsListView.ChildrenGetter = childrenGetter;
            //tell the control which image to show
            ImageGetterDelegate imageGetter = delegate (object rowObject)
               {
                   if (rowObject is EDDTable)
                   {
                       return "table";
                   }

                   if (rowObject is EDDColumn)
                   {
                       return "column";
                   }
                   else
                   {
                       return string.Empty;
                   }
               };
            this.C_NameColumn.ImageGetter = imageGetter;
            this.dC_NameColumn.ImageGetter = imageGetter;
        }

        private void enableDisable()
        {
            this.openPropertiesButton.Enabled = this.selectedItem != null;
            this.navigateProjectBrowserButton.Enabled = this.selectedItem != null;
            this.C_SizeUpDown.Enabled = ((BaseDataType)this.C_DatatypeDropdown.SelectedItem)?.hasLength == true;
            if (!this.C_SizeUpDown.Enabled)
            {
                this.C_SizeUpDown.Text = string.Empty;
            }

            this.C_PrecisionUpDown.Enabled = ((BaseDataType)this.C_DatatypeDropdown.SelectedItem)?.hasPrecision == true;
            if (!this.C_PrecisionUpDown.Enabled)
            {
                this.C_PrecisionUpDown.Text = string.Empty;
            }

            this.newButton.Enabled = this.selectedDomain != null;
            //make the specific columns button invisible
            this.showAllColumnsButton.Visible = this.columnsVisible();
            this.getTableButton.Visible = this.columnsVisible();
            this.label1.Visible = this.columnsVisible();
            this.showHideTablesButton.Visible = this.selectedTab == GlossaryTab.DataItems;
            if (this.selectedTab == GlossaryTab.DataItems || this.selectedItem is EDDColumn)
            {
                this.newLinkedButton.Image = Properties.Resources.newLeft;
                if (this.selectedItem is EDDColumn)
                {
                    this.newLinkedButton.Tag = "Create new linked Data Item";
                }
                else
                {
                    this.newLinkedButton.Tag = "Create new linked Business Item";
                }
            }
            else
            {
                this.newLinkedButton.Image = Properties.Resources.newRight;
                this.newLinkedButton.Tag = "Create new linked Data Item";
            }
            //set tooltip
            this.myToolTip.SetToolTip(this.newLinkedButton, this.newLinkedButton.Tag.ToString());
            this.newLinkedButton.Enabled = this.selectedTab == GlossaryTab.DataItems && this.selectedDataItem != null && this.selectedDataItem.businessItem == null
                                        || this.selectedItem is EDDColumn
                                        || this.selectedTab == GlossaryTab.BusinessItems && this.selectedBusinessItem != null;

            if (this.isColumnsFocussed)
            {
                this.linkedLeftButton.Image = Properties.Resources.linkedDataItemsLeft;
                this.linkedLeftButton.Tag = "Show linked Data Items";

            }
            else
            {
                this.linkedLeftButton.Image = Properties.Resources.linkedBusinessItems;
                this.linkedLeftButton.Tag = "Show linked Business Items";
            }
            this.myToolTip.SetToolTip(this.linkedLeftButton, this.linkedLeftButton.Tag.ToString());
            this.linkedLeftButton.Enabled = this.selectedItem is EDDColumn
                                            || this.selectedItem is DataItem;
            //takeover properties from linkedLeftButton to context menu item
            this.showLeftToolStripMenuItem.Image = this.linkedLeftButton.Image;
            this.showLeftToolStripMenuItem.Text = this.linkedLeftButton.Tag.ToString();
            this.showLeftToolStripMenuItem.Enabled = this.linkedLeftButton.Enabled;


            if (this.selectedTab == GlossaryTab.BusinessItems)
            {
                this.linkedRightButton.Image = Properties.Resources.linkedDataItemsRight;
                this.linkedRightButton.Tag = "Show linked Data Items";
            }
            else
            {
                this.linkedRightButton.Image = Properties.Resources.linkedColums;
                this.linkedRightButton.Tag = "Show linked Columns";
            }
            this.myToolTip.SetToolTip(this.linkedRightButton, this.linkedRightButton.Tag.ToString());
            this.linkedRightButton.Enabled = this.selectedItem is DataItem
                                            || this.selectedItem is BusinessItem;
            //take over properties from linkedRightButton
            this.showRightToolStripMenuItem.Image = this.linkedRightButton.Image;
            this.showRightToolStripMenuItem.Text = this.linkedRightButton.Tag.ToString();
            this.showRightToolStripMenuItem.Enabled = this.linkedRightButton.Enabled;
            //takeover properties from openProperties Button
            this.openPropertiesToolStripMenuItem.Enabled = this.openPropertiesButton.Enabled;
            this.openPropertiesToolStripMenuItem.Image = this.openPropertiesButton.Image;
            //takeover properties from navigateProjectBrowserbutton
            this.findInProjectBrowserToolStripMenuItem.Enabled = this.navigateProjectBrowserButton.Enabled;
            this.findInProjectBrowserToolStripMenuItem.Image = this.navigateProjectBrowserButton.Image;
            if (this.dataItemsSplitContainer.Panel2Collapsed)
            {
                //set left or right image on button
                this.showHideTablesButton.Image = Properties.Resources.moveLeftArrow;
                this.myToolTip.SetToolTip(this.showHideTablesButton, "Show Tables");
            }
            else
            {
                //set left or right image on button
                this.showHideTablesButton.Image = Properties.Resources.moveRightArrow;
                this.myToolTip.SetToolTip(this.showHideTablesButton, "Hide Tables");
            }
            //Enable/Disable fields on Business Items pane
            this.enableDisableBusinessItemFields();
            //Enable/Disable fields on the data items pane
            this.enableDisableDataItemFields();

        }
        private void enableDisableBusinessItemFields()
        {
            this.BU_NameTextBox.Enabled = this.selectedBusinessItem != null;
            this.BU_DescriptionTextBox.Enabled = this.selectedBusinessItem != null;
            this.BU_DomainComboBox.Enabled = this.selectedBusinessItem != null;
            this.BU_VersionTextBox.Enabled = this.selectedBusinessItem != null;
            this.BU_StatusCombobox.Enabled = this.selectedBusinessItem != null;
            this.BU_KeywordsTextBox.Enabled = this.selectedBusinessItem != null;
        }
        private void enableDisableDataItemFields()
        {
            this.DI_NameTextBox.Enabled = this.selectedDataItem != null;
            this.DI_DescriptionTextBox.Enabled = this.selectedDataItem != null;
            this.DI_DomainComboBox.Enabled = this.selectedDataItem != null;
            this.DI_LabelTextBox.Enabled = this.selectedDataItem != null;
            this.DI_DatatypeDropDown.Enabled = this.selectedDataItem != null;
            this.DI_SizeNumericUpDown.Enabled = this.selectedDataItem != null
                                                && this.DI_DatatypeDropDown.SelectedItem is LogicalDatatype
                                                && ((LogicalDatatype)this.DI_DatatypeDropDown.SelectedItem).hasLength;
            this.DI_PrecisionUpDown.Enabled = this.selectedDataItem != null
                                               && this.DI_DatatypeDropDown.SelectedItem is LogicalDatatype
                                               && ((LogicalDatatype)this.DI_DatatypeDropDown.SelectedItem).hasPrecision;
            this.DI_FormatTextBox.Enabled = this.selectedDataItem != null;
            this.DI_InitialValueTextBox.Enabled = this.selectedDataItem != null;
            this.DI_VersionTextBox.Enabled = this.selectedDataItem != null;
            this.DI_StatusComboBox.Enabled = this.selectedDataItem != null;
            this.DI_KeywordsTextBox.Enabled = this.selectedDataItem != null;
        }

        private bool columnsVisible()
        {
            return this.selectedTab == GlossaryTab.DataItems && !this.dataItemsSplitContainer.Panel2Collapsed;
        }

        public void clear()
        {
            //validate changes first
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.validateBusinessItemChanges();
                    break;
                case GlossaryTab.DataItems:
                    this.validateDataItemsChanges();
                    this.validateColumnChanges();
                    break;
            }
            this.BusinessItemsListView.ClearObjects();
            this.dataItemsListView.ClearObjects();
            this.columnsListView.ClearObjects();
            this.dColumnsListView.ClearObjects();
            this.loadSelectedItemData();
        }
        public List<BusinessItem> getBusinessItems()
        {
            return this.BusinessItemsListView.Objects.Cast<BusinessItem>().ToList();
        }
        public void setBusinessItems(IEnumerable<BusinessItem> businessItems, Domain domain)
        {
            //turn showing off
            this.businessItemsShowing = false;
            //set selected tab
            this.selectedTab = GlossaryTab.BusinessItems;
            this.BusinessItemsListView.Objects = businessItems;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = this.getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            this.BusinessItemsListView.SelectedObject = businessItems.FirstOrDefault();
            //make sure we are showing the correct data
            this.reloadBusinessItems();
            //turn back on
            this.businessItemsShowing = true;
        }
        public void setDataItems(IEnumerable<DataItem> dataItems, Domain domain)
        {
            //turn showing off
            this.dataItemsShowing = false;
            //set selected tab
            this.selectedTab = GlossaryTab.DataItems;
            this.dataItemsListView.Objects = dataItems;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = this.getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            this.dataItemsListView.SelectedObject = dataItems.FirstOrDefault();
            this.reloadDataItems();
            //turn showing back on
            this.dataItemsShowing = true;
        }
        internal void setColumns(List<EDDTable> tables, Domain domain)
        {
            this.columnsListView.Objects = tables;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = this.getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            //expand all
            this.columnsListView.ExpandAll();
            //select the first one
            this.columnsListView.SelectedObject = tables.FirstOrDefault()?.columns.FirstOrDefault();
        }
        internal void setTable(EDDTable table)
        {
            this.setTables(new List<EDDTable> { table });
            this.dColumnsListView.Objects = new List<EDDTable> { table };
            //expand all
            this.dColumnsListView.ExpandAll();
            //select the first one
            this.dColumnsListView.SelectedObject = table.columns.FirstOrDefault();
        }
        private void setTables(IEnumerable<EDDTable> tables)
        {
            this.dColumnsListView.Objects = tables;
            //expand all
            this.dColumnsListView.ExpandAll();
            //select the first one
            this.dColumnsListView.SelectedObject = tables.FirstOrDefault()?.columns.FirstOrDefault();
        }
        public List<DataItem> dataItems => this.dataItemsListView.FilteredObjects.Cast<DataItem>().ToList();
        internal void addItem(GlossaryItem newItem)
        {
            if (newItem is BusinessItem)
            {
                //add item to top of  list
                this.BusinessItemsListView.InsertObjects(0, new List<BusinessItem>() { (BusinessItem)newItem });
                //select it
                this.BusinessItemsListView.SelectObject(newItem);
            }
            else if (newItem is DataItem)
            {
                //add item to top of  list
                this.dataItemsListView.InsertObjects(0, new List<DataItem>() { (DataItem)newItem });
                //select it
                this.dataItemsListView.SelectObject(newItem);
            }
        }
        private KryptonBreadCrumbItem getBreadCrumbSubItem(KryptonBreadCrumbItem parentItem, Domain domain)
        {
            foreach (var subItem in parentItem.Items)
            {
                var foundDomain = (Domain)subItem.Tag;
                if (foundDomain.uniqueID == domain.uniqueID)
                {
                    return subItem; //found it
                }
                //not found, look deeper
                var foundItem = this.getBreadCrumbSubItem(subItem, domain);
                if (foundItem != null)
                {
                    return foundItem;
                }
            }
            //not found, return null
            return null;
        }
        public void setStatusses(List<String> statusses)
        {
            this.statusses = statusses;
            this.BU_StatusCombobox.DataSource = this.statusses;
            this.DI_StatusComboBox.DataSource = this.statusses;
        }
        private IEnumerable<LogicalDatatype> logicalDatatypes { get; set; }
        public void setLogicalDatatypes(IEnumerable<LogicalDatatype> logicalDatatypes)
        {
            this.logicalDatatypes = logicalDatatypes;
            this.DI_DatatypeDropDown.DataSource = this.logicalDatatypes;
            this.DI_DatatypeDropDown.DisplayMember = "name";
        }
        public void setDomains(List<Domain> domains)
        {
            this.domains = domains;
            this.BU_DomainComboBox.DataSource = this.domains;
            this.BU_DomainComboBox.DisplayMember = "displayName";
            this.DI_DomainComboBox.DataSource = this.domains;
            this.DI_DomainComboBox.DisplayMember = "displayName";

            //set the domains breadcrumb
            foreach (var domain in domains)
            {
                if (domain.parentDomain == null) //only process top level domains
                {
                    this.domainBreadCrumb.RootItem.Items.Add(this.createDomainBreadCrumbItem(domain));
                }
            }
        }

        public KryptonBreadCrumbItem createDomainBreadCrumbItem(Domain domain)
        {
            var breadCrumbItem = new KryptonBreadCrumbItem(domain.name);
            breadCrumbItem.Tag = domain;
            foreach (var subDomain in domain.subDomains)
            {
                breadCrumbItem.Items.Add(this.createDomainBreadCrumbItem(subDomain));
            }
            return breadCrumbItem;
        }
        private BusinessItem previousBusinessItem { get; set; }
        public BusinessItem selectedBusinessItem => this.BusinessItemsListView.SelectedObject as BusinessItem;
        private DataItem previousDataItem { get; set; }
        public DataItem selectedDataItem => this.dataItemsListView.SelectedObject as DataItem;
        private EDDColumn previousColumn { get; set; }
        public EDDColumn selectedColumn
        {
            get
            {
                switch (this.selectedTab)
                {
                    case GlossaryTab.DataItems:
                        return this.dColumnsListView.SelectedObject as EDDColumn;
                    default:
                        return null;
                }
            }
        }
        public IEDDItem selectedItem
        {
            get
            {
                switch (this.selectedTab)
                {
                    case GlossaryTab.BusinessItems:
                        return this.selectedBusinessItem;
                    case GlossaryTab.DataItems:
                        if (this.isColumnsFocussed)
                        {
                            if (this.selectedColumn != null)
                            {
                                return this.selectedColumn;
                            }

                            if (this.selectedTable != null)
                            {
                                return this.selectedTable;
                            }
                        }
                        return this.selectedDataItem;
                    default:
                        //should never happen
                        return this.selectedBusinessItem;
                }
            }
        }

        private void BusinessItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.reloadBusinessItems();
        }

        private void reloadBusinessItems()
        {
            //check if the business item data has been changed
            this.validateBusinessItemChanges();
            //then load the next item
            this.loadSelectedItemData();
            this.enableDisable();
            //set the previous business item
            this.previousBusinessItem = this.selectedBusinessItem;
        }

        private void validateBusinessItemChanges()
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
                //reset
                this.previousBusinessItem = null;
            }
        }
        private void dataItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.reloadDataItems();
            //if (this.tablesVisible)
            //{
            //    this.navigateRight();
            //}
        }

        private void reloadDataItems()
        {
            //validate the possible changes
            this.validateDataItemsChanges();
            //then load the next item
            this.loadSelectedItemData();
            this.enableDisable();
            //set the previous business item
            this.previousDataItem = this.selectedDataItem;
        }

        private void validateDataItemsChanges()
        {
            //check if the previous item has been changed
            if (this.previousDataItem != null)
            {
                if (this.hasDataItemChanged(this.previousDataItem))
                {
                    var response = MessageBox.Show(this, string.Format("Save changes to {0}?", this.previousDataItem.Name)
                                                     , "Unsaved changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (response == DialogResult.Yes)
                    {
                        this.unloadDataItem(this.previousDataItem);
                        this.saveDataItem(this.previousDataItem);
                    }
                }
                //reset
                this.previousDataItem = null;
            }
        }
        private bool hasBusinessitemChanged(BusinessItem businessItem)
        {
            return businessItem.Name != this.BU_NameTextBox.Text
                    || businessItem.domain?.domainPath != ((Domain)this.BU_DomainComboBox.SelectedItem)?.domainPath
                    || businessItem.Description != this.BU_DescriptionTextBox.Text
                    || businessItem.Status != this.BU_StatusCombobox.Text
                    || businessItem.Version != this.BU_VersionTextBox.Text
                    || string.Join(",", businessItem.Keywords) != this.BU_KeywordsTextBox.Text;
        }
        private bool hasDataItemChanged(DataItem dataItem)
        {
            return dataItem.Name != this.DI_NameTextBox.Text
                    || dataItem.domain?.domainPath != ((Domain)this.DI_DomainComboBox.SelectedItem)?.domainPath
                    || dataItem.Description != this.DI_DescriptionTextBox.Text
                    || dataItem.Status != this.DI_StatusComboBox.Text
                    || dataItem.Version != this.DI_VersionTextBox.Text
                    || string.Join(",", dataItem.Keywords) != this.DI_KeywordsTextBox.Text
                    || dataItem.businessItem?.GUID != ((BusinessItem)this.DI_BusinessItemTextBox.Tag)?.GUID
                    || dataItem.Label != this.DI_LabelTextBox.Text
                    || dataItem.logicalDatatype?.GUID != ((LogicalDatatype)this.DI_DatatypeDropDown.SelectedItem)?.GUID
                    || !dataItem.Size.HasValue && !string.IsNullOrEmpty(this.DI_SizeNumericUpDown.Text)
                    || dataItem.Size.HasValue && string.IsNullOrEmpty(this.DI_SizeNumericUpDown.Text)
                    || dataItem.Size.HasValue && dataItem.Size.Value != decimal.ToInt32(this.DI_SizeNumericUpDown.Value)
                    || !dataItem.Precision.HasValue && !string.IsNullOrEmpty(this.DI_PrecisionUpDown.Text)
                    || dataItem.Precision.HasValue && string.IsNullOrEmpty(this.DI_PrecisionUpDown.Text)
                    || dataItem.Precision.HasValue && dataItem.Precision.Value != decimal.ToInt32(this.DI_PrecisionUpDown.Value)
                    || dataItem.Format != this.DI_FormatTextBox.Text
                    || dataItem.InitialValue != this.DI_InitialValueTextBox.Text;

        }
        private bool hasColumnChanged(EDDColumn column)
        {
            return column.dataItem?.GUID != ((DataItem)this.dC_DataItemTextBox.Tag)?.GUID;
        }

        private void loadSelectedItemData()
        {
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.BU_NameTextBox.Text = this.selectedBusinessItem?.Name;
                    this.BU_DomainComboBox.SelectedItem = this.selectedBusinessItem?.domain;
                    this.BU_DescriptionTextBox.Text = this.selectedBusinessItem?.Description;
                    this.BU_StatusCombobox.Text = this.selectedBusinessItem?.Status;
                    this.BU_VersionTextBox.Text = this.selectedBusinessItem?.Version;
                    this.BU_KeywordsTextBox.Text = this.selectedBusinessItem != null ? string.Join(",", this.selectedBusinessItem.Keywords) : string.Empty;
                    this.BU_CreatedTextBox.Text = this.selectedBusinessItem?.CreateDate.ToString("G");
                    this.BU_CreatedByTextBox.Text = this.selectedBusinessItem?.Author;
                    this.BU_ModifiedDateTextBox.Text = this.selectedBusinessItem?.UpdateDate.ToString("G");
                    this.BU_ModifiedByTextBox.Text = this.selectedBusinessItem?.UpdatedBy;
                    break;
                case GlossaryTab.DataItems:
                    this.DI_NameTextBox.Text = this.selectedDataItem?.Name;
                    this.DI_DescriptionTextBox.Text = this.selectedDataItem?.Description;
                    this.DI_DomainComboBox.SelectedItem = this.selectedDataItem?.domain;
                    this.DI_BusinessItemTextBox.Text = this.selectedDataItem?.businessItem?.Name;
                    this.DI_BusinessItemTextBox.Tag = this.selectedDataItem?.businessItem;
                    this.DI_LabelTextBox.Text = this.selectedDataItem?.Label;
                    this.DI_DatatypeDropDown.SelectedItem = this.logicalDatatypes.FirstOrDefault(x => x.GUID == this.selectedDataItem?.logicalDatatype?.GUID);
                    this.DI_SizeNumericUpDown.Value = this.selectedDataItem != null && this.selectedDataItem.Size.HasValue ? this.selectedDataItem.Size.Value : 0;
                    this.DI_SizeNumericUpDown.Text = this.selectedDataItem != null && this.selectedDataItem.Size.HasValue ? this.selectedDataItem.Size.ToString() : string.Empty;
                    this.DI_PrecisionUpDown.Value = this.selectedDataItem != null && this.selectedDataItem.Precision.HasValue ? this.selectedDataItem.Precision.Value : 0;
                    this.DI_PrecisionUpDown.Text = this.selectedDataItem != null && this.selectedDataItem.Precision.HasValue ? this.selectedDataItem.Precision.ToString() : string.Empty;
                    this.DI_FormatTextBox.Text = this.selectedDataItem?.Format;
                    this.DI_InitialValueTextBox.Text = this.selectedDataItem?.InitialValue;
                    this.DI_StatusComboBox.Text = this.selectedDataItem?.Status;
                    this.DI_VersionTextBox.Text = this.selectedDataItem?.Version;
                    this.DI_KeywordsTextBox.Text = this.selectedDataItem != null ? string.Join(",", this.selectedDataItem.Keywords) : string.Empty;
                    this.DI_CreationDateTextBox.Text = this.selectedDataItem?.CreateDate.ToString("G");
                    this.DI_CreatedUserTextBox.Text = this.selectedDataItem?.Author;
                    this.DI_ModifiedDateTextBox.Text = this.selectedDataItem?.UpdateDate.ToString("G");
                    this.DI_ModifiedUserTextBox.Text = this.selectedDataItem?.UpdatedBy;
                    if (!this.dataItemsSplitContainer.Panel2Collapsed)
                    {
                        this.dC_DataItemTextBox.Text = this.selectedColumn?.dataItem?.Name;
                        this.dC_DataItemTextBox.Tag = this.selectedColumn?.dataItem;
                    }
                    break;
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    if (this.selectedBusinessItem != null)
                    {
                        this.saveBusinessItem(this.selectedBusinessItem);
                        //refresh listview
                        this.BusinessItemsListView.RefreshSelectedObjects();
                    }
                    break;
                case GlossaryTab.DataItems:
                    if (this.selectedDataItem != null)
                    {
                        this.saveDataItem(this.selectedDataItem);
                        //refresh listview
                        this.dataItemsListView.RefreshSelectedObjects();
                    }
                    break;
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
            this.unloadBusinessItemData(item);
            item.save();
        }
        private void saveDataItem(DataItem item)
        {
            this.unloadDataItem(item);
            item.save();
        }
        private void saveColumn(EDDColumn column)
        {
            this.unloadColumnData(column);
            column.save();
        }

        private void unloadDataItem(DataItem item)
        {
            item.Name = this.DI_NameTextBox.Text;
            item.businessItem = (BusinessItem)this.DI_BusinessItemTextBox.Tag;
            item.Description = this.DI_DescriptionTextBox.Text;
            item.domain = (Domain)this.DI_DomainComboBox.SelectedItem;
            item.Label = this.DI_LabelTextBox.Text;
            item.logicalDatatype = (LogicalDatatype)this.DI_DatatypeDropDown.SelectedItem;
            if (string.IsNullOrEmpty(this.DI_SizeNumericUpDown.Text))
            {
                item.Size = null;
            }
            else
            {
                item.Size = decimal.ToInt32(this.DI_SizeNumericUpDown.Value);
            }

            if (string.IsNullOrEmpty(this.DI_PrecisionUpDown.Text))
            {
                item.Precision = null;
            }
            else
            {
                item.Precision = decimal.ToInt32(this.DI_PrecisionUpDown.Value);
            }

            item.Format = this.DI_FormatTextBox.Text;
            item.InitialValue = this.DI_InitialValueTextBox.Text;
            item.Status = this.DI_StatusComboBox.Text;
            item.Version = this.DI_VersionTextBox.Text;
            item.Keywords = this.DI_KeywordsTextBox.Text.Split(',')
                                                .Select(x => x.Trim())
                                                .Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

        }
        private void unloadColumnData(EDDColumn column)
        {
            column.dataItem = this.dC_DataItemTextBox.Tag as DataItem;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.loadSelectedItemData();
        }
        public Domain selectedDomain
        {
            get => this.domainBreadCrumb.SelectedItem.Tag as Domain;
            set
            {
                if (value != null)
                {
                    //select corresponding domain item
                    this.domainBreadCrumb.SelectedItem = this.getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, value) ?? this.domainBreadCrumb.RootItem;
                }
            }
        }



        public event EventHandler selectedDomainChanged;
        private void domainBreadCrumb_SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.selectedTab == GlossaryTab.BusinessItems && this.businessItemsShowing
               || this.selectedTab == GlossaryTab.DataItems && this.dataItemsShowing)
            {
                selectedDomainChanged?.Invoke(this.selectedDomain, e);
            }

            this.enableDisable();
        }

        private void navigateProjectBrowserButton_Click(object sender, EventArgs e)
        {
            this.selectedItem?.selectInProjectBrowser();
        }

        private void openPropertiesButton_Click(object sender, EventArgs e)
        {
            this.selectedItem?.openProperties();
        }

        private void BusinessItemsListView_DoubleClick(object sender, EventArgs e)
        {
            this.selectedBusinessItem?.openProperties();
        }
        public event EventHandler newButtonClicked;
        private void newButton_Clicked(object sender, EventArgs e)
        {
            newButtonClicked?.Invoke(sender, e);
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (this.DetailsTabControl.SelectedTab == this.BusinessItemsTabPage)
            {
                var businessItemToDelete = this.selectedBusinessItem;
                var index = this.BusinessItemsListView.SelectedIndex;
                if (businessItemToDelete != null)
                {
                    this.BusinessItemsListView.RemoveObject(businessItemToDelete);
                    businessItemToDelete.delete();
                    this.BusinessItemsListView.SelectedIndex = index > 0 ? index - 1 : 0;
                }
            }
            else if (this.DetailsTabControl.SelectedTab == this.DataItemsTabPage)
            {
                var dataItemToDelete = this.selectedDataItem;
                var index = this.dataItemsListView.SelectedIndex;
                if (dataItemToDelete != null)
                {
                    this.dataItemsListView.RemoveObject(dataItemToDelete);
                    dataItemToDelete.delete();
                    this.dataItemsListView.SelectedIndex = index > 0 ? index - 1 : 0;
                }
            }
        }
        private void DetailsTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set mousecursor
            Cursor.Current = Cursors.WaitCursor;
            //validate possible changes
            switch (this.previousTab)
            {
                case GlossaryTab.BusinessItems:
                    this.validateBusinessItemChanges();
                    break;
                case GlossaryTab.DataItems:
                    this.validateDataItemsChanges();
                    break;
            }
            //set previous tab
            this.previousTab = this.selectedTab;
            //reset previous items
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.previousBusinessItem = null;
                    this.isColumnsFocussed = false;
                    break;
                case GlossaryTab.DataItems:
                    this.previousDataItem = null;
                    break;
            }
            this.enableDisable();

            Cursor.Current = Cursors.Default;
        }
        public GlossaryTab selectedTab
        {
            get
            {
                if (this.DetailsTabControl.SelectedTab == this.BusinessItemsTabPage)
                {
                    return GlossaryTab.BusinessItems;
                }
                //default dataitems
                return GlossaryTab.DataItems;
            }
            set
            {
                switch (value)
                {
                    case GlossaryTab.BusinessItems:
                        this.DetailsTabControl.SelectedTab = this.BusinessItemsTabPage;
                        break;
                    case GlossaryTab.DataItems:
                        this.DetailsTabControl.SelectedTab = this.DataItemsTabPage;
                        break;
                }
            }
        }
        public GlossaryTab previousTab { get; set; }

        private void DI_BusinessItemSelectButton_Click(object sender, EventArgs e)
        {
            if (this.selectedDataItem != null)
            {
                var businessItem = this.selectedDataItem.selectBusinessItem();
                this.DI_BusinessItemTextBox.Text = businessItem?.Name;
                this.DI_BusinessItemTextBox.Tag = businessItem;
                this.enableDisable();
            }
        }
        private void dataItemsListView_DoubleClick(object sender, EventArgs e)
        {
            this.selectedDataItem?.openProperties();
        }

        private void DI_DatatypeSelectButton_Click(object sender, EventArgs e)
        {
            if (this.selectedDataItem != null)
            {
                var dataType = this.selectedDataItem.selectLogicalDataType();
                //get the appropriate datatype
                this.DI_DatatypeDropDown.SelectedItem = this.logicalDatatypes.FirstOrDefault(x => x.GUID == dataType?.GUID);
            }
        }

        private void C_DataItemSelectButton_Click(object sender, EventArgs e)
        {
            if (this.selectedColumn != null)
            {
                var dataItem = this.selectedColumn.selectDataItem();
                this.C_DataItemTextBox.Text = dataItem?.Name;
                this.C_DataItemTextBox.Tag = dataItem;
            }
        }
        private void columnsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if column data has been changed
            this.validateColumnChanges();
            //then load the next item
            this.loadSelectedItemData();
            this.enableDisable();
            //set the previous business item
            this.previousColumn = this.selectedColumn;
        }
        private void validateColumnChanges()
        {
            //check if the previous item has been changed
            if (this.previousColumn != null)
            {
                if (this.hasColumnChanged(this.previousColumn))
                {
                    var response = MessageBox.Show(this, string.Format("Save changes to {0}?", this.previousColumn.name)
                                                     , "Unsaved changes!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (response == DialogResult.Yes)
                    {
                        this.unloadColumnData(this.previousColumn);
                        this.saveColumn(this.previousColumn);
                    }
                }
                //reset
                this.previousColumn = null;
            }
        }

        private void C_DatatypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        private EDDTable selectedTable
        {
            get
            {
                var table = this.dColumnsListView.SelectedObject as EDDTable;
                return table != null ? table : this.selectedColumn?.table;
            }
        }
        private void showAllColumnsButton_Click(object sender, EventArgs e)
        {
            if (this.selectedTable != null)
            {
                this.selectedTable.loadAllColumns();
                this.dColumnsListView.RefreshObject(this.selectedTable);
            }
        }

        private void nameFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.settingDefaults)
            {
                return; //not while setting the defaults
            }

            this.nameFilter = this.nameFilterTextBox.Text;

        }

        private void descriptionFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.settingDefaults)
            {
                return; //not while setting the defaults
            }

            this.descriptionFilter = this.descriptionFilterTextBox.Text;

        }
        public event EventHandler filterButtonClicked;
        private void filterButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            filterButtonClicked?.Invoke(sender, e);
            Cursor.Current = Cursors.Default;
        }

        private void nameFilterTextBox_Enter(object sender, EventArgs e)
        {
            if (this.nameFilterDefaultActive)
            {
                this.settingDefaults = true;
                this.nameFilterTextBox.Clear();
                this.settingDefaults = false;
                this.nameFilterDefaultActive = false;
                this.nameFilterTextBox.ForeColor = Color.Black;
            }
        }

        private void descriptionFilterTextBox_Enter(object sender, EventArgs e)
        {
            {
                if (this.descriptionFilterDefaultActive)
                {
                    this.settingDefaults = true;
                    this.descriptionFilterTextBox.Clear();
                    this.settingDefaults = false;
                    this.descriptionFilterDefaultActive = false;
                    this.descriptionFilterTextBox.ForeColor = Color.Black;
                }
            }
        }

        private void descriptionFilterTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.descriptionFilterTextBox.Text))
            {
                this.setFilterDefaults();
            }
        }

        private void nameFilterTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.nameFilterTextBox.Text))
            {
                this.setFilterDefaults();
            }
        }

        private void showHideTablesButton_Click(object sender, EventArgs e)
        {
            this.toggleTablesVisible();
        }
        private bool tablesVisible => !this.dataItemsSplitContainer.Panel2Collapsed;
        private void setTablesVisible()
        {
            if (!this.tablesVisible)
            {
                this.toggleTablesVisible();
            }
        }
        private void toggleTablesVisible()
        {
            this.dataItemsSplitContainer.Panel2Collapsed = !this.dataItemsSplitContainer.Panel2Collapsed;
            this.enableDisable();
        }
        public event EventHandler getTableButtonClicked;
        private void getTableButton_Click(object sender, EventArgs e)
        {
            getTableButtonClicked?.Invoke(sender, e);
        }

        private void dColumnsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if column data has been changed
            this.validateColumnChanges();
            //then load the next item
            this.loadSelectedItemData();
            this.enableDisable();
            //set the previous business item
            this.previousColumn = this.selectedColumn;
        }


        private void dC_DataItemSelectButton_Click(object sender, EventArgs e)
        {
            if (this.selectedColumn != null)
            {
                var dataItem = this.selectedColumn.selectDataItem();
                this.setLinkedDataItemData(this.selectedColumn, dataItem);
            }
        }
        private void setLinkedDataItemData(EDDColumn column, DataItem dataItem)
        {
            this.dC_DataItemTextBox.Text = dataItem?.Name;
            this.dC_DataItemTextBox.Tag = dataItem;
        }
        private void linkedRightButton_Click(object sender, EventArgs e)
        {
            this.navigateRight();
        }
        private void navigateRight()
        {
            //set mousecursor
            Cursor.Current = Cursors.WaitCursor;
            if (this.selectedItem is DataItem)
            {
                this.setTablesVisible();
                this.setTables(this.selectedDataItem.getLinkedColumns());
            }
            else if (this.selectedItem is BusinessItem)
            {
                this.setDataItems(this.selectedBusinessItem.linkedDataItems, this.selectedBusinessItem.domain);
            }
            //set mousecursor back to normal
            Cursor.Current = Cursors.Default;
        }

        private void linkedLeftButton_Click(object sender, EventArgs e)
        {
            this.navigateleft();
        }
        private void navigateleft()
        {
            //set mousecursor
            Cursor.Current = Cursors.WaitCursor;
            if (this.isColumnsFocussed && this.selectedColumn != null)
            {
                this.setDataItems(new List<DataItem> { this.selectedColumn.dataItem }, this.selectedColumn.dataItem?.domain);
            }
            else if (this.selectedItem is DataItem)
            {
                this.setBusinessItems(new List<BusinessItem> { this.selectedDataItem.businessItem }, this.selectedDataItem.domain);
            }
            //set mousecursor back to normal
            Cursor.Current = Cursors.Default;
        }

        private void dColumnsListView_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            var targetTable = e.TargetModel as EDDTable;
            var targetColumn = e.TargetModel as EDDColumn;
            var dataItem = e.SourceModels.Cast<Object>().FirstOrDefault() as DataItem;
            if (dataItem != null)
            {
                if (targetTable != null)
                {
                    //check if the dataItem has a valid logical datatype
                    if (dataItem.logicalDatatype == null)
                    {
                        e.Effect = DragDropEffects.None;
                        e.InfoMessage = "Data Item has no logical data type";
                    }
                    else if(dataItem.logicalDatatype.getBaseDatatype(targetTable.wrappedTable.factory.databaseName) == null)
                    {
                        //check if base datatype found
                        e.Effect = DragDropEffects.None;
                        e.InfoMessage = $"No technical datatype found for {targetTable.wrappedTable.factory.databaseName}";
                    }
                    else if (string.IsNullOrEmpty(dataItem.Label))
                    {
                        //check if data item has label
                        e.Effect = DragDropEffects.None;
                        e.InfoMessage = "Data Item has no label";
                    }
                    else
                    {
                        e.Effect = DragDropEffects.Link;
                        e.InfoMessage = "Create new column";
                    }
                }
                else if (targetColumn != null)
                {
                    e.Effect = DragDropEffects.Link;
                    e.InfoMessage = "Link dataitem to column";
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
                e.InfoMessage = "You can only drop DataItems";
            }
        }

        private void dColumnsListView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            try
            {
                var targetTable = e.TargetModel as EDDTable;
                var targetColumn = e.TargetModel as EDDColumn;
                var dataItem = e.SourceModels.Cast<Object>().FirstOrDefault() as DataItem;
                if (dataItem != null)
                {
                    if (targetTable != null)
                    {
                        //create new column
                        targetColumn = targetTable.addNewColumn(dataItem.Label);
                        targetColumn.dataItem = dataItem;
                        targetColumn.setDataItemDefaults();
                    }
                    if (targetColumn != null)
                    {
                        //refresh
                        this.dColumnsListView.RefreshObject(targetTable);
                        //select the target column
                        this.dColumnsListView.SelectedObject = targetColumn;
                        //set the data field
                        this.setLinkedDataItemData(targetColumn, dataItem);
                    }
                }
            }
            catch (Exception exc)
            {
                EAAddinFramework.EAAddinBase.processException(exc);
            }

        }

        private void saveColumnButton_Click(object sender, EventArgs e)
        {
            this.selectedColumn?.save();
        }

        private void cancelColumnButton_Click(object sender, EventArgs e)
        {
            this.selectedColumn?.reload();
            this.dColumnsListView.RefreshSelectedObjects();
        }

        public event EventHandler newLinkedButtonClicked;
        private void newLinkedButton_Click(object sender, EventArgs e)
        {
            newLinkedButtonClicked?.Invoke(sender, e);
        }
        public void refreshObject(IEDDItem item)
        {
            var dataItem = item as DataItem;
            if (dataItem != null)
            {
                this.dataItemsListView.RefreshObject(dataItem);
            }

            var businessItem = item as BusinessItem;
            if (businessItem != null)
            {
                this.BusinessItemsListView.RefreshObject(dataItem);
            }

            var table = item as EDDTable;
            if (table != null)
            {
                this.dColumnsListView.RefreshObject(table);
            }
        }
        bool isColumnsFocussed;
        private void dataItemsSplitContainer_Panel2_Enter(object sender, EventArgs e)
        {
            this.isColumnsFocussed = true;
            this.enableDisable();
        }

        private void dataItemsSplitContainer_Panel1_Enter(object sender, EventArgs e)
        {
            this.isColumnsFocussed = false;
            this.enableDisable();
        }

        private void dColumnsListView_DoubleClick(object sender, EventArgs e)
        {
            this.selectedItem?.openProperties();
        }

        private void findInProjectBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedItem?.selectInProjectBrowser();
        }

        private void openPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.selectedItem?.openProperties();
        }

        private void showLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.navigateleft();
        }

        private void showRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.navigateRight();
        }

        private void descriptionFilterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Cursor.Current = Cursors.WaitCursor;
                filterButtonClicked?.Invoke(sender, e);
                Cursor.Current = Cursors.Default;
            }

        }

        private void nameFilterTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                Cursor.Current = Cursors.WaitCursor;
                filterButtonClicked?.Invoke(sender, e);
                Cursor.Current = Cursors.Default;
            }
        }

        private void DI_DatatypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //enableDisable
            this.enableDisableDataItemFields();
            //get defaults
            var logicalDatatype = (LogicalDatatype)this.DI_DatatypeDropDown.SelectedItem;
            if (logicalDatatype == null)
            {
                return;
            }
            //set the defaults for size, precision, initial value and format
            this.DI_SizeNumericUpDown.Value = logicalDatatype.defaultSize.HasValue && logicalDatatype.hasLength ?
                                              logicalDatatype.defaultSize.Value
                                              : 0;
            this.DI_PrecisionUpDown.Value = logicalDatatype.defaultPrecision.HasValue && logicalDatatype.hasPrecision ?
                                            logicalDatatype.defaultPrecision.Value
                                             : 0;
            this.DI_InitialValueTextBox.Text = logicalDatatype.defaultInitialValue;
            this.DI_FormatTextBox.Text = logicalDatatype.defaultFormat;
        }

        private void dataItemsListView_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            var targetDataItem = e.TargetModel as DataItem;
            var sourceColumn = e.SourceModels.Cast<Object>().FirstOrDefault() as EDDColumn;
            if (sourceColumn != null)
            {
                if (targetDataItem != null)
                {
                    e.Effect = DragDropEffects.Link;
                    e.InfoMessage = "Link Column to Datatype";
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
                e.InfoMessage = "You can only drop Columns";
            }
        }

        private void dataItemsListView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            try
            {
                var targetDataItem = e.TargetModel as DataItem;
                var sourceColumn = e.SourceModels.Cast<Object>().FirstOrDefault() as EDDColumn;
                if (sourceColumn != null
                    && targetDataItem != null)
                {
                    //refresh
                    this.dColumnsListView.RefreshObject(sourceColumn.table);
                    //select the target column
                    this.dColumnsListView.SelectedObject = sourceColumn;
                    //set the data field
                    this.setLinkedDataItemData(sourceColumn, targetDataItem);
                }
            }
            catch (Exception exc)
            {
                EAAddinFramework.EAAddinBase.processException(exc);
            }
        }


    }

    public enum GlossaryTab
    {
        BusinessItems,
        DataItems
    }
}
