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
			this.enableDisable();
			
		}
		private void loadData()
		{
			// get the ignored stereotypes
			loadGridData(ignoredStereoTypesGrid, settings.ignoredStereotypes);
			//get the ignored tagged values
			loadGridData(ignoredTaggedValuesGrid, settings.ignoredTaggedValues);
			//get the datatypes to copy
			loadGridData(dataTypesGridView, settings.dataTypesToCopy);
			//get the hidden element types
			loadGridData(hiddenElementGrid, settings.hiddenElementTypes);
			//copy generalizations
			this.generalCopyGeneralizationsCheckbox.Checked = settings.copyGeneralizations;
			//addDataTypes checkbox
			this.addDataTypesCheckBox.Checked = this.settings.addDataTypes;
			//addSourceElements checkbox
			this.addSourceElementCheckBox.Checked = this.settings.addSourceElements;
            //copySourceElements checkbox
            this.copyDatatypesCheckbox.Checked = settings.copyDataTypes;
            //limit datatypes checkbox
            this.limitDatatypesCheckBox.Checked = settings.limitDataTypes;
            //copy Generalizations checkbox
            this.copyDataTypeGeneralizationsCheckBox.Checked = settings.copyDataTypeGeneralizations;
            //sourceAttributeTag
            this.attributeTagTextBox.Text = settings.sourceAttributeTagName;
            //sourceAssociationTag
            this.associationTagTextBox.Text = settings.sourceAssociationTagName;
            //redirectGeneralizationsToSubset
            this.RedirectGeneralizationsCheckBox.Checked = this.settings.redirectGeneralizationsToSubset;
            this.prefixNotesCheckBox.Checked =  this.settings.prefixNotes;
		    this.notesPrefixTextBox.Text = this.settings.prefixNotesText;
		    this.checkSecurityCheckBox.Checked = this.settings.checkSecurity;
		    this.deleteUnusedElementsCheckBox.Checked = this.settings.deleteUnusedSchemaElements;
		    this.usePackageSubsetsOnlyCheckBox.Checked = this.settings.usePackageSchemasOnly;
		    //xml schema options
		    this.noAttributeDependenciesCheckbox.Checked = this.settings.dontCreateAttributeDependencies;
		    this.orderAssociationsCheckbox.Checked = this.settings.orderAssociationsAlphabetically;
		    this.orderAssociationsAmongstAttributesCheckbox.Checked = this.settings.orderAssociationsAmongstAttributes;
		    this.tvInsteadOfTraceCheckBox.Checked = this.settings.tvInsteadOfTrace;
		    this.elementTagTextBox.Text = this.settings.elementTagName;
		}
		private void enableDisable()
		{
			this.limitDatatypesCheckBox.Enabled = copyDatatypesCheckbox.Checked;
			this.dataTypesGridView.Enabled = this.limitDatatypesCheckBox.Checked;
			this.deleteDataTypeButton.Enabled = this.dataTypesGridView.Enabled;
			this.copyDataTypeGeneralizationsCheckBox.Enabled = this.copyDatatypesCheckbox.Checked;
			this.notesPrefixTextBox.Enabled = this.prefixNotesCheckBox.Checked;
			this.orderAssociationsAmongstAttributesCheckbox.Enabled = this.orderAssociationsCheckbox.Checked;
			this.elementTagTextBox.Enabled = tvInsteadOfTraceCheckBox.Checked;
		}
		private void saveChanges()
		{
			//get the stereotypes from the grid
			this.extractStereotypes();
			//get the tagged values from the grid
			this.extractTaggedValues();
			//get the datatypes from the grid
			this.extractDataTypes();
			//get the hidden 
			this.extractHiddenElementTypes();
			//general options
			this.settings.copyGeneralizations = this.generalCopyGeneralizationsCheckbox.Checked;
		    this.settings.redirectGeneralizationsToSubset = this.RedirectGeneralizationsCheckBox.Checked;
		    this.settings.prefixNotes = this.prefixNotesCheckBox.Checked;
		    this.settings.prefixNotesText = this.notesPrefixTextBox.Text;
		    this.settings.checkSecurity = this.checkSecurityCheckBox.Checked;
		    this.settings.deleteUnusedSchemaElements = this.deleteUnusedElementsCheckBox.Checked;
		    this.settings.usePackageSchemasOnly = this.usePackageSubsetsOnlyCheckBox.Checked;
			//diagram options
			this.settings.addDataTypes = this.addDataTypesCheckBox.Checked;
			this.settings.addSourceElements = this.addSourceElementCheckBox.Checked;
			//datatype options
		    this.settings.copyDataTypes = this.copyDatatypesCheckbox.Checked;
		    this.settings.limitDataTypes = this.limitDatatypesCheckBox.Checked;
		    this.settings.copyDataTypeGeneralizations = this.copyDataTypeGeneralizationsCheckBox.Checked;
		    //tracebility tag names
		    this.settings.sourceAttributeTagName = this.attributeTagTextBox.Text;
		    this.settings.sourceAssociationTagName = this.associationTagTextBox.Text;
		    //xml schema settings
		    this.settings.dontCreateAttributeDependencies = this.noAttributeDependenciesCheckbox.Checked;
		    this.settings.orderAssociationsAlphabetically = this.orderAssociationsCheckbox.Checked;
		    this.settings.orderAssociationsAmongstAttributes = this.orderAssociationsAmongstAttributesCheckbox.Checked;
		    this.settings.tvInsteadOfTrace = this.tvInsteadOfTraceCheckBox.Checked;
		    this.settings.elementTagName = this.elementTagTextBox.Text;
			//save changes
			this.settings.save();
		}
		private void loadGridData(DataGridView datagrid, List<string> data)
		{
			foreach (var rowvalue in data) 
			{
				datagrid.Rows.Add(rowvalue);
			}
		}
		private void extractDataTypes()
		{
			this.settings.dataTypesToCopy = getListFromDataGrid(dataTypesGridView);
		}
		private void extractStereotypes()
		{
			this.settings.ignoredStereotypes = getListFromDataGrid(ignoredStereoTypesGrid);
		}
		private void extractTaggedValues()
		{
			this.settings.ignoredTaggedValues = getListFromDataGrid(ignoredTaggedValuesGrid);
		}
		private void extractHiddenElementTypes()
		{
			this.settings.hiddenElementTypes = getListFromDataGrid(hiddenElementGrid);
		}
		private List<string> getListFromDataGrid(DataGridView datagrid)
		{
			//make new tagged values list
			var returnedList = new List<string>();
			//get ignored tagged values from grid
			foreach (DataGridViewRow row in datagrid.Rows)
			{
				string rowValue = row.Cells[0].Value != null ? row.Cells[0].Value.ToString() : string.Empty;
				if (rowValue != string.Empty)
				{
					returnedList.Add(rowValue);
				}
			}
			return returnedList;
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
		void DeleteDataTypeButtonClick(object sender, EventArgs e)
		{
			foreach (DataGridViewRow row in this.dataTypesGridView.SelectedRows) 
			{
				if (!row.IsNewRow)
				{
					dataTypesGridView.Rows.Remove(row);
				}
			}
		}
		void CopyDatatypesCheckboxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void LimitDatatypesCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void PrefixNotesCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void OrderAssociationsCheckboxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}
		void TtvInsteadOfTraceCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			this.enableDisable();
		}


	
	}
}
