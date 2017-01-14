using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML=TSF.UmlToolingFramework.UML;

namespace EAMapping
{
	/// <summary>
	/// Description of ImportMappingForm.
	/// </summary>
	public partial class ImportMappingForm : Form
	{
		
		public ImportMappingForm()
		{
			InitializeComponent();
			
			enableDisable();
		}
		void enableDisable()
		{
			//only enable import if the file exists
			this.importButton.Enabled = System.IO.File.Exists(this.importFileTextBox.Text);
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
            }
		}
		public event EventHandler ImportButtonClicked = delegate { }; 
		void ImportButtonClick(object sender, EventArgs e)
		{
			ImportButtonClicked(this, e);
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		public event EventHandler SourcePathBrowseButtonClicked = delegate { };
		void SourcePathBrowseButtonClick(object sender, EventArgs e)
		{
			SourcePathBrowseButtonClicked(this, e);
		}
		public event EventHandler TargetPathBrowseButtonClicked = delegate { };
		void TargetPathBrowseButtonClick(object sender, EventArgs e)
		{
			TargetPathBrowseButtonClicked(this, e);
		}
		void ImportFileTextBoxTextChanged(object sender, EventArgs e)
		{
			enableDisable();
		}
		UML.Classes.Kernel.Element _sourcePathElement;
		internal UML.Classes.Kernel.Element sourcePathElement 
		{
			get
			{
				return _sourcePathElement;
			}
			set
			{
				_sourcePathElement = value;
				this.sourcePathTextBox.Text = _sourcePathElement != null ? _sourcePathElement.fqn : string.Empty;
			}
		}
		UML.Classes.Kernel.Element _targetPathElement;
		public UML.Classes.Kernel.Element targetPathElement {
			get 
			{
				return _targetPathElement;
			}
			set 
			{
				_targetPathElement = value;
				this.targetPathTextBox.Text = _targetPathElement != null ? _targetPathElement.fqn : string.Empty;
			}
		}
		internal string importFilePath
		{
			get
			{
				return this.importFileTextBox.Text;
			}
		}
	}
}
