
using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;

namespace EAImvertor
{
	/// <summary>
	/// Description of EAImvertorException.
	/// </summary>
	public class EAImvertorException
	{
		public string exceptionType {get;set;}
		public string guid {get;set;}
		public string step {get;set;}
		public string construct {get;set;}
		public string message {get;set;}
		private UTF_EA.Model _model;
		private UML.Extended.UMLItem _item = null;
		public EAImvertorException(UTF_EA.Model model, string exceptionType, string guid, string step, string construct, string message)
		{
			this._model = model;
			this.exceptionType = exceptionType;
			this.guid = guid;
			this.step = step;
			this.construct = construct;
			this.message = message;
		}
		public UML.Extended.UMLItem item
		{
			get
			{
				if (_item != null && this.guid.Length > 0)
				{
					_item = _model.getItemFromGUID(this.guid);
				}
				return item;
				
			}
		}
		
	}
}
