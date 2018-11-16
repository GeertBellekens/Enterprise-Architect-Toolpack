using EAMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAMappingApp
{
    public partial class MappingForm : Form
    {
        public MappingForm()
        {
            InitializeComponent();
        }
        public MappingControlGUI mappingControlGUI => this._mappingControlGUI;
    }
}
