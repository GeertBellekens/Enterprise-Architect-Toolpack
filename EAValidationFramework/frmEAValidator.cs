using System;
using System.Windows.Forms;
using EAValidator;

namespace EAValidatorApp
{
    public partial class frmEAValidator : Form
    {
        public frmEAValidator(EAValidatorController controller)
        {
            InitializeComponent();
            this.ucEAValidator.setController(controller);
        }
    }
}
