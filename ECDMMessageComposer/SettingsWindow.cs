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
		}
		private void saveChanges()
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
			//save changes
			this.settings.save();
		}

		void DeleteStereotypeButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.ignoredStereoTypesGrid.SelectedRows) 
			{
				if (! row.IsNewRow)
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
	}
}
