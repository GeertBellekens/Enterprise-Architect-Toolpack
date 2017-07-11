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
                   	,string.Format("{0} Get association with ownedEnds '{1}' and '{2}' from source files"
                  	,DateTime.Now.ToLongTimeString()
                  	,mdAssociation.ownedEnd_1
                  	,mdAssociation.ownedEnd_2)
                    	
       			,0
      			,LogTypeEnum.log);
				
				//Get the EA actors and use cases
				var ownedEnds_1 = this.model.getElementWrappersByQuery(@"select so.[Object_ID]
																			from (t_object so
																			inner join [t_objectproperties] sop
																			on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdAssociation.ownedEnd_1 +"'))");
				
				
				var ownedEnds_2 = this.model.getElementWrappersByQuery(@"select so.[Object_ID]
																			from (t_object so
																			inner join [t_objectproperties] sop
																			on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdAssociation.ownedEnd_2 +"'))");
				
				
				if (ownedEnds_1 != null
			        && ownedEnds_1.Any()
			        && ownedEnds_1[0] != null
			        && ownedEnds_2 != null
			        && ownedEnds_2.Any()
			        && ownedEnds_2[0] != null)
				{
					//Create the new association
				    EAOutputLogger.log(this.model,this.outputName
	                   	,string.Format("{0} Create new association between '{1}' and '{2}'"
	                  	,DateTime.Now.ToLongTimeString()
	                  	,ownedEnds_1[0].name
	                  	,ownedEnds_2[0].name)
					,ownedEnds_1[0].id
	      			,LogTypeEnum.log);
					
					
					TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(ownedEnds_1[0], string.Empty);
					newAssociation.target = ownedEnds_2[0];
					newAssociation.addStereotype(this.model.factory.createStereotype(newAssociation,"participates"));
					newAssociation.save();
				}
				else
				{
					EAOutputLogger.log(this.model,this.outputName
	                   	,string.Format("{0} Could not create association between '{1}' and '{2}'"
	                  	,DateTime.Now.ToLongTimeString()
	                  	,mdAssociation.ownedEnd_1
	                  	,mdAssociation.ownedEnd_2)
	                    	
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
