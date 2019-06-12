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
            this.InitializeComponent();
            this.settings = settings;
            this.loadData();
            this.enableDisable();

        }
        private void loadData()
        {
            // get the ignored stereotypes
            this.loadGridData(this.ignoredStereoTypesGrid, this.settings.ignoredStereotypes);
            //get the ignored tagged values
            this.loadGridData(this.ignoredTaggedValuesGrid, this.settings.ignoredTaggedValues);
            //get the ignored constraints
            this.loadGridData(this.ignoredConstraintsGrid, this.settings.ignoredConstraintTypes);
            //get the datatypes to copy
            this.loadGridData(this.dataTypesGridView, this.settings.dataTypesToCopy);
            //get the hidden element types
            this.loadGridData(this.hiddenElementGrid, this.settings.hiddenElementTypes);
            //copy generalizations
            this.generalCopyGeneralizationsCheckbox.Checked = this.settings.copyGeneralizations;
            //Generate to artifact package
            this.generateToArtifactPackageCheckBox.Checked = this.settings.generateToArtifactPackage;
            //generate diagram checkbox
            this.generateDiagramCheckbox.Checked = this.settings.generateDiagram;
            //addDataTypes checkbox
            this.addDataTypesCheckBox.Checked = this.settings.addDataTypes;
            //addSourceElements checkbox
            this.addSourceElementCheckBox.Checked = this.settings.addSourceElements;
            //copySourceElements checkbox
            this.copyDatatypesCheckbox.Checked = this.settings.copyDataTypes;
            //limit datatypes checkbox
            this.limitDatatypesCheckBox.Checked = this.settings.limitDataTypes;
            //copy Generalizations checkbox
            this.copyDataTypeGeneralizationsCheckBox.Checked = this.settings.copyDataTypeGeneralizations;
            //Attribute options
            this.keepAttributeOrderRadio.Checked = this.settings.keepOriginalAttributeOrder;
            this.setAttributesOrderZeroRadio.Checked = this.settings.setAttributeOrderZero;
            this.addNewAttributesLastRadio.Checked = !this.settings.setAttributeOrderZero && !this.settings.keepOriginalAttributeOrder;
            //sourceAttributeTag
            this.attributeTagTextBox.Text = this.settings.sourceAttributeTagName;
            //sourceAssociationTag
            this.associationTagTextBox.Text = this.settings.sourceAssociationTagName;
            //redirectGeneralizationsToSubset
            this.RedirectGeneralizationsCheckBox.Checked = this.settings.redirectGeneralizationsToSubset;
            this.checkSecurityCheckBox.Checked = this.settings.checkSecurity;
            this.deleteUnusedElementsCheckBox.Checked = this.settings.deleteUnusedSchemaElements;
            this.usePackageSubsetsOnlyCheckBox.Checked = this.settings.usePackageSchemasOnly;
            //notes options
            this.prefixNotesCheckBox.Checked = this.settings.prefixNotes;
            this.notesPrefixTextBox.Text = this.settings.prefixNotesText;
            this.keepNotesInSyncCheckBox.Checked = this.settings.keepNotesInSync;
            //xml schema options
            this.noAttributeDependenciesCheckbox.Checked = this.settings.dontCreateAttributeDependencies;
            this.orderAssociationsCheckbox.Checked = this.settings.orderAssociationsAlphabetically;
            this.orderAssociationsAmongstAttributesCheckbox.Checked = this.settings.orderAssociationsAmongstAttributes;
            this.choiceBeforeAttributesCheckbox.Checked = this.settings.orderXmlChoiceBeforeAttributes;
            this.customOrderTagTextBox.Text = this.settings.customPositionTag;
            this.tvInsteadOfTraceCheckBox.Checked = this.settings.tvInsteadOfTrace;
            this.elementTagTextBox.Text = this.settings.elementTagName;
            
        }
        private void enableDisable()
        {
            this.limitDatatypesCheckBox.Enabled = this.copyDatatypesCheckbox.Checked;
            this.dataTypesGridView.Enabled = this.limitDatatypesCheckBox.Checked;
            this.deleteDataTypeButton.Enabled = this.dataTypesGridView.Enabled;
            this.copyDataTypeGeneralizationsCheckBox.Enabled = this.copyDatatypesCheckbox.Checked;
            this.notesPrefixTextBox.Enabled = this.prefixNotesCheckBox.Checked && !this.keepNotesInSyncCheckBox.Checked; ;
            this.prefixNotesCheckBox.Enabled = !this.keepNotesInSyncCheckBox.Checked;
            this.orderAssociationsAmongstAttributesCheckbox.Enabled = this.orderAssociationsCheckbox.Checked;
            this.elementTagTextBox.Enabled = this.tvInsteadOfTraceCheckBox.Checked;
            this.prefixNotesCheckBox.Enabled = !this.keepNotesInSyncCheckBox.Checked;
        }
        private void saveChanges()
        {
            //get the stereotypes from the grid
            this.settings.ignoredStereotypes = this.getListFromDataGrid(this.ignoredStereoTypesGrid);
            //get the tagged values from the grid
            this.settings.ignoredTaggedValues = this.getListFromDataGrid(this.ignoredTaggedValuesGrid);
            //get the constraint types from the grid
            this.settings.ignoredConstraintTypes = this.getListFromDataGrid(this.ignoredConstraintsGrid);
            //get the datatypes from the grid
            this.settings.dataTypesToCopy = this.getListFromDataGrid(this.dataTypesGridView);
            //get the hidden 
            this.settings.hiddenElementTypes = this.getListFromDataGrid(this.hiddenElementGrid);
            //general options
            this.settings.copyGeneralizations = this.generalCopyGeneralizationsCheckbox.Checked;
            this.settings.redirectGeneralizationsToSubset = this.RedirectGeneralizationsCheckBox.Checked;

            this.settings.checkSecurity = this.checkSecurityCheckBox.Checked;
            this.settings.deleteUnusedSchemaElements = this.deleteUnusedElementsCheckBox.Checked;
            this.settings.usePackageSchemasOnly = this.usePackageSubsetsOnlyCheckBox.Checked;
            this.settings.generateToArtifactPackage = this.generateToArtifactPackageCheckBox.Checked;
            //notes options
            this.settings.prefixNotes = this.prefixNotesCheckBox.Checked;
            this.settings.prefixNotesText = this.notesPrefixTextBox.Text;
            this.settings.keepNotesInSync = this.keepNotesInSyncCheckBox.Checked;
            //diagram options
            this.settings.generateDiagram = this.generateDiagramCheckbox.Checked;
            this.settings.addDataTypes = this.addDataTypesCheckBox.Checked;
            this.settings.addSourceElements = this.addSourceElementCheckBox.Checked;
            //datatype options
            this.settings.copyDataTypes = this.copyDatatypesCheckbox.Checked;
            this.settings.limitDataTypes = this.limitDatatypesCheckBox.Checked;
            this.settings.copyDataTypeGeneralizations = this.copyDataTypeGeneralizationsCheckBox.Checked;
            //Attribute options
            this.settings.keepOriginalAttributeOrder = this.keepAttributeOrderRadio.Checked;
            this.settings.setAttributeOrderZero = this.setAttributesOrderZeroRadio.Checked;
            //tracebility tag names
            this.settings.sourceAttributeTagName = this.attributeTagTextBox.Text;
            this.settings.sourceAssociationTagName = this.associationTagTextBox.Text;
            //xml schema settings
            this.settings.dontCreateAttributeDependencies = this.noAttributeDependenciesCheckbox.Checked;
            this.settings.orderAssociationsAlphabetically = this.orderAssociationsCheckbox.Checked;
            this.settings.orderAssociationsAmongstAttributes = this.orderAssociationsAmongstAttributesCheckbox.Checked;
            this.settings.orderXmlChoiceBeforeAttributes = this.choiceBeforeAttributesCheckbox.Checked;
            this.settings.customPositionTag = this.customOrderTagTextBox.Text;
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
                    this.ignoredStereoTypesGrid.Rows.Remove(row);
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
                    this.ignoredTaggedValuesGrid.Rows.Remove(row);
                }
            }
        }
        private void deleteConstraintTypeButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.ignoredConstraintsGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.ignoredConstraintsGrid.Rows.Remove(row);
                }
            }
        }
        void DeleteDataTypeButtonClick(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.dataTypesGridView.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.dataTypesGridView.Rows.Remove(row);
                }
            }
        }
        private void deleteHiddenElementButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.hiddenElementGrid.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    this.hiddenElementGrid.Rows.Remove(row);
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

        private void keepNotesInSyncCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }


    }
}
