/*
 * Created by SharpDevelop.
 * User: LaptopGeert
 * Date: 2/07/2016
 * Time: 7:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace EAImvertor
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAImvertorAddin: EAAddinFramework.EAAddinBase
	{
		 // define menu constants
        const string menuName = "-&EA Imvertor";
        const string menuAbout = "&About";
        const string menuSettings = "&Settings";
        const string menuPublish = "&Publish to Imvertor";
        
        //attributes
        private UTF_EA.Model model = null;
    	private ImvertorControl _imvertorControl;
       	private bool fullyLoaded = false;
       	private EAImvertorSettings settings;
		//constructor
        public EAImvertorAddin():base()
		{
			this.menuHeader = menuName;
			
			this.menuOptions = new string[] {menuSettings,menuAbout,"Test"};
			this.settings = new EAImvertorSettings();
		}
		
        private ImvertorControl imvertorControl
		{
			get
			{
				//we cannot show windows in the lite edition
				if (this.fullyLoaded && ! this.model.isLiteEdition)
				{
					if (this._imvertorControl == null)
					{
						this._imvertorControl = this.model.addWindow("Imvertor", "EAImvertor.ImvertorControl") as ImvertorControl;
						this._imvertorControl.resultsButtonClick += this.resultsButtonClick;
						this._imvertorControl.retryButtonClick += this.retryButtonClick;
//		                this._navigatorControl.BeforeExpand += new TreeViewCancelEventHandler(this.NavigatorTreeBeforeExpand);
//		                this._navigatorControl.NodeDoubleClick += new TreeNodeMouseClickEventHandler(this.NavigatorTreeNodeDoubleClick);
//		                this._navigatorControl.fqnButtonClick += new EventHandler(this.FqnButtonClick);
//		                this._navigatorControl.guidButtonClick += new EventHandler(this.GuidButtonClick);
//		                this._navigatorControl.quickSearchTextChanged += new EventHandler(this.quickSearchTextChanged);
//		                this._navigatorControl.openInNavigatorClick += new EventHandler( this.openInNavigatorClick);
//		                this._navigatorControl.settings = this.settings;
					}
				}
				return this._imvertorControl;
			}
		}
        /// <summary>
        /// Initializes the model and schemaFactory with the new Repository object.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		public override void EA_FileOpen(EA.Repository Repository)
		{
			//initialize the model
			this.initialize(Repository);

		}
		/// <summary>
		/// initialize the add-in class
		/// </summary>
		/// <param name="Repository"></param>
		private void initialize(EA.Repository Repository)
		{
			//initialize the model
			this.model = new UTF_EA.Model(Repository);
			// indicate that we are now fully loaded
	        this.fullyLoaded = true;
	        if (this.imvertorControl != null)
	        {
	        	this.imvertorControl.clear();
	        }
		}
		/// <summary>
		/// reacts to the event that the resultsButton is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void resultsButtonClick(object sender, EventArgs e)
		{
			//TODO
		}
		/// <summary>
		/// reacts to the event that the retryButton is clicked in the ImvertorControl
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">arguments</param>
		void retryButtonClick(object sender, EventArgs e)
		{
			//TODO
		}		
		/// <summary>
		/// only needed for the about menu
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch(ItemName) 
			{
		       case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
		       case menuSettings:
		            new EAImvertorSettingsForm(this.settings).ShowDialog();
		            break;
		       case "Test":
		            //debugging
		            new EAImvertorJob(null).startJob(this.settings.imvertorURL,this.settings.defaultPIN,this.settings.defaultProcessName
		                                             ,this.settings.defaultProperties,this.settings.defaultPropertiesFilePath,"");
		            break;
			}
		}       
	}
}