using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;
using System.Xml;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Check if association between actor and use case are correctly displayed and if the stereotype 
	/// 'participates' is correctly applied onto the association
	/// </summary>
	public class AddParticipationAssociations:MagicDrawCorrector
	{
		public AddParticipationAssociations(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			
		}
	}
}
