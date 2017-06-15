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
	/// Description of CorrectStereotypesAndTaggedValues.
	/// </summary>
	public class CorrectStereotypesAndTaggedValues:MagicDrawCorrector
	{
		public CorrectStereotypesAndTaggedValues(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			// Packages: ABIE, BDT
			//{FDF1DB48-A330-4d1c-86EA-90DE44F7E5A1} Guid for an ABIE package
			
			// Classes: ABIE, BDT
			
			// Stereotype for attribute ABIE: BBIE
		}
	}
}
