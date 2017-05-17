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
		protected MagicDrawReader magicDrawReader {get;set;}
		protected TSF_EA.Package mdPackage{get;set;}
		protected MagicDrawCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage)
		{
			this.magicDrawReader = magicDrawReader;
			this.model = model;
			this.mdPackage = mdPackage;
		}
		public abstract void correct();
	}
}
