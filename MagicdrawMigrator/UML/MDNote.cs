using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDNote.
	/// </summary>
	public class MDNote
	{
		public string text {get;set;}
		public string linkedElement {get;set;}
		
		public MDNote(string text,string linkedElement)
		{
			this.text = text;
			this.linkedElement = linkedElement;
		}
		
		

	}
}
