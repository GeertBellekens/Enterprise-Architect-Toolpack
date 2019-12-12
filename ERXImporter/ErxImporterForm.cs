using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using System.IO;

namespace ERXImporter
{
    public partial class ErxImporterForm : Form
    {
        private ERXImporter importer = new ERXImporter();
        public ErxImporterForm()
        {
            InitializeComponent();
            enableDisable();
        }
        private void enableDisable()
        {
            this.synchFKsButton.Enabled = !string.IsNullOrEmpty(this.exportFileTextBox.Text) &&
                                    this.exportFileTextBox.Text.IndexOfAny(Path.GetInvalidPathChars()) < 0;
            this.createRelationsButton.Enabled = importer.relations.Any() 
                                                && importer.selectedPackage != null;
        }

        private void browseImportFileButton_Click(object sender, EventArgs e)
        {
            //let the user select a file
            var browseImportFileDialog = new OpenFileDialog();
            browseImportFileDialog.Filter = "ERX Files|*.erx";
            browseImportFileDialog.FilterIndex = 1;
            browseImportFileDialog.Multiselect = false;
            var dialogResult = browseImportFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                //if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.importFileTextBox.Text = browseImportFileDialog.FileName;
                //move cursor to end to make sure the end of the path is visible
                this.importFileTextBox.Select(this.importFileTextBox.Text.Length, 0);
            }
        }

        private void importButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.errorTextBox.Clear();
            var errors =  importer.import(this.importFileTextBox.Text);
            this.errorTextBox.Text = string.IsNullOrEmpty(errors) ? "Finished without errors" : errors;
            Cursor.Current = Cursors.Default;
        }

        private void synchFKsButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.errorTextBox.Text = "Creating foreign keys...";
            importer.synchronizeForeignKeys(this.exportFileTextBox.Text);
            var createdFKCount = this.importer.relations.Count(x => x.FKStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase));
            this.errorTextBox.Text = $"Finished creating foreign keys!\n{createdFKCount} of {this.importer.relations.Count} foreign keys created.";
            this.enableDisable();
            Cursor.Current = Cursors.Default;
        }

        private void browseExportFileButton_Click(object sender, EventArgs e)
        {
            //let the user select a file
            var browseExportFileDialog = new SaveFileDialog();
            browseExportFileDialog.Filter = "CSV Files|*.csv";
            browseExportFileDialog.FilterIndex = 1;
            var dialogResult = browseExportFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                //if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.exportFileTextBox.Text = browseExportFileDialog.FileName;
                //move cursor to end to make sure the end of the path is visible
                this.exportFileTextBox.Select(this.exportFileTextBox.Text.Length, 0);
            }
        }

        private void exportFileTextBox_TextChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }

        private void browseTDMPackage_Click(object sender, EventArgs e)
        {
            try
            {
                this.importer.selectPackage();
            }catch(Exception)
            {
                MessageBox.Show("Please open the EA model");
            }
            this.tdmPackageTextBox.Text = this.importer.selectedPackage?.fqn;
            //move cursor to end to make sure the end of the path is visible
            this.tdmPackageTextBox.Select(this.tdmPackageTextBox.Text.Length, 0);
            this.enableDisable();
        }

        private void createRelationsButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.errorTextBox.Text = "Creating relations...";
            this.importer.createRelations(this.exportFileTextBox.Text);
            var createdCount = importer.relations.Count(x => x.createdInEA);
            this.errorTextBox.Text = $"Finished creating relations!\n{createdCount} of {importer.relations.Count} relations created in EA";
            Cursor.Current = Cursors.Default;
        }
    }
}
