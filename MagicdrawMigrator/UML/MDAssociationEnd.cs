using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDAssociationEnd.
	/// </summary>
	public class MDAssociationEnd
	{
		public MDAssociationEnd()
		{
		}
		public string name{get;set;}
		public string lowerBound{get;set;}
		public string upperBound{get;set;}
		public string aggregationKind{get;set;}
		public string endClassID{get;set;}
	}
}
