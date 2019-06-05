using EA;
using EAAddinFramework;
using System;
using System.Linq;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAJSON
{
    public class EAJSONAddin : EAAddinBase
    {
        // menu constants
        const string menuName = "-&EA JSON";
        const string menuGenerate = "&Generate JSON Schema";
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
                    IsEnabled = selectedElement != null && selectedElement.stereotypes.Any(x => x.name.Equals("JSONSchema", StringComparison.InvariantCultureIgnoreCase));
                    break;
            }

        }
        public override void EA_MenuClick(Repository Repository, string MenuLocation, string MenuName, string ItemName)
        {
            switch(ItemName)
            {
                case menuGenerate:
                    this.GenerateJSONSchema();
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
        /// Generate a new JSON Schema from the selected File
        /// </summary>
        private void GenerateJSONSchema()
        {
            var selectedElement = this.model.selectedElement as TSF_EA.ElementWrapper;
            var eaJsonSchema = new EAJSONSchema(selectedElement);
            var schema = eaJsonSchema.schema;
            //debug
            var debugFile = @"c:\temp\jsonSchema.json";
            System.IO.File.WriteAllText(debugFile, schema.ToString());
            System.Diagnostics.Process.Start(debugFile);
        }
    }
}
