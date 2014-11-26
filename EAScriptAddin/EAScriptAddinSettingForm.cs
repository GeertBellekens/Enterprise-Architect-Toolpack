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
		private bool showAllOperations = false;
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
			//TODO Save Settings
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
			new AboutWindow().ShowDialog();
		}
		
		void OperationsListBoxItemCheck(object sender, ItemCheckEventArgs e)
		{
			//don't allow the user to change the checked state
			if (checkBoxesReadOnly)
			{
				e.NewValue = e.CurrentValue;
			}
		}
		
		void AllOperationsCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.showAllOperations = this.allOperationsCheckBox.Checked;
			this.reloadOperations();
		}
		
		void AddFunctionButtonClick(object sender, EventArgs e)
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
}
