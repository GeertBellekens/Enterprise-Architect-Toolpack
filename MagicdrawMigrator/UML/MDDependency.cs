using System.Collections.Generic;
using System.Linq;
using System;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDDependency.
	/// </summary>
	public class MDDependency
	{

			
		public MDDependency()
		{
			
		}
		
		public string sourceGuid{get;set;}
		public string targetGuid{get;set;}
		public string sourceName{get;set;}
		public string targetName{get;set;}
		public string sourceParentGuid{get;set;}
		public string targetParentGuid{get;set;}
	}
}
