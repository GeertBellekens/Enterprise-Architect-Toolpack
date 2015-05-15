/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 13/05/2015
 * Time: 4:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EAAddinManager
{
	/// <summary>
	/// Description of EAAddinManagerSettingsForm.
	/// </summary>
	public partial class EAAddinManagerSettingsForm : Form
	{
		private EAAddinManagerConfig config;
		private EAAddinManagerAddinClass addinManager;
		public EAAddinManagerSettingsForm(EAAddinManagerAddinClass addinManager)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.addinManager = addinManager;
			this.config = addinManager.config;
			this.initialize();
			
		}
		private void addConfig(AddinConfig addinConfig)
		{
			ListViewItem item = new ListViewItem(addinConfig.name);
			item.Tag = addinConfig;
			item.SubItems.Add(addinConfig.load.ToString());
			this.addinsListView.Items.Add(item);
		}
		private void initialize()
		{
			//add the addin configs to the list
			foreach (AddinConfig addinConfig in this.config.addinConfigs) 
			{
				addConfig(addinConfig);
			}
			//select the first one
			if (this.addinsListView.Items.Count > 0)
			{
				this.addinsListView.Items[0].Selected = true;
			}
			//add the add-in locations
			foreach (string  addinLocation in this.config.addinSearchPaths) 
			{
				this.locationsListBox.Items.Add(addinLocation);
			}
		}

		
		void AddinsListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.addinsListView.SelectedItems.Count > 0)
			{
				AddinConfig selectedConfig = (AddinConfig)this.addinsListView.SelectedItems[0].Tag;
				this.refreshAddinFields(selectedConfig);
			}
			else
			{
				this.nameTextBox.Text = string.Empty;
				this.fileTextBox.Text = string.Empty;
				this.versionTextBox.Text = string.Empty;
				this.loadCheckBox.Checked = false;
			}
		}
		
		void DeleteAddinButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem selectedItem in this.addinsListView.SelectedItems)
			{
				this.addinsListView.Items.Remove(selectedItem);
			}
		}
		
		void DeleteLocationButtonClick(object sender, EventArgs e)
		{
			this.locationsListBox.Items.Remove(this.locationsListBox.SelectedItem); 
		}
		void OkButtonClick(object sender, System.EventArgs e)
		{
			//TODO save
			this.Close();
		}
		
		void AddAddinButtonClick(object sender, EventArgs e)
		{
			// Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "EA Addin files (.dll)|*.dll|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (config.addinSearchPaths.Count > 0)
            {
            	openFileDialog.InitialDirectory = this.config.addinSearchPaths[0];
            }
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
            	AddinConfig newConfig = this.addinManager.loadAddin(openFileDialog.FileName);
            	this.addConfig(newConfig);
            	//select the last one
            	this.addinsListView.Items[this.addinsListView.Items.Count -1].Selected = true;
            }
		}
		
		void BrowseNameButtonClick(object sender, EventArgs e)
		{
			if (this.addinsListView.SelectedItems.Count > 0)
			{
				AddinConfig currentConfig = (AddinConfig)this.addinsListView.SelectedItems[0].Tag;
				// Create an instance of the open file dialog box.
	            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				//set the initial directory
	            folderBrowserDialog.SelectedPath = currentConfig.directory;
	            
	            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
	            {
	            	currentConfig.directory = folderBrowserDialog.SelectedPath;
	            	//update fields
	            	this.refreshAddinFields(currentConfig);
	            }
			}
		}
		void refreshAddinFields(AddinConfig selectedConfig)
		{
			this.nameTextBox.Text = selectedConfig.name;
			this.fileTextBox.Text = selectedConfig.dllPath;
			this.versionTextBox.Text = selectedConfig.version;
			this.loadCheckBox.Checked = selectedConfig.load;
		}
	}
}
