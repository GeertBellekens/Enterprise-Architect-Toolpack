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
		public MDAssociation(MDAssociationEnd source, MDAssociationEnd target, string md_guid)
		{
			this.source = source;
			this.target = target;
			this.md_guid = md_guid;
		}
		public MDAssociation()
		{
			
		}
		public MDAssociationEnd source {get;set;}
		public MDAssociationEnd target {get;set;}
		public string name {get;set;}
		public string stereotype {get;set;}
		public string md_guid {get;set;}
		public bool isCrossMDZip {get;set;}
	}
}
