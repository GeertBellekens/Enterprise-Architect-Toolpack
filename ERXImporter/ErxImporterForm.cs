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

namespace ERXImporter
{
    public partial class ErxImporterForm : Form
    {
        public ErxImporterForm()
        {
            InitializeComponent();
        }

        private void browseImportFileButton_Click(object sender, EventArgs e)
        {
            //let the user select a file
            OpenFileDialog browseImportFileDialog = new OpenFileDialog();
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
            var importer = new ERXImporter();
            var errors =  importer.import(this.importFileTextBox.Text);
            this.errorTextBox.Text = string.IsNullOrEmpty(errors) ? "Finished without errors" : errors;
            Cursor.Current = Cursors.Default;
        }

        private void synchFKsButton_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            this.errorTextBox.Clear();
            var importer = new ERXImporter();
            var errors = importer.synchronizeForeignKeys();
            this.errorTextBox.Text = string.IsNullOrEmpty(errors) ? "Finished without errors" : errors;
            Cursor.Current = Cursors.Default;
        }

    }
}
