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

namespace EAScriptAddin
{
    public partial class RefdataSplitterControl : UserControl
    {
        private EARefDataSplitter.RefDataSplitterForm refDataSplitterForm {get;set;}
        public RefdataSplitterControl()
        {
            InitializeComponent();
            this.refDataSplitterForm = new RefDataSplitterForm();
            this.refDataSplitterForm.TopLevel = false;
            this.refDataSplitterForm.FormBorderStyle = FormBorderStyle.None;
            this.Controls.Add(this.refDataSplitterForm);
            this.refDataSplitterForm.Dock = DockStyle.Fill;
            this.refDataSplitterForm.Show();
        }
    }
}
