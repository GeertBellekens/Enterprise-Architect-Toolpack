
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAImvertor
{
	/// <summary>
	/// Description of ImvertorStartJobForm.
	/// </summary>
	public partial class ImvertorStartJobForm : Form
	{
		private EAImvertorSettings settings;
		public ImvertorStartJobForm(EAImvertorSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.settings = settings;
			this.loadData();

		}
				/// <summary>
		/// load the data from the settings to the form
		/// </summary>
		private void loadData()
		{
			this.defaultPropertiesTextBox.Text = this.settings.defaultProperties;
			this.defaultPropertiesTextBox.Items.AddRange(this.settings.availableProperties.ToArray());
			this.defaultPropertiesPathTextBox.Text = this.settings.defaultPropertiesFilePath;
			this.defaultHistoryFileTextBox.Text = this.settings.defaultHistoryFilePath;
		}
		/// <summary>
		/// save the data from the form to the settings
		/// </summary>
		private void saveChanges()
		{
			this.settings.defaultProperties = this.defaultPropertiesTextBox.Text;
			this.settings.defaultPropertiesFilePath = this.defaultPropertiesPathTextBox.Text;	
			this.settings.defaultHistoryFilePath = this.defaultHistoryFileTextBox.Text;			
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		void BrowseDefaultPropertiesFileButtonClick(object sender, EventArgs e)
		{
			//let the user select a file
            OpenFileDialog browsePropertiesFileDialog = new OpenFileDialog();
            browsePropertiesFileDialog.Filter = "Properties Files (.properties)|*.properties";
            browsePropertiesFileDialog.FilterIndex = 1;
            browsePropertiesFileDialog.Multiselect = false;
            var dialogResult = browsePropertiesFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
            	//if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.defaultPropertiesPathTextBox.Text = browsePropertiesFileDialog.FileName;
            }
		}
		void BrowseDefaultHistoryFileButtonClick(object sender, EventArgs e)
		{
			//let the user select a file
            OpenFileDialog browseHistoryFileDialog = new OpenFileDialog();
            browseHistoryFileDialog.Filter = "History files|*.xls;*.xlsx";
            browseHistoryFileDialog.FilterIndex = 1;
            browseHistoryFileDialog.Multiselect = false;
            var dialogResult = browseHistoryFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
            	//if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.defaultHistoryFileTextBox.Text = browseHistoryFileDialog.FileName;
            }			
		}
	}
}
