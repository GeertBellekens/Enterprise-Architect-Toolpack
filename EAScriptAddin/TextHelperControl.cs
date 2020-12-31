using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TextHelper;

namespace EAScriptAddin
{
    public partial class TextHelperControl : UserControl
    {
        private MainTextHelperForm mainTextHelperForm  { get; set; }
        public TextHelperControl()
        {
            InitializeComponent();
            this.mainTextHelperForm = new MainTextHelperForm();
            this.mainTextHelperForm.TopLevel = false;
            this.mainTextHelperForm.FormBorderStyle = FormBorderStyle.None;
            this.mainPanel.Controls.Add(mainTextHelperForm);
            this.mainTextHelperForm.Dock = DockStyle.Fill;
            this.mainTextHelperForm.Show();
        }
    }
}
