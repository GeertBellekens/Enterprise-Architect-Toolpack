
using BrightIdeasSoftware;
using Cobol_Object_Mapper;
using EAAddinFramework;
using EAAddinFramework.Utilities;
using MappingFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EA_MP = EAAddinFramework.Mapping;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace EAMapping
{
    /// <summary>
    /// Description of MyClass.
    /// </summary>
    public class EAMappingAddin : EAAddinBase
    {
        // define menu constants
        const string menuName = "-&EA Mapping";
        const string menuMapAsSource = "&Map";
        const string menuImportMapping = "&Import Mapping";
        const string menuImportCopybook = "&Import Copybook";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About";

        const string mappingControlName = "Mapping";
        //private attributes
        private TSF_EA.Model model = null;
        private bool fullyLoaded = false;
        private MappingControlGUI _mappingControl;
        private EAMappingSettings settings = new EAMappingSettings();
        private string[] menuSubAddNodeOptions = { string.Empty };
        private string[] mainMenuOptions;
        private string[] otherMenuOptions;
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EAMappingAddin() : base()
        {
            this.menuHeader = menuName;
            this.mainMenuOptions = new string[]
                                {
                                menuMapAsSource,
                                menuImportMapping,
                                menuImportCopybook,
                                menuSettings,
                                menuAbout
                                };
            this.otherMenuOptions = new string[]
            {
                menuMapAsSource,
            };
        }
        private MappingControlGUI mappingControl
        {
            get
            {
                if (this._mappingControl == null
                   && this.model != null)
                {
                    this._mappingControl = this.model.addTab(mappingControlName, "EAMapping.MappingControlGUI") as MappingControlGUI;
                    this._mappingControl.HandleDestroyed += this.mappingControl_HandleDestroyed;
                    this._mappingControl.selectNewMappingTarget += this.mappingControl_SelectNewMappingTarget;
                    this._mappingControl.selectNewMappingSource += this.mappingControl_SelectNewMappingSource;
                    this._mappingControl.exportMappingSet += this.mappingControl_ExportMappingSet;
                    this._mappingControl.sourceToTargetDropped += this.mappingControl_sourceToTargetDropped;
                    this._mappingControl.targetToSourceDropped += this.mappingControl_targetToSourceDropped;
                }
                return this._mappingControl;
            }
        }

        private void mappingControl_sourceToTargetDropped(object sender, ModelDropEventArgs e)
        {            
            handleNodeDropped(e, false);
        }
        private void mappingControl_targetToSourceDropped(object sender, ModelDropEventArgs e)
        {
            handleNodeDropped(e, true);
        }

        private static void handleNodeDropped(ModelDropEventArgs e,bool reverse)
        {
            //handle exceptions as otherwise the treeview will swallow them
            try
            {
                var sourceNode = reverse ? 
                    e.TargetModel as MappingNode : 
                    e.SourceModels.Cast<object>().FirstOrDefault() as MappingNode;
                var targetNode = reverse ?
                    e.SourceModels.Cast<object>().FirstOrDefault() as MappingNode :
                    e.TargetModel as MappingNode; 
                if (targetNode != null && sourceNode != null)
                {
                    sourceNode.mapTo(targetNode);
                }
            }
            catch (Exception exc)
            {
                processException(exc);
            }
        }

        private void mappingControl_SelectNewMappingSource(object sender, EventArgs e)
        {
            //let the user select a new root
            var newSourceElement = this.model.getUserSelectedElement(new List<string>() { "Class", "Package" }) as TSF_EA.ElementWrapper;
            //create the mapping set
            var mappingSet = EA_MP.MappingFactory.createMappingSet(newSourceElement, this.settings);
            //load the mapping set
            this.loadMapping(mappingSet);
        }

        private void mappingControl_SelectNewMappingTarget(object sender, EventArgs e)
        {
            var mappingSet = sender as EA_MP.MappingSet;
            //let the user select a new element
            var newTargetElement = this.model.getUserSelectedElement(new List<string>() { "Class", "Package" }) as TSF_EA.ElementWrapper;
            //create mapping node for target element and set it as target of the mapping set
            if (newTargetElement != null)
            {
                mappingSet.target = EA_MP.MappingFactory.createNewRootNode(newTargetElement, mappingSet.settings);
                mappingSet.source.mapTo(mappingSet.target);
            }
        }

        void mappingControl_SelectSource(object sender, EventArgs e)
        {
            var selectedMapping = sender as Mapping;
            if (selectedMapping != null
                && selectedMapping.source != null
                && selectedMapping.source.source != null)
            {
                selectedMapping.source.source.select();
            }
        }

        void mappingControl_SelectTarget(object sender, EventArgs e)
        {
            var selectedMapping = sender as Mapping;
            if (selectedMapping != null
                && selectedMapping.target != null
                && selectedMapping.target.source != null)
            {
                selectedMapping.target.source.select();
            }
        }

        void mappingControl_CreateMapping(Mapping mapping)
        {
            MessageBox.Show("TODO EAMappingAddin::CreateMapping: " + mapping);
        }

        void mappingControl_DeleteMapping(Mapping mapping)
        {
            MessageBox.Show("TODO EAMappingAddin::DeleteMapping: " + mapping);
        }

        void mappingControl_EditMappingLogic(Mapping mapping)
        {
            MessageBox.Show("TODO EAMappingAddin::EditMappingLogic: " + mapping);
        }

        void mappingControl_DeleteMappingLogic(Mapping mapping)
        {
            MessageBox.Show("TODO EAMappingAddin::DeleteMappingLogic: " + mapping);
        }


        void mappingControl_ExportMappingSet(object sender, EventArgs e)
        {
            MappingSet mappingSet = sender as MappingSet;
            //let the user select a file
            var browseExportFileDialog = new SaveFileDialog();
            browseExportFileDialog.Title = "Save export file";
            browseExportFileDialog.Filter = "Mapping Files|*.csv";
            browseExportFileDialog.FilterIndex = 1;
            var dialogResult = browseExportFileDialog.ShowDialog(this.model.mainEAWindow);
            if (dialogResult == DialogResult.OK)
            {
                EA_MP.MappingFactory.exportMappingSet((EA_MP.MappingSet)mappingSet, browseExportFileDialog.FileName);
            }

        }

        void mappingControl_HandleDestroyed(object sender, EventArgs e)
        {
            this._mappingControl = null;
        }
        public override void EA_FileOpen(EA.Repository Repository)
        {
            // initialize the model
            this.model = new TSF_EA.Model(Repository);
            // indicate that we are now fully loaded
            this.fullyLoaded = true;
        }

        public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
        {
            switch (MenuLocation)
            {
                case "MainMenu":
                    this.menuOptions = this.mainMenuOptions;
                    break;
                default:
                    this.menuOptions = this.otherMenuOptions;
                    break;
            }
            return base.EA_GetMenuItems(Repository, MenuLocation, MenuName);
        }
        public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
            switch (ItemName)
            {
                case menuImportCopybook:
                    IsEnabled = this.fullyLoaded &&
                          (this.model.selectedElement != null);
                    break;
                default:
                    IsEnabled = true;
                    break;
            }
        }

        /// <summary>
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the selected menu item</param>
        public override void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
                case menuMapAsSource:
                    this.loadMapping(this.getCurrentMappingSet());
                    break;
                case menuAbout:
                    new AboutWindow().ShowDialog(this.model.mainEAWindow);
                    break;
                case menuImportMapping:
                    this.startImportMapping();
                    break;
                case menuImportCopybook:
                    this.importCopybook();
                    break;
                case menuSettings:
                    new MappingSettingsForm(this.settings).ShowDialog(this.model.mainEAWindow);
                    break;
            }
        }


        void loadMapping(MappingFramework.MappingSet mappingSet)
        {
            if (mappingSet != null)
            {
                this.mappingControl.loadMappingSet(mappingSet);
                this.model.activateTab(mappingControlName);
            }
        }

        void startImportMapping()
        {
            var importDialog = new ImportMappingForm();
            var selectedElement = this.model.selectedElement as TSF_EA.Element;
            if (selectedElement is UML.Classes.Kernel.Class
                || selectedElement is UML.Classes.Kernel.Package)
            {
                importDialog.sourcePathElement = selectedElement;
                importDialog.targetPathElement = this.findTarget(selectedElement);
            }
            importDialog.ImportButtonClicked += this.importMapping;
            importDialog.SourcePathBrowseButtonClicked += this.browseSourcePath;
            importDialog.TargetPathBrowseButtonClicked += this.browseTargetPath;
            importDialog.ShowDialog(this.model.mainEAWindow);
        }

        TSF_EA.Element findTarget(TSF_EA.Element sourceElement)
        {
            var trace = sourceElement.getRelationships<UML.Classes.Dependencies.Abstraction>().FirstOrDefault(x => x.stereotypes.Any(y => y.name.Equals("trace", StringComparison.InvariantCultureIgnoreCase))
                                                                                        && (x.target is UML.Classes.Kernel.Package || x.target is UML.Classes.Kernel.Class));
            if (trace != null)
            {
                return trace.target as TSF_EA.Element;
            }
            //if nothing found then return null
            return null;
        }

        void importMapping(object sender, EventArgs e)
        {
            this.clearOutput();
            var importDialog = sender as ImportMappingForm;
            if (importDialog != null)
            {
                var mappingSet = EA_MP.MappingFactory.createMappingSet(this.model, importDialog.importFilePath, this.settings
                                                      , (TSF_EA.ElementWrapper)importDialog.sourcePathElement, (TSF_EA.ElementWrapper)importDialog.targetPathElement);
                if (mappingSet != null)
                {
                    this.loadMapping(mappingSet);
                }
            }
        }
        private void clearOutput()
        {
            EAOutputLogger.clearLog(this.model, this.settings.outputName);
        }
        void browseSourcePath(object sender, EventArgs e)
        {
            var importDialog = (ImportMappingForm)sender;
            importDialog.sourcePathElement = this.getuserSelectedClassOrPackage();
        }

        void browseTargetPath(object sender, EventArgs e)
        {
            var importDialog = (ImportMappingForm)sender;
            importDialog.targetPathElement = this.getuserSelectedClassOrPackage();
        }
        private TSF_EA.Element getuserSelectedClassOrPackage()
        {
            return this.model.getUserSelectedElement(new List<string> { "Class", "Package" }) as TSF_EA.Element;
        }
        private MappingSet getCurrentMappingSet()
        {
            var selectedElement = this.model.selectedElement as TSF_EA.ElementWrapper;
            return selectedElement != null ? EA_MP.MappingFactory.createMappingSet(selectedElement, this.settings) : null;
        }

        // Cobol Copybook support
        void importCopybook()
        {
            // get user selected DDL file
            var selection = new OpenFileDialog()
            {
                Filter = "Copybook File |*.cob;*.cobol;*.txt",
                FilterIndex = 1,
                Multiselect = false
            };
            if (selection.ShowDialog() == DialogResult.OK)
            {
                string source = new StreamReader(
                  new FileStream(selection.FileName,
                     FileMode.Open, FileAccess.Read, FileShare.ReadWrite
                  )).ReadToEnd();

                this.importCopybook(source, (TSF_EA.Package)this.model.selectedElement);
            }
        }

        // cached mapping of external UIDs to EA classes
        private Dictionary<string, UML.Classes.Kernel.Class> classes;

        private void importCopybook(string source, TSF_EA.Package package)
        {
            // parse copybook into OO data-representation
            var mapped = new Mapper();
            try
            {
                mapped.Parse(source);
            }
            catch (ParseException e)
            {
                // recurse down the Exception tree, to reach the most specific one
                this.log("!!! IMPORT FAILED");
                do
                {
                    foreach (var line in e.Message.Split('\n'))
                    {
                        this.log(line);
                    }
                    e = e.InnerException as ParseException;
                } while (e != null);
                return;
            }

            // import mapped OO data-representation

            // prepare cache
            this.classes = new Dictionary<string, UML.Classes.Kernel.Class>();

            // step 1: create diagram
            this.log("*** creating diagram");
            var diagram = this.createDiagram(package, package.name);

            // step 2: classes with properties
            this.log("*** importing classes+properties");
            foreach (var clazz in mapped.Model.Classes)
            {
                var eaClass = this.createClass(clazz.Name, package, clazz.Id);
                this.classes.Add(clazz.Id, eaClass);
                foreach (var property in clazz.Properties)
                {
                    this.addProperty(
                      eaClass, property.Name, property.Type, property.Stereotype
                    );
                }
                diagram.addToDiagram(eaClass);
            }
            // step 3: generalisations and associations
            this.log("*** importing generalisations and associations");
            foreach (var clazz in mapped.Model.Classes)
            {
                foreach (var association in clazz.Associations)
                {
                    this.addAssociation(
                      this.classes[association.Source.Id],
                      this.classes[association.Target.Id],
                      association.Multiplicity,
                      association.DependsOn
                    );
                }
                if (clazz.Super != null)
                {
                    this.addGeneralization(
                      this.classes[clazz.Super.Id],
                      this.classes[clazz.Id]
                    );
                }
            }
            //layout diagram
            diagram.autoLayout();
        }

        private UML.Diagrams.ClassDiagram createDiagram(TSF_EA.Package package,
                                                        string name)
        {
            var diagram = this.model.factory.createNewDiagram<UML.Diagrams.ClassDiagram>(
              package, name
            );
            diagram.save();
            return diagram;
        }

        private UML.Classes.Kernel.Class createClass(string name,
                                                     TSF_EA.Package package,
                                                     string uid)
        {
            var clazz = this.model.factory.createNewElement<UML.Classes.Kernel.Class>(
                package, name
            );
            clazz.save();
            return clazz;
        }

        private UML.Classes.Kernel.Property addProperty(UML.Classes.Kernel.Class clazz,
                                                        string name,
                                                        string type = null,
                                                        string stereotype = null)
        {
            var property =
              this.model.factory.createNewElement<UML.Classes.Kernel.Property>(
                clazz, name
              );
            if (type != null)
            {
                property.type = this.model.factory.createPrimitiveType(type);
            }
            if (stereotype != null)
            {
                var stereotypes = new HashSet<UML.Profiles.Stereotype>();
                stereotypes.Add(new TSF_EA.Stereotype(
                  this.model, property as TSF_EA.Element, stereotype)
                    );
                property.stereotypes = stereotypes;
            }
            property.save();
            return property;
        }

        private UML.Classes.Kernel.Association addAssociation(UML.Classes.Kernel.Class source,
                                                              UML.Classes.Kernel.Class target,
                                                              string targetMultiplicity = null,
                                                              string dependsOn = null)
        {
            var association =
              this.model.factory.createNewElement<UML.Classes.Kernel.Association>(
                source, dependsOn
              );
            association.addRelatedElement(target);
            //set source multiplicity and aggregationkind
            ((TSF_EA.Association)association).sourceEnd.aggregation = UML.Classes.Kernel.AggregationKind.composite;
            if (targetMultiplicity == null)
            {
                targetMultiplicity = "1"; //default target multiplicity
            }

            if (targetMultiplicity != null)
            {
                ((TSF_EA.Association)association).targetEnd.EAMultiplicity =
                  new TSF_EA.Multiplicity(targetMultiplicity);
            }
          //set target navibable
          ((TSF_EA.Association)association).targetEnd.isNavigable = true;

            association.save();
            return association;
        }

        private TSF_EA.Generalization addGeneralization(UML.Classes.Kernel.Class parent,
                                                        UML.Classes.Kernel.Class child)
        {
            var generalization =
          this.model.factory.createNewElement<TSF_EA.Generalization>(
            child, string.Empty
          );
            generalization.addRelatedElement(parent);
            generalization.save();
            return generalization;
        }

        private void log(string msg)
        {
            EAOutputLogger.log(this.model, this.settings.outputName, msg);
        }

    }
}
