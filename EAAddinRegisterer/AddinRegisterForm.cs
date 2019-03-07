using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAAddinRegisterer
{
    public partial class AddinRegisterForm : Form
    {
        public AddinRegisterForm()
        {
            InitializeComponent();
        }

        private void browseAddinFolder_Click(object sender, EventArgs e)
        {
            //let the user select a file
            OpenFileDialog browseAddinFileDialog = new OpenFileDialog();
            browseAddinFileDialog.Filter = "Addin files|*.dll";
            browseAddinFileDialog.FilterIndex = 1;
            browseAddinFileDialog.Multiselect = false;
            var dialogResult = browseAddinFileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                //if the user selected the file then put the filename in the abbreviationsfileTextBox
                this.addinFileTextBox.Text = browseAddinFileDialog.FileName;
                //move cursor to end to make sure the end of the path is visible
                this.addinFileTextBox.Select(this.addinFileTextBox.Text.Length, 0);
            }
        }

        private void enableDisable()
        {
            this.registerButton.Enabled = !string.IsNullOrEmpty(addinFileTextBox.Text)
                                                && File.Exists(addinFileTextBox.Text);
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            var fileName = this.addinFileTextBox.Text;
            var addinName = new FileInfo(fileName)?.Name;
            //create the addin from the filename
            var addin = new EAAddinFramework.EASpecific.EAAddin(fileName, addinName);
            MessageBox.Show(this,"Finished");
        }

        private void addinFileTextBox_TextChanged(object sender, EventArgs e)
        {
            this.enableDisable();
        }
    }
}
