using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of debugForm.
	/// </summary>
	public partial class debugForm : Form
	{
		public debugForm(DBCompareControl dbCompareControl)
		{
			this.dbCompareControl1 = dbCompareControl;
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
			
		}
	}
}
