using System.Collections.Generic;
using System.Linq;
using System;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDAttribute.
	/// </summary>
	public class MDAttribute
	{

			
		public MDAttribute()
		{
			
		}
		
		public string name{get;set;}
		public string parentGuid{get;set;}
		public string guid{get;set;}
		public string mdParentGuid{get;set;}
		public string mdGuid{get;set;}
		
	}
}
