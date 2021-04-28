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
    public partial class CheckEditorForm : Form
    {
        private Check check { get; set; }
        public CheckEditorForm(Check check)
        {
            InitializeComponent();
            this.check = check;
            this.setDataBindings();
        }
        private void setDataBindings()
        {
            this.idTextBox.DataBindings.Add("Text", this.check, "CheckId", false, DataSourceUpdateMode.OnPropertyChanged);
            this.typeComboBox.DataBindings.Add("Text", this.check, "WarningType", false, DataSourceUpdateMode.OnPropertyChanged);
            this.descriptionTextBox.DataBindings.Add("Text", this.check, "CheckDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            this.rationaleTextBox.DataBindings.Add("Text", this.check, "Rationale", false, DataSourceUpdateMode.OnPropertyChanged);
            this.proposedSolutionTextBox.DataBindings.Add("Text", this.check, "ProposedSolution", false, DataSourceUpdateMode.OnPropertyChanged);
            this.helpUrlTextBox.DataBindings.Add("Text", this.check, "helpUrl", false, DataSourceUpdateMode.OnPropertyChanged);
            this.elementsToCheckTextBox.DataBindings.Add("Text", this.check, "QueryToFindElements", false, DataSourceUpdateMode.OnPropertyChanged);
            this.invalidElementsTextBox.DataBindings.Add("Text", this.check, "QueryToCheckFoundElements", false, DataSourceUpdateMode.OnPropertyChanged);
            this.packageFilterTextBox.DataBindings.Add("Text", this.check, "packageFilter", false, DataSourceUpdateMode.OnPropertyChanged);
            this.changeFilterTextBox.DataBindings.Add("Text", this.check, "changeFilter", false, DataSourceUpdateMode.OnPropertyChanged);
            this.releaseFilterTextBox.DataBindings.Add("Text", this.check, "releaseFilter", false, DataSourceUpdateMode.OnPropertyChanged);
            this.diagramFilterTextBox.DataBindings.Add("Text", this.check, "diagramFilter", false, DataSourceUpdateMode.OnPropertyChanged);
            this.resolveCodeTextBox.DataBindings.Add("Text", this.check.r, "diagramFilter", false, DataSourceUpdateMode.OnPropertyChanged);
        }

        
        private void normalizeLineEndings(TextBox textBox)
        {
            //replace LF with CR+LF
            textBox.Text = textBox.Text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
        }
        private void elementsToCheckTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }
        private void invalidElementsTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.check.save();
            this.Close();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.check.reload();
            this.Close();
        }
    }
}
