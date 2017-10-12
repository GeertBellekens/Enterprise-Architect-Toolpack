using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDDiagram.
	/// </summary>
	public class MDDiagram
	{
		public string name {get;set;}
		List<MDDiagramObject> _diagramObjects;
		List<MDNote> _diagramNotes;
		public string id {get;set;}
		
		public MDDiagram(string name)
		{
			this.name = name;
			_diagramObjects = new List<MDDiagramObject>();
			_diagramNotes = new List<MDNote>();
			
		}
		
		public List<MDDiagramObject> diagramObjects 
		{
			get 
			{
				return _diagramObjects;
			}
			set 
			{
				_diagramObjects = value;
			}
		}
		
		public List<MDNote> diagramNotes
		{
			get 
			{
				return _diagramNotes;
			}
			set 
			{
				_diagramNotes = value;
			}
		}
		
		public void addDiagramObject(MDDiagramObject diagramObject)
		{
			this.diagramObjects.Add(diagramObject);
		}
		
		public void addDiagramNote(MDNote diagramNote)
		{
			this.diagramNotes.Add(diagramNote);
		}
		
	}
}
