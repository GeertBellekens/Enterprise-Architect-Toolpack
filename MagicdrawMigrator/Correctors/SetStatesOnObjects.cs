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
	/// Description of SetStatesOnObjects.
	/// </summary>
	public class SetStatesOnObjects:MagicDrawCorrector
	{
		public SetStatesOnObjects(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
			// Look for all the classes under an activity
			
			// Change the type to Object
			
			// Set the state for that Object to the corresponding state in the MagicDrawReader
		}
	}
}
