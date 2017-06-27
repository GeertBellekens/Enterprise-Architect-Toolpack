using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace GlossaryManager {

	public class ColumnLinkTabPage : FocusableTabPage {

    private GlossaryManagerUI ui;
    
    public ColumnLinkTabPage(GlossaryManagerUI ui) : base() {
      this.ui   = ui;
      this.Text = "Column Links";
    }

    public override void HandleFocus() {
      MessageBox.Show("Column Linking Tabpage got focus!");
    }

  }
}
