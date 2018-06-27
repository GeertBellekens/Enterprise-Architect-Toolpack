using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GlossaryManager.GUI
{
    public partial class EDD_SettingsForm : Form
    {
        GlossaryManagerSettings settings { get; set; }
        public EDD_SettingsForm(GlossaryManagerSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            this.loadData();
        }
        private void loadData()
        {
            this.businessItemsPackageTextBox.Text = this.settings.businessItemsPackage != null 
                                                    ? this.settings.businessItemsPackage.name 
                                                    : string.Empty;
            this.dataItemsPackageTextBox.Text = this.settings.dataItemsPackage != null
                                                    ? this.settings.dataItemsPackage.name
                                                    : string.Empty;
            this.showWindowCheckbox.Checked = this.settings.showWindowAtStartup;
        }
        private void saveData()
        {
            this.settings.showWindowAtStartup = this.showWindowCheckbox.Checked;
            this.settings.save();
        }

        public event EventHandler browseBusinessItemsPackage;
        private void browseBusinessItemsPackageButton_Click(object sender, EventArgs e)
        {
            if (this.browseBusinessItemsPackage != null) browseBusinessItemsPackage(sender, e);
            this.loadData();
        }

        public event EventHandler browseDataItemsPackage;
        private void browseDataItemsPackageButton_Click(object sender, EventArgs e)
        {
            if (this.browseDataItemsPackage != null) browseDataItemsPackage(sender, e);
            this.loadData();
        }

        private void applyButton_Click(object sender, EventArgs e)
        {
            this.saveData();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.saveData();
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            //cancel changes and get the data again
            settings.refresh();
            this.loadData();
        }
    }
}
