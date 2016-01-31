/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 27/01/2016
 * Time: 5:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ECDMMessageComposer
{
	/// <summary>
	/// Description of SettingsWindow.
	/// </summary>
	public partial class SettingsWindow : Form
	{
		private ECDMMessageComposerSettings settings;
		public SettingsWindow(ECDMMessageComposerSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.settings = settings;
			this.loadData();
			
		}
		private void loadData()
		{
			// get the ignored stereotypes
			foreach (string  stereotype in this.settings.ignoredStereotypes) 
			{
				this.ignoredStereoTypesGrid.Rows.Add(stereotype);
			}
			//get the ignored tagged values
			foreach (string  stereotype in this.settings.ignoredTaggedValues) 
			{
				this.ignoredTaggedValuesGrid.Rows.Add(stereotype);
			}
		}
		private void saveChanges()
		{
			//get the stereotypes from the grid
			this.extractStereotypes();
			//get the tagged values from the grid
			this.extractTaggedValues();
			//save changes
			this.settings.save();
		}
		private void extractStereotypes()
		{
			//make new stereotypes list
			var stereotypes = new List<string>();
			//get ignored steretoypes from grid
			foreach (DataGridViewRow row in this.ignoredStereoTypesGrid.Rows)
			{
				string stereotype = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
				if (stereotype != string.Empty)
				{
					stereotypes.Add(stereotype);
				}
			}
			this.settings.ignoredStereotypes = stereotypes;
		}
		private void extractTaggedValues()
		{
			//make new tagged values list
			var tagedValues = new List<string>();
			//get ignored tagged values from grid
			foreach (DataGridViewRow row in this.ignoredTaggedValuesGrid.Rows)
			{
				string tagedValue = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
				if (tagedValue != string.Empty)
				{
					tagedValues.Add(tagedValue);
				}
			}
			this.settings.ignoredTaggedValues = tagedValues;
		}

		void DeleteStereotypeButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.ignoredStereoTypesGrid.SelectedRows) 
			{
				if (!row.IsNewRow)
				{
					ignoredStereoTypesGrid.Rows.Remove(row);
				}
			}
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
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		void DeleteTaggedValueButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.ignoredTaggedValuesGrid.SelectedRows) 
			{
				if (!row.IsNewRow)
				{
					ignoredTaggedValuesGrid.Rows.Remove(row);
				}
			}
		}
		
	}
}
