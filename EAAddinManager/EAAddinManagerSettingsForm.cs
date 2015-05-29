/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 13/05/2015
 * Time: 4:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
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
		private void addConfigToListView(AddinConfig addinConfig)
		{
			//add it to the view
			ListViewItem item = new ListViewItem(addinConfig.name);
			item.Tag = addinConfig;
			item.SubItems.Add(addinConfig.load.ToString());
			this.addinsListView.Items.Add(item);
		}
		private void initialize()
		{
			//add the addin configs to the list
			this.refreshAddinConfigs();
			//select the first one
			if (this.addinsListView.Items.Count > 0)
			{
				this.addinsListView.Items[0].Selected = true;
			}
			//add the add-in locations
			refreshAddinSearchPaths();

		}
		private void refreshAddinConfigs()
		{
			this.addinsListView.Items.Clear();
			foreach (AddinConfig addinConfig in this.config.addinConfigs) 
			{
				addConfigToListView(addinConfig);
			}
				
		}
		private void refreshAddinSearchPaths()
		{
			this.locationsListBox.Items.Clear();
			//add the add-in locations
			foreach (string  addinLocation in this.config.addinSearchPaths) 
			{
				this.locationsListBox.Items.Add(addinLocation);
			}
		}
		
		
		void DeleteAddinButtonClick(object sender, EventArgs e)
		{
			foreach (ListViewItem selectedItem in this.addinsListView.SelectedItems)
			{
				//remove from config
				this.config.addinConfigs.Remove((AddinConfig)selectedItem.Tag);
				//remove from listview
				this.addinsListView.Items.Remove(selectedItem);
			}
		}
		
		void DeleteLocationButtonClick(object sender, EventArgs e)
		{
			this.config.addinSearchPaths.Remove((string)this.locationsListBox.SelectedItem);
			this.refreshAddinSearchPaths();
		}
		void OkButtonClick(object sender, System.EventArgs e)
		{
			this.save();
			//close the window
			this.Close();
		}
		private void save()
		{
			//save the config
			this.config.save();
			//load the add-ins
			this.addinManager.loadAddins();
		}
		
		void AddAddinButtonClick(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = getAddinFileDialog("Select a new add-in dll",string.Empty);
            if(openFileDialog.ShowDialog() == DialogResult.OK)
            {
            	AddinConfig newConfig = this.addinManager.loadAddin(openFileDialog.FileName);
            	this.config.addinConfigs.Add(newConfig);
            	this.addConfigToListView(newConfig);
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
		
		void BrowseFileButtonClick(object sender, EventArgs e)
		{
			if (this.addinsListView.SelectedItems.Count == 1)
			{
				AddinConfig currentConfig = (AddinConfig)this.addinsListView.SelectedItems[0].Tag;
				OpenFileDialog openFileDialog = getAddinFileDialog("Select the add-in dll", currentConfig.directory);
	            if(openFileDialog.ShowDialog() == DialogResult.OK)
	            {
	            	
	            	currentConfig = this.addinManager.loadAddin(openFileDialog.FileName);
	            	//show the new fields
	            	this.refreshAddinFields(currentConfig);
	            }		
			}
		}

		private OpenFileDialog getAddinFileDialog(string title, string initialDirectory)
		{
			// Create an instance of the open file dialog box.
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog.Filter = "EA Addin files (.dll)|*.dll|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = title;
			if (initialDirectory == string.Empty && config.addinSearchPaths.Count > 0)
            {
            	openFileDialog.InitialDirectory = this.config.addinSearchPaths[0];
            }
			else
			{
				openFileDialog.InitialDirectory = initialDirectory;
			}
            return openFileDialog;
		}
		
		void ApplyButtonClick(object sender, EventArgs e)
		{
			this.save();
		}
		
		void AddinsListViewItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
		{
			AddinConfig selectedConfig = (AddinConfig)e.Item.Tag;
			if (e.IsSelected)
			{
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

		
		void LoadCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			if (this.addinsListView.SelectedItems.Count == 1)
			{
				AddinConfig selectedConfig = (AddinConfig)this.addinsListView.SelectedItems[0].Tag;
				selectedConfig.load = this.loadCheckBox.Checked;
			}
			else
			{
				this.loadCheckBox.Checked = false;
			}
		}
		
		void AddLocationButtonClick(object sender, EventArgs e)
		{
							
				// Create an instance of the open file dialog box.
	            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				//set the initial directory
				if (this.config.addinSearchPaths.Count > 0)
				{
					folderBrowserDialog.SelectedPath = this.config.addinSearchPaths[0];
				}
	            
	            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
	            {
	            	this.config.addinSearchPaths.Add(folderBrowserDialog.SelectedPath);
	            	//trigger update TODO: catch Add event in config.
	            	this.config.addinSearchPaths = this.config.addinSearchPaths;
	            	//update fields
	            	this.refreshAddinSearchPaths();
	            }
		}
	}
}
