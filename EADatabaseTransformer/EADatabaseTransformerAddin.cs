
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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
        const string menuComparetoDatabase = "&Compare Database";
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
			this.menuOptions = new string[]{menuComparetoDatabase, menuSettings, menuAbout};
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
					_dbCompareControl.selectDatabaseItem += selectDatabaseItem;
					_dbCompareControl.selectLogicalItem += selectLogicalItem;
					_dbCompareControl.renameButtonClick += renameButtonClick;
					_dbCompareControl.overrideButtonClick += overrideButtonClick;
				}
				return _dbCompareControl;
			}
		}
		void dbControl_HandleDestroyed(object sender, EventArgs e)
		{
			_dbCompareControl = null;
		}

		void renameButtonClick(object sender, EventArgs e)
		{
			var comparedItem = sender as DB.Compare.DatabaseItemComparison;
			if (comparedItem != null
			    && comparedItem.newDatabaseItem != null)
			{
				var renamePopup = new RenameWindow(comparedItem.newDatabaseItem.name);
				if (renamePopup.ShowDialog(this._dbCompareControl) == DialogResult.OK)
				{
					_databaseTransformer.renameItem(comparedItem.newDatabaseItem, renamePopup.newName);
				}
			}
			this.refreshCompare(false);
		}

		void overrideButtonClick(object sender, EventArgs e)
		{
			var comparedItem = sender as DB.Compare.DatabaseItemComparison;
			if (comparedItem != null
			    && comparedItem.existingDatabaseItem != null)
			{
				if (comparedItem.newDatabaseItem != null)
				{
					comparedItem.newDatabaseItem.update(comparedItem.existingDatabaseItem,false);
					comparedItem.newDatabaseItem.isOverridden = true;
				}
				comparedItem.existingDatabaseItem.isOverridden = true;
			}
			this.refreshCompare(false);
		}

		void saveDatabaseButtonClicked(object sender, EventArgs e)
		{
			_comparer.save();
			this.refreshCompare(true);
			this.model.showTab(compareControlName);
		}

		void refreshButtonClicked(object sender, EventArgs e)
		{
			this.refreshCompare(true);
		}

		void selectLogicalItem(object sender, EventArgs e)
		{
			var databaseItem = sender as DB.DatabaseItem;
			if (databaseItem != null
			    && databaseItem.logicalElement != null)
			{
				databaseItem.logicalElement.select();
			}
		}

		void selectDatabaseItem(object sender, EventArgs e)
		{
			var databaseItem = sender as DB.DatabaseItem;
			if (databaseItem != null) databaseItem.Select();
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
            	case menuComparetoDatabase:
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
                case menuComparetoDatabase:
                	this.compareDatabase();
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
		void compareDatabase()
		{
			var selectedPackage = this.model.selectedElement as UTF_EA.Package;
			//TODO: allow the user to select either database or logical package if not already linked, or if multiple are linked
			if (selectedPackage != null)
			{
				var nameTranslator = new DB_EA.Transformation.NameTranslator(this.settings.abbreviationsPath,"_");
				if (selectedPackage.stereotypes.Any( x => x.name.Equals("database",StringComparison.InvariantCultureIgnoreCase)))
			    {
				    
				    var existingDatabase = DB2DatabaseTransformer.getFactory(this.model).createDataBase(selectedPackage);
					_databaseTransformer = new DB2DatabaseTransformer(this.model,nameTranslator);
				    _databaseTransformer.existingDatabase = existingDatabase;
				}
				else
				{
					_databaseTransformer = new DB2DatabaseTransformer((UTF_EA.Package)selectedPackage,nameTranslator);
				}
				
				refreshCompare(true);	
			}
		}

		private void refreshCompare(bool refreshTransform)
		{
			if (refreshTransform)_databaseTransformer.refresh();
			//refresh transformation and load of new and original database
			_comparer = new DB_EA.Compare.EADatabaseComparer((DB_EA.Database) _databaseTransformer.newDatabase, (DB_EA.Database) _databaseTransformer.existingDatabase);
			_comparer.compare();
			this.dbCompareControl.loadComparison(_comparer);
		}

		

	}

}