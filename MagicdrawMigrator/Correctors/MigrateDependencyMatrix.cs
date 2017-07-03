using System.Collections;
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
	/// Description of MigrateDependencyMatrix.
	/// </summary>
	public class MigrateDependencyMatrix:MagicDrawCorrector
	{
		public MigrateDependencyMatrix(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
				/* Check how the Depencency matrix information can be transferred to EA. 
 				Since this seems to be linking attributes with other attributes or associations we 
				will probably need to use tagged values to create the links and specific SQL Searches 
				to get the information out of EA as well. 
				(to be check with Thibault if the information is present or not)*/
				
				EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections for relationship matrixes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
				
				
				foreach(var mdRowCO in magicDrawReader.allMatrixes)
				{
					
				}
				
//				foreach(var mdAttribute in magicDrawReader.allAttributes)
//				{
//						EAOutputLogger.log(this.model,this.outputName
//					                   	,string.Format("{0} Get attribute '{1}' with GUID '{2}'"
//	                                  	,DateTime.Now.ToLongTimeString()
//	                                 	,mdAttribute.name
//	                                	,mdAttribute.mdGuid)
//	                   ,0
//	                  ,LogTypeEnum.log);
//				}
				


		}
	}
}
