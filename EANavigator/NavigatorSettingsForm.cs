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
			this.useContextMenuCheckBox.Checked = this.settings.contextmenuVisible;
			this.trackSelectedElementCheckBox.Checked = this.settings.trackSelectedElement;
			//quicksearch settings
			this.quickSearchElementsCheck.Checked = this.settings.quickSearchElements;
			this.quickSearchOperationsCheck.Checked = this.settings.quickSearchOperations;
			this.quickSearchAttributesCheck.Checked = this.settings.quickSearchAttributes;
			this.quickSearchDiagramsCheck.Checked = this.settings.quickSearchDiagrams;
			this.quickSearchAddToDiagramCheck.Checked = this.settings.quickSearchAddToDiagram;
			this.quickSearchSelectCheckBox.Checked = this.settings.quickSearchSelectProjectBrowser ;
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
		
		void UseContextMenuCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.settings.contextmenuVisible = this.useContextMenuCheckBox.Checked;
		}
		
		void TrackSelectedElementCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.settings.trackSelectedElement = this.trackSelectedElementCheckBox.Checked;
		}
		
		void QuickSearchElementsCheckCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchElements = this.quickSearchElementsCheck.Checked;
		}
		
		void QuickSearchOperationsCheckCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchOperations = this.quickSearchOperationsCheck.Checked;
		}
		
		void QuickSearchAttributesCheckCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchAttributes = this.quickSearchAttributesCheck.Checked;
		}
		
		void QuickSearchDiagramsCheckCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchDiagrams = this.quickSearchDiagramsCheck.Checked;
		}
		
		void QuickSearchAddToDiagramCheckCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchAddToDiagram = this.quickSearchAddToDiagramCheck.Checked;
		}
		
		void QuickSearchSelectCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.settings.quickSearchSelectProjectBrowser = this.quickSearchSelectCheckBox.Checked;	
		}
	}
}
