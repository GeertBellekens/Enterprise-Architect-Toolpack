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
            //test
            foreach (var odcsObject in contract.schema?.objects)
            {
                EAOutputLogger.log(this.model, outputName
                       , $"Found object: {odcsObject.name}"
                       , 0
                      , LogTypeEnum.log);
                foreach (var property in odcsObject.properties)
                {
                    EAOutputLogger.log(this.model, outputName
                           , $"Found property: {property.name} of type {property.logicalType?.type} with options {property.logicalType?.options} "
                           , 0
                          , LogTypeEnum.log);
                }
            }
            var selectedPackage = this.model.selectedTreePackage as Element;
            EAOutputLogger.log(this.model, outputName
                           , $"Starting import of datacontract {contract.name} in  {selectedPackage.name} "
                           , 0
                          , LogTypeEnum.log);
            contract.importToModel(selectedPackage);
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
