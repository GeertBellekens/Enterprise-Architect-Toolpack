using System.Collections.Generic;
using System.Linq;
using System;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDAttribute.
	/// </summary>
	public class MDAttribute
	{
		string _name;
		string _mdGuid;
		string _mdParentGuid;
		string _guid;
		string _parentGuid;
			
			
		public MDAttribute(string name, string mdGuid, string mdParentGuid, string guid, string parentGuid)
		{
			this.name = name;
			this.mdGuid = mdGuid;
			this.mdParentGuid = mdParentGuid;
			this.guid = guid;
			this.parentGuid = parentGuid;
			
		}
		
		public string parentGuid
		{
			get
			{
				return this._parentGuid;
			}
			set
			{
				this.parentGuid = value;
			}
		}
		
		public string guid
		{
			get
			{
				return this._guid;
			}
			set
			{
				this.guid = value;
			}
		}
		
		public string mdParentGuid
		{
			get
			{
				return this._mdParentGuid;
			}
			set
			{
				this.mdParentGuid = value;
			}
		}
		
		public string name
		{
			get
			{
				return this._name;
			}
			set
			{
				this.name = value;
			}
		}
		
		public string mdGuid
		{
			get
			{
				return this._mdGuid;
			}
			set
			{
				this.mdGuid = value;
			}
		}
	}
}
