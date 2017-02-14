
using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework;
using EA_MP = EAAddinFramework.Mapping;
using System.Linq;
using MappingFramework;
using Cobol_Object_Mapper;

namespace EAMapping
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAMappingAddin:EAAddinBase
	{
		// define menu constants
        const string menuName = "-&EA Mapping";
        const string menuMapAsSource = "&Map as Source";
        const string menuImportMapping = "&Import Mapping";
        const string menuImportCopybook = "&Import Copybook";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About";
        
        const string mappingControlName = "Mapping";
        //private attributes
        private TSF_EA.Model model = null;
        private bool fullyLoaded = false;
        private MappingControl _mappingControl;
        private MappingSet _currentMappingSet = null;
        private EAMappingSettings settings = new EAMappingSettings();
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EAMappingAddin():base()
        {
        	this.menuHeader = menuName;
          this.menuOptions = new string[] {
            menuMapAsSource,
            menuImportMapping,
            menuImportCopybook,
            menuSettings,
            menuAbout
          };
        }


        private MappingControl mappingControl
		{
			get
			{
				if (_mappingControl == null
				   && this.model != null)
				{
					_mappingControl = this.model.addTab(mappingControlName, "EAMapping.MappingControl") as MappingControl;
					_mappingControl.HandleDestroyed += mappingControl_HandleDestroyed;
					_mappingControl.selectSource += mappingControl_SelectSource;
					_mappingControl.selectTarget += mappingControl_SelectTarget;
					_mappingControl.exportMappingSet += mappingControl_ExportMappingSet;
				}
				return _mappingControl;
			}
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
            	//if the user selected the file then put the filename in the abbreviationsfileTextBox
            	EA_MP.MappingFactory.exportMappingSet((EA_MP.MappingSet)mappingSet,browseExportFileDialog.FileName);
            }
			
		}

        void mappingControl_SelectSource(object sender, EventArgs e)
		{
			var selectedMapping = sender as Mapping;
			if (selectedMapping != null
			    && selectedMapping.source != null
			    && selectedMapping.source.mappedEnd != null)
			{
				selectedMapping.source.mappedEnd.select();
			}
		}

		void mappingControl_SelectTarget(object sender, EventArgs e)
		{
			var selectedMapping = sender as Mapping;
			if (selectedMapping != null
			    && selectedMapping.target != null
			    && selectedMapping.target.mappedEnd != null)
			{
				selectedMapping.target.mappedEnd.select();
			}
		}

		void mappingControl_HandleDestroyed(object sender, EventArgs e)
		{
			_mappingControl = null;
		}
		public override void EA_FileOpen(EA.Repository Repository)
		{
			// initialize the model
	        this.model = new TSF_EA.Model(Repository);
			// indicate that we are now fully loaded
	        this.fullyLoaded = true;
		}

    public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
    {
    	switch( ItemName ) {
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
          		loadMapping(this.getCurrentMappingSet(true));
                  break;
  		        case menuAbout :
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
      		this.mappingControl.loadMappingSet(this.getCurrentMappingSet(true));
            model.activateTab(mappingControlName);
        }
		void startImportMapping()
		{
			var importDialog = new ImportMappingForm();
			var selectedElement = model.selectedElement as TSF_EA.Element;
			if (selectedElement is UML.Classes.Kernel.Class
			    || selectedElement is UML.Classes.Kernel.Package)
			{
				importDialog.sourcePathElement = selectedElement;
				importDialog.targetPathElement = findTarget(selectedElement);
			}
			importDialog.ImportButtonClicked += importMapping;
			importDialog.SourcePathBrowseButtonClicked += browseSourcePath;
			importDialog.TargetPathBrowseButtonClicked += browseTargetPath;
			importDialog.ShowDialog(this.model.mainEAWindow);
		}
		TSF_EA.Element findTarget(TSF_EA.Element sourceElement)
		{
			 var trace = sourceElement.getRelationships<UML.Classes.Dependencies.Abstraction>().FirstOrDefault(x => x.stereotypes.Any(y => y.name.Equals("trace",StringComparison.InvariantCultureIgnoreCase))
			                                                                             && (x.target is UML.Classes.Kernel.Package || x.target is UML.Classes.Kernel.Class) );
			if (trace != null) return trace.target as TSF_EA.Element;
			//if nothing found then return null
			return null;
		}

		void importMapping(object sender, EventArgs e)
		{
			clearOutput();
			var importDialog = sender as ImportMappingForm;
			if (importDialog != null)
			{
				var mappingSet = EA_MP.MappingFactory.createMappingSet(this.model,importDialog.importFilePath,this.settings
				                                      ,importDialog.sourcePathElement,importDialog.targetPathElement);
				if (mappingSet != null)
				{
					loadMapping(mappingSet);
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
			importDialog.sourcePathElement = getuserSelectedClassOrPackage();
		}

		void browseTargetPath(object sender, EventArgs e)
		{
			var importDialog = (ImportMappingForm)sender;
			importDialog.targetPathElement = getuserSelectedClassOrPackage();
		}
		private TSF_EA.Element getuserSelectedClassOrPackage()
		{
			return this.model.getUserSelectedElement(new List<string>{"Class","Package"}) as TSF_EA.Element;
		}
        private MappingFramework.MappingSet getCurrentMappingSet(bool source)
        {
    		var selectedItem = model.selectedItem;
    		var selectedPackage = selectedItem as UML.Classes.Kernel.Package;
    		//check if package is selected
    		if (selectedPackage != null)
    		{
    			_currentMappingSet = getCurrentMappingSet(selectedPackage, source);
    		}
    		else 
    		{
	    		//check if element is selected
	    		var selectedElement = selectedItem as TSF_EA.ElementWrapper;
	    		if (selectedElement != null)
	    		{
	    			_currentMappingSet = getCurrentMappingSet(selectedElement, source);
	    		}
    		}
    		return _currentMappingSet;	
        	
        }
        private MappingFramework.MappingSet getCurrentMappingSet(UML.Classes.Kernel.Package package, bool source)
        {
        	return new EA_MP.PackageMappingSet((TSF_EA.Package)package,source);
        }
        private MappingFramework.MappingSet getCurrentMappingSet(TSF_EA.ElementWrapper rootElement, bool source)
        {
        	return new EA_MP.ElementMappingSet(rootElement,source);
        }

    // Cobol Copybook support
    void importCopybook() {
      // initialize database
      var selectedPackage = this.model.selectedElement as TSF_EA.Package;

      // get user selected DDL file
      var selection = new OpenFileDialog() {
        Filter      = "Copybook File |*.cob;*.cobol;*.txt",
        FilterIndex = 1,
        Multiselect = false
      };
      if( selection.ShowDialog() == DialogResult.OK ) {
        string source = new StreamReader(
          new FileStream( selection.FileName,
             FileMode.Open, FileAccess.Read, FileShare.ReadWrite
          )).ReadToEnd();

        var mapper = new Mapper();
        mapper.Parse(source);
        foreach(var line in mapper.Model.ToString().Split('\n')) {
          EAOutputLogger.log( this.model, this.settings.outputName, line );
        }
        // TODO analyse AST and create corresponding class model in EA
      }
    }

	}
}