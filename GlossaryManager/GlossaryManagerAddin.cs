using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using GlossaryManager.GUI;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using DB = DatabaseFramework;
using DB_EA = EAAddinFramework.Databases;

using EAAddinFramework;
using EAAddinFramework.Utilities;
using EA;

namespace GlossaryManager
{

    public delegate void NewContextHandler(TSF_EA.ElementWrapper context);

    public class GlossaryManagerAddin : EAAddinBase
    {

        // menu constants
        const string menuName = "-&EDD";
        const string menuManage = "&Manage...";
        const string menuImportBusinessItems = "&Import Business Items...";
        const string menuImportDataItems = "&Import Data Items...";
        const string menuExportBusinessItems = "&Export Business Items...";
        const string menuExportDataItems = "&Export Data Items...";
        const string menuSettings = "&Settings...";
        const string menuAbout = "&About";
        const string menuTest = "Test";


        const string appTitle = "Enterprise Data Dictionary";
        const string appFQN = "GlossaryManager.GlossaryManagerUI";
        const string guiFQN = "GlossaryManager.GUI.EDD_MainControl";

        
        private bool showing = false;

        private GlossaryManagerSettings settings = null;
        private GlossaryItemFactory factory = null;
        private IEnumerable<LogicalDatatype> logicalDatatypes;


        private EDD_MainControl _mainControl;
        private EDD_MainControl mainControl
        {
            get
            {
                this.initialiseMainControl();
                return this._mainControl;
            }
        }
        private void initialiseMainControl()
        {
            if (this._mainControl == null && this.model != null)
            {
                this._mainControl = this.model.addTab(appTitle, guiFQN) as EDD_MainControl;
                this._mainControl.HandleDestroyed += this.handleHandleDestroyed;
                this._mainControl.selectedDomainChanged += this.selectedDomainChanged;
                this._mainControl.newButtonClicked += this.newButtonClicked;
                this._mainControl.filterButtonClicked += this.filterButtonClicked;
                this._mainControl.getTableButtonClicked += this.getTableButtonClicked;
                this._mainControl.newLinkedButtonClicked += this.newLinkedButtonClicked;
                this._mainControl.setDomains(Domain.getAllDomains(this.settings.businessItemsPackage, this.settings.dataItemsPackage));
                this._mainControl.setStatusses(statusses: this.model.getStatusses());
                this._mainControl.setLogicalDatatypes(this.logicalDatatypes);
            }
        }

        private void newLinkedButtonClicked(object sender, EventArgs e)
        {
            if (this._mainControl.selectedTab == GlossaryTab.DataItems)
            {
                var column = this._mainControl.selectedItem as EDDColumn;
                if (column != null)
                {
                    //create new DataItem
                    var newDataItem = this.addNewDataItem();
                    //set fields
                    newDataItem.Name = column.name;
                    newDataItem.Label = column.name;
                    newDataItem.Size = column.column?.type?.length;
                    newDataItem.Precision = column.column?.type?.precision;
                    newDataItem.domain = this._mainControl.selectedDomain;
                    //find the logical datatype corresponding to the datatype of the column
                    if (column?.column?.type?.type != null
                        && column.table?.wrappedTable?.databaseOwner != null)
                    {
                        newDataItem.logicalDatatype = this.logicalDatatypes.FirstOrDefault(x =>
                            x.getBaseDatatype(column.table.wrappedTable.databaseOwner.type)?.name
                                == column.column.type.type.name);
                    }
                    //set dataitem on column
                    column.dataItem = newDataItem;
                    column.save();
                    //move to the DataItems tab
                    this._mainControl.setDataItems(new List<DataItem> { newDataItem }, newDataItem.domain);
                }
                else
                {
                    //get selected dataitem
                    var dataItem = this._mainControl.selectedItem as DataItem;
                    if (dataItem == null) return;
                    //switch to business item tab
                    this._mainControl.selectedTab = GlossaryTab.BusinessItems;
                    //create new Business Item
                    var newBusinessItem = this.addNewBusinessItem();
                    //set fields
                    newBusinessItem.Name = dataItem.Name;
                    newBusinessItem.Description = dataItem.Description;
                    newBusinessItem.Keywords = dataItem.Keywords;
                    newBusinessItem.domain = dataItem.domain;
                    //link the dataItem to the businessItem
                    dataItem.businessItem = newBusinessItem;
                    dataItem.save();
                    //move to the BusinessItemsTab
                    this._mainControl.setBusinessItems(new List<BusinessItem> { newBusinessItem }, newBusinessItem.domain);
                    //refresh the dataItem
                    this._mainControl.refreshObject(dataItem);
                }
            }
            else if (this._mainControl.selectedTab == GlossaryTab.BusinessItems)
            {
                //get selected businessItem
                var businessItem = this._mainControl.selectedItem as BusinessItem;
                if (businessItem == null) return;
                //switch to data item tab
                this._mainControl.selectedTab = GlossaryTab.DataItems;
                //create new DataItem
                var newDataItem = this.addNewDataItem();
                //set fields
                newDataItem.Name = businessItem.Name;
                newDataItem.Description = businessItem.Description;
                newDataItem.Keywords = businessItem.Keywords;
                newDataItem.domain = businessItem.domain;
                newDataItem.businessItem = businessItem;
                //move to the BusinessItemsTab
                this._mainControl.setDataItems(new List<DataItem> { newDataItem }, newDataItem.domain);
                //refresh the dataItem
                this._mainControl.refreshObject(businessItem);
            }
        }

        private void getTableButtonClicked(object sender, EventArgs e)
        {
            var table = EDDTable.selectTable(this.model, this.settings);
            if (table != null)
            {
                table.loadAllColumns();
                this._mainControl.setTable(table);
            }
        }

        public void test()
        {
            var testForm = new EDD_TestForm();
            testForm.mainControl.selectedDomainChanged += this.selectedDomainChanged;
            testForm.mainControl.newButtonClicked += this.newButtonClicked;
            //testForm.mainControl.selectedTabChanged += this.selectedTabChanged;
            testForm.mainControl.setDomains(Domain.getAllDomains(this.settings.businessItemsPackage, this.settings.dataItemsPackage));
            testForm.mainControl.setStatusses(statusses: this.model.getStatusses());
            this._mainControl = testForm.mainControl;
            testForm.Show();
        }
        private void selectedDomainChanged(object sender, EventArgs e)
        {
            this.mainControl.clear();
            if (!showing)
                showItemsForDomain(this._mainControl.selectedDomain, this._mainControl.searchCriteria);
            showing = false;
        }
        private void selectedTabChanged(object sender, EventArgs e)
        {
            showItemsForDomain(this._mainControl.selectedDomain, this._mainControl.searchCriteria);
        }
        private void filterButtonClicked(object sender, EventArgs e)
        {
            showItemsForDomain(this.mainControl.selectedDomain, this.mainControl.searchCriteria);
        }
        private void showItemsForDomain(Domain domain, GlossaryItemSearchCriteria criteria)
        {
            switch (this.mainControl.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    var package = domain != null
                                        ? (TSF_EA.Package)domain.businessItemsPackage
                                        : (TSF_EA.Package)this.settings.businessItemsPackage;
                    this.showBusinessItems(package, domain, criteria);
                    break;
                case GlossaryTab.DataItems:
                    
                    this.showDataItems(domain, criteria);
                    break;
            }
        }

        private void newButtonClicked(object sender, EventArgs e)
        {
            switch (this.mainControl.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.mainControl.addItem(addNewBusinessItem());
                    break;
                case GlossaryTab.DataItems:
                    this.mainControl.addItem(addNewDataItem());
                    break;
            }
        }
        private DataItem addNewDataItem()
        {
            var package = this.mainControl.selectedDomain != null
                    ? this.mainControl.selectedDomain.dataItemsPackage
                    : this.settings.dataItemsPackage;
            if (package == null
                && this.mainControl.selectedDomain != null)
            {
                //add the missing package
                this.mainControl.selectedDomain.createMissingPackage();
                package = this.mainControl.selectedDomain.dataItemsPackage;
            }
            return this.factory.addNew<DataItem>(package);
        }
        private BusinessItem addNewBusinessItem()
        {
            UML.Classes.Kernel.Package package = this.mainControl.selectedDomain != null
                                                    ? this.mainControl.selectedDomain.businessItemsPackage
                                                    : this.settings.businessItemsPackage;
            if (package == null
            && this.mainControl.selectedDomain != null)
            {
                //add the missing package
                this.mainControl.selectedDomain.createMissingPackage();
                package = this.mainControl.selectedDomain.businessItemsPackage;
            }
            var newBusinessItem = this.factory.addNew<BusinessItem>(package);
            return newBusinessItem;
        }

        public GlossaryManagerAddin() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuManage,
                                //menuImportBusinessItems,
                                //menuImportDataItems,
                                //menuExportBusinessItems,
                                //menuExportDataItems,
                                menuSettings,
                                menuAbout
                                //,menuTest
                              };
        }

        private void handleHandleDestroyed(object sender, EventArgs e)
        {
            this._mainControl = null;
        }

        public override void EA_FileOpen(EA.Repository repository)
        {
            base.EA_FileOpen(repository);
            //close the tab if still open
            this.model.closeTab(appTitle);
            this._mainControl = null;
            //get settings
            this.settings = new GlossaryManagerSettings(this.model);
            //get the logical datatypes
            this.logicalDatatypes = LogicalDatatype.getAllLogicalDatatypes(this.model);
            //(re)-initialize
            if (this.settings.showWindowAtStartup) this.initialiseMainControl();
            this.factory = GlossaryItemFactory.getFactory(this.model, this.settings);
        }

        public override void EA_GetMenuState(EA.Repository repository,
                                             string location, string menuName,
                                             string itemName, ref bool isEnabled,
                                             ref bool isChecked)
        {
            switch (itemName)
            {
                case menuImportBusinessItems:
                //case menuImportDataItems:
                //    isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
                //    break;
                //case menuExportBusinessItems:
                //case menuExportDataItems:
                //    isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
                //    break;
                case menuSettings:
                    isEnabled = this.fullyLoaded;
                    break;
                default:
                    isEnabled = true;
                    break;
            }
        }

        public override void EA_MenuClick(EA.Repository repository, string location,
                                          string menuName, string itemName)
        {
            switch (itemName)
            {
                case menuManage:
                    this.manage();
                    break;
                //case menuImportBusinessItems:
                //    this.import<BusinessItem>();
                //    break;
                //case menuImportDataItems:
                //    this.import<DataItem>();
                //    break;
                //case menuExportBusinessItems:
                //    this.export<BusinessItem>();
                //    break;
                //case menuExportDataItems:
                //    this.export<DataItem>();
                //    break;
                case menuSettings:
                    this.openSettings();
                    break;
                case menuAbout:
                    this.about();
                    break;
                //case menuTest:
                //    this.test();
                //    break;
            }
        }
        /// <summary>
        /// return the MDG content for the EDD MDG (so it doesn't have to be loaded separately
        /// </summary>
        /// <param name="Repository"></param>
        /// <returns>the MDG file contents</returns>
        public override object EA_OnInitializeTechnologies(Repository Repository)
        {
            return Properties.Resources.EDD_MDG;
        }

        /// <summary>
        /// open the settign form
        /// </summary>
        private void openSettings()
        {
            var settingsForm = new EDD_SettingsForm(this.settings);
            settingsForm.browseBusinessItemsPackage += this.SettingsForm_browseBusinessItemsPackage;
            settingsForm.browseDataItemsPackage += this.SettingsForm_browseDataItemsPackage;
            settingsForm.ShowDialog(this.model.mainEAWindow);
        }
        /// <summary>
        /// browse the package for the data items
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void SettingsForm_browseDataItemsPackage(object sender, EventArgs e)
        {
            var dataItemsPackage = this.model.getUserSelectedPackage();
            if (dataItemsPackage != null)
                this.settings.dataItemsPackage = dataItemsPackage;
        }
        /// <summary>
        /// browse the package for the business items items
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">arguments</param>
        private void SettingsForm_browseBusinessItemsPackage(object sender, EventArgs e)
        {
            var businessItemsPackage = this.model.getUserSelectedPackage();
            if (businessItemsPackage != null)
                this.settings.businessItemsPackage = businessItemsPackage;
        }


        // activities

        internal TSF_EA.Package managedPackage { get; private set; }



        private void manage()
        {
            if (this.model == null) { return; }
            this.managedPackage = (TSF_EA.Package)this.model.selectedTreePackage;
            this.mainControl.clear();
            this.mainControl.selectedDomain = Domain.getDomain(this.managedPackage);
        }

        private void showBusinessItems(TSF_EA.Package package, Domain domain, GlossaryItemSearchCriteria searchCriteria)
        {
            if (domain == null) domain = Domain.getDomain(package);
            if (domain?.businessItemsPackage != null) package = (TSF_EA.Package)domain.businessItemsPackage;
            this.mainControl.setBusinessItems(this.list<BusinessItem>(package, searchCriteria), domain);
        }
        private void showDataItems( Domain domain, GlossaryItemSearchCriteria searchCriteria)
        {
            var package = (TSF_EA.Package)domain?.dataItemsPackage;
            if (package == null) package = (TSF_EA.Package)this.settings.dataItemsPackage;
            this.mainControl.setDataItems(this.list<DataItem>(package, searchCriteria),domain);
        }

        private void showColumns(List<DataItem> dataItems, Domain domain)
        {
            if (!dataItems.Any())
            {
                //call default method if no data items given
                showColumns();
            }
            else
            {
                //create the columns based on the DataItems shown in the GUI
                var columns = EDDColumn.createColumns(this.model, dataItems, this.settings);
                this.mainControl.setColumns(columns, domain);
            }
        }
        private void showColumns()
        {
            //get the databases
            var databases = this.getDatabases(this.model.selectedTreePackage);
            //get the list of columns
            var tables = new List<EDDTable>();
            foreach (var db in databases)
            {
                foreach (var table in db.tables)
                {
                    var eddTable = new EDDTable((DB_EA.Table)table, this.settings);
                    foreach (DB_EA.Column column in table.columns)
                    {
                        eddTable.addColumn(new EDDColumn(column, eddTable, this.settings));
                    }
                }
            }
            this.mainControl.setColumns(tables, null);
        }
        /// <summary>
        /// returns all databases under this package and the one above
        /// </summary>
        /// <param name="package">the package to start from</param>
        /// <returns>all databases under the given package or above it</returns>
        private List<DB.Database> getDatabases(UML.Classes.Kernel.Package package)
        {
            var foundDatabases = new List<DB.Database>();
            //return empty list if package null
            if (package == null) return foundDatabases;
            var databasePackages = new List<UML.Classes.Kernel.Package>();
            //first get the «database» packages from the selected package
            databasePackages.AddRange(package.getNestedPackageTree(true));
            //then add the parent package(s)
            databasePackages.AddRange(package.getAllOwners().OfType<UML.Classes.Kernel.Package>());
            foreach (TSF_EA.Package databasePackage in databasePackages.Where(x => x.stereotypes.Any(y => y.name.ToLower() == "database")))
            {
                string databaseType = databasePackage.taggedValues.FirstOrDefault(x => x.name.ToLower() == "dbms")?.tagValue.ToString();
                if (!string.IsNullOrEmpty(databaseType))
                {
                    var dbFactory = DB_EA.DatabaseFactory.getFactory(databaseType, this.model, new DB_EA.Strategy.StrategyFactory());
                    foundDatabases.Add(dbFactory.createDataBase(databasePackage));
                }
            }
            return foundDatabases;
        }

        private void import<T>() where T : GlossaryItem, new()
        {
            this.import<T>((TSF_EA.Package)this.model.selectedElement);
        }

        internal void import<T>(TSF_EA.Package package) where T : GlossaryItem, new()
        {
            var file = this.getFileFor<T>(CSV.Loading);
            if (file == null) { return; }

            this.log("importing in package " + package.ToString());

            Dictionary<string, TSF_EA.Class> index = this.index<T>(package);
            List<T> items = GlossaryItem.Load<T>(file);

            foreach (T item in items)
            {
                if (item.GUID == "")
                {                           // create
                    this.log("importing " + item.ToString());
                    item.AsClassIn(package);
                }
                else
                {
                    if (index.ContainsKey(item.GUID))
                    {
                        if (item.toBeDeleted)
                        {                             // delete
                            this.log("removing " + item.Name);
                            package.deleteOwnedElement(index[item.GUID]);
                        }
                        else
                        {   // update
                            this.log("updating " + item.Name);
                            item.save();//TODO: check does this work without parameter??
                        }
                    }
                    else
                    {
                        this.log("WARNING: item (" + item.GUID + ") is not part of this package.");
                    }
                }
            }
            //this.refresh();
        }

        private void export<T>() where T : GlossaryItem, new()
        {
           // this.export<T>(this.list<T>());
        }

        internal void export<T>(List<T> exported) where T : GlossaryItem
        {
            var topic = typeof(T).Name;
            var file = this.getFileFor<T>(CSV.Saving);
            if (file != null)
            {
                this.log("exporting to " + file.ToString());
                // TODO: why iterating a list of the same type ?!
                //       why can an item be null?
                List<T> items = new List<T>();
                foreach (var item in exported)
                {
                    if (item != null)
                    {
                        this.log("exporting " + item.ToString());
                        items.Add(item);
                    }
                }
                GlossaryItem.Save<T>(file, items);
            }
        }

        private void about()
        {
            new AboutWindow().ShowDialog(this.model.mainEAWindow);
        }

        // support for listing and indexing a/the selected package


        private List<T> list<T>(TSF_EA.Package package, GlossaryItemSearchCriteria criteria) where T : GlossaryItem, new()
        {
            if (package == null) return new List<T>();
            return this.factory.getGlossaryItemsFromPackage<T>(package, criteria);
        }

        private Dictionary<string, TSF_EA.Class> index<T>() where T : GlossaryItem, new()
        {
            return this.index<T>((TSF_EA.Package)this.model.selectedElement);
        }

        private Dictionary<string, TSF_EA.Class> index<T>(TSF_EA.Package package)
          where T : GlossaryItem, new()
        {
            Dictionary<string, TSF_EA.Class> map =
              new Dictionary<string, TSF_EA.Class>();
            foreach (TSF_EA.Class clazz in package.ownedElements.OfType<TSF_EA.Class>())
            {
                if (this.factory.IsA<T>(clazz))
                {
                    map.Add(clazz.guid, clazz);
                }
            }
            return map;
        }

        // support for CSV File IO Dialog boxes

        private enum CSV { Loading, Saving }

        private string getFileFor<T>(CSV activity)
        {
            var topic = typeof(T).Name + "s";
            FileDialog selection = activity == CSV.Loading ?
              (FileDialog)new OpenFileDialog()
              {
                  Filter = "CSV File | *.csv;*.txt",
                  Title = "Load a CSV with " + topic,
                  FilterIndex = 1,
                  Multiselect = false
              }
              :
              (FileDialog)new SaveFileDialog()
              {
                  Filter = "CSV File | *.csv;*.txt",
                  Title = "Save " + topic + " to a CSV File",
                  FilterIndex = 1
              }
            ;
            if (selection.ShowDialog() == DialogResult.OK)
            {
                return selection.FileName;
            }
            return null;
        }

        // support for logging to the EA log window

        internal void clearLog()
        {
            if (this.model == null) { return; }
            EAOutputLogger.clearLog(this.model, this.settings.outputName);
        }

        internal void log(string msg)
        {
            if (this.model == null) { return; }
            EAOutputLogger.log(this.model, this.settings.outputName, msg);
        }
        internal void logError(string msg)
        {
            if (this.model == null) { return; }
            EAOutputLogger.log(this.model, this.settings.outputName, msg, 0, LogTypeEnum.error);
        }

    }
}
