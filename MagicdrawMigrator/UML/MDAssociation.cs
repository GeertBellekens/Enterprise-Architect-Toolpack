using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDAssociation.
	/// </summary>
	public class MDAssociation
	{
		public MDAssociation(MDAssociationEnd source, MDAssociationEnd target)
		{
			this.source = source;
			this.target = target;
		}
		public MDAssociation()
		{
			
		}
		public MDAssociationEnd source {get;set;}
		public MDAssociationEnd target {get;set;}
		public string name {get;set;}
		public string stereotype {get;set;}
		public string actor {get;set;}
		public string usecase {get;set;}
	}
}
