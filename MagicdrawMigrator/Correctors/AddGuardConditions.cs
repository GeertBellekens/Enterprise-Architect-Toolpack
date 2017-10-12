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
	/// Description of AddGuardConditions.
	/// </summary>
	public class AddGuardConditions:MagicDrawCorrector
	{
		public AddGuardConditions(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting add guard conditions'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			//Add the guard conditions
			// PDATA2 For ControlFlow: Constraints/Guard property
			foreach (var mdGuardObject in magicDrawReader.allGuards) 
			{

				var sourceObject = getElementByMDid(mdGuardObject.sourceMdGuid);
				var targetObject = getElementByMDid(mdGuardObject.targetMdGuid);
				
				if (sourceObject != null && targetObject != null)
				{
					EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Add guard '{1}' between'{2}' and '{3}' "
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,mdGuardObject.guardCondition
		                                  ,sourceObject.name
		                                  ,targetObject.name)
		                             
		                   ,sourceObject.id
		                  ,LogTypeEnum.log);
				
					int startObjectId = sourceObject.id;
					int endObjectId = targetObject.id;
					string guardCondition = mdGuardObject.guardCondition;
					guardCondition = guardCondition.Replace("[",string.Empty);
					guardCondition = guardCondition.Replace("]",string.Empty);
					guardCondition = guardCondition.Trim();
					
					
					string sqlGetRelations = @"select con.[Connector_ID]
												from t_connector con
												where con.start_object_id = " + startObjectId + @"
												and con.end_object_id = " + endObjectId + @"
												and con.connector_type = 'ControlFlow'";
					
					
					var controlFlows = this.model.getRelationsByQuery(sqlGetRelations);
					
					var controlFlow = controlFlows.FirstOrDefault(x => string.IsNullOrEmpty(x.guardCondition));
					
					if (controlFlow != null)
					{
						controlFlow.guardCondition = guardCondition;
					}

				}
				
			
				
			}
			
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished add guard conditions"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			
			
		}
	}
}
