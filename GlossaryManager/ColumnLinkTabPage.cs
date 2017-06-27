using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager {

	public class ColumnLinkTabPage : FocusableTabPage {

    private GlossaryManagerUI ui;
    private ToolStripLabel notificationLabel;
    
    public ColumnLinkTabPage(GlossaryManagerUI ui) : base() {
      this.ui   = ui;
      this.Text = "Column Links";
      
      this.addToolbar();
      
      this.ui.NewContext += new NewContextHandler(this.handleContextChange);
    }
    
    private void handleContextChange(EAWrapped.ElementWrapper context) {
      if(context == null) { return; }
      this.checkContext(context);
    }

    public override void HandleFocus() {
      this.checkContext();
    }

    private void checkContext() {
      this.checkContext(this.ui.Addin.SelectedItem);
    }

    private void checkContext(EAWrapped.ElementWrapper context) {
      if(context != null) {
        if(context is EAWrapped.Class) {
          EAWrapped.Class clazz = context as EAWrapped.Class;
          if( clazz.stereotypes.Count == 1 ) {
            if( clazz.stereotypes.ToList()[0].name.Equals("Data Item") ) {
              this.hideNotifications();
              return;
            } 
          }
        }
      }
      // TODO expand possibilities
      // TODO load appropriate UI
      this.notify("Please select a Data Item, Table or Column...");
    }

    private void addToolbar() {
      ToolStrip toolStrip = new ToolStrip() {
        Dock = DockStyle.Bottom
      };

      this.notificationLabel = new ToolStripLabel();
      toolStrip.Items.Add(this.notificationLabel); 

      this.Controls.Add(toolStrip);
    }

    private void notify(string msg) {
      this.notificationLabel.Text = msg;
      this.notificationLabel.Visible = true;
    }

    private void hideNotifications() {
      this.notificationLabel.Visible = false;
    }

  }
}
