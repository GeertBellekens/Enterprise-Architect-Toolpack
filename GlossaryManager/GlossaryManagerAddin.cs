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
    const string menuName   = "-&Glossary Manager";
    const string menuManage = "&Manage...";
    const string menuImport = "&Import...";
    const string menuAbout  = "&About";

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
        menuImport,
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
        case menuImport:
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
        case menuManage: this.manage(); break;
        case menuImport: this.import(); break;
        case menuAbout : this.about();  break;
      }
    }

    // activities
    
    private void manage() {
      if( this.model == null ) { return; }
      this.log("starting glossary management activity...");
      this.ui.Start();
      this.model.activateTab(appTitle);
    }

    private void import() {
      // TODO
    }

    private void about() {
      new AboutWindow().ShowDialog(this.model.mainEAWindow);
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
