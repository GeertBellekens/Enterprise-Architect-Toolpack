using System;
using System.Configuration;
using System.Reflection;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{

    public class GlossaryManagerSettings
    : EAAddinFramework.Utilities.AddinSettings
    {
        private UML.Extended.UMLModel model { get; set; }
        public GlossaryManagerSettings(UML.Extended.UMLModel model)
        {
            this.model = model;
        }
        protected override string configSubPath
        {
            get { return @"\Bellekens\EDD\"; }
        }

        protected override string defaultConfigFilePath
        {
            get { return Assembly.GetExecutingAssembly().Location; }
        }

        public string outputName
        {
            get { return this.getValue("outputName"); }
            set { this.setValue("outputName", value); }
        }
        private string businessItemsPackageGUID
        {
            get { return this.getValue("businessItemsPackageGUID"); }
            set { this.setValue("businessItemsPackageGUID", value); }
        }
        private UML.Classes.Kernel.Package _businessItemsPackage;
        public UML.Classes.Kernel.Package businessItemsPackage
        {
            get
            {
                if (_businessItemsPackage == null)
                {
                    _businessItemsPackage = this.model.getItemFromGUID(this.businessItemsPackageGUID) as UML.Classes.Kernel.Package;
                }
                return _businessItemsPackage;
            }
            set
            {
                _businessItemsPackage = value;
                this.businessItemsPackageGUID = value != null ? value.uniqueID : string.Empty;
            }
        }
        private string dataItemsPackageGUID
        {
            get { return this.getValue("dataItemsPackageGUID"); }
            set { this.setValue("dataItemsPackageGUID", value); }
        }
        private UML.Classes.Kernel.Package _dataItemsPackage;
        public UML.Classes.Kernel.Package dataItemsPackage
        {
            get
            {
                if (_dataItemsPackage == null)
                {
                    _dataItemsPackage = this.model.getItemFromGUID(this.dataItemsPackageGUID) as UML.Classes.Kernel.Package;
                }
                return _dataItemsPackage;
            }
            set
            {
                _dataItemsPackage = value;
                this.dataItemsPackageGUID = value != null ? value.uniqueID : string.Empty;
            }
        }

    }
}
