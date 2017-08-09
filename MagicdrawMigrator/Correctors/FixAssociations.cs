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
	/// Fix the (Unspecified)..(Unspecified) multiplicities on associations by setting the cardinality field to NULL in the database.
	/// </summary>
	public class FixAssociations:MagicDrawCorrector
	{
		public FixAssociations(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting fix associations"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			
			
			this.model.executeSQL(@"update t_connector 
											set [DestCard] = NULL
											where [DestCard] = '(Unspecified)..(Unspecified)'");
			this.model.executeSQL(@"update t_connector 
											set [SourceCard] = NULL
											where [SourceCard] = '(Unspecified)..(Unspecified)'");
			
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished fix associations"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
	}
}
