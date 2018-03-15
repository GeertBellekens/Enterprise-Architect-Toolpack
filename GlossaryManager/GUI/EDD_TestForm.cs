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
    public partial class EDD_TestForm : Form
    {
        public EDD_TestForm()
        {
            InitializeComponent();
        }
        public EDD_MainControl mainControl
        {
            get
            {
                return this.edD_MainControl1;
            }
        }
    }
}
