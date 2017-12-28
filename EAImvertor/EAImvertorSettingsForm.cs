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
using System.Linq;

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
			this.proxyTextBox.Text = this.settings.proxy;
			this.defaultPinTextBox.Text = this.settings.defaultPIN;
			this.defaultProcessTextBox.Text = this.settings.defaultProcessName;
			this.defaultProcessTextBox.Items.Clear();
			this.defaultProcessTextBox.Items.AddRange(this.settings.availableProcesses.ToArray());
			this.defaultPropertiesTextBox.Text = this.settings.defaultProperties;
			this.defaultPropertiesTextBox.Items.Clear();
			this.defaultPropertiesTextBox.Items.AddRange(this.settings.availableProperties.ToArray());
			this.defaultPropertiesPathTextBox.Text = this.settings.defaultPropertiesFilePath;
			this.defaultHistoryFileTextBox.Text = this.settings.defaultHistoryFilePath;
			this.timeOutUpDown.Value = this.settings.timeOutInSeconds;
			this.retryIntervalUpDown.Value = this.settings.retryInterval;
			this.resultsFolderTextBox.Text = this.settings.resultsPath;
			this.includeDiagramsCheckBox.Checked = this.settings.includeDiagrams;
		}
		private void unloadData()
		{
			this.settings.imvertorURL = this.ImvertorURLTextbox.Text;
			this.settings.proxy = this.proxyTextBox.Text;
			this.settings.defaultProcessName = this.defaultProcessTextBox.Text;
			this.settings.defaultProperties = this.defaultPropertiesTextBox.Text;
			this.settings.defaultPropertiesFilePath = this.defaultPropertiesPathTextBox.Text;	
			this.settings.defaultHistoryFilePath = this.defaultHistoryFileTextBox.Text;			
			this.settings.timeOutInSeconds = int.Parse(this.timeOutUpDown.Value.ToString()) ;
			this.settings.retryInterval = int.Parse (this.retryIntervalUpDown.Value.ToString());
			this.settings.resultsPath = this.resultsFolderTextBox.Text;
			this.settings.defaultPIN = this.defaultPinTextBox.Text;
			this.settings.includeDiagrams = includeDiagramsCheckBox.Checked;
		}
		/// <summary>
		/// save the data from the form to the settings
		/// </summary>
		private void saveChanges()
		{
			unloadData();
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
		void ResultsButtonBrowseFolderClick(object sender, EventArgs e)
		{
			FolderBrowserDialog browseResultFolderDialog = new FolderBrowserDialog();
			browseResultFolderDialog.SelectedPath = this.resultsFolderTextBox.Text;
			var dialogResult = browseResultFolderDialog.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.resultsFolderTextBox.Text = browseResultFolderDialog.SelectedPath;
			}
		}
		void DefaultPinTextBoxTextChanged(object sender, EventArgs e)
		{
			//if the default pin changes then we have to get the configurations again
			if (defaultPinTextBox.Text != settings.defaultPIN)
			{
				this.unloadData();
				this.loadData();
			}
		}
		void ProxyTextBoxTextChanged(object sender, EventArgs e)
		{
			if (proxyTextBox.Text != settings.proxy)
			{
				this.unloadData();
				this.loadData();
			}
		}
		
	}
}
