using System;
using System.Collections.Generic;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework.EASpecific;

namespace EAWorksetSharing
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAWorksetSharingAddin:EAAddinFramework.EAAddinBase
	{
		private const string menuName = "&Share Working Sets";
		
				
		public EAWorksetSharingAddin():base()
		{
			this.menuHeader = menuName;
		}
		/// <summary>
		/// returns the base implemenation if called from the main menu
		/// </summary>
		/// <param name="Repository"></param>
		/// <param name="MenuLocation"></param>
		/// <param name="MenuName"></param>
		/// <returns></returns>
		public override object EA_GetMenuItems(EA.Repository Repository, string MenuLocation, string MenuName)
		{
			//this option will only be shown in the main menu.
			if (MenuLocation == "MainMenu")
			{
				return base.EA_GetMenuItems(Repository,MenuLocation,MenuName);
			}
			else
			{
				return null;
			}
		}
		
        /// <summary>
        /// EA_MenuClick events are received by an Add-In in response to user selection of a menu option.
        /// The event is raised when the user clicks on a particular menu option. When a user clicks on one of your non-parent menu options, your Add-In receives a MenuClick event, defined as follows:
        /// Sub EA_MenuClick(Repository As EA.Repository, ByVal MenuName As String, ByVal ItemName As String)
        /// Notice that your code can directly access Enterprise Architect data and UI elements using Repository methods.
        /// Also look at EA_GetMenuItems.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
		/// <param name="MenuLocation">String representing the part of the user interface that brought up the menu. 
        /// Can be TreeView, MainMenu or Diagram.</param>
        /// <param name="MenuName">The name of the parent menu for which sub-items must be defined. In the case of the top-level menu it is an empty string.</param>
        /// <param name="ItemName">The name of the option actually clicked, for example, Create a New Invoice.</param>
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			//initialize model
			this.model = new UTF_EA.Model(Repository);
			//get all users
			List<User> allUsers = this.model.users;
			//get current user
			User currentUser = this.model.currentUser;
			//debug
			//currentUser = new User(this.model,"login1","firstname1","lastname1");
			//get all workingsets
			List<WorkingSet> allWorkingSets = this.model.workingSets;
			//open window
			WorkingSetSharingWindow window	= new WorkingSetSharingWindow(allWorkingSets,allUsers,currentUser);
			window.Show();
		}
		
	}
}