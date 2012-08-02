/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 31/07/2012
 * Time: 5:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace TSF.UmlToolingFramework.EANavigator
{
	/// <summary>
	/// Description of NavigatorSettingsForm.
	/// </summary>
	public partial class NavigatorSettingsForm : Form
	{
		NavigatorSettings settings;
		public NavigatorSettingsForm(NavigatorSettings settings)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.settings = settings;
			
			this.projectBrowserRadioButton.Checked = settings.projectBrowserDefaultAction;
			this.propertiesRadioButton.Checked = ! settings.projectBrowserDefaultAction;
			this.showToolbarCheckBox.Checked = this.settings.toolbarVisible;
		}
		
		
		

		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.settings.save();
			this.Close();
		}
		
		void ProjectBrowserRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			this.settings.projectBrowserDefaultAction = this.projectBrowserRadioButton.Checked;
		}
		
		
		void PropertiesRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			this.settings.projectBrowserDefaultAction = !this.propertiesRadioButton.Checked;
		}
		
		void CancelButtonClick(object sender, EventArgs e)
		{
			this.settings.refresh();
		}
		
		void ShowToolbarCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.settings.toolbarVisible = this.showToolbarCheckBox.Checked;
		}
	}
}
