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
		
		#region Add-in specific operations
		/// <summary>
		/// All available functions in the scripts of the model
		/// </summary>
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
		/// <summary>
		/// constructor
		/// </summary>
		public EAScriptAddinAddinClass():base()
		{
			this.menuHeader = menuMain;
			this.menuOptions = new String[] {menuAbout};
		}
		/// <summary>
		/// executes the scriptfunctions with the given functionName and returns the boolean result of the function
		/// </summary>
		/// <param name="functionName">the name of the function to call</param>
		/// <param name="parameters">the parameters to pass to the function</param>
		/// <returns>the object returned by the scriptfunction</returns>		
		private bool callBooleanFunctions(string functionName, object[] parameters)
		{
			bool returnValue = false;
			//call the object function method
			object result = this.callObjectFunctions(functionName, parameters);
			//if the returnvalue is a booolean then we return that.
			if (result is bool)
			{
				returnValue = (bool)result;
			}
			return returnValue;
		}
		
		/// <summary>
		/// executes the scriptfunctions with the given functionName and returns wathever object is being returned by the scriptfunction
		/// </summary>
		/// <param name="functionName">the name of the function to call</param>
		/// <param name="parameters">the parameters to pass to the function</param>
		/// <returns>the object returned by the scriptfunction</returns>
		private object callObjectFunctions(string functionName, object[] parameters)
		{
			object returnValue = null;
			List<ScriptFunction> functions = this.allFunctions.FindAll(f => f.name == functionName && f.numberOfParameters == parameters.Length);
			foreach (ScriptFunction function in functions) 
			{
				if (function != null)
				{
					object result = function.execute(parameters);
					if (result != null)
					{
						returnValue = result;
					}
				}
			}
			return returnValue;
		}
		#endregion
		#region EA Add-In Events
		/// <summary>
		/// need to make sure we use the right model
		/// </summary>
		/// <param name="Repository"></param>
		public override void EA_FileOpen(EA.Repository Repository)
		{
			//add-in part
			this.model = new UTF_EA.Model(Repository);
			this._allFunctions = null;
			//call scriptfunctions
			//TODO
		}
		/// <summary>
		/// react to a menu click
		/// </summary>
		/// <param name="Repository"></param>
		/// <param name="MenuLocation"></param>
		/// <param name="MenuName"></param>
		/// <param name="ItemName"></param>
		public override void EA_MenuClick(EA.Repository Repository, string MenuLocation, string MenuName, string ItemName)
		{
			//add-in part
			switch (ItemName) {
				case menuAbout:
					//this.test(Repository);
					break;
			}
			//call scriptfunctions
			//TODO
		}

        /// <summary>
        /// EA_OnContextItemDoubleClicked notifies Add-Ins that the user has double-clicked the item currently in context.
        /// This event occurs when a user has double-clicked (or pressed [Enter]) on the item in context, either in a diagram or in the Project Browser. Add-Ins to handle events can subscribe to this broadcast function.
        /// Also look at EA_OnContextItemChanged and EA_OnNotifyContextItemModified.
        /// </summary>
        /// <param name="Repository">An EA.Repository object representing the currently open Enterprise Architect model.
        /// Poll its members to retrieve model data and user interface status information.</param>
        /// <param name="GUID">Contains the GUID of the new context item. 
        /// This value corresponds to the following properties, depending on the value of the ot parameter:
        /// ot (ObjectType)	- GUID value
        /// otElement  		- Element.ElementGUID
        /// otPackage 		- Package.PackageGUID
        /// otDiagram		- Diagram.DiagramGUID
        /// otAttribute		- Attribute.AttributeGUID
        /// otMethod		- Method.MethodGUID
        /// otConnector		- Connector.ConnectorGUID
        /// </param>
        /// <param name="ot">Specifies the type of the new context item.</param>
        /// <returns>Return True to notify Enterprise Architect that the double-click event has been handled by an Add-In. Return False to enable Enterprise Architect to continue processing the event.</returns>
		public override bool EA_OnContextItemDoubleClicked(EA.Repository Repository, string GUID, EA.ObjectType ot)
		{
			return this.callBooleanFunctions(MethodBase.GetCurrentMethod().Name,new object[]{GUID,ot});
		}
		#endregion
	}
}