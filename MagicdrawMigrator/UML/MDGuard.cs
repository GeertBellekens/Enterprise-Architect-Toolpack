using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDGuard.
	/// </summary>
	public class MDGuard
	{
		public string guardCondition {get;set;}
		public string sourceMdGuid {get;set;}
		public string targetMdGuid {get;set;}
		
		public MDGuard(string guardCondition, string sourceMdGuid, string targetMdGuid)
		{
			this.guardCondition = guardCondition;
			this.sourceMdGuid = sourceMdGuid;
			this.targetMdGuid = targetMdGuid;
		}
	}
}
