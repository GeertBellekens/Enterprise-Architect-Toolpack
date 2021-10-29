/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 26/10/2014
 * Time: 6:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using EAAddinFramework.EASpecific;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using System.Reflection;
using System.Linq;
using BrightIdeasSoftware;

namespace EAScriptAddin
{
	/// <summary>
	/// Description of EAScriptAddinSettingForm.
	/// </summary>
	public partial class EAScriptAddinSettingForm : Form
	{
		private List<MethodInfo> addinOperations;
		private List<ScriptFunction> modelFunctions;
		private List<Script> modelScripts;
		private EAScriptAddinAddinClass controller;
		
		public EAScriptAddinSettingForm(List<MethodInfo> operations, List<ScriptFunction> functions,List<Script> scripts,EAScriptAddinAddinClass scriptAddin)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.setDelegates();
			
			this.controller = scriptAddin;
			
			this.addinOperations = operations;
			this.modelFunctions = functions;
			this.modelScripts = scripts;
									
			//load the settings data
			this.loadData();
			
		}

		private void enableDisable()
		{
			this.scriptPathTextBox.Enabled = this.developerModeCheckBox.Checked;
			this.addFunctionButton.Enabled = this.selectedScript != null;
		}
		private void loadData()
		{
			//this.functionDropdown.DisplayMember = "Name";
			//this.functionDropdown.ValueMember = null;
			this.functionDropdown.DataSource = this.addinOperations;

			this.developerModeCheckBox.Checked = this.controller.settings.developerMode;
			this.scriptPathTextBox.Text = this.controller.settings.scriptPath;
			this.scriptTreeView.Objects = this.getScriptGroups().OrderBy(x => x.name);
			enableDisable();
		}
		private List<ScriptGroup> getScriptGroups()
		{
			var scriptGroups = new List<ScriptGroup>();
			foreach (var script in this.modelScripts)
			{
				if (!scriptGroups.Any(x => x.name == script.groupName))
				{
					scriptGroups.Add(script.group);
				}
			}
			return scriptGroups;
		}
		private void saveChanges()
		{
			this.controller.settings.developerMode = this.developerModeCheckBox.Checked;
			this.controller.settings.scriptPath = this.scriptPathTextBox.Text;
			this.controller.settings.save();
		}
		private void setDelegates()
		{
			//tell the control who can expand 
			TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object o)
			{
				var scriptGroup = o as ScriptGroup;
				if (scriptGroup != null)
				{
					return scriptGroup.scripts.Any();
				}
				var script = o as Script;
				if (script != null)
				{
					//return script.includedModelScripts.Any();
					return script.addinFunctions.Any();
				}
				return false;
				//return ((ScriptInclude)o).hasIncludes;
			};
			this.scriptTreeView.CanExpandGetter = canExpandGetter;
			//tell the control how to expand
			TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object o)
			{
				var scriptGroup = o as ScriptGroup;
				if (scriptGroup != null)
				{
					return scriptGroup.scripts.OrderBy(x => x.name);
				}
				var script = o as Script;
				if (script != null)
				{
					//return script.includedModelScripts;
					return script.addinFunctions.OrderBy(x => x.name);
				}
				return new List<string>();
				//return ((ScriptInclude)o).scriptIncludes;
			};
			this.scriptTreeView.ChildrenGetter = childrenGetter;
			//tell the control which image to show
			ImageGetterDelegate imageGetter = delegate (object o)
			{
				
				var scriptGroup = o as ScriptGroup;
				if (scriptGroup != null)
				{
					return "ScriptGroup";
				}
				var script = o as Script;
				if (script != null)
				{
					return "Script";
				}
				else
				{
					return "Operation";
				}
			};
			this.nameColumn.ImageGetter = imageGetter;
		}


		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
			this.Close();
		}
		
		
		void AboutButtonClick(object sender, EventArgs e)
		{
			new AboutWindow().ShowDialog(this);
		}
		
		
		void AddFunctionButtonClick(object sender, EventArgs e)
		{
			this.addFunction();
		}
		private Script selectedScript
		{
			get
			{
				Script selectedScript = null;
				var selectedObject = this.scriptTreeView.SelectedObject;
				selectedScript = selectedObject as Script;
				//if script was selected, return the script
				if (selectedScript == null)
				{
					var selectedFunction = selectedObject as ScriptFunction;
					// if parent
					if (selectedFunction != null)
					{
						selectedScript = selectedFunction.owner;
					}
				}
				return selectedScript;
			}
		}
		/// <summary>
		/// add a function for the selected operation to the selected script
		/// </summary>
		private void addFunction()
		{
			var script = this.selectedScript;
			var selectedFunction = functionDropdown.SelectedItem as MethodInfo;
			if (selectedFunction != null
				&& script != null
				&& script?.isStatic == false)
			{
				ScriptFunction newFunction = this.controller.addNewScriptFunction(selectedFunction, script);
			}
			//refresh scriptTreeView
			this.scriptTreeView.RefreshObject(script);
		}
		
		
		void LicenseExpiredLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
		
		private void scriptPathSelectButton_Click(object sender, EventArgs e)
		{
			// Create an instance of the open file dialog box.
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				this.scriptPathTextBox.Text = folderBrowserDialog.SelectedPath;
			}
		}

		private void developerModeCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}

		private void scriptTreeView_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
	}
}
