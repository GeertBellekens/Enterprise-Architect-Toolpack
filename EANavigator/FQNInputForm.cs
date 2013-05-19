/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 4/08/2012
 * Time: 7:38
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of FQNInputForm.
	/// </summary>
	public partial class FQNInputForm : Form
	{
		public string fqn {get;set;}
		public FQNInputForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			//
			// TODO: Add constructor code after the InitializeComponent() call.
			//
		}
		public FQNInputForm(string message, string title,string label):this()
		{
			this.fqnLabel.Text = label;
			this.Text = title;
			if (message != string.Empty)
			{
				this.messageLabel.Text = message;
				this.flowLayoutPanel1.Height += this.messageLabel.Height;
				this.Height += this.messageLabel.Height;
				this.messageLabel.Show();
			}
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.fqn = fqnTextBox.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		

	}
}
