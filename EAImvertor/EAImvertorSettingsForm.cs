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
		}
		/// <summary>
		/// save the data from the form to the settings
		/// </summary>
		private void saveChanges()
		{
			this.settings.imvertorURL = this.ImvertorURLTextbox.Text;
			this.settings.defaultPIN = this.defaultPinTextBox.Text;
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
	}
}
