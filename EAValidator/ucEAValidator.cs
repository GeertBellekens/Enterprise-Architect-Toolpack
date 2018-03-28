using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.IO;

namespace EAValidator
{
    /// <summary>
    /// User Control that contains interface elements
    /// </summary>
    public partial class ucEAValidator : UserControl
    {
        private TSF_EA.Element EA_element { get; set; }
        private TSF_EA.Diagram EA_diagram { get; set; }
        private EAValidatorController controller { get; set; }

        public ucEAValidator()
        {
            InitializeComponent();  // needed for Windows Form

            txtDirectoryValidationChecks.ReadOnly = true;

            chkExcludeArchivePackages.Checked = true;

            progressBar1.Enabled = false;
            progressBar1.UseWaitCursor = true;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            string path = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Files\logo.gif";
            if (Utils.FileOrDirectoryExists(path))
                pictureBox1.ImageLocation = path;
            else
                pictureBox1.Hide();

            olvChecks.EmptyListMsg = "No checks found to validate.";
            olvChecks.CheckBoxes = true;
            olvChecks.CheckedAspectName = "Selected";
            olvChecks.Cursor = DefaultCursor;
            olvChecks.FullRowSelect = true;
            olvChecks.MultiSelect = true;
            olvChecks.UseCellFormatEvents = true;  // necessary to use colours in cells
            olvChecks.IncludeColumnHeadersInCopy = true;
            olvChecks.CellEditUseWholeCell = false;

            olvColCheckDescription.HeaderCheckBox = true;

            olvChecks.ShowGroups = false;
            //olvColCheckDescription.Groupable = false;
            //olvColCheckId.Groupable = false;
            //olvColCheckStatus.Groupable = false;
            //olvColCheckWarningType.Groupable = false;
            //olvColCheckNumberOfElementsFound.Groupable = false;
            //olvColCheckNumberOfValidationResults.Groupable = false;
            //olvColCheckGroup.Groupable = false;

            olvColCheckDescription.IsEditable = false;
            olvColCheckId.IsEditable = false;
            olvColCheckStatus.IsEditable = false;
            olvColCheckWarningType.IsEditable = false;
            olvColCheckNumberOfElementsFound.IsEditable = false;
            olvColCheckNumberOfValidationResults.IsEditable = false;
            olvColCheckGroup.IsEditable = false;

            olvColCheckNumberOfElementsFound.Sortable = false;
            olvColCheckNumberOfValidationResults.Sortable = false;

            txtElementName.ReadOnly = true;
            txtElementType.ReadOnly = true;
            txtDiagramName.ReadOnly = true;
            txtDiagramType.ReadOnly = true;

            olvValidations.EmptyListMsg = "";
            olvValidations.CheckBoxes = false;
            olvValidations.Cursor = DefaultCursor;
            olvValidations.FullRowSelect = true;
            olvValidations.MultiSelect = true;
            olvValidations.IncludeColumnHeadersInCopy = true;
            olvValidations.ShowItemCountOnGroups = true;
            olvValidations.ShowCommandMenuOnRightClick = true;
            olvValidations.UseFilterIndicator = true;
            olvValidations.UseFiltering = true;
            olvValidations.CellEditUseWholeCell = false;
        }

        public void setController(EAValidatorController controller)
        {
            this.controller = controller;
            Initiate();
        }

        private void Initiate()
        {
            // Default settings
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            // Default directory
            txtDirectoryValidationChecks.Text = this.controller.settings.ValidationChecks_Directory;

            // Clear lists
            clearLists();

            // Load files from directory as checks
            this.controller.loadChecksFromDirectory(this.controller.settings.ValidationChecks_Directory);

            // Show checks/validations in objectListViews     
            olvColCheckDescription.HeaderCheckState = CheckState.Checked;
            this.olvChecks.SetObjects(this.controller.checks);
            this.olvValidations.SetObjects(this.controller.validations);

            // Set focus to "Start Validation"
            this.btnDoValidation.Select();
        }

        private void clearLists()
        {
            // Clear the lists
            this.controller.checks.Clear();
            this.controller.validations.Clear();
        }

        public bool getExcludeArchivedPackagesState()
        {
            return chkExcludeArchivePackages.Checked;
        }

        public void InitProgressbar(int max)
        {
            // Initialize the progressbar
            progressBar1.Minimum = 0;
            progressBar1.Maximum = max;
            progressBar1.Value = 0;
        }

        public void IncrementProgressbar()
        {
            progressBar1.Increment(1);
        }

        private void btnDoValidation_Click(object sender, EventArgs e)
        {
            // Verify if any check is selected
            var listOfChecksForValidation = olvChecks.CheckedObjects.Cast<Check>().ToList();
            if (!listOfChecksForValidation.Any())
            {
                MessageBox.Show("No checks selected. Please select at least one check.");
            }
            else
            {
                var msg = "";
                // Confirm execution of validations
                if (listOfChecksForValidation.Count() == olvChecks.GetItemCount())
                    msg = "Validate all checks?";
                else
                {
                    msg = "Validate the selected checks?";
                    // Set status to default "Not Validated"
                    foreach (Check check in this.controller.checks)
                    {
                        check.SetStatus("Not Validated");
                        check.NumberOfElementsFound = "";
                        check.NumberOfValidationResults = "";
                    }
                }

                DialogResult result = MessageBox.Show(msg, "Warning!", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    bool successful = true;

                    // Validate alle checked checks
                    successful = this.controller.ValidateChecks(this, listOfChecksForValidation, EA_element, EA_diagram);

                    // Show validation results on screen
                    this.olvValidations.Objects = this.controller.validations;

                    // Update status of validated checks
                    this.olvChecks.RefreshObjects(this.controller.checks);

                    // Show user that validation is done.
                    string message;
                    if (successful)
                        message = "Validation finished succesfully.";
                    else
                        message = "Validation finished with errors.";
                    MessageBox.Show(message);
                }
            }
        }

        private void ClearScopeFields()
        {
            txtElementName.Text = "";
            txtElementType.Text = "";
            EA_element = null as TSF_EA.Element;

            txtDiagramName.Text = "";
            txtDiagramType.Text = "";
            EA_diagram = null as TSF_EA.Diagram;
        }

        private void btnSelectElement_Click(object sender, EventArgs e)
        {
            ClearScopeFields();

            // Select one element using EA Package Browser  (EA must be connected to a project)
            try
            {
                EA_element = this.controller.getUserSelectedElement();
            }
            catch (Exception) { }

            if (EA_element != null)
            {
                string filterType;
                if (EA_element is TSF_EA.Package)
                {
                    filterType = "Package";
                }
                else
                {
                    filterType = EA_element.stereotypeNames.FirstOrDefault();
                }

                // Show element details on screen
                txtElementName.Text = EA_element.name;
                txtElementType.Text = filterType;
            }
            // (Re-)Initialize screen fields
            Initiate();
        }

        private void btnSelectDiagram_Click(object sender, EventArgs e)
        {
            ClearScopeFields();

            // Select the diagram that is selected in the EA Package Browser
            EA_diagram = this.controller.getSelectedDiagram();
            if (EA_diagram != null)
            {
                string diagramtype = EA_diagram.GetType().ToString().Substring(EA_diagram.GetType().ToString().LastIndexOf(".") + 1);
                // Only keep diagram if it is a Use Case diagram
                if (diagramtype == "UseCaseDiagram")
                {
                    txtDiagramName.Text = EA_diagram.name;
                    txtDiagramType.Text = diagramtype;
                }
                else
                {
                    MessageBox.Show("Please select a Use Case-diagram in the Project Browser.");
                    EA_diagram = null as TSF_EA.Diagram;
                }
            }
            else
            {
                MessageBox.Show("Please select a Use Case-diagram in the Project Browser.");
            }

            Initiate();
        }

        private void btnSelectQueryDirectory_Click(object sender, EventArgs e)
        {
            // Change the setting to the selected directory
            this.controller.settings.ValidationChecks_Directory = Utils.selectDirectory(this.controller.settings.ValidationChecks_Directory);
            this.controller.settings.save();

            // Refresh fields on screen
            Initiate();
        }

        private void olvChecks_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            // Colour (only) the column Status depending on its value
            if (e.ColumnIndex == this.olvColCheckStatus.Index)
            {
                Check check = (Check)e.Model;
                switch (check.Status)
                {
                    case "Passed":
                        e.SubItem.ForeColor = Color.Green;
                        break;
                    case "Failed":
                        e.SubItem.ForeColor = Color.Red;
                        break;
                    default:  // Not Validated, ...
                        e.SubItem.ForeColor = Color.Black;
                        break;
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
            if (e.ColumnIndex == this.olvColCheckDescription.Index)
            {
                Check check = (Check)e.Model;
                e.Text = check.Rationale;
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
                    e.Text = check.ProposedSolution;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }

        private void btnClearScope_Click(object sender, EventArgs e)
        {
            ClearScopeFields();
        }
    }
}
