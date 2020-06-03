using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace EAValidator
{
    /// <summary>
    /// User Control that contains interface elements
    /// </summary>
    public partial class ucEAValidator : UserControl
    {
        private TSF_EA.Element scopeElement { get; set; }
        private TSF_EA.Diagram scopeDiagram { get; set; }
        private EAValidatorController controller { get; set; }

        public ucEAValidator()
        {
            this.InitializeComponent();  // needed for Windows Form
            this.setDelegates();
        }
        private void setDelegates()
        {
            //tell the control who can expand 
            TreeListView.CanExpandGetterDelegate canExpandGetter = delegate (object x)
            {
                var checkGroup = x as CheckGroup;
                return checkGroup != null && checkGroup.subItems.Any();
            };
            this.olvChecks.CanExpandGetter = canExpandGetter;
            //tell the control how to expand
            TreeListView.ChildrenGetterDelegate childrenGetter = delegate (object x)
            {
                var checkGroup = x as CheckGroup;
                return checkGroup?.subItems;
            };
            this.olvChecks.ChildrenGetter = childrenGetter;
            //tell the control which image to show
            ImageGetterDelegate imageGetter = delegate (object rowObject)
            {
                if (rowObject is Check)
                {
                    return "Check";
                }
                else
                {
                    return "package";
                }
            };
            this.olvColName.ImageGetter = imageGetter;
        }

        public void setController(EAValidatorController controller)
        {
            this.controller = controller;
            // Load files from directory as checks
            this.controller.loadChecksFromDirectory(this.controller.settings.ValidationChecks_Directory);

            // Show checks/validations in objectListViews     
            //this.olvColCheckDescription.HeaderCheckState = CheckState.Checked;
            if (this.controller.rootGroup != null)
            {
                this.olvChecks.Objects = new List<object>() { this.controller.rootGroup };
                expandToGroups(this.controller.rootGroup);
                this.olvChecks.CheckAll();
            }
            else
            {
                this.olvChecks.ClearObjects();
            }
            this.Initiate();
        }
        private void expandToGroups(CheckGroup group)
        {
            //expand to the level of the groups, but not to the level of the individual checks
            if (! group.checks.Any())
            {
                this.olvChecks.Expand(group);
            }
            foreach(var subGroup in group.subGroups)
            {
                this.expandToGroups(subGroup);
            }
        }

        private void Initiate()
        {
            // Default settings
            this.progressBar1.Minimum = 0;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = 100;

            // Clear lists
            this.clearLists();
            
            // Set focus to "Start Validation"
            this.btnDoValidation.Select();
        }

        private void clearLists()
        {
            // Clear the lists
            this.controller.validations.Clear();
        }


        public void InitProgressbar(int max)
        {
            // Initialize the progressbar
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = max;
            this.progressBar1.Value = 0;
        }

        public void IncrementProgressbar()
        {
            this.progressBar1.Increment(1);
        }
        public void refreshCheck(Check check)
        {
            this.olvChecks.RefreshObject(check);
            this.olvChecks.EnsureModelVisible(check);
        }

        private void btnDoValidation_Click(object sender, EventArgs e)
        {
            // Verify if any check is selected
            var listOfChecksForValidation = this.olvChecks.Objects.OfType<CheckGroup>().FirstOrDefault()?
                .GetAllChecks().Where(x => x.selected == true).ToList();
 
            if (listOfChecksForValidation == null || !listOfChecksForValidation.Any())
            {
                MessageBox.Show("No checks selected. Please select at least one check.");
            }
            else
            {
                //set cursor
                Cursor.Current = Cursors.WaitCursor;
                bool successful = true;
                //clear validations
                this.olvValidations.ClearObjects();

                // Validate alle checked checks
                successful = this.controller.ValidateChecks(this, listOfChecksForValidation, this.scopeElement, this.scopeDiagram);

                // Show validation results on screen
                this.olvValidations.Objects = this.controller.validations;

                //set cursor back
                Cursor.Current = Cursors.Default;
            }
        }

        private void ClearScopeFields()
        {
            this.txtElementName.Text = "";
            this.scopeElement = null;

            this.txtDiagramName.Text = "";
            this.scopeDiagram = null;
        }

        private void btnSelectElement_Click(object sender, EventArgs e)
        {
            this.ClearScopeFields();

            // Select one element using EA Package Browser  (EA must be connected to a project)
            try
            {
                this.scopeElement = this.controller.getUserSelectedScopeElement();
            }
            catch (Exception) { }

            if (this.scopeElement != null)
            {
                // Show element details on screen
                this.txtElementName.Text = this.scopeElement.name;
            }
            // (Re-)Initialize screen fields
            this.Initiate();
        }

        private void btnSelectDiagram_Click(object sender, EventArgs e)
        {
            this.ClearScopeFields();
            // Select the diagram that is selected in the EA Package Browser
            this.scopeDiagram = this.controller.getSelectedScopeDiagram();
            this.txtDiagramName.Text = this.scopeDiagram?.name;
            this.Initiate();
        }


        private void olvChecks_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            // Colour (only) the column Status depending on its value

            if (e.ColumnIndex == this.olvColCheckStatus.Index)
            {
                var checkItem = e.Model as CheckItem;
                if (checkItem != null)
                {
                    switch (checkItem.Status)
                    {
                        case CheckStatus.Passed:
                            e.SubItem.ForeColor = Color.Green;
                            break;
                        case CheckStatus.Failed:
                            e.SubItem.ForeColor = Color.Red;
                            break;
                        default:  // Not Validated, ...
                            e.SubItem.ForeColor = Color.Black;
                            break;
                    }
                }
            }
        }

        private void olvValidations_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // Get the ElementGuid of the selected row  (max 1 row)
            Validation validation = (Validation)this.olvValidations.SelectedObject;
            if (validation != null)
            {
                // Open the selected item in Enterprise Architect
                this.controller.OpenInEA(validation);
            }
        }

        private void olvChecks_CellToolTipShowing(object sender, BrightIdeasSoftware.ToolTipShowingEventArgs e)
        {
            // Show Rationale as tooltip for column Description
            if (e.ColumnIndex == this.olvColName.Index)
            {
                var check = e.Model as Check;
                if (check != null)
                {
                    e.Text = check.Rationale;
                }
            }
        }

        private void olvValidations_CellToolTipShowing(object sender, BrightIdeasSoftware.ToolTipShowingEventArgs e)
        {
            // Show Proposed Solution as tooltip on Item Name
            if (e.ColumnIndex == this.olvColItemName.Index)
            {
                Validation validation = (Validation)e.Model;
                Check check = this.controller.checks.FirstOrDefault(x => x.CheckId == validation.CheckId);
                if (check != null)
                {
                    e.Text = check.ProposedSolution;
                }
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }

        private void btnClearScope_Click(object sender, EventArgs e)
        {
            this.ClearScopeFields();
        }

        private void olvChecks_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //get selected check
            var selectedCheck = this.olvChecks.SelectedObject as Check;
            if (selectedCheck != null)
            {
                //get the first validationResult for this type of check
                var validation = this.olvValidations.Objects
                                    .OfType<Validation>()
                                    .LastOrDefault(x => x.CheckId == selectedCheck.CheckId);
                //TODO: figure out a fast way to get the first item shown
                //show the validation
                if (validation != null)
                {
                    this.olvValidations.EnsureModelVisible(validation);
                }
            }
        }

        private void olvChecks_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            this.olvChecks.RefreshObject(((OLVListItem)e.Item).RowObject);
        }
    }
}
