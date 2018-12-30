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
        const string menuSettings = "&Settings";
        const string menuAbout = "&About EA Validator";

        const string appTitle = "EA Validator";
        const string guiFQN = "EAValidator.ucEAValidator";
        public TSF_EA.Model Model { get; private set; } = null;
        private bool fullyLoaded = false;

        private ucEAValidator _ucEAValidator;
        private EAValidatorSettings settings { get; set; } = new EAValidatorSettings();

        public EAValidatorAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuOpen,
                                menuSettings,
                                menuAbout
                              };
        }

        private ucEAValidator ucEAValidator
        {
            get
            {
                if (this._ucEAValidator == null && this.Model != null)
                {
                    this._ucEAValidator = this.Model.addTab(appTitle, guiFQN) as ucEAValidator;
                    this._ucEAValidator.HandleDestroyed += this.handleHandleDestroyed;
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
            this.Model = new TSF_EA.Model(repository);
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
                case menuSettings:
                    new SettingsForm(this.settings).ShowDialog(this.Model.mainEAWindow);
                    break;
                case menuAbout:
                    new AboutWindow().ShowDialog(this.Model.mainEAWindow);
                    break;
            }
        }

        private void openEAValidator()
        {
            // Open the EA Validator (user control)
            if (this.Model == null) { return; }
            var controller = new EAValidatorController(this.Model, this.settings);
            this.ucEAValidator.setController(controller);

        }
    }
}
