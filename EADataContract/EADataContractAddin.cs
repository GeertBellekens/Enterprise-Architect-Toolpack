using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class EADataContractAddin : EAAddinFramework.EAAddinBase
    {
        const string menuName = "-&EA Data Contract";
        const string menuImport = "&Import Data Contract";
        const string menuExport = "&Export Data Contract";
        const string menuAbout = "&About EA Data Contract";
        const string outputName = "EA Data contract";
        public EADataContractAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuImport,
                                menuExport,
                                menuAbout
                              };
        }
        public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuImport:
                    this.import();
                    break;
                case menuExport:
                    this.export();
                    break;
                case menuAbout:
                    //TODO: new AboutWindow().ShowDialog(this.model.mainEAWindow);
                    break;
            }
        }

        private void import()
        {

            var contract = ODCSDataContract.getUserSelectedContract();
            if (contract == null) return;
            var selectedPackage = this.model.selectedTreePackage;
            EAOutputLogger.clearLog(this.model, outputName);
            EAOutputLogger.log(this.model, outputName
                           , $"Starting import of datacontract {contract.name} in  {selectedPackage.name} "
                           , 0
                          , LogTypeEnum.log);
            this.model.wrappedModel.EnableUIUpdates = false;
            //this.model.wrappedModel.BatchAppend = true;
            contract.importToModel(selectedPackage as Element, 0);
            this.model.wrappedModel.EnableUIUpdates = true;
            //reload package to see changes
            selectedPackage.refresh();
            //this.model.wrappedModel.BatchAppend = false;
            EAOutputLogger.log(this.model, outputName
                           , $"Finished import of datacontract {contract.name} in  {selectedPackage.name} "
                           , 0
                          , LogTypeEnum.log);

        }

        private void export()
        {
            throw new NotImplementedException();
        }
    }
}
