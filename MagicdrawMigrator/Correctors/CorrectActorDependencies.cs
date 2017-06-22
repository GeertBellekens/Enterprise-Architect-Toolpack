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
			foreach(var mdDependency in magicDrawReader.allDependencies)
			{
				
				//logging	                               
				EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Get MD dependency with source '{1}' and target '{2}' from source files"
                  	,DateTime.Now.ToLongTimeString()
                  	,mdDependency.Key
                  	,mdDependency.Value)
                    	
       			,0
      			,LogTypeEnum.log);
				
				//get the ea guid from the md guid
				// start object guid & end object guid
				// source _17_0_2_2_b9402f1_1364316968995_855441_30725
				// target _9_0_2_f54035b_1115716788113_931934_1206 

			    // BusinessPartner -> Harmonized_Role  & Harmonized_Role -> AutorizedRole
			    KeyValuePair<string,string> objectIdGuidSource = this.model.getObjectIdAndGuid(@"select so.[ea_guid], so.[Object_ID]
																								from (t_object so
																								inner join [t_objectproperties] sop
																								on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdDependency.Key +"'))");
			    
			    KeyValuePair<string,string> objectIdGuidTarget = this.model.getObjectIdAndGuid(@"select so.[ea_guid], so.[Object_ID]
																								from (t_object so
																								inner join [t_objectproperties] sop
																								on (so.[Object_ID] = sop.[Object_ID] and sop.VALUE = '" + mdDependency.Value +"'))");
			    
			    //Create the new dependency
			    EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Create new dependency with source '{1}' and target '{2}'"
                  	,DateTime.Now.ToLongTimeString()
                  	,objectIdGuidSource.Value
                  	,objectIdGuidTarget.Value)
                    	
       			,0
      			,LogTypeEnum.log);
				
			    
			    TSF_EA.Dependency newDependency = this.model.factory.createNewElement<TSF_EA.Dependency>(this.model.getElementByGUID(objectIdGuidSource.Value), string.Empty);
			    newDependency.target = this.model.getElementByGUID(objectIdGuidTarget.Value);
			   
			
			    newDependency.addStereotype(this.model.factory.createStereotype(newDependency,"mapsTo"));
			   
			    newDependency.save();
				
			 
			    
				
			}
			
			
			
					
			
			//Log Finished
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished correct actor dependencies'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		
		}
	}
}
