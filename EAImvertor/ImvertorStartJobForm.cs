
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace EAImvertor
{
	/// <summary>
	/// Description of ImvertorStartJobForm.
	/// </summary>
	public partial class ImvertorStartJobForm : Form
	{
		private EAImvertorJobSettings settings;
		public ImvertorStartJobForm(EAImvertorJobSettings settings)
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
			this.defaultPropertiesTextBox.Text = this.settings.Properties;
			this.defaultPropertiesTextBox.Items.AddRange(this.settings.availableProperties.ToArray());
			this.defaultPropertiesPathTextBox.Text = this.settings.PropertiesFilePath;
			this.defaultHistoryFileTextBox.Text = this.settings.HistoryFilePath;
		}
		/// <summary>
		/// save the data from the form to the settings
		/// </summary>
		private void saveChanges()
		{
			this.settings.Properties = this.defaultPropertiesTextBox.Text;
			this.settings.PropertiesFilePath = this.defaultPropertiesPathTextBox.Text;	
			this.settings.HistoryFilePath = this.defaultHistoryFileTextBox.Text;			
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
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
