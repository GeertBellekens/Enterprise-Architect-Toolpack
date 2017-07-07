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
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections <<participates>> associations'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			
			foreach(var mdAssociation in magicDrawReader.allAssociations)
			{
				//Logging	                               
				EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Get association with actor '{1}' and use case '{2}' from source files"
                  	,DateTime.Now.ToLongTimeString()
                  	,mdAssociation.actor
                  	,mdAssociation.usecase)
                    	
       			,0
      			,LogTypeEnum.log);
				
				//Get the EA actors and use cases
				var actors = this.model.getElementWrappersByQuery(@"select so.[Object_ID]
																			from (t_object so
																			inner join [t_objectproperties] sop
																			on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdAssociation.actor +"'))");
				
				
				var usecases = this.model.getElementWrappersByQuery(@"select so.[Object_ID]
																			from (t_object so
																			inner join [t_objectproperties] sop
																			on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdAssociation.usecase +"'))");
				
				
				if (actors != null
			        && actors.Any()
			        && actors[0] != null
			        && usecases != null
			        && usecases.Any()
			        && usecases[0] != null)
				{
					//Create the new association
				    EAOutputLogger.log(this.model,this.outputName
	                   	,string.Format("{0} Create new association with actor '{1}' and usecase '{2}'"
	                  	,DateTime.Now.ToLongTimeString()
	                  	,actors[0].name
	                  	,usecases[0].name)
					,actors[0].id
	      			,LogTypeEnum.log);
					
					
					TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(actors[0], string.Empty);
					newAssociation.target = usecases[0];
					newAssociation.addStereotype(this.model.factory.createStereotype(newAssociation,"participates"));
					newAssociation.save();
				}
				else
				{
					EAOutputLogger.log(this.model,this.outputName
	                   	,string.Format("{0} Could not create association between actor with md_guid '{1}' and usecase '{2}'"
	                  	,DateTime.Now.ToLongTimeString()
	                  	,mdAssociation.actor
	                  	,mdAssociation.usecase)
	                    	
	       			,0
	      			,LogTypeEnum.error);
				}
				
			}
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished correct <<participates>> associations"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
	}
}
