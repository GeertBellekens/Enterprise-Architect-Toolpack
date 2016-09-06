
using System;
using System.Collections.Generic;
using System.Linq;
using EAAddinFramework.Databases.Transformation.DB2;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using DB=DatabaseFramework;
using DB_EA = EAAddinFramework.Databases;
using EAAddinFramework.Utilities;

namespace EADatabaseTransformer
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EADatabaseTransformerAddin : EAAddinFramework.EAAddinBase
    {
        // define menu constants
        const string menuName = "-&Database Transformer";
        const string menuTransform = "&Transform to database";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About";
        const string compareControlName = "Database Compare";
        
        //private attributes
        private UTF_EA.Model model = null;
        private bool fullyLoaded = false;
        private EADatabaseTransformerSettings settings;
        private DBCompareControl _dbCompareControl;
        private DB2DatabaseTransformer _databaseTransformer;
        private DB.Compare.DatabaseComparer _comparer;
        /// <summary>
        /// constructor where we set the menuheader and menuOptions
        /// </summary>
		public EADatabaseTransformerAddin():base()
		{
			this.menuHeader = menuName;
			this.menuOptions = new string[]{menuTransform, menuSettings, menuAbout};
			this.settings = new EADatabaseTransformerSettings();
		}
		private DBCompareControl dbCompareControl
		{
			get
			{
				if (_dbCompareControl == null)
				{
					_dbCompareControl = this.model.addTab(compareControlName, "EADatabaseTransformer.DBCompareControl") as DBCompareControl;
					_dbCompareControl.HandleDestroyed += dbControl_HandleDestroyed;
					_dbCompareControl.saveDatabaseButtonClick += saveDatabaseButtonClicked;
					_dbCompareControl.refreshButtonClicked += refreshButtonClicked;
				}
				return _dbCompareControl;
			}
		}
		void dbControl_HandleDestroyed(object sender, EventArgs e)
		{
			_dbCompareControl = null;
		}

		void saveDatabaseButtonClicked(object sender, EventArgs e)
		{
			_comparer.save();
			this.refreshCompare();
		}

		void refreshButtonClicked(object sender, EventArgs e)
		{
			this.refreshCompare();
		}
		public override void EA_FileOpen(EA.Repository Repository)
		{
			// initialize the model
	        this.model = new UTF_EA.Model(Repository);
			// indicate that we are now fully loaded
	        this.fullyLoaded = true;
		}
        /// <summary>
        /// Called once Menu has been opened to see what menu items should active.
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the menu item</param>
        /// <param name="IsEnabled">boolean indicating whethe the menu item is enabled</param>
        /// <param name="IsChecked">boolean indicating whether the menu is checked</param>
        public override void EA_GetMenuState(EA.Repository Repository, string Location, string MenuName, string ItemName, ref bool IsEnabled, ref bool IsChecked)
        {
        	switch (ItemName)
                {
                	case menuTransform:
                		if (this.fullyLoaded
                		    && this.model.selectedElement is UML.Classes.Kernel.Package)
                		{
                			IsEnabled = true;
                		}
                		else
                		{
                			IsEnabled = false;
                		}
                		break;
                	case menuAbout:
                		IsEnabled = true;
                		break;
                	case menuSettings:
                		IsEnabled = true;
                		break;
                    // there shouldn't be any other, but just in case disable it.
                    default:
                        IsEnabled = false;
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
                case menuTransform:
                	this.startTransformation();
                	Repository.ActivateTab(compareControlName);
                    break;
		        case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
	            case menuSettings:
	                new EADatabaseTransformerSettingsForm(this.settings).ShowDialog();
	                break;
            }
        }
  		/// <summary>
  		/// start the transformation from the logical model to the database model
  		/// </summary>
		void startTransformation()
		{
			var selectedPackage = this.model.selectedElement as UML.Classes.Kernel.Package;
			if (selectedPackage != null)
			{
				//debug
				transform(selectedPackage);
				//figure out which the database model is for this package
				//if the database model is not defined yet then ask the user
				//make a list of all the differences and show it to the user
				
			}
		}

		void transform(UML.Classes.Kernel.Package selectedPackage)
		{	
			var nameTranslator = new DB_EA.Transformation.NameTranslator(this.settings.abbreviationsPath,"_");
			_databaseTransformer = new DB2DatabaseTransformer((UTF_EA.Package)selectedPackage,nameTranslator);
			refreshCompare();
				
		}
		private void refreshCompare()
		{
			//refresh transformation and load of new and original database
			_databaseTransformer.refresh();
			_comparer = new DB_EA.Compare.EADatabaseComparer((DB_EA.Database) _databaseTransformer.newDatabase, (DB_EA.Database) _databaseTransformer.existingDatabase);
			_comparer.compare();
			this.dbCompareControl.loadComparison(_comparer);
		}

		

	}

}