using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using UML = TSF.UmlToolingFramework.UML;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace GlossaryManager
{

    public class GlossaryManagerSettings
    : EAAddinFramework.Utilities.AddinSettings
    {
        private UML.Extended.UMLModel model { get; set; }
        private TSF_EA.Model EAModel => (TSF_EA.Model)this.model;
        public GlossaryManagerSettings(UML.Extended.UMLModel model)
        {
            this.model = model;
        }
        protected override string addinName => "EDD";
        protected override string configSubPath
        {
            get { return @"\Bellekens\EDD\"; }
        }

        protected override string defaultConfigAssemblyFilePath
        {
            get { return Assembly.GetExecutingAssembly().Location; }
        }
        protected override void clearCache()
        {
            this.businessItemsPackage = null;
            this.dataItemsPackage = null;
        }
        public string outputName
        {
            get { return this.getValue("outputName"); }
            set { this.setValue("outputName", value); }
        }
        public Dictionary<string, string> businessItemsPackages
        {
            get
            {
                return getDictionaryValue("businessItemsPackages");
            }
            set
            {
                this.setDictionaryValue("businessItemsPackages", value);
            }
        }
        private UML.Classes.Kernel.Package _businessItemsPackage;
        public UML.Classes.Kernel.Package businessItemsPackage
        {
            get
            {
                string businessItemsPackageGUID;
                if (_businessItemsPackage == null
                    && this.businessItemsPackages.TryGetValue(this.EAModel.projectGUID, out businessItemsPackageGUID))
                {
                    _businessItemsPackage = this.model.getItemFromGUID(businessItemsPackageGUID) as UML.Classes.Kernel.Package;
                }
                return _businessItemsPackage;
            }
            set
            {
                _businessItemsPackage = value;
                Dictionary<string, string> businessItemPackageDictionary = this.businessItemsPackages;
                businessItemPackageDictionary[this.EAModel.projectGUID] = value?.uniqueID;
                //set the dictionary so it can be saved
                this.businessItemsPackages = businessItemPackageDictionary;
            }
        }

        public Dictionary<string, string> dataItemsPackages
        {
            get
            {
                return getDictionaryValue("dataItemsPackages");
            }
            set
            {
                this.setDictionaryValue("dataItemsPackages", value);
            }
        }
        private UML.Classes.Kernel.Package _dataItemsPackage;
        public UML.Classes.Kernel.Package dataItemsPackage
        {
            get
            {
                string dataItemsPackageGUID;
                if (_dataItemsPackage == null
                    && this.dataItemsPackages.TryGetValue(this.EAModel.projectGUID, out dataItemsPackageGUID))
                {
                    _dataItemsPackage = this.model.getItemFromGUID(dataItemsPackageGUID) as UML.Classes.Kernel.Package;
                }
                return _dataItemsPackage;
            }
            set
            {
                _dataItemsPackage = value;
                Dictionary<string, string> dataItemPackageDictionary = this.dataItemsPackages;
                dataItemPackageDictionary[this.EAModel.projectGUID] = value?.uniqueID;
                //set the dictionary so it can be saved
                this.dataItemsPackages = dataItemPackageDictionary;
            }
        }
        public bool showWindowAtStartup
        {
            get
            {
                return this.getBooleanValue("showWindowAtStartup");
            }
            set
            {
                this.setBooleanValue("showWindowAtStartup", value);
            }
        }

    }
}
