using System;
using System.ComponentModel;

using System.Collections.Generic;
using System.Linq;

using System.Drawing;
using System.Windows.Forms;

using EAWrapped=TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager {

	public partial class GlossaryManagerUI : UserControl {

    private GlossaryManagerAddin addin;
    public GlossaryManagerAddin Addin {
      get {
        return this.addin;
      }
      set {
        this.addin = value;
        this.addin.NewContext += new NewContextHandler(this.handleContextChange);
      }
    }

    public event NewContextHandler NewContext;
    
    private void handleContextChange(EAWrapped.ElementWrapper context) {
      this.NewContext(context);
    }

		public GlossaryManagerUI() {
			InitializeComponent();
		}
  }

}
