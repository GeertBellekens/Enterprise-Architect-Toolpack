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
using System.Reflection;

namespace EAScriptAddin
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAScriptAddinAddinClass: EAAddinFramework.EAAddinBase
	{
		private const string menuMain = "-&Scripting Addin";
		private const string menuAbout = "&About EA Script Add-in";
		private UTF_EA.Model model;
		private List<Script> allScripts {get;set;}
		private List<ScriptFunction> _allFunctions;
		private List<ScriptFunction> allFunctions
		{
			get
			{
				if (this._allFunctions == null)
				{
					this._allFunctions = new List<ScriptFunction>();
					this.allScripts = Script.getAllScripts(model);
					foreach ( Script script in this.allScripts) 
					{
						this._allFunctions.AddRange(script.functions);
					}
				}
				return this._allFunctions;
			}
		}
		public EAScriptAddinAddinClass():base()
		{
			this.menuHeader = menuMain;
			this.menuOptions = new String[] {menuAbout};
		}
		public override void EA_FileOpen(EA.Repository Repository)
		{
			this.model = new UTF_EA.Model(Repository);
			
		}
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			switch (ItemName) {
				case menuAbout:
					this.test(Repository);
					break;
			}
		}
		
		public override bool EA_OnContextItemDoubleClicked(EA.Repository Repository, string GUID, EA.ObjectType ot)
		{
			object[] parameter = new object[]{GUID,ot};
			bool returnValue = false;
			MethodBase currentMethod = MethodBase.GetCurrentMethod();
			ScriptFunction function = this._allFunctions.Find (f => f.name == currentMethod.Name && f.numberOfParameters == parameter.Length);
			if (function != null)
			{
				object result = function.execute(parameter);
				if (result is bool)
				{
					returnValue = (bool)result;
				}
			}
			return returnValue;
		}
		void test(EA.Repository repository)
		{
			UTF_EA.Model model = new UTF_EA.Model(repository);
			List<Script> scripts = Script.getAllScripts(model);
			foreach ( Script script in scripts) 
			{
				foreach (ScriptFunction function in script.functions) 
				{
					object[] parameter = new object[]{"{D67CC034-D288-4935-B302-2049F5890DBD}"};
					function.execute(parameter);
				}	
			}
		}
	}
}