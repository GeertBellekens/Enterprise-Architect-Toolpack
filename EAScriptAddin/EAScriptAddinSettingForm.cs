﻿/*
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
		private bool checkBoxesReadOnly = false;
		private bool showAllOperations = true;
		private EAScriptAddinAddinClass controller;
		
		public EAScriptAddinSettingForm(List<MethodInfo> operations, List<ScriptFunction> functions,List<Script> scripts,EAScriptAddinAddinClass scriptAddin)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.controller = scriptAddin;
			
			this.addinOperations = operations;
			this.modelFunctions = functions;
			this.modelScripts = scripts;
									
			this.ScriptCombo.DisplayMember = "displayName";
			//add the scripts to the combobox
			foreach (Script script in this.modelScripts) 
			{
				this.ScriptCombo.Items.Add(script);
			}
			//select the first script
			if (this.ScriptCombo.Items.Count > 0)
			{
				this.ScriptCombo.SelectedIndex = 0;
			}
			this.operationsListBox.DisplayMember = "Name";
			this.functionsListBox.DisplayMember = "fullName";
			//load the operations
			this.reloadOperations();
			//load the settings data
			this.loadData();
			
		}

		private void enableDisable()
		{
			this.scriptPathTextBox.Enabled = this.developerModeCheckBox.Checked;
		}
		private void loadData()
		{
			this.developerModeCheckBox.Checked = this.controller.settings.developerMode;
			this.scriptPathTextBox.Text = this.controller.settings.scriptPath;
			enableDisable();
		}
		private void saveChanges()
		{
			this.controller.settings.developerMode = this.developerModeCheckBox.Checked;
			this.controller.settings.scriptPath = this.scriptPathTextBox.Text;
			this.controller.settings.save();
		}


		/// <summary>
		/// reloads the operations in the list box
		/// </summary>
		private void reloadOperations()
		{
			//set the checkboxes read/write
			this.checkBoxesReadOnly = false;
			
			//clear the listboxes
			this.operationsListBox.Items.Clear();
			this.functionsListBox.Items.Clear();
			
			//add the operations
			foreach (MethodInfo operation in this.addinOperations)
			{
				bool hasEquivalentFunction = this.modelFunctions.Exists(x => x.name == operation.Name);
				if (hasEquivalentFunction || showAllOperations)
				{
					this.operationsListBox.Items.Add(operation,hasEquivalentFunction);
				}
			}
			//finished loading, now set the checkboxes back to readonly
			this.checkBoxesReadOnly = true;
			//select the first one
			if (this.operationsListBox.Items.Count > 0)
			{
				this.operationsListBox.SelectedIndex = 0;
			}
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			this.saveChanges();
			this.Close();
		}
		
		void OperationsListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			//clear the functions
			this.functionsListBox.Items.Clear();
			//get the selected operations
			MethodInfo selectedOperation = (MethodInfo)this.operationsListBox.SelectedItem;
			//add each corresponding function to the functions checkbox list
			foreach (ScriptFunction function in this.modelFunctions.Where(x => x.name == selectedOperation.Name))
			{
				this.functionsListBox.Items.Add(function,true);
			}
		}
		
		void AboutButtonClick(object sender, EventArgs e)
		{
			new AboutWindow().ShowDialog(this);
		}
		
		void OperationsListBoxItemCheck(object sender, ItemCheckEventArgs e)
		{
			//only allow user to check, not to uncheck
			if (checkBoxesReadOnly)
			{
				if (e.NewValue == CheckState.Unchecked ||
				   (ScriptCombo.SelectedItem != null &&  ((Script)this.ScriptCombo.SelectedItem).isStatic))
				{
					e.NewValue = e.CurrentValue;
				}
				//add the function if the user has checked the operation
				else if (e.CurrentValue == CheckState.Unchecked && e.NewValue == CheckState.Checked)
				{
					this.addFunction();
				}
			}
		}
		
		void AllOperationsCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.showAllOperations = this.allOperationsCheckBox.Checked;
			this.reloadOperations();
		}
		
		void AddFunctionButtonClick(object sender, EventArgs e)
		{
			this.addFunction();
		}
		/// <summary>
		/// add a function for the selected operation to the selected script
		/// </summary>
		private void addFunction()
		{
			if (this.operationsListBox.SelectedItem != null
			    && this.ScriptCombo.SelectedItem != null
			    && ! ((Script)this.ScriptCombo.SelectedItem).isStatic )
			{
				ScriptFunction newFunction = this.controller.addNewScriptFunction(this.operationsListBox.SelectedItem as MethodInfo, this.ScriptCombo.SelectedItem as Script);
				//add this function to the list	
				if (newFunction != null)
				{
					this.modelFunctions.Add(newFunction);
					this.functionsListBox.Items.Add(newFunction,true);
					this.checkBoxesReadOnly = false;
					this.operationsListBox.SetItemChecked(operationsListBox.SelectedIndex, true);
					this.checkBoxesReadOnly = true;
				}
			}
		}
		
		
		void LicenseExpiredLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
		}
		
		void ScriptComboSelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.ScriptCombo.SelectedItem != null
			    && ! ((Script)this.ScriptCombo.SelectedItem).isStatic)
			{
				this.addFunctionButton.Enabled = true;
			}
			else
			{
				this.addFunctionButton.Enabled = false;
			}
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
	}
}
