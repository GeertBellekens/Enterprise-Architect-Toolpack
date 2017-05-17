using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MDMigratorForm : Form
	{
		
		MDMigratorController controller {get;set;}
		public MDMigratorForm(MDMigratorController controller)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//set the controller
			this.controller = controller;
			//enableDisable
			this.enableDisable();
			
		}
		void StartCorrectionButtonClick(object sender, EventArgs e)
		{
			this.controller.startCorrections(this.mdzipPathTextBox.Text);
		}
		void BrowseMDZipFolderClick(object sender, EventArgs e)
		{
			FolderBrowserDialog browseMdZipFolderDialog = new FolderBrowserDialog();
			browseMdZipFolderDialog.SelectedPath = this.mdzipPathTextBox.Text;
			var dialogResult = browseMdZipFolderDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.mdzipPathTextBox.Text = browseMdZipFolderDialog.SelectedPath;
			}
			this.enableDisable();
		}
		void enableDisable()
		{
			this.startCorrectionButton.Enabled = ! string.IsNullOrEmpty(mdzipPathTextBox.Text) && Directory.Exists(mdzipPathTextBox.Text);
		}
	}
}
