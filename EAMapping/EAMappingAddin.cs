
using System;
using System.Collections.Generic;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework;

namespace EAMapping
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAMappingAddin:EAAddinBase
	{
		// define menu constants
        const string menuName = "-&EA Mapping";
        const string menuMap = "&Show Mapping";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About";
        
        const string mappingControlName = "Mapping";
        //private attributes
        private TSF_EA.Model model = null;
        private bool fullyLoaded = false;
        private MappingControl _mappingControl;
        private MappingFramework.MappingSet _currentMappingSet = null;
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EAMappingAddin():base()
        {
        	this.menuHeader = menuName;
			this.menuOptions = new string[]{menuMap, menuSettings, menuAbout};
        }
        
        private MappingControl mappingControl
		{
			get
			{
				if (_mappingControl == null
				   && this.model != null)
				{
					_mappingControl = this.model.addTab(mappingControlName, "EAMapping.MappingControl") as MappingControl;
					_mappingControl.HandleDestroyed += dbControl_HandleDestroyed;
				}
				return _mappingControl;
			}
		}
		void dbControl_HandleDestroyed(object sender, EventArgs e)
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
                case menuMap:
            		this.mappingControl.loadMappingSet(this.currentMappingSet);
                	Repository.ActivateTab(mappingControlName);
                    break;
		        case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
	            case menuSettings:
	                //TODO
	                break;
            }
        }
        private MappingFramework.MappingSet currentMappingSet
        {
        	get
        	{
        		//TODO: select the currently selected mapping set from the model
        		return _currentMappingSet;
        	}
        	
        }
	}
}