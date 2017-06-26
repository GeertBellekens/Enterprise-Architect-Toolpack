using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace GlossaryManager {

	public partial class GlossaryManagerUI : UserControl {

    public GlossaryManagerAddin Addin { get; set; }

		public GlossaryManagerUI() {
			InitializeComponent();
		}
  }

}
