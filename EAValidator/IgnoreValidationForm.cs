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
    public partial class IgnoreValidationForm : Form
    {
        EAValidatorController controller { get; set; }
        Validation validation { get; set; }
        public IgnoreValidationForm(EAValidatorController controller, Validation validation)
        {
            InitializeComponent();
            this.controller = controller;
            this.validation = validation;
            this.checkTextBox.Text = validation.CheckDescription;
            this.itemTextBox.Text = validation.ItemName;
        }


        private void reasonTextbox_TextChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
        private void enableDisable()
        {
            this.okButton.Enabled = this.reasonTextBox.Text.Length > 0;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.controller.ignoreValidation(this.validation, this.reasonTextBox.Text);
            this.Close();
        }
    }
}
