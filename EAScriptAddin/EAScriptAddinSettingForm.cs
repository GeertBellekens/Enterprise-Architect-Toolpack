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
		private Boolean checkBoxesReadOnly = false;
		
		public EAScriptAddinSettingForm(List<MethodInfo> operations, List<ScriptFunction> functions)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.addinOperations = operations;
			this.modelFunctions = functions;
			
			this.operationsListBox.DisplayMember = "Name";
			this.functionsListBox.DisplayMember = "fullName";
			
			foreach (MethodInfo operation in addinOperations)
			{
				bool hasEquivalentFunction = functions.Exists(x => x.name == operation.Name);
				this.operationsListBox.Items.Add(operation,hasEquivalentFunction);
			}
			//finished loading, now set the checkboxes readonly
			this.checkBoxesReadOnly = true;
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
			         	this.functionsListBox.Items.Add(function);
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
	}
}
