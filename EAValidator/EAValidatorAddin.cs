using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using EAAddinFramework;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public class EAValidatorAddin : EAAddinBase
    {
        // menu constants
        const string menuName = "-&EA Validator";
        const string menuOpen = "&Open EA Validator...";

        const string appTitle = "EA Validator";
        const string appFQN = "EAValidator.ucEAValidator";
        const string guiFQN = "EAValidator.ucEAValidator";

        private TSF_EA.Model model = null;
        public TSF_EA.Model Model { get { return this.model; } }
        private bool fullyLoaded = false;

        private ucEAValidator _ucEAValidator;

        public EAValidatorAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuOpen
                              };
        }

        private ucEAValidator ucEAValidator
        {
            get
            {
                if (this._ucEAValidator == null && this.model != null)
                {
                    this._ucEAValidator = this.model.addTab(appTitle, guiFQN) as ucEAValidator;
                    this._ucEAValidator.HandleDestroyed += this.handleHandleDestroyed;
                    //TODO: add additional events
                }
                return this._ucEAValidator;
            }
        }

        private void handleHandleDestroyed(object sender, EventArgs e)
        {
            // Handle must be present for clean destroy
            this._ucEAValidator = null;
        }

        public override void EA_FileOpen(EA.Repository repository)
        {
            // initialize the model
            this.model = new TSF_EA.Model(repository);
            // indicate that we are now fully loaded
            this.fullyLoaded = true;
        }

        public override void EA_MenuClick(EA.Repository repository, string location,
                                          string menuName, string itemName)
        {
            // Selection of a menu-item of the Add-in
            switch (itemName)
            {
                case menuOpen:
                    this.openEAValidator();
                    break;
            }
        }

        private void openEAValidator()
        {
            // Open the EA Validator (user control)
            if (this.model == null) { return; }
            var controller = new EAValidatorController(this.model);
            this.ucEAValidator.setController(controller);

        }
    }
}
