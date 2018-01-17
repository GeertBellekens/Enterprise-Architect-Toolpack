using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.IO;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MDMigratorForm : Form
	{
		
		MDMigratorController controller {get;set;}
        TSF_EA.Package mdPackage { get; set; }
        List<MagicDrawCorrector> correctors { get; set; }
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
            var correctionsToStart = new List<MagicDrawCorrector>();
            foreach (ListViewItem item in correctorsListView.Items)
            {
                if (item.Checked) correctionsToStart.Add((MagicDrawCorrector)item.Tag);
            }
			this.controller.startCorrections(this.mdzipPathTextBox.Text,correctionsToStart);
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
			this.startCorrectionButton.Enabled = this.correctionsCanBeLoaded();
		}
        private Boolean correctionsCanBeLoaded()
        {
            return !string.IsNullOrEmpty(mdzipPathTextBox.Text)
                                                && Directory.Exists(mdzipPathTextBox.Text)
                                                && mdPackage != null;
        }

        private void browseImportPackageButton_Click(object sender, EventArgs e)
        {
            mdPackage = this.controller.getMagicDrawPackage();
            this.importPackageTextBox.Text = mdPackage != null ? mdPackage.name : string.Empty;
            if (correctionsCanBeLoaded()) refreshConnectors();
            enableDisable();
        }

        private void refreshConnectors()
        {
            this.correctors = this.controller.createCorrectors(this.mdzipPathTextBox.Text, mdPackage);
            foreach(var corrector in correctors)
            {
                var item = new ListViewItem(corrector.GetType().Name);
                item.Tag = corrector;
                this.correctorsListView.Items.Add(item);
            }
        }
    }
}
