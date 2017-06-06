using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDDiagramObject.
	/// </summary>
	public class MDDiagramObject
	{
		string _geometry;
		public MDDiagramObject(string mdID, string geometry)
		{
			this.mdID = mdID;
			this.geometry = geometry;
		}
		public string mdID {get;set;}
		
		public string geometry {
			get {
				return _geometry;
			}
			set {
				_geometry = value;
				//set the individual parts
				int _x;
				int _y;
				int _width;
				int _height;
				var parts = _geometry.Split(',');
				x = parts.Count() >= 1 && int.TryParse(parts[0],out _x) ? _x : 0;
				y = parts.Count() >= 2 && int.TryParse(parts[1],out _y) ?  _y : 0;
				width = parts.Count() >= 3 && int.TryParse(parts[2],out _width) ? _width : 0;
				height = parts.Count() >= 4 && int.TryParse(parts[3],out _height) ? _height : 0;
			}
		}
		public int x {get;private set;}
		public int y {get;private set;}
		public int width {get;private set;}
		public int height {get;private set;}

	}
}
