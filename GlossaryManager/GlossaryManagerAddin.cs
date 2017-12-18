using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using GlossaryManager.GUI;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

using EAAddinFramework;
using EAAddinFramework.Utilities;

namespace GlossaryManager {

  public delegate void NewContextHandler(TSF_EA.ElementWrapper context);

  public class GlossaryManagerAddin : EAAddinBase  {

    // menu constants
    const string menuName                = "-&Glossary Manager";
    const string menuManage              = "&Manage...";
    const string menuImportBusinessItems = "&Import Business Items...";
    const string menuImportDataItems     = "&Import Data Items...";
    const string menuExportBusinessItems = "&Export Business Items...";
    const string menuExportDataItems     = "&Export Data Items...";
    const string menuAbout               = "&About";

    const string appTitle                = "Enterprise Data Dictionary";
    const string appFQN                  = "GlossaryManager.GlossaryManagerUI";
    const string guiFQN                  = "GlossaryManager.GUI.EDD_MainControl";

    private TSF_EA.Model         model       = null;
    public  TSF_EA.Model Model { get { return this.model; } }
    private bool                    fullyLoaded = false;

    private GlossaryManagerSettings settings    = new GlossaryManagerSettings();

    private GlossaryManagerUI _ui;
    private GlossaryManagerUI ui {
      get {
        if( this._ui == null && this.model != null ) {
          this._ui = this.model.addTab(appTitle, appFQN) as GlossaryManagerUI;
          // this is annoying, during construction of the UI, the Addin cannot
          // be back-referenced from the ui yet, until we set it here
          this._ui.Addin = this;
          this._ui.Activate(); // so we delay the creation and trigger it here
					this._ui.HandleDestroyed += this.handleHandleDestroyed; 
        }
        return this._ui;
      }
    }
    private EDD_MainControl _mainControl;
    private EDD_MainControl mainControl
    {
    	get
    	{
	        if( this._mainControl == null && this.model != null ) 
	        {
	        	this._mainControl = this.model.addTab(appTitle, guiFQN) as EDD_MainControl;
				this._mainControl.HandleDestroyed += this.handleHandleDestroyed;
				//TODO: add additional events
        	}
	        return this._mainControl;
    	}
    	
    	
    }

    public GlossaryManagerAddin() : base() {
      this.menuHeader = menuName;
      this.menuOptions = new string[] {
        menuManage,
        menuImportBusinessItems,
        menuImportDataItems,
        menuExportBusinessItems,
        menuExportDataItems,
        menuAbout
      };
    }

    private void handleHandleDestroyed(object sender, EventArgs e) {
      this._ui = null;
      this._mainControl = null;
    }

    public override void EA_FileOpen(EA.Repository repository) {
      this.model = new TSF_EA.Model(repository);
      this.fullyLoaded = true;
    }

    public override void EA_GetMenuState(EA.Repository repository,
                                         string location, string menuName,
                                         string itemName, ref bool isEnabled,
                                         ref bool isChecked)
    {
      switch( itemName ) {
        case menuImportBusinessItems:
        case menuImportDataItems:
          isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
          break;
        case menuExportBusinessItems:
        case menuExportDataItems:
          isEnabled = this.fullyLoaded && (this.model.selectedElement != null);
          break;
        default:
          isEnabled = true;
        break;
      }
    }

    public override void EA_MenuClick(EA.Repository repository, string location,
                                      string menuName, string itemName)
    {
      switch( itemName ) {
        case menuManage:              this.manage();               break;
        case menuImportBusinessItems: this.import<BusinessItem>(); break;
        case menuImportDataItems:     this.import<DataItem>();     break;
        case menuExportBusinessItems: this.export<BusinessItem>(); break;
        case menuExportDataItems:     this.export<DataItem>();     break;
        case menuAbout:               this.about();                break;
      }
    }


    public event NewContextHandler NewContext;

    public override void EA_OnContextItemChanged(EA.Repository Repository,
                                                 string GUID,
                                                 EA.ObjectType ot)
    {
		try 
			{
			if(this.NewContext == null)                                   { return; }
			if(this.model      == null)                                   { return; }
			var selectedElementWrapper = this.model.selectedItem as TSF_EA.ElementWrapper;
			if (selectedElementWrapper != null)
			{
				this.NewContext((TSF_EA.ElementWrapper)this.model.selectedItem);
			}
		} 
		catch(Exception e)
		{
	    	
	  	}
    }

    public TSF_EA.ElementWrapper SelectedItem {
      get {
        if( this.model.selectedItem is TSF_EA.Attribute &&
            this.model.selectedItem.owner is TSF_EA.ElementWrapper)
        {
          return this.model.selectedItem.owner as TSF_EA.ElementWrapper;
        }
        if( ! (this.model.selectedItem is TSF_EA.ElementWrapper) ) { return null; }
        return (TSF_EA.ElementWrapper)this.model.selectedItem;
      }
      set {
        this.model.selectedItem = value;
      }
    }

    // activities

    internal TSF_EA.Package managedPackage { get; private set; }
    
    
    private void manage() {
      if( this.model == null ) { return; }
      this.managedPackage = (TSF_EA.Package)this.Model.selectedTreePackage;
      //this.refresh();
      this.showGlossaryItems();
    }
    
    private void showGlossaryItems()
    {
    	this.mainControl.setBusinessItems(this.list<BusinessItem>(this.managedPackage));
    }
    
    public void refresh() 
    {
      this.ui.BusinessItems.Show<BusinessItem>(this.list<BusinessItem>(this.managedPackage));
      List<DataItem> dataItems = this.list<DataItem>(this.managedPackage);
      this.ui.DataItems.Show<DataItem>(dataItems);
      // add all Logical DataTypes from this package and optionally others
	  // from the dataItems
	  List<FieldValue> logicalDataTypes = new List<FieldValue>();
      if (this.managedPackage != null)
      {
	      foreach(TSF_EA.Class clazz in
	              this.managedPackage.ownedElements.OfType<TSF_EA.Class>())
	      {
	        if(clazz.stereotypes.Count == 1) {
	          if( clazz.stereotypes.ToList()[0].name == "LogicalDataType") {
	            logicalDataTypes.Add(new FieldValue() {
	              Key   = clazz.name,
	              Value = clazz.guid
	            });
	          }
	        }
	      }
      }
      foreach(DataItem item in dataItems) {
				TSF_EA.Class element = this.model.getElementByGUID(item.LogicalDatatypeName) as TSF_EA.Class;
        if(element != null && ! logicalDataTypes.Any(x => x.Value == element.guid)) {
          logicalDataTypes.Add(new FieldValue() {
            Key   = element.name,
            Value = element.guid
          });
        }
      }

      this.ui.DataItems.LogicalDataTypes = logicalDataTypes;
      this.ui.ColumnLinks.DataItems = dataItems;
      this.model.activateTab(appTitle);
    }

    private void import<T>() where T : GlossaryItem, new(){
      this.import<T>((TSF_EA.Package)this.model.selectedElement);
    }
    
    internal void import<T>(TSF_EA.Package package) where T : GlossaryItem, new() {
      var file = this.getFileFor<T>(CSV.Loading);
      if(file == null) { return; }

      this.log("importing in package " + package.ToString());

      Dictionary<string,TSF_EA.Class> index = this.index<T>(package);
      List<T> items = GlossaryItem.Load<T>(file);

      foreach(T item in items) {
        if( item.GUID == "" ) {                           // create
          this.log("importing " + item.ToString());
          item.AsClassIn(package);          
        } else {
          if(index.ContainsKey(item.GUID)) {
            if(item.Delete) {                             // delete
              this.log("removing " + item.Name);
              package.deleteOwnedElement(index[item.GUID]);
            } else  {                                     // update
              this.log("updating " + item.Name);
              item.Update(index[item.GUID]);
            }
          } else {
            this.log("WARNING: item (" + item.GUID + ") is not part of this package.");
          }
        }        
      }
      this.refresh();
    }

    private void export<T>() where T : GlossaryItem, new() {
      this.export<T>(this.list<T>());
    }

    internal void export<T>(List<T> exported) where T : GlossaryItem {
      var topic = typeof(T).Name;
      var file = this.getFileFor<T>(CSV.Saving);
      if(file != null) {
        this.log("exporting to " + file.ToString());
        // TODO: why iterating a list of the same type ?!
        //       why can an item be null?
        List<T> items = new List<T>();
        foreach(var item in exported) {
          if( item != null) {
            this.log("exporting " + item.ToString());
            items.Add(item);
          }
        }
        GlossaryItem.Save<T>(file, items);
      }
    }

    private void about() {
      new AboutWindow().ShowDialog(this.model.mainEAWindow);
    }

    // support for listing and indexing a/the selected package
    
    private List<T> list<T>() where T : GlossaryItem, new() {
      return this.list<T>((TSF_EA.Package)this.model.selectedElement);
    }

    private List<T> list<T>(TSF_EA.Package package) where T : GlossaryItem,new()
    {
      	if (package == null) return new List<T>();
      	return GlossaryItemFactory<T>.getGlossaryItemsFromPackage(package);
    }

    private Dictionary<string,TSF_EA.Class> index<T>() where T : GlossaryItem, new() {
      return this.index<T>((TSF_EA.Package)this.model.selectedElement);
    }

    private Dictionary<string,TSF_EA.Class> index<T>(TSF_EA.Package package)
      where T : GlossaryItem, new()
    {
      Dictionary<string,TSF_EA.Class> map =
        new Dictionary<string,TSF_EA.Class>();
      foreach(TSF_EA.Class clazz in package.ownedElements.OfType<TSF_EA.Class>()) {
        if( GlossaryItemFactory<T>.IsA(clazz) ) {
          map.Add(clazz.guid, clazz);
        }
      }
      return map;
    }

    // support for CSV File IO Dialog boxes
    
    private enum CSV { Loading, Saving }

    private string getFileFor<T>(CSV activity) {
      var topic = typeof(T).Name + "s";
      FileDialog selection = activity == CSV.Loading ?
        (FileDialog)new OpenFileDialog() {
          Filter      = "CSV File | *.csv;*.txt",
          Title       = "Load a CSV with " + topic,
          FilterIndex = 1,
          Multiselect = false
        }
        :
        (FileDialog)new SaveFileDialog() {
          Filter      = "CSV File | *.csv;*.txt",
          Title       = "Save " + topic + " to a CSV File",
          FilterIndex = 1
        }
      ;
      if( selection.ShowDialog() == DialogResult.OK ) {
        return selection.FileName;
      }
      return null;
    }

    // support for logging to the EA log window

    internal void clearLog() 
    {
      if( this.model == null ) { return; }
      EAOutputLogger.clearLog( this.model, this.settings.outputName );
    }

    internal void log(string msg) 
    {
      if( this.model == null ) { return; }
      EAOutputLogger.log( this.model, this.settings.outputName, msg );
    }
    internal void logError(string msg) 
    {
      if( this.model == null ) { return; }
      EAOutputLogger.log( this.model, this.settings.outputName, msg,0,LogTypeEnum.error );
    }

  }
}
