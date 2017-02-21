
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace EATFSConnector
{
	/// <summary>
	/// Description of TFSConnectorSettingsForm.
	/// </summary>
	public partial class TFSConnectorSettingsForm : Form
	{
		private EATFSConnectorSettings settings;
		private TSF_EA.Model model;

		public TFSConnectorSettingsForm(EATFSConnectorSettings settings, TSF_EA.Model model )
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
			this.tfsUrlTextBox.Text = this.settings.getTFSUrl(this.model);
			this.defaultUserTextBox.Text = this.settings.defaultUserName;
			this.defaultWorkitemTypeTextBox.Text = this.settings.defaultWorkitemType;
			this.defaultStatusTextBox.Text = this.settings.defaultStatus;
			this.defaultProjectTextBox.Text = this.settings.defaultProject;
			this.tfsFilterTagTextBox.Text = this.settings.TFSFilterTag;
			this.defaultCollectionTextBox.Text = this.settings.defaultCollection;
			this.loadMappings();

		}
		private void loadMappings()
		{
			foreach (var mapping in this.settings.workitemMappings) 
			{
				this.workitemMappingsgrid.Rows.Add(mapping.Key, mapping.Value);
			}
		}
		private void enableDisable()
		{
			//TODO
		}
		private void saveChanges()
		{
			saveTFSUrl();
			this.settings.defaultUserName =this.defaultUserTextBox.Text ;
			this.settings.defaultWorkitemType = this.defaultWorkitemTypeTextBox.Text;
			this.settings.defaultStatus = this.defaultStatusTextBox.Text;
			this.settings.defaultProject = this.defaultProjectTextBox.Text ;
			this.settings.TFSFilterTag = this.tfsFilterTagTextBox.Text;
			this.settings.defaultCollection = this.defaultCollectionTextBox.Text;
			this.unloadMappings();
			this.settings.save();
		}
		private void saveTFSUrl()
		{
			var projectConnections = this.settings.projectConnections;
			if (projectConnections.ContainsKey(this.model.projectGUID))
		    {
				projectConnections[this.model.projectGUID] = this.tfsUrlTextBox.Text;
		    }
			else
			{
				projectConnections.Add(this.model.projectGUID,this.tfsUrlTextBox.Text);
			}
			this.settings.projectConnections = projectConnections;
		}
		private void unloadMappings()
		{
			var workitemMappings = new Dictionary<string,string>();
			foreach (DataGridViewRow row in this.workitemMappingsgrid.Rows)
			{
				string EAType = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
				string TFSType = row.Cells[1].Value != null ? row.Cells[1].Value.ToString() : string.Empty;
				if (EAType.Length > 0 && TFSType.Length > 0
				    && !workitemMappings.ContainsKey(EAType))
				{
					workitemMappings.Add(EAType,TFSType);
				}
			}
			this.settings.workitemMappings = workitemMappings;
		}
		void DeleteMappingButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.workitemMappingsgrid.SelectedRows) 
			{
				if (!row.IsNewRow)
				{
					workitemMappingsgrid.Rows.Remove(row);
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
