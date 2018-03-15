using System;
using System.Windows.Forms;

namespace EAValidationFramework
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
