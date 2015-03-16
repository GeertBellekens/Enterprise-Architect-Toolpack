/*
 * Created by SharpDevelop.
 * User: wij
 * Date: 15/03/2015
 * Time: 5:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Reflection;

using EAAddinFramework.EASpecific;

namespace EAAddinManager
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EAAddinManagerAddinClass: EAAddinFramework.EAAddinBase
	{
		public List<EAAddin> addins = new List<EAAddin>();
		
		public EAAddinManagerAddinClass():base()
		{
			EAAddinFramework.Utilities.Logger.log("EAAddinManagerAddinClass():base() executed");
			addins.Add(new EAAddinFramework.EASpecific.EAAddin( @"C:\Users\wij\Documents\BellekensIT\Development\Enterprise-Architect-Add-in-Framework\MyAddin\bin\Debug\MyAddin.dll"));
		}
		
		public override void EA_OnNotifyContextItemModified(EA.Repository Repository, string GUID, EA.ObjectType ot)
		{
			this.callmethods(MethodBase.GetCurrentMethod().Name, new object[]{Repository, GUID, ot});
		}
		/// <summary>
		/// executes the corresponding methods on the addins with the given metho name and returns result of the methods
		/// </summary>
		/// <param name="methodname">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private object callmethods(string methodname, object[] parameters)
		{
			object returnValue = null;
			foreach (EAAddin addin in this.addins) 
			{
				object result = addin.callmethod(methodname, parameters);
				if (result != null)
				{
					returnValue = result;
				}
			}
			return returnValue;
		}
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the boolean result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private bool callmethods(string methodname, object[] parameters, bool defaultValue)
		{
			bool returnValue = defaultValue;
			//call the object method method
			object result = this.callmethods(methodname, parameters);
			//if the returnvalue is a booolean then we return that.
			if (result is bool)
			{
				returnValue = (bool)result;
			}
			return returnValue;
		}
		
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the long result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private long callmethods(string methodname, object[] parameters, long defaultValue)
		{
			long returnValue = defaultValue;
			//call the object method method
			object result = this.callmethods(methodname, parameters);
			//if the returnvalue is a long then we return that.
			if (result is long)
			{
				returnValue = (long)result;
			}
			return returnValue;
		}
		
		/// <summary>
		/// executes the scriptmethods with the given methodName and returns the string result of the method
		/// </summary>
		/// <param name="methodName">the name of the method to call</param>
		/// <param name="parameters">the parameters to pass to the method</param>
		/// <returns>the object returned by the addinmethod</returns>		
		private string callmethods(string methodname, object[] parameters, string defaultValue)
		{
			string returnValue = defaultValue;
			//call the object method method
			object result = this.callmethods(methodname, parameters);
			//if the returnvalue is a booolean then we return that.
			if (result is string)
			{
				returnValue = (string)result;
			}
			return returnValue;
		}
		
	}
}