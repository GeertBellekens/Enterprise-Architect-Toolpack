using System;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAMapping
{
    /// <summary>
    /// Description of ImportMappingForm.
    /// </summary>
    public partial class ImportMappingForm : Form
    {

        public ImportMappingForm()
        {
            this.InitializeComponent();

            this.enableDisable();
        }
        void enableDisable()
        {
            //only enable import if the file exists
            this.importButton.Enabled = System.IO.File.Exists(this.importFileTextBox.Text)
                                        && this.sourcePathElement != null
                                        && this.targetPathElement != null;
        }
        void BrowseMappingFileButtonClick(object sender, EventArgs e)
        {
            //let the user select a file
            OpenFileDialog browseImportFileDialog = new OpenFileDialog();
            browseImportFileDialog.Filter = "Mapping Files|*.csv";
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
        public event EventHandler ImportButtonClicked;
        void ImportButtonClick(object sender, EventArgs e)
        {
            ImportButtonClicked?.Invoke(this, e);
            this.Close();
        }
        void CancelButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }
        public event EventHandler SourcePathBrowseButtonClicked;
        void SourcePathBrowseButtonClick(object sender, EventArgs e)
        {
            SourcePathBrowseButtonClicked?.Invoke(this, e);
        }
        public event EventHandler TargetPathBrowseButtonClicked;
        void TargetPathBrowseButtonClick(object sender, EventArgs e)
        {
            TargetPathBrowseButtonClicked?.Invoke(this, e);
        }
        void ImportFileTextBoxTextChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        TSF_EA.Element _sourcePathElement;
        internal TSF_EA.Element sourcePathElement
        {
            get => this._sourcePathElement;
            set
            {
                this._sourcePathElement = value;
                this.sourcePathTextBox.Text = this._sourcePathElement != null ? this._sourcePathElement.fqn : string.Empty;
                //move cursor to end to make sure the end of the path is visible
                this.sourcePathTextBox.Select(this.sourcePathTextBox.Text.Length, 0);
                this.enableDisable();
            }
        }
        TSF_EA.Element _targetPathElement;
        public TSF_EA.Element targetPathElement
        {
            get => this._targetPathElement;
            set
            {
                this._targetPathElement = value;
                this.targetPathTextBox.Text = this._targetPathElement != null ? this._targetPathElement.fqn : string.Empty;
                //move cursor to end to make sure the end of the path is visible
                this.targetPathTextBox.Select(this.targetPathTextBox.Text.Length, 0);
                this.enableDisable();
            }
        }
        internal string importFilePath => this.importFileTextBox.Text;
    }
}
