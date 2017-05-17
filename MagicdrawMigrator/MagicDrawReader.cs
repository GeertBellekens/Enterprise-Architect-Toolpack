using System.Collections.Generic;
using System.Linq;
using System;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MagicDrawReader.
	/// </summary>
	public class MagicDrawReader
	{
		string mdzipPath {get;set;}
		public MagicDrawReader(string mdzipPath)
		{
			this.mdzipPath = mdzipPath;
		}
	}
}
