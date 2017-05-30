using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of FixCallBehaviorActionCorrector.
	/// </summary>
	public class FixCallBehaviorActionCorrector:MagicDrawCorrector
	{
		public FixCallBehaviorActionCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
			
		}
		#region implemented abstract members of MagicDrawCorrector
		
		public override void correct()
		{
		
		}
		#endregion
	}
}
