using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EARefDataSplitter;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAScriptAddin
{
    public partial class RefdataSplitterControl : UserControl
    {
        private EARefDataSplitter.RefDataSplitterForm refDataSplitterForm { get; set; }

        private EAScriptAddinSettings settings { get; set; }


        public RefdataSplitterControl()
        {
            InitializeComponent();
            this.refDataSplitterForm = new RefDataSplitterForm();
            this.refDataSplitterForm.TopLevel = false;
            this.refDataSplitterForm.FormBorderStyle = FormBorderStyle.None;
            this.Controls.Add(this.refDataSplitterForm);
            this.refDataSplitterForm.Dock = DockStyle.Fill;
            this.refDataSplitterForm.transferToButtonClick += new System.EventHandler(this.transferToButtonClick);
            this.refDataSplitterForm.Show();
            
        }

        private async void transferToButtonClick(object sender, EventArgs e)
        {
            var environmentKey = this.refDataSplitterForm.selectedEnvironment;
            if (this.settings.environments.ContainsKey(environmentKey))
            {
                var myTask = Task.Run(() =>
                {
                    var tempfile = System.IO.Path.GetTempFileName();
                    this.refDataSplitterForm.exportToFile(tempfile);
                    if (EAAddinFramework.EASpecific.Script.importScripts(tempfile, this.settings.environments[environmentKey]))
                    {
                        MessageBox.Show($"Import scripts to {environmentKey} successful!");
                    }
                    else
                    {
                        MessageBox.Show($"Import scripts to {environmentKey} failed!", "Import Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                });
                await myTask;
                this.refDataSplitterForm.setTransferFinished();
            }
        }

        public void loadTempfile(string fileName)
        {
            this.refDataSplitterForm.loadTempfile(fileName);
        }
        public void Init(EAScriptAddinSettings settings)
        {
            this.settings = settings;
            this.refDataSplitterForm.setEnvironments(this.settings.environments);
        }
    }
}
