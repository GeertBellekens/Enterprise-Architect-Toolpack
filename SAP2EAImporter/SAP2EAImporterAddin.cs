using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EA;
using EAAddinFramework;
using UML = TSF.UmlToolingFramework.UML;
//SAP2EAImporter.SAP2EAImporterAddin
namespace SAP2EAImporter
{
    public class SAP2EAImporterAddin : EAAddinBase

    {
        // define menu constants
        const string menuName = "-&SAP2EA Importer";
        const string menuImport = "&Import";


        /// <summary>
        /// constructor where we set the menuheader and menuOptions
        /// </summary>
        public SAP2EAImporterAddin() : base()
        {
            this.menuHeader = menuName;
            this.menuOptions = new string[] { menuImport };
        }

        public override void EA_MenuClick(global::EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuImport:
                    // Check where the user clicked.
                    UML.Classes.Kernel.Package package = null;
                    switch (MenuLocation)
                    {
                        case locationMainMenu: // Ribbon 
                            package = this.model.getUserSelectedPackage();
                            break;
                        case locationTreeview: // Project browser
                            package = this.model.selectedTreePackage;
                            break;
                    }

                    this.import(package);
                    break;
            }
        }

        private void import(UML.Classes.Kernel.Package package)
        {
            new SAP2EAXmlImporter().import(package);
        }

        public override object EA_GetMenuItems(Repository Repository, string MenuLocation, string MenuName)
        {
            switch (MenuLocation)
            {
                case locationDiagram:
                    return null;
                default:
                    return base.EA_GetMenuItems(Repository, MenuLocation, MenuName);
            }
        }
    }
}
