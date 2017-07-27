using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

using EAAddinFramework;
using EAAddinFramework.Utilities;

namespace GlossaryManager {

  public delegate void NewContextHandler(EAWrapped.ElementWrapper context);

  public class GlossaryManagerAddin : EAAddinBase  {

    // menu constants
    const string menuName                = "-&Glossary Manager";
    const string menuManage              = "&Manage...";
    const string menuImportBusinessItems = "&Import Business Items...";
    const string menuImportDataItems     = "&Import Data Items...";
    const string menuExportBusinessItems = "&Export Business Items...";
    const string menuExportDataItems     = "&Export Data Items...";
    const string menuAbout               = "&About";

    const string appTitle                = "GlossaryManager";
    const string appFQN                  = "GlossaryManager.GlossaryManagerUI"; 

    private EAWrapped.Model         model       = null;
    public  EAWrapped.Model Model { get { return this.model; } }
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
    }

    public override void EA_FileOpen(EA.Repository repository) {
      this.model = new EAWrapped.Model(repository);
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
      if(this.NewContext == null)                                   { return; }
      if(this.model      == null)                                   { return; }
      if( ! (this.model.selectedItem is EAWrapped.ElementWrapper) ) { return; }
      this.NewContext((EAWrapped.ElementWrapper)this.model.selectedItem);
    }

    public EAWrapped.ElementWrapper SelectedItem {
      get {
        return (EAWrapped.ElementWrapper)this.model.selectedItem;
      }
      set {
        this.model.selectedItem = value;
      }
    }

    // activities

    internal EAWrapped.Package managedPackage { get; private set; }
    
    private void manage() {
      if( this.model == null ) { return; }
      this.managedPackage = (EAWrapped.Package)this.model.selectedElement;
      this.refresh();
    }
    
    private void refresh() {
      this.ui.BusinessItems.Show<BusinessItem>(
        this.list<BusinessItem>(this.managedPackage)
      );
      List<DataItem> dataItems = this.list<DataItem>(this.managedPackage);
      this.ui.DataItems.Show<DataItem>(dataItems);

      // add all Logical DataTypes from this package and optionally others
      // from the dataItems
      List<FieldValue> logicalDataTypes = new List<FieldValue>();
      foreach(EAWrapped.Class clazz in
              this.managedPackage.ownedElements.OfType<EAWrapped.Class>())
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
      foreach(DataItem item in dataItems) {
				EAWrapped.Class element = this.model.getElementByGUID(item.LogicalDataType) as EAWrapped.Class;
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

    private void import<T>() where T : GlossaryItem {
      this.import<T>((EAWrapped.Package)this.model.selectedElement);
    }
    
    internal void import<T>(EAWrapped.Package package) where T : GlossaryItem {
      var file = this.getFileFor<T>(CSV.Loading);
      if(file == null) { return; }

      this.log("importing in package " + package.ToString());

      Dictionary<string,EAWrapped.Class> index = this.index<T>(package);
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

    private void export<T>() where T : GlossaryItem {
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
    
    private List<T> list<T>() where T : GlossaryItem {
      return this.list<T>((EAWrapped.Package)this.model.selectedElement);
    }

    private List<T> list<T>(EAWrapped.Package package) where T : GlossaryItem {
      List<T> items = new List<T>();
      foreach(EAWrapped.Class clazz in package.ownedElements.OfType<EAWrapped.Class>()) {
        try {
          T item = GlossaryItemFactory<T>.FromClass(clazz);
          if( item != null ) { items.Add(item); }
        } catch(Exception e) {
          MessageBox.Show(e.ToString());
        }
      }
      return items;
    }

    private Dictionary<string,EAWrapped.Class> index<T>() where T : GlossaryItem {
      return this.index<T>((EAWrapped.Package)this.model.selectedElement);
    }

    private Dictionary<string,EAWrapped.Class> index<T>(EAWrapped.Package package)
      where T : GlossaryItem
    {
      Dictionary<string,EAWrapped.Class> map =
        new Dictionary<string,EAWrapped.Class>();
      foreach(EAWrapped.Class clazz in package.ownedElements.OfType<EAWrapped.Class>()) {
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

    internal void clearLog() {
      if( this.model == null ) { return; }
      EAOutputLogger.clearLog( this.model, this.settings.outputName );
    }

    internal void log(string msg) {
      if( this.model == null ) { return; }
      EAOutputLogger.log( this.model, this.settings.outputName, msg );
    }

  }
}
