using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDElementRelation.
	/// </summary>
	public class MDElementRelation
	{
		public string sourceMDGUID {get;set;}
		public string targetMDGUID {get;set;} 
		public string relationType {get;set;}
		public string name {get;set;}
		public MDElementRelation(string sourceMDGUID, string targetMDGUID, string relationType)
		{
			this.sourceMDGUID = sourceMDGUID;
			this.targetMDGUID = targetMDGUID;
			this.relationType = relationType;
		}
	}
}
