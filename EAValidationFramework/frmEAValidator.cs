using System;
using System.Windows.Forms;
using EAValidator;

namespace EAValidatorApp
{
    public partial class frmEAValidator : Form
    {
        private EAValidatorController controller { get; set; }
        public frmEAValidator(EAValidatorController controller)
        {
            InitializeComponent();
            this.controller = controller;
            this.ucEAValidator.setController(this.controller);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm(this.controller.settings).ShowDialog(this);
            //re-initialize
            this.ucEAValidator.setController(this.controller);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutWindow().ShowDialog(this);
        }
    }
}
