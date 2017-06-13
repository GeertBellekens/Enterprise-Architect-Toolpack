using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;

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

    private void import<T>() where T : class {
      var file = this.getFileFor<T>(CSV.Loading);
      if(file != null) {
        List<T> items = GlossaryItem.Load<T>(file);
        foreach(T item in items) {
          this.log("importing " + item.ToString());
          // TODO populate selected package with items
        }        
      }
    }

    private void export<T>() where T : class {
      var topic = typeof(T).Name;
      var file = this.getFileFor<T>(CSV.Saving);
      if(file != null) {
        List<T> items = new List<T>();
        // TODO collect items from selected package
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
