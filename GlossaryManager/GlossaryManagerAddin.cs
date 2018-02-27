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


        const string appTitle = "Enterprise Data Dictionary";
        const string appFQN = "GlossaryManager.GlossaryManagerUI";
        const string guiFQN = "GlossaryManager.GUI.EDD_MainControl";

        private TSF_EA.Model model = null;
        public TSF_EA.Model Model { get { return this.model; } }
        private bool fullyLoaded = false;

        private GlossaryManagerSettings settings = null;
        private GlossaryItemFactory factory = null;


        private EDD_MainControl _mainControl;
        private EDD_MainControl mainControl
        {
            get
            {
                if (this._mainControl == null && this.model != null)
                {
                    this._mainControl = this.model.addTab(appTitle, guiFQN) as EDD_MainControl;
                    this._mainControl.HandleDestroyed += this.handleHandleDestroyed;
                    this._mainControl.selectedDomainChanged += this.selectedDomainChanged;
                    this._mainControl.newButtonClick += this._mainControl_newButtonClick;
                    this._mainControl.selectedTabChanged += this._mainControl_selectedTabChanged;
                    this._mainControl.setDomains(Domain.getAllDomains(this.settings.businessItemsPackage, this.settings.dataItemsPackage));
                    this._mainControl.setStatusses(statusses: this.model.getStatusses());
                    //TODO: add additional events
                }
                return this._mainControl;
            }
        }
        private void selectedDomainChanged(object sender, EventArgs e)
        {
            showItemsForSelectedDomain();
        }
        private void _mainControl_selectedTabChanged(object sender, EventArgs e)
        {
            showItemsForSelectedDomain();
        }
        private void showItemsForSelectedDomain()
        {
            var selectedDomain = this._mainControl.selectedDomain;
            if (selectedDomain != null)
            {
                this.managedPackage = (TSF_EA.Package)selectedDomain.businessItemsPackage;
            }
            else
            {
                this.managedPackage = (TSF_EA.Package)this.settings.businessItemsPackage;
            }
            switch (this.mainControl.selectedTab)
            {
                case GlossaryTab.BusinessItems:
                    this.managedPackage = selectedDomain != null
                                        ? (TSF_EA.Package)selectedDomain.businessItemsPackage
                                        : (TSF_EA.Package)this.settings.businessItemsPackage;
                    this.showBusinessItems();
                    break;
                case GlossaryTab.DataItems:
                    this.managedPackage = selectedDomain != null
                                        ? (TSF_EA.Package)selectedDomain.dataItemsPackage
                                        : (TSF_EA.Package)this.settings.dataItemsPackage;
                    this.showDataItems();
                    break;
                case GlossaryTab.Columns:
                    this.showColumns();
                    break;
            }
        }

        private void _mainControl_newButtonClick(object sender, EventArgs e)
        {
            //crete new item in the selected package
            //BusinessItem
            var parentPackage = mainControl.selectedDomain?.businessItemsPackage;
            if (parentPackage == null) parentPackage = this.settings.businessItemsPackage;
            var newItem = this.factory.addNew<BusinessItem>(parentPackage);
            this.mainControl.addItem(newItem);
        }

        public GlossaryManagerAddin() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
        menuManage,
        menuImportBusinessItems,
        menuImportDataItems,
        menuExportBusinessItems,
        menuExportDataItems,
        menuSettings,
        menuAbout
      };
        }

        private void handleHandleDestroyed(object sender, EventArgs e)
        {
            this._mainControl = null;
        }

        public override void EA_FileOpen(EA.Repository repository)
        {
            this.model = new TSF_EA.Model(repository);
            this.settings = new GlossaryManagerSettings(this.model);
            this.factory = new GlossaryItemFactory(this.settings);
            this.fullyLoaded = true;
        }

        public override void EA_GetMenuState(EA.Repository repository,
                                             string location, string menuName,
                                             string itemName, ref bool isEnabled,
                                             ref bool isChecked)
        {
            switch (itemName)
            {
                case menuImportBusinessItems:
                case menuImportDataItems:
                    isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
                    break;
                case menuExportBusinessItems:
                case menuExportDataItems:
                    isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
                    break;
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
                case menuImportBusinessItems:
                    this.import<BusinessItem>();
                    break;
                case menuImportDataItems:
                    this.import<DataItem>();
                    break;
                case menuExportBusinessItems:
                    this.export<BusinessItem>();
                    break;
                case menuExportDataItems:
                    this.export<DataItem>();
                    break;
                case menuSettings:
                    this.openSettings();
                    break;
                case menuAbout: this.about(); break;
            }
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
            this.managedPackage = (TSF_EA.Package)this.Model.selectedTreePackage;
            //this.refresh();
            this.showBusinessItems();
        }

        private void showBusinessItems()
        {
            this.mainControl.setBusinessItems(this.list<BusinessItem>(this.managedPackage), Domain.getDomain(this.managedPackage));
        }



        private void showColumns()
        {
            //get the databases
            var databases = this.getDatabases(this.model.selectedTreePackage);
            //get the list of columns
            var columns = new List<EDDColumn>();
            foreach (var db in databases)
            {
                foreach (var table in db.tables)
                {
                    foreach (DB_EA.Column column in table.columns)
                    {
                        columns.Add(new EDDColumn(column));
                    }
                }
            }
            this.mainControl.setColumns(columns);

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
            //first get the «database» packages from the selected package
            foreach (TSF_EA.Package databasePackage in package.getNestedPackageTree(true).Where(x => x.stereotypes.Any(y => y.name.ToLower() == "database")))
            {
                string databaseType = databasePackage.taggedValues.FirstOrDefault(x => x.name.ToLower() == "dbms")?.tagValue.ToString();
                var dbFactory = DB_EA.DatabaseFactory.getFactory(databaseType, this.model, new DB_EA.Strategy.StrategyFactory());
                foundDatabases.Add(dbFactory.createDataBase(databasePackage));
            }
            return foundDatabases;
        }

        private void showDataItems()
        {
            this.mainControl.setDataItems(this.list<DataItem>(this.managedPackage), Domain.getDomain(this.managedPackage));
        }

        public void refresh()
        {
            List<DataItem> dataItems = this.list<DataItem>(this.managedPackage);

            // add all Logical DataTypes from this package and optionally others
            // from the dataItems

            if (this.managedPackage != null)
            {
                foreach (TSF_EA.Class clazz in
                        this.managedPackage.ownedElements.OfType<TSF_EA.Class>())
                {
                    if (clazz.stereotypes.Count == 1)
                    {
                        if (clazz.stereotypes.ToList()[0].name == "LogicalDataType")
                        {

                        }
                    }
                }
            }
            foreach (DataItem item in dataItems)
            {
                TSF_EA.Class element = this.model.getElementByGUID(item.datatypeDisplayName) as TSF_EA.Class;
            }
            this.model.activateTab(appTitle);
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
                            item.Save();//TODO: check does this work without parameter??
                        }
                    }
                    else
                    {
                        this.log("WARNING: item (" + item.GUID + ") is not part of this package.");
                    }
                }
            }
            this.refresh();
        }

        private void export<T>() where T : GlossaryItem, new()
        {
            this.export<T>(this.list<T>());
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

        private List<T> list<T>() where T : GlossaryItem, new()
        {
            return this.list<T>((TSF_EA.Package)this.model.selectedElement);
        }

        private List<T> list<T>(TSF_EA.Package package) where T : GlossaryItem, new()
        {
            if (package == null) return new List<T>();
            return this.factory.getGlossaryItemsFromPackage<T>(package);
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
