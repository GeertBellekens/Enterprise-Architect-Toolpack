using System.Collections.Generic;
using System.Linq;
using System;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDConstraint.
	/// </summary>
	public class MDConstraint
	{
		string _name;
		string _body;
		string _language;
		public MDConstraint(string name, string body,string language)
		{
			this.name = name;
			this.body = body;
			this.language = language;
		}

		public string name 
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		public string body
		{
			get
			{
				return this._body;
			}
			set
			{
				this._body = value;
			}
		}
		public string language 
		{
			get 
			{
				return _language;
			}
			set 
			{
				_language = value;
			}
		}
	}
}
