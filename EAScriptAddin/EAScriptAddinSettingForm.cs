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
		
		public EAScriptAddinSettingForm(List<MethodInfo> operations, List<ScriptFunction> functions)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			this.addinOperations = operations;
			this.modelFunctions = functions;
			
			this.operationsListBox.DisplayMember = "Name";
			this.functionsCheckBox.DisplayMember = "fullName";
			
			foreach (MethodInfo operation in addinOperations)
			{
				bool hasEquivalentFunction = functions.Exists(x => x.name == operation.Name);
				this.operationsListBox.Items.Add(operation,hasEquivalentFunction);
			}
		}
		
		void OkButtonClick(object sender, EventArgs e)
		{
			//TODO Save Settings
			this.Close();
		}
		
		void OperationsListBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			MethodInfo selectedOperation = (MethodInfo)this.operationsListBox.SelectedItem;
			foreach (ScriptFunction function in this.modelFunctions.Where(x => x.name == selectedOperation.Name))
			{
			         	this.functionsCheckBox.Items.Add(function);
			}
		}
	}
}
