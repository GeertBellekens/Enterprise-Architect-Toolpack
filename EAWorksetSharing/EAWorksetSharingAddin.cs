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
		private UTF_EA.Model model;
					
		public EAWorksetSharingAddin():base()
		{
			this.menuHeader = menuName;
		}
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