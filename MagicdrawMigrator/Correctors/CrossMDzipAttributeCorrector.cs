using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of CrossMDzipAttributeCorrector.
	/// </summary>
	public class CrossMDzipAttributeCorrector:MagicDrawCorrector
	{
		public CrossMDzipAttributeCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
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
