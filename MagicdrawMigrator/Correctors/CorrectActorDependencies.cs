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
	/// In some cases the dependencies between the actors is missing.
	///
	///
	/// </summary>
	public class CorrectActorDependencies:MagicDrawCorrector
	{
		public CorrectActorDependencies(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections for actor dependencies'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			//Get all the dependencies
			
			//Get all the actors with the stereotypes 'BusinessPartners' 'Harmonized_Role' and 'AuthorizedRole'
			//_1d100e3_1078391217020_167244_13292
			
			//Get the dependencies between the actors
			
			//Create the dependencies in EA
		}
	}
}
