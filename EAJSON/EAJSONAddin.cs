using EA;
using EAAddinFramework;
using EAAddinFramework.Utilities;
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
        const string outputName = "EA JSON";
        
        public EAJSONAddin() : base()
        {
            // Add menu's to the Add-in in EA
            this.menuHeader = menuName;
            this.menuOptions = new string[] {
                                menuGenerate,
                                menuTransform,
                                //menuSettings,
                                menuAbout
                              };
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
                        (selectedElement.stereotypes.Any(x => x.name.Equals(EAJSONSchema.schemaStereotype, StringComparison.InvariantCultureIgnoreCase))
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
                    new AboutWindow().ShowDialog(this.model.mainEAWindow);
                    break;
            }
        }
        /// <summary>
        /// return the MDG content for the EDD MDG (so it doesn't have to be loaded separately
        /// </summary>
        /// <param name="Repository"></param>
        /// <returns>the MDG file contents</returns>
        public override object EA_OnInitializeTechnologies(Repository Repository)
        {
            return Properties.Resources.JSON_MDG;
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
            if (selectedPackage != null)
            {
                //inform user
                EAOutputLogger.clearLog(this.model, outputName);
                EAOutputLogger.log(this.model, outputName
                                   , $"{DateTime.Now.ToLongTimeString()} Starting transform of package '{selectedPackage.name}'"
                                   , ((TSF_EA.ElementWrapper)selectedPackage).id
                                  , LogTypeEnum.log);
                //perform the actual transformation
                EAJSONSchema.transformPackage(selectedPackage, rootObject);
                //inform user
                EAOutputLogger.log(this.model, outputName
                                   , $"{DateTime.Now.ToLongTimeString()} Finished transform of package '{selectedPackage.name}'"
                                   , ((TSF_EA.ElementWrapper)selectedPackage).id
                                  , LogTypeEnum.log);
            }
            
        }
        /// <summary>
        /// Generate a new JSON Schema from the selected File
        /// </summary>
        private void generateJSONSchema()
        {
            //inform user
            EAOutputLogger.clearLog(this.model, outputName);

            var selectedPackage = this.model.selectedElement as TSF_EA.Package;
            if (selectedPackage != null)
            {
                //generate for package
                generateJSONSchemas(selectedPackage);
                //inform the user the generation has finished
                MessageBox.Show($"Finished generating JSON schema's for package '{selectedPackage.name}'"
                                , "JSON Schema's generated"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Information);
            }
            else
            {
                var selectedElement = this.model.selectedElement as TSF_EA.ElementWrapper;
                if (selectedElement != null)
                {
                    var jsonSchema = this.generateJSONSchema(selectedElement);
                    //allow the user to open the file
                    var response = MessageBox.Show($"Finished generating JSON schema for element '{selectedElement.name}'{Environment.NewLine}Would you like to open the JSON Schema?"
                                                    , "JSON Schema's generated"
                                                    , MessageBoxButtons.YesNo
                                                    , MessageBoxIcon.Question);
                    if (response == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(jsonSchema.schemaFileName);
                    }
                }
            }
            //inform user
            EAOutputLogger.log(this.model, outputName
                   , $"{DateTime.Now.ToLongTimeString()} Finished generating Schema(s)"
                   , 0
                  , LogTypeEnum.log);
        } 
        private void generateJSONSchemas(TSF_EA.Package package)
        {
            //inform user
            EAOutputLogger.log(this.model, outputName
                   , $"{DateTime.Now.ToLongTimeString()} Generating Schema's in package '{package.name}'"
                   , package.id
                  , LogTypeEnum.log);
            //get the «JSONSchema» elements in this package recursively
            var sqlGetJSONSchemas = "select o.Object_ID from t_object o                                                " + Environment.NewLine +
                                    " inner join t_xref x on x.Client = o.ea_guid                                      " + Environment.NewLine +
                                    $" where o.Package_ID in ({package.packageTreeIDString})                           " + Environment.NewLine +
                                    "                and x.Name = 'Stereotypes'                                        " + Environment.NewLine +
                                    $"               and x.Description like '%Name={EAJSONSchema.schemaStereotype};%'  ";
            var jsonSchemaElements =  this.model.getElementWrappersByQuery(sqlGetJSONSchemas);
            foreach (var jsonSchemaElement in jsonSchemaElements)
            {
                generateJSONSchema(jsonSchemaElement);
            }
        }
        private EAJSONSchema generateJSONSchema(TSF_EA.ElementWrapper element)
        {
            EAOutputLogger.log(this.model, outputName
                   , $"{DateTime.Now.ToLongTimeString()} Generating Schema for element '{element.name}'"
                   , element.id
                  , LogTypeEnum.log);
            var eaJsonSchema = new EAJSONSchema(element);
            //print the schema to the file
            eaJsonSchema.print();
            return eaJsonSchema;
        }
    }
}
