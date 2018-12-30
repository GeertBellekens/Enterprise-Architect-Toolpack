using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAValidator
{
    public partial class SettingsForm : Form
    {
        EAValidatorSettings settings { get; set; }
        public SettingsForm(EAValidatorSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.loadSettings();
        }
        private void loadSettings()
        {
            this.txtDirectoryValidationChecks.Text = this.settings.ValidationChecks_Directory;
            this.excludeArchivedPackagesCheckbox.Checked = this.settings.excludeArchivedPackages;
            this.archivedPackagesQueryTextBox.Text = this.settings.QueryExcludeArchivedPackages;
            this.enableDisable();
        }
        private void unloadSettings()
        {
            this.settings.ValidationChecks_Directory = this.txtDirectoryValidationChecks.Text;
            this.settings.excludeArchivedPackages = this.excludeArchivedPackagesCheckbox.Checked;
            this.settings.QueryExcludeArchivedPackages = this.archivedPackagesQueryTextBox.Text ;
        }
        private void save()
        {
            this.unloadSettings();
            this.settings.save();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            this.save();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSelectQueryDirectory_Click(object sender, EventArgs e)
        {
            // Change the setting to the selected directory
            this.txtDirectoryValidationChecks.Text = Utils.selectDirectory(this.settings.ValidationChecks_Directory);
        }

        private void excludeArchivedPackagesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }

        private void enableDisable()
        {
            this.archivedPackagesQueryTextBox.Enabled = this.excludeArchivedPackagesCheckbox.Checked;
        }
    }
}
