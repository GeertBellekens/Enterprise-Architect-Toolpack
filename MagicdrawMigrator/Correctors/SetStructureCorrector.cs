using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Sets the structure of the model according to the original Magicdraw Model
	/// </summary>
	public class SetStructureCorrector:MagicDrawCorrector
	{
		public SetStructureCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
