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
		public string note_Id {get;set;}
		
		public MDNote(string note_Id, string text,string linkedElement)
		{
			this.text = text;
			this.linkedElement = linkedElement;
			this.note_Id = note_Id;
		}
		

	}
}
