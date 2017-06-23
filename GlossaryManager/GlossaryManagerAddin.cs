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

  public class GlossaryManagerAddin : EAAddinBase  {

    // menu constants
    const string menuName                = "-&Glossary Manager";
    const string menuManage              = "&Manage...";
    const string menuImportBusinessItems = "&Import Business Items...";
    const string menuImportDataItems     = "&Import Data Items...";
    const string menuExportBusinessItems = "&Export Business Items...";
    const string menuExportDataItems     = "&Export Data Items...";
    const string menuAbout               = "&About";

    const string appTitle   = "GlossaryManager";
    const string appFQN     = "GlossaryManager.GlossaryManagerUI"; 

    private EAWrapped.Model         model       = null;
    private bool                    fullyLoaded = false;

    private GlossaryManagerSettings settings    = new GlossaryManagerSettings();

    private GlossaryManagerUI _ui;
    private GlossaryManagerUI ui {
      get {
        if( this._ui == null && this.model != null ) {
          this.log("creating new Glossary Manager UI instance...");
          this._ui = this.model.addTab(appTitle, appFQN) as GlossaryManagerUI;
          this._ui.Addin = this;
					this._ui.HandleDestroyed += this.handleHandleDestroyed; 
        } else {
          this.log("could not create new Glossary Manager UI instance!");
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

    // activities
    
    private void manage() {
      if( this.model == null ) { return; }
      this.log("starting glossary management activity...");
      this.ui.Start();
      this.model.activateTab(appTitle);
    }

    private void import<T>() where T : GlossaryItem {
      var file = this.getFileFor<T>(CSV.Loading);
      if(file == null) { return; }

      List<T> items = GlossaryItem.Load<T>(file);

      EAWrapped.Package package = (EAWrapped.Package)this.model.selectedElement;
      Dictionary<string,EAWrapped.Class> index = this.index<T>(package);
      this.log("importing in package " + package.ToString());

      foreach(T item in items) {
        if( ! index.ContainsKey(item.Name) ) {          // create
          this.log("importing " + item.ToString());
          item.AsClassIn(package);          
        } else {
          if(item.Delete) {                             // delete
            this.log("removing " + item.Name);
            package.deleteOwnedElement(index[item.Name]);
          } else  {                                     // update
            this.log("updating " + item.Name);
            item.Update(index[item.Name]);
          }
        }        
      }
    }

    private Dictionary<string,EAWrapped.Class> index<T>(EAWrapped.Package package)
      where T : GlossaryItem
    {
      Dictionary<string,EAWrapped.Class> map =
        new Dictionary<string,EAWrapped.Class>();
      foreach(EAWrapped.Class clazz in package.ownedElements.OfType<EAWrapped.Class>()) {
        if( GlossaryItemFactory<T>.IsA(clazz) ) {
          map.Add(clazz.name, clazz);
        }
      }
      return map;
    }

    private void export<T>() where T : GlossaryItem {
      var topic = typeof(T).Name;
      var file = this.getFileFor<T>(CSV.Saving);
      if(file != null) {
        this.log("exporting to " + file.ToString());
        List<T> items = new List<T>();
        EAWrapped.Package package = (EAWrapped.Package)this.model.selectedElement;
        this.log("exporting package " + package.ToString());
        foreach(EAWrapped.Class clazz in package.ownedElements.OfType<EAWrapped.Class>()) {
          T item = GlossaryItemFactory<T>.FromClass(clazz);
          if( item != null) {
            this.log("exporting " + item.ToString());
            items.Add(item);
          } else {
            this.log("skipping " + clazz.ToString());
          }
        }
        GlossaryItem.Save<T>(file, items);
      }
    }

    private void about() {
      new AboutWindow().ShowDialog(this.model.mainEAWindow);
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

    private void clearLog() {
      if( this.model == null ) { return; }
      EAOutputLogger.clearLog( this.model, this.settings.outputName );
    }

    private void log(string msg) {
      if( this.model == null ) { return; }
      EAOutputLogger.log( this.model, this.settings.outputName, msg );
    }

  }
}
