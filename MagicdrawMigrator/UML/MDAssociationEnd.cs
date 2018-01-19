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
        public int sequenceKey { get; set; }
		string _lowerBound;
		public string lowerBound 
		{
			get 
			{
				return _lowerBound;
			}
			set 
			{
				_lowerBound = value.Replace("(Unspecified)",string.Empty);
			}
		}
		string _upperBound;
		public string upperBound {
			get {
				return _upperBound;
			}
			set 
			{
				_upperBound = value.Replace("(Unspecified)",string.Empty);
			}
		}
		public string aggregationKind{get;set;}
		public string endClassID{get;set;}
		public bool isNavigable{get;set;}
		public bool hasForeignType {get;set;}
	}
}
