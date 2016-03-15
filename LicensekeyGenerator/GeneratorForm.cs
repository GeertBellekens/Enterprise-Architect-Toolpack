/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 5/12/2014
 * Time: 4:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
//using EAAddinFramework.Licensing;

namespace LicensekeyGenerator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class GeneratorForm : Form
	{
		
		public GeneratorForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			
			
		}
		
		
		void ValidateButtonClick(object sender, EventArgs e)
		{
//			string publicKey = "ACAACACc+VyfQ687AQMAAQAB";
//			License license = new License(this.keyTextBox.Text, publicKey);
//			if (license.isValid)
//			{
//				this.floatingCheckbox.Checked = license.floating;
//				this.clientTextBox.Text = license.client;
//				this.keyTextBox.ForeColor = Color.ForestGreen;
//			}
//			else
//			{
//				this.floatingCheckbox.Checked = false;
//				this.clientTextBox.Clear();
//				this.keyTextBox.ForeColor = Color.Red;
//			}
			
		}
	}
}
