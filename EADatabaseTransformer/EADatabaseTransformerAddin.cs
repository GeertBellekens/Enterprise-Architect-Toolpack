
using System;
using System.Collections.Generic;
using System.Linq;
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
        
        //private attributes
        private UTF_EA.Model model = null;
        private bool fullyLoaded = false;
        private EADatabaseTransformerSettings settings;
        private DBCompareControl _dbCompareControl;
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
					_dbCompareControl = this.model.addTab("Database Compare", "EADatabaseTransformer.DBCompareControl") as DBCompareControl;
				}
				return _dbCompareControl;
			}
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
				test(selectedPackage);
				//figure out which the database model is for this package
				//if the database model is not defined yet then ask the user
				//make a list of all the differences and show it to the user
				
			}
		}

		void test(UML.Classes.Kernel.Package selectedPackage)
		{
			//TODO: setup DB2 factory, from config somewhere? Could be taken from EA?
			List<DB_EA.BaseDataType> baseDatatypes = new List<DB_EA.BaseDataType>();
			baseDatatypes.Add(new DB_EA.BaseDataType("CHAR",true, false));
			baseDatatypes.Add(new DB_EA.BaseDataType("TIMESTAMP",false, false));
			baseDatatypes.Add(new DB_EA.BaseDataType("DATE",false, false));
			baseDatatypes.Add(new DB_EA.BaseDataType("DECIMAL",true, true));
			DB_EA.DatabaseFactory.addFactory("DB2",baseDatatypes);
			var factory = DB_EA.DatabaseFactory.getFactory("DB2");
			
			//DB_EA.Database database = factory.createDataBase(selectedPackage as UTF_EA.Package);
			//this.dbCompareControl.loadOriginalDatabase(database);
			
			var newDatabase = transformLDMToDB(selectedPackage  as UTF_EA.Package, factory);
			this.dbCompareControl.loadOriginalDatabase(newDatabase);
			
		}
//			Logger.log ("Database: " + database.name);
//			foreach (var table in database.tables) 
//			{
//				Logger.log("Table: " + table.name);
//				foreach (var column in table.columns) 
//				{
//					Logger.log("column: " + column.name +" "+ column.type.type.name + "(" + column.type.length + "," + column.type.precision + ")" + " Not Null: " + column.isNotNullable.ToString());
//				}
//				foreach (var constraint in table.constraints) 
//				{
//					Logger.log("constraint: " + constraint.name);
//					var foreignKey = constraint as DB.ForeignKey;
//					if (foreignKey != null)
//					{
//						if (foreignKey.foreignTable  != null)
//						{
//							Logger.log("foreign table: " + foreignKey.foreignTable.name);
//						}
//						else
//						{
//							Logger.log("foreign table is null");
//						}
//					}
//					foreach (var involvedColumn in constraint.involvedColumns) 
//					{
//						Logger.log("involvedColumn: " + involvedColumn.name);
//					}
//				}
//			}	
		
		//transform the daabase to
		private DB.Database transformLDMToDB(UTF_EA.Package ldmPackage, DB_EA.DatabaseFactory factory)
		{
			DB_EA.Database database = factory.createDatabase(ldmPackage.alias);
			foreach (UTF_EA.Class classElement in ldmPackage.ownedElements.OfType<UTF_EA.Class>()) 
			{
				if (classElement.alias == string.Empty) classElement.alias = "unknown table name";
				DB_EA.Table table = new DB_EA.Table(database, classElement.alias);
				foreach (UTF_EA.Attribute attribute in classElement.attributes) 
				{
					//TODO: translate name to alias
					DB_EA.Column column = new DB_EA.Column(table, attribute.alias);
					//get base type
					var attributeType = attribute.type as UTF_EA.ElementWrapper;
					if (attributeType == null) Logger.logError (string.Format("Attribute {0}.{1} does not have a element as datatype"
					                                                    ,classElement.name, attribute.name));
					else
					{
						DB.DataType datatype = factory.createDataType(attributeType.alias);
						if (datatype == null) Logger.logError (string.Format("Could not find translate {0} as Datatype for attribute {1}.{2}"
						                                                    ,attributeType.alias, classElement.name, attribute.name));
						else
						{
							column.type = datatype;
						}
					}

				}
			}
			return database;
		}
	}

}