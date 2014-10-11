/*
 * Created by SharpDevelop.
 * User: Geert
 * Date: 7/10/2014
 * Time: 19:02
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using EAAddinFramework.EASpecific;
using UTF_EA=TSF.UmlToolingFramework.Wrappers.EA;

namespace EAScriptAddin
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAScriptAddinAddinClass: EAAddinFramework.EAAddinBase
	{
		private const string menuMain = "-&Scripting Addin";
		private const string menuAbout = "&About EA Script Add-in";
		public EAScriptAddinAddinClass():base()
		{
			this.menuHeader = menuMain;
			this.menuOptions = new String[] {menuAbout};
		}
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch (ItemName) {
				case menuAbout:
					this.test(Repository);
					break;
			}
		}
		
		void test(EA.Repository repository)
		{
			UTF_EA.Model model = new UTF_EA.Model(repository);
			List<Script> scripts = Script.getAllScripts(model);
		}
	}
}