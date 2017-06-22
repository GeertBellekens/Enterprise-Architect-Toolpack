using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDFragment.
	/// </summary>
	public class MDFragment
	{
		public string fragmentType {get;set;}
		public List<string> operandGuards {get;set;}
		public string mdID {get;set;}
		public string ownerMdID {get;set;}
		public MDFragment(string ownerMdID, string mdID, string fragmentType)
		{
			this.ownerMdID = ownerMdID;
			this.mdID = mdID;
			this.fragmentType = fragmentType;
			this.operandGuards = new List<string>();
		}
	}
}
