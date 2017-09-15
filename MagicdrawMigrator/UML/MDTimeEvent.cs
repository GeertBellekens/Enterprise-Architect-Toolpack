using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDTimeEvent.
	/// </summary>
	public class MDTimeEvent
	{
		
		public string mdGuid {get;set;}
		public string value {get;set;}
		
		public MDTimeEvent(string mdGuid, string value)
		{
			this.mdGuid = mdGuid;
			this.value = value;
		}
		
		
	}
}
