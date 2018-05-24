
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace EADoorsNGConnector
{
	/// <summary>
	/// Description of TFSConnectorSettingsForm.
	/// </summary>
	internal partial class EADoorsNGSettingsForm : Form
	{
		private EADoorsNGSettings settings;
		private TSF_EA.Model model;

		public EADoorsNGSettingsForm(EADoorsNGSettings settings, TSF_EA.Model model )
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.settings = settings;
			this.model = model;
			this.loadData();
			this.enableDisable();
			
		}
		private void loadData()
		{
			this.doorsNGUrlTextBox.Text = this.settings.getURL(this.model);
			this.defaultUserTextBox.Text = this.settings.defaultUserName;
			this.defaultStatusTextBox.Text = this.settings.defaultStatus;
			this.defaultProjectTextBox.Text = this.settings.defaultProject;
			
			this.loadMappings();

		}
		private void loadMappings()
		{
			foreach (var mapping in this.settings.requirementMappings) 
			{
				this.requirementMappingsgrid.Rows.Add(mapping.Key, mapping.Value);
			}
		}
		private void enableDisable()
		{
			//TODO
		}
		private void saveChanges()
		{
			this.saveURL();
			this.settings.defaultUserName =this.defaultUserTextBox.Text ;
			this.settings.defaultStatus = this.defaultStatusTextBox.Text;
			this.settings.defaultProject = this.defaultProjectTextBox.Text ;
			this.unloadMappings();
			this.settings.save();
		}
		private void saveURL()
		{
			var projectConnections = this.settings.projectConnections;
			if (projectConnections.ContainsKey(this.model.projectGUID))
		    {
				projectConnections[this.model.projectGUID] = this.doorsNGUrlTextBox.Text;
		    }
			else
			{
				projectConnections.Add(this.model.projectGUID,this.doorsNGUrlTextBox.Text);
			}
			this.settings.projectConnections = projectConnections;
		}
		private void unloadMappings()
		{
			var workitemMappings = new Dictionary<string,string>();
			foreach (DataGridViewRow row in this.requirementMappingsgrid.Rows)
			{
				string EAType = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
				string TFSType = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;
				if (EAType.Length > 0 && TFSType.Length > 0
				    && !workitemMappings.ContainsKey(EAType))
				{
					workitemMappings.Add(EAType,TFSType);
				}
			}
			this.settings.requirementMappings = workitemMappings;
		}
		void DeleteMappingButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.requirementMappingsgrid.SelectedRows) 
			{
				if (!row.IsNewRow)
				{
					requirementMappingsgrid.Rows.Remove(row);
				}
			}
		}
		void OkButtonClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.saveChanges();
			this.Close();
		}
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
