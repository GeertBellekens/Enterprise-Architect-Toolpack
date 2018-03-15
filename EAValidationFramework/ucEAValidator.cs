using System;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.IO;

namespace EAValidationFramework
{
    /// <summary>
    /// User Control that contains interface elements
    /// </summary>
    public partial class ucEAValidator : UserControl
    {
        private TSF_EA.Element EA_element { get; set; }
        private EAValidatorController controller { get; set; }

        public ucEAValidator()
        {
            InitializeComponent();  // needed for Windows Form
        }        

        public void setController(EAValidatorController controller)
        {
            this.controller = controller;
        }
            
        private void ucEAValidator_Load(object sender, EventArgs e)
        {
            // Load of user control ucEAValidator
            chkExcludeArchivePackages.Checked = true;
            txtDirectoryValidationChecks.Text = this.controller.settings.ValidationChecks_Directory;

            pictureBox1.ImageLocation = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName + @"\Files\logo.gif";
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            olvChecks.EmptyListMsg = "No checks found to validate.";
            olvChecks.CheckBoxes = true;
            olvChecks.CheckedAspectName = "Selected";
            olvChecks.Cursor = DefaultCursor;
            olvChecks.FullRowSelect = true;
            olvChecks.MultiSelect = false;
            olvChecks.UseCellFormatEvents = true;  // necessary to use colours in cells
            olvChecks.IncludeColumnHeadersInCopy = true;

            olvValidations.EmptyListMsg = "";
            olvValidations.CheckBoxes = false;
            olvValidations.Cursor = DefaultCursor;
            olvValidations.FullRowSelect = true;
            olvValidations.MultiSelect = true;
            olvValidations.IncludeColumnHeadersInCopy = true;
            olvValidations.ShowItemCountOnGroups = true;

            Initiate();
        }

        private void Initiate()
        {
            // Default settings
            progressBar1.Minimum = 0;
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            // Clear lists
            clearLists();

            // Load files from directory as checks
            this.controller.loadChecksFromDirectory(this.controller.settings.ValidationChecks_Directory);

            // Get subdirectories for main directory
            if(Utils.FileOrDirectoryExists(this.controller.settings.ValidationChecks_Directory))
            { 
                string[] subdirectories = Utils.getSubdirectoriesForDirectory(this.controller.settings.ValidationChecks_Directory);

                // Load files from subdirectories as checks
                foreach (string subdir in subdirectories)
                    this.controller.loadChecksFromDirectory(subdir);
            }

            // Show checks/validations in objectListViews     
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
                    successful = this.controller.ValidateChecks(this, listOfChecksForValidation, EA_element);

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

        private void btnSelectElement_Click(object sender, EventArgs e)
        {
            // Default
            txtElementName.Text = "";
            txtElementType.Text = "";

            // Select one element using EA Package Browser  (EA must be connected to a project)
            try
            { 
                EA_element = this.controller.getUserSelectedElement();
            }
            catch(Exception){}

            if (EA_element != null)
            {
                // Show element details on screen
                txtElementName.Text = EA_element.name;

                string filterType;
                if (EA_element is TSF_EA.Package)
                    filterType = "Package";
                else
                    filterType = EA_element.stereotypeNames.FirstOrDefault();
                txtElementType.Text = filterType;

                // Clear lists 
                Initiate();
            }

            // Set focus to "Start Validation"
            this.btnDoValidation.Select();
        }

        private void btnSelectQueryDirectory_Click(object sender, EventArgs e)
        {
            // Change the setting to the selected directory
            this.controller.settings.ValidationChecks_Directory = Utils.selectDirectory(this.controller.settings.ValidationChecks_Directory);

            // Refresh fields on screen
            Initiate();
        }

        private void olvChecks_FormatCell(object sender, BrightIdeasSoftware.FormatCellEventArgs e)
        {
            // Colour (only) the column Status depending on its value
            if (e.ColumnIndex == this.olvColCheckStatus.Index)
            {
                Check check = (Check)e.Model;
                switch(check.Status)
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
            Validation validation = (Validation) this.olvValidations.SelectedObject;

            // Open the selected item in Enterprise Architect
            this.controller.OpenInEA(validation);
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
                if(check != null)
                    e.Text = check.ProposedSolution;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }
    }
}
