using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MagicDrawCorrector.
	/// </summary>
	public abstract class MagicDrawCorrector
	{
		protected TSF_EA.Model model {get;set;}
		protected string mdZipPath {get;set;}
		protected TSF_EA.Package mdPackage{get;set;}
		protected MagicDrawCorrector(string mdZipPath, TSF_EA.Model model, TSF_EA.Package mdPackage)
		{
			this.mdZipPath = mdZipPath;
			this.model = model;
			this.mdPackage = mdPackage;
		}
		public abstract void correct();
	}
}
