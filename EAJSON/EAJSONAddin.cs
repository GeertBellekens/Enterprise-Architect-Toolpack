using EA;
using EAAddinFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace EAJSON
{
    public class EAJSONAddin : EAAddinBase
    {
        // menu constants
        const string menuName = "-&EA JSON";
        const string menuGenerate = "&Generate JSON Schema";
        const string menuTransform = "&Transform to JSON profile";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About EA JSON";
        public TSF_EA.Model model { get; private set; } = null;
        private bool fullyLoaded = false;


        public EAJSONAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuGenerate,
                                menuTransform,
                                menuSettings,
                                menuAbout
                              };
        }
        public override void EA_FileOpen(EA.Repository repository)
        {
            // initialize the model
            this.model = new TSF_EA.Model(repository);
            // indicate that we are now fully loaded
            this.fullyLoaded = true;
        }
        public override void EA_GetMenuState(Repository Repository, string MenuLocation, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            switch (ItemName)
            {
                //only allow generate on an element with steroetype «JSONSchema»
                case menuGenerate:
                    var selectedElement = this.model.selectedElement;
                    IsEnabled = selectedElement != null
                        &&
                        (selectedElement.stereotypes.Any(x => x.name.Equals("JSONSchema", StringComparison.InvariantCultureIgnoreCase))
                        ||
                        selectedElement is UML.Classes.Kernel.Package);
                    break;
                case menuTransform:
                    var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
                    IsEnabled = selectedPackage != null;
                    break;
            }

        }
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch(ItemName)
            {
                case menuGenerate:
                    this.generateJSONSchema();
                    break;
                case menuTransform:
                    this.transform();
                    break;
                case menuSettings:
                    //TODO
                    break;
                case menuAbout:
                    //TODO
                    break;
            }
        }
        /// <summary>
        /// transform the selected package to the JSON profile
        /// </summary>
        private void transform()
        {
            //let the user select a class to be the root class
            MessageBox.Show("Please select the root element");
            var rootObject = this.model.getUserSelectedElement(new List<string> { "Class" }) as UML.Classes.Kernel.Class;
            var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
            EAJSONSchema.transformPackage(selectedPackage, rootObject);
        }
        /// <summary>
        /// Generate a new JSON Schema from the selected File
        /// </summary>
        private void generateJSONSchema()
        {
            var selectedPackage = this.model.selectedElement as TSF_EA.Package;
            if (selectedPackage != null)
            {
                //generate for package
                generateJSONSchemas(selectedPackage);
            }
            else
            {
                var selectedElement = this.model.selectedElement as TSF_EA.ElementWrapper;
                if (selectedElement != null)
                {
                    this.generateJSONSchema(selectedElement);
                }
            }
        } 
        private void generateJSONSchemas(TSF_EA.Package package)
        {
            //get the «JSONSchema» elements in this package recursively
            var sqlGetJSONSchemas = "select o.Object_ID from t_object o                           " + Environment.NewLine +
                                    " inner join t_xref x on x.Client = o.ea_guid                 " + Environment.NewLine +
                                    "                and x.Name = 'Stereotypes'                   " + Environment.NewLine +
                                    "                and x.Description like '%Name=JSONSchema;%'  " + Environment.NewLine +
                                    $" where o.Package_ID in ({package.packageTreeIDString})       ";
            var jsonSchemaElements =  this.model.getElementWrappersByQuery(sqlGetJSONSchemas);
            foreach (var jsonSchemaElement in jsonSchemaElements)
            {
                generateJSONSchema(jsonSchemaElement);
            }
        }
        private void generateJSONSchema(TSF_EA.ElementWrapper element)
        {
            var eaJsonSchema = new EAJSONSchema(element);
            //print the schema to the file
            eaJsonSchema.print();
        }
    }
}
