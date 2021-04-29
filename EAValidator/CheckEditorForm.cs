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
            this.idTextBox.DataBindings.Add("Text", this.check, "CheckId"); ;
            this.typeComboBox.DataBindings.Add("Text", this.check, "WarningType");
            this.descriptionTextBox.DataBindings.Add("Text", this.check, "CheckDescription");
            this.rationaleTextBox.DataBindings.Add("Text", this.check, "Rationale");
            this.proposedSolutionTextBox.DataBindings.Add("Text", this.check, "ProposedSolution");
            this.helpUrlTextBox.DataBindings.Add("Text", this.check, "helpUrlText");
            this.elementsToCheckTextBox.DataBindings.Add("Text", this.check, "QueryToFindElements"); 
            this.invalidElementsTextBox.DataBindings.Add("Text", this.check, "QueryToCheckFoundElements"); 
            this.packageFilterTextBox.DataBindings.Add("Text", this.check, "packageFilter");
            this.changeFilterTextBox.DataBindings.Add("Text", this.check, "changeFilter");
            this.releaseFilterTextBox.DataBindings.Add("Text", this.check, "releaseFilter");
            this.diagramFilterTextBox.DataBindings.Add("Text", this.check, "diagramFilter");
            this.languageComboBox.DataBindings.Add("Text", this.check, "resolveCodeLanguage");
            this.resolveCodeTextBox.DataBindings.Add("Text", this.check, "resolveCode");
        }

        
        private void normalizeLineEndings(TextBox textBox)
        {
            //replace LF with CR+LF
            textBox.Text = textBox.Text.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n")
                .Replace("\r\n\t\t\t\t", "\r\n").Replace("\r\n\t\t\t","\r\n"); //remove tabs at the start of a new line as well.
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

        private void packageFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }

        private void changeFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }

        private void releaseFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }

        private void diagramFilterTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }

        private void resolveCodeTextBox_TextChanged(object sender, EventArgs e)
        {
            normalizeLineEndings((TextBox)sender);
        }
    }
}
