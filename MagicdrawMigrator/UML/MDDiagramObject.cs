using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDDiagramObject.
	/// </summary>
	public class MDDiagramObject
	{
		public MDDiagramObject(string mdID, string geometry)
		{
			this.mdID = mdID;
			this.geometry = geometry;
		}
		public string mdID {get;set;}
		public string geometry{get;set;}
	}
}
