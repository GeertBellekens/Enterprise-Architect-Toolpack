using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public partial class SettingsForm : Form
    {
        EAValidatorSettings settings { get; set; }
        public SettingsForm(EAValidatorSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.allowedRepositoryTypesListBox.DataSource = Enum.GetValues(typeof(RepositoryType));
            this.loadSettings();
        }
        private void loadSettings()
        {
            this.txtDirectoryValidationChecks.Text = this.settings.ValidationChecks_Directory;
            this.excludeArchivedPackagesCheckbox.Checked = this.settings.excludeArchivedPackages;
            this.archivedPackagesQueryTextBox.Text = this.settings.QueryExcludeArchivedPackages;
            //set allowed RepositoryTypes
            foreach (var repositoryType in this.settings.AllowedRepositoryTypes)
            {
                for (int i = 0; i < allowedRepositoryTypesListBox.Items.Count; i++)
                {
                    if (repositoryType == (RepositoryType)allowedRepositoryTypesListBox.Items[i])
                    {
                        allowedRepositoryTypesListBox.SetItemChecked(i, true);
                    }
                }
            }
            this.enableDisable();
        }
        private void unloadSettings()
        {
            this.settings.ValidationChecks_Directory = this.txtDirectoryValidationChecks.Text;
            this.settings.excludeArchivedPackages = this.excludeArchivedPackagesCheckbox.Checked;
            this.settings.QueryExcludeArchivedPackages = this.archivedPackagesQueryTextBox.Text ;
            this.settings.AllowedRepositoryTypes = this.allowedRepositoryTypesListBox.CheckedItems.Cast<RepositoryType>().ToList();
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
