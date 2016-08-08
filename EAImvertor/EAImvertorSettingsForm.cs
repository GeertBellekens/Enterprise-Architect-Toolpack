/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 9/07/2016
 * Time: 6:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAImvertor
{
	/// <summary>
	/// Description of EAImvertorSettingsForm.
	/// </summary>
	public partial class EAImvertorSettingsForm : Form
	{
		private EAImvertorSettings settings;
		public EAImvertorSettingsForm(EAImvertorSettings settings)
		{
			InitializeComponent();
			this.settings = settings;
			this.loadData();
		}
		/// <summary>
		/// load the data from the settings to the form
		/// </summary>
		private void loadData()
		{
			this.ImvertorURLTextbox.Text = this.settings.imvertorURL;
			this.defaultPinTextBox.Text = this.settings.defaultPIN;
			this.defaultProcessTextBox.Text = this.settings.defaultProcessName;
			this.defaultProcessTextBox.Items.AddRange(this.settings.availableProcesses.ToArray());
			this.defaultPropertiesTextBox.Text = this.settings.defaultProperties;
			this.defaultPropertiesTextBox.Items.AddRange(this.settings.availableProperties.ToArray());
			this.defaultPropertiesPathTextBox.Text = this.settings.defaultPropertiesFilePath;
			this.defaultHistoryFileTextBox.Text = this.settings.defaultHistoryFilePath;
			this.timeOutUpDown.Value = this.settings.timeOutInSeconds;
			this.retryIntervalUpDown.Value = this.settings.retryInterval;
		}
		/// <summary>
		/// save the data from the form to the settings
		/// </summary>
		private void saveChanges()
		{
			this.settings.imvertorURL = this.ImvertorURLTextbox.Text;
			this.settings.defaultPIN = this.defaultPinTextBox.Text;
			this.settings.defaultProcessName = this.defaultProcessTextBox.Text;
			this.settings.defaultProperties = this.defaultPropertiesTextBox.Text;
			this.settings.defaultPropertiesFilePath = this.defaultPropertiesPathTextBox.Text;	
			this.settings.defaultHistoryFilePath = this.defaultHistoryFileTextBox.Text;			
			this.settings.timeOutInSeconds = int.Parse(this.timeOutUpDown.Value.ToString()) ;
			this.settings.retryInterval = int.Parse (this.retryIntervalUpDown.Value.ToString());
			this.settings.save();
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
			this.Close();
		}
		void ApplyButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
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
