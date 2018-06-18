using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using DatabaseFramework;
using DB_EA = EAAddinFramework.Databases;
using BrightIdeasSoftware;

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
            InitializeComponent();
            enableDisable();
            setColumnsListViewDelegates();
            setFilterDefaults();
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
        public GlossaryItemSearchCriteria searchCriteria
        {
            get
            {
                return new GlossaryItemSearchCriteria()
                {
                    nameSearchTerm = this.nameFilter,
                    descriptionSearchTerm = this.descriptionFilter,
                    showAll = this.showAllCheckBox.Checked
                };
            }
        }
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
                   if (rowObject is EDDTable) return "table";
                   if (rowObject is EDDColumn) return "column";
                   else return string.Empty;
               };
            this.C_NameColumn.ImageGetter = imageGetter;
            this.dC_NameColumn.ImageGetter = imageGetter;
        }

        private void enableDisable()
        {
            this.openPropertiesButton.Enabled = this.selectedItem != null;
            this.navigateProjectBrowserButton.Enabled = this.selectedItem != null;
            C_SizeUpDown.Enabled = ((BaseDataType)C_DatatypeDropdown.SelectedItem)?.hasLength == true;
            if (!C_SizeUpDown.Enabled) C_SizeUpDown.Text = string.Empty;
            C_PrecisionUpDown.Enabled = ((BaseDataType)C_DatatypeDropdown.SelectedItem)?.hasPrecision == true;
            if (!C_PrecisionUpDown.Enabled) C_PrecisionUpDown.Text = string.Empty;
            this.newButton.Enabled = this.selectedDomain != null;
            this.showLinkedColumnsButton.Enabled = this.selectedDataItem != null;
            //make the specific columns button invisible
            this.showAllColumnsButton.Visible = columnsVisible();
            this.getTableButton.Visible = columnsVisible();
        }

        private bool columnsVisible()
        {
            return this.selectedTab == GlossaryTab.Columns
                || (this.selectedTab == GlossaryTab.DataItems && !this.dataItemsSplitContainer.Panel2Collapsed);
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
            this.businessItemsShowing = true;
            this.BusinessItemsListView.Objects = businessItems;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            this.BusinessItemsListView.SelectedObject = businessItems.FirstOrDefault();
        }
        public void setDataItems(IEnumerable<DataItem> dataItems, Domain domain)
        {
            this.dataItemsShowing = true;
            this.dataItemsListView.Objects = dataItems;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
            }
            this.dataItemsListView.SelectedObject = dataItems.FirstOrDefault();
        }
        internal void setColumns(List<EDDTable> tables, Domain domain)
        {
            this.columnsListView.Objects = tables;
            if (domain != null)
            {
                //select corresponding domain item
                this.domainBreadCrumb.SelectedItem = getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, domain) ?? this.domainBreadCrumb.RootItem;
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
        public List<DataItem> dataItems
        {
            get
            {
                return this.dataItemsListView.FilteredObjects.Cast<DataItem>().ToList();
            }
        }
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
            DI_StatusComboBox.DataSource = this.statusses;
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
            BU_DomainComboBox.DataSource = this.domains;
            BU_DomainComboBox.DisplayMember = "displayName";
            DI_DomainComboBox.DataSource = this.domains;
            DI_DomainComboBox.DisplayMember = "displayName";

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
        public BusinessItem selectedBusinessItem
        {
            get
            {
                return this.BusinessItemsListView.SelectedObject as BusinessItem;
            }
        }
        private DataItem previousDataItem { get; set; }
        public DataItem selectedDataItem
        {
            get
            {
                return this.dataItemsListView.SelectedObject as DataItem;
            }
        }
        private EDDColumn previousColumn { get; set; }
        public EDDColumn selectedColumn
        {
            get
            {
                switch (this.selectedTab)
                {
                    case GlossaryTab.DataItems:
                        return this.dColumnsListView.SelectedObject as EDDColumn;
                    case GlossaryTab.Columns:
                        return this.columnsListView.SelectedObject as EDDColumn;
                    default:
                        return null;
                }
            }
        }
        private IEDDItem selectedItem
        {
            get
            {
                switch (this.selectedTab)
                {
                    case GlossaryTab.BusinessItems:
                        return this.selectedBusinessItem;
                    case GlossaryTab.DataItems:
                        return this.selectedDataItem;
                    default:
                        //should never happen
                        return this.selectedBusinessItem;
                }
            }
        }

        private void BusinessItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if the business item data has been changed
            validateBusinessItemChanges();
            //then load the next item
            loadSelectedItemData();
            enableDisable();
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
            //validate the possible changes
            validateDataItemsChanges();
            //then load the next item
            loadSelectedItemData();
            enableDisable();
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
                    || dataItem.businessItem?.GUID != ((BusinessItem)DI_BusinessItemTextBox.Tag)?.GUID
                    || dataItem.Label != this.DI_LabelTextBox.Text
                    || dataItem.logicalDatatype?.GUID != ((LogicalDatatype)DI_DatatypeDropDown.SelectedItem)?.GUID
                    || !dataItem.Size.HasValue && !string.IsNullOrEmpty(DI_SizeNumericUpDown.Text)
                    || dataItem.Size.HasValue && string.IsNullOrEmpty(DI_SizeNumericUpDown.Text)
                    || dataItem.Size.HasValue && dataItem.Size.Value != decimal.ToInt32(DI_SizeNumericUpDown.Value)
                    || !dataItem.Precision.HasValue && !string.IsNullOrEmpty(DI_PrecisionUpDown.Text)
                    || dataItem.Precision.HasValue && string.IsNullOrEmpty(DI_PrecisionUpDown.Text)
                    || dataItem.Precision.HasValue && dataItem.Precision.Value != decimal.ToInt32(DI_PrecisionUpDown.Value)
                    || dataItem.Format != DI_FormatTextBox.Text
                    || dataItem.InitialValue != DI_InitialValueTextBox.Text;

        }
        private bool hasColumnChanged(EDDColumn column)
        {
            //TODO
            return column.dataItem?.GUID != ((DataItem)this.dC_DataItemTextBox.Tag)?.GUID;
        }

        private void loadSelectedItemData()
        {
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.BU_NameTextBox.Text = selectedBusinessItem?.Name;
                    this.BU_DomainComboBox.SelectedItem = selectedBusinessItem?.domain;
                    this.BU_DescriptionTextBox.Text = selectedBusinessItem?.Description;
                    this.BU_StatusCombobox.Text = selectedBusinessItem?.Status;
                    this.BU_VersionTextBox.Text = selectedBusinessItem?.Version;
                    this.BU_KeywordsTextBox.Text = selectedBusinessItem != null ? string.Join(",", selectedBusinessItem.Keywords) : string.Empty;
                    this.BU_CreatedTextBox.Text = selectedBusinessItem?.CreateDate.ToString("G");
                    this.BU_CreatedByTextBox.Text = selectedBusinessItem?.Author;
                    this.BU_ModifiedDateTextBox.Text = selectedBusinessItem?.UpdateDate.ToString("G");
                    this.BU_ModifiedByTextBox.Text = selectedBusinessItem?.UpdatedBy;
                    break;
                case GlossaryTab.DataItems:
                    this.DI_NameTextBox.Text = selectedDataItem?.Name;
                    this.DI_DescriptionTextBox.Text = selectedDataItem?.Description;
                    this.DI_DomainComboBox.SelectedItem = selectedDataItem?.domain;
                    this.DI_BusinessItemTextBox.Text = selectedDataItem?.businessItem?.Name;
                    this.DI_BusinessItemTextBox.Tag = selectedDataItem?.businessItem;
                    this.DI_LabelTextBox.Text = selectedDataItem?.Label;
                    this.DI_DatatypeDropDown.SelectedItem = this.logicalDatatypes.FirstOrDefault(x => x.GUID == this.selectedDataItem?.logicalDatatype?.GUID);
                    this.DI_SizeNumericUpDown.Value =  this.selectedDataItem != null && this.selectedDataItem.Size.HasValue ? this.selectedDataItem.Size.Value : 0;
                    this.DI_SizeNumericUpDown.Text = this.selectedDataItem != null && this.selectedDataItem.Size.HasValue ? this.selectedDataItem.Size.ToString() : string.Empty;
                    this.DI_PrecisionUpDown.Value = this.selectedDataItem != null && this.selectedDataItem.Precision.HasValue ? this.selectedDataItem.Precision.Value : 0;
                    this.DI_PrecisionUpDown.Text = this.selectedDataItem != null && this.selectedDataItem.Precision.HasValue ? this.selectedDataItem.Precision.ToString() : string.Empty;
                    this.DI_FormatTextBox.Text = this.selectedDataItem?.Format;
                    this.DI_InitialValueTextBox.Text = this.selectedDataItem?.InitialValue;
                    this.DI_StatusComboBox.Text = selectedDataItem?.Status;
                    this.DI_VersionTextBox.Text = selectedDataItem?.Version;
                    this.DI_KeywordsTextBox.Text = selectedDataItem != null ? string.Join(",", selectedDataItem.Keywords) : string.Empty ;
                    this.DI_CreationDateTextBox.Text = selectedDataItem?.CreateDate.ToString("G");
                    this.DI_CreatedUserTextBox.Text = selectedDataItem?.Author;
                    this.DI_ModifiedDateTextBox.Text = selectedDataItem?.UpdateDate.ToString("G");
                    this.DI_ModifiedUserTextBox.Text = selectedDataItem?.UpdatedBy;
                    if (! this.dataItemsSplitContainer.Panel2Collapsed)
                    {
                        this.dC_DataItemTextBox.Text = this.selectedColumn?.dataItem?.Name;
                        this.dC_DataItemTextBox.Tag = this.selectedColumn?.dataItem;
                    }
                    break;
                case GlossaryTab.Columns:
                    this.C_NameTextBox.Text = this.selectedColumn?.name;
                    this.C_SizeUpDown.Text = this.selectedColumn?.column.type?.length.ToString();
                    this.C_SizeUpDown.Value = this.selectedColumn?.column.type != null ? this.selectedColumn.column.type.length : 0;
                    this.C_PrecisionUpDown.Value = this.selectedColumn?.column.type != null ? this.selectedColumn.column.type.precision : 0;
                    this.C_PrecisionUpDown.Text = this.selectedColumn?.column.type?.precision.ToString();
                    C_DatatypeDropdown.Items.Clear();
                    if (this.selectedColumn != null)
                    {
                        foreach (var datatype in this.selectedColumn.column.factory.baseDataTypes)
                        {
                            C_DatatypeDropdown.Items.Add(datatype);
                        }
                    }
                    this.C_DatatypeDropdown.DisplayMember = "name";
                    this.C_DatatypeDropdown.Text = this.selectedColumn?.column.type?.name;
                    if (this.selectedColumn?.column.type != null
                        && this.C_DatatypeDropdown.SelectedItem == null)
                    {
                        //find the datatype
                        this.C_DatatypeDropdown.Items.Add(this.selectedColumn.column.type.type);
                        this.C_DatatypeDropdown.SelectedItem = this.selectedColumn.column.type.type;
                    }
                    this.C_NotNullCheckBox.Checked = this.selectedColumn != null ? this.selectedColumn.column.isNotNullable : false;
                    this.C_DefaultTextBox.Text = this.selectedColumn?.column.initialValue;
                    this.C_DataItemTextBox.Text = this.selectedColumn?.dataItem?.Name;
                    this.C_DataItemTextBox.Tag = this.selectedColumn?.dataItem;
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
                        saveBusinessItem(this.selectedBusinessItem);
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
                    if (this.selectedColumn != null)
                    {
                        this.saveColumn(this.selectedColumn);
                        //refresh listview
                        this.dColumnsListView.RefreshSelectedObjects();
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
            unloadBusinessItemData(item);
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
                item.Size = null;
            else
                item.Size = decimal.ToInt32(this.DI_SizeNumericUpDown.Value);
            if (string.IsNullOrEmpty(DI_PrecisionUpDown.Text))
                item.Precision = null;
            else
                item.Precision = decimal.ToInt32(this.DI_PrecisionUpDown.Value);
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
            loadSelectedItemData();
        }
        public Domain selectedDomain
        {
            get { return domainBreadCrumb.SelectedItem.Tag as Domain; }
            set
            {
                if (value != null)
                {
                    //select corresponding domain item
                    this.domainBreadCrumb.SelectedItem = getBreadCrumbSubItem(this.domainBreadCrumb.RootItem, value) ?? this.domainBreadCrumb.RootItem;
                }
            }
        }



        public event EventHandler selectedDomainChanged;
        private void domainBreadCrumb_SelectedItemChanged(object sender, EventArgs e)
        {
            if (this.selectedTab == GlossaryTab.BusinessItems && businessItemsShowing
               || this.selectedTab == GlossaryTab.DataItems && dataItemsShowing)
                    this.selectedDomainChanged?.Invoke(this.selectedDomain, e);
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
            this.newButtonClicked?.Invoke(sender, e);
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
                case GlossaryTab.Columns:
                    this.validateColumnChanges();
                    break;
            }
            //set previous tab
            this.previousTab = this.selectedTab;
            //reset previous items
            switch (this.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.previousBusinessItem = null;
                    break;
                case GlossaryTab.DataItems:
                    this.previousDataItem = null;
                    break;
                case GlossaryTab.Columns:
                    this.previousColumn = null;
                    break;
            }
            this.enableDisable();

            Cursor.Current = Cursors.Default;
        }
        public GlossaryTab selectedTab
        {
            get
            {
                if (this.DetailsTabControl.SelectedTab == this.BusinessItemsTabPage) return GlossaryTab.BusinessItems;
                if (this.DetailsTabControl.SelectedTab == this.DataItemsTabPage) return GlossaryTab.DataItems;
                else return GlossaryTab.Columns;
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

                if (dataItem != null &&
                    this.selectedColumn.dataItem.GUID != dataItem.GUID)
                {
                    //reset to defaults based on the logical datatype and its technical mapping
                    this.C_NameTextBox.Text = dataItem.Label;
                    this.C_SizeUpDown.Value = dataItem.Size.HasValue ? dataItem.Size.Value : 0;
                    this.C_SizeUpDown.Text = dataItem.Size.HasValue ? dataItem.Size.Value.ToString() : string.Empty;
                    this.C_PrecisionUpDown.Value = dataItem.Precision.HasValue ? dataItem.Precision.Value : 0;
                    this.C_PrecisionUpDown.Text = dataItem.Precision.HasValue ? dataItem.Precision.Value.ToString() : string.Empty;
                    this.C_DatatypeDropdown.Text = dataItem.logicalDatatype?.getBaseDatatype(this.selectedColumn.column.factory.databaseName)?.name;
                    this.C_DefaultTextBox.Text = dataItem.InitialValue;
                }
            }
        }
        private void columnsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //check if column data has been changed
            validateColumnChanges();
            //then load the next item
            loadSelectedItemData();
            enableDisable();
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
            enableDisable();
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
            if (settingDefaults) return; //not while setting the defaults
            this.nameFilter = this.nameFilterTextBox.Text;

        }

        private void descriptionFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            if (settingDefaults) return; //not while setting the defaults
            this.descriptionFilter = descriptionFilterTextBox.Text;

        }
        public event EventHandler filterButtonClicked;
        private void filterButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.filterButtonClicked?.Invoke(sender, e);
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
                this.setFilterDefaults();
        }

        private void nameFilterTextBox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.nameFilterTextBox.Text))
                this.setFilterDefaults();
        }

        private void showHideTablesButton_Click(object sender, EventArgs e)
        {
            this.toggleTablesVisible();
        }
        private void setTablesVisible()
        {
            if (dataItemsSplitContainer.Panel2Collapsed) this.toggleTablesVisible();
        }
        private void toggleTablesVisible()
        {
            //set left or right image on button
            this.showHideTablesButton.Image = dataItemsSplitContainer.Panel2Collapsed ?
                                            Properties.Resources.moveRightArrow :
                                            Properties.Resources.moveLeftArrow;
            dataItemsSplitContainer.Panel2Collapsed = !dataItemsSplitContainer.Panel2Collapsed;
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
            validateColumnChanges();
            //then load the next item
            loadSelectedItemData();
            enableDisable();
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
            //set the dataitem
            column.dataItem = dataItem;
        }
        private void showLinkedColumnsButton_Click(object sender, EventArgs e)
        {
            //set mousecursor
            Cursor.Current = Cursors.WaitCursor;
            this.setTablesVisible();
            this.setTables(this.selectedDataItem.getLinkedColumns());
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
                    e.Effect = DragDropEffects.Link;
                    e.InfoMessage = "Create new column";
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
                }
                if (targetColumn != null)
                {
                    //refresh
                    dColumnsListView.RefreshObject(targetTable);
                    //select the target column
                    this.dColumnsListView.SelectedObject = targetColumn;
                    //set the data field
                    this.setLinkedDataItemData(targetColumn, dataItem);
                    
                }
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
    }

    public enum GlossaryTab
    {
        BusinessItems,
        DataItems,
        Columns
    }
}
