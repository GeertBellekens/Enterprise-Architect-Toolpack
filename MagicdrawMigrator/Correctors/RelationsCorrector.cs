using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;

namespace MagicdrawMigrator
{
	/// <summary>
	/// This corrector corrects all the types of relations, cross mdzip or not
	/// </summary>
	public class RelationsCorrector:MagicDrawCorrector
	{
		public RelationsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
				EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Starting corrections for all the relations"
	                          ,DateTime.Now.ToLongTimeString())
	           ,0
	          ,LogTypeEnum.log);
			//Dependency, Usage, Realization --> mogen samen genomen worden
			
			//Abstractions
			this.correctAbstractions();
			
			//Associations
			//this.correctAssociations();
			
			//ControlFlows
			this.correctControlFlows();
			
			//Dependencies
			this.correctDependencies();
			
			
			//Finished
				EAOutputLogger.log(this.model,this.outputName
           ,string.Format("{0} Finished corrections for the all the relations"
                          ,DateTime.Now.ToLongTimeString())
           ,0
          ,LogTypeEnum.log);

		}
		
		void correctAbstractions()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the abstractions"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//First get all the abstractions
			
			
			
			EAOutputLogger.log(this.model,this.outputName
           ,string.Format("{0} Finished corrections for the abstractions"
                          ,DateTime.Now.ToLongTimeString())
           ,0
          ,LogTypeEnum.log);
			
		}
		
		void correctAssociations()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the associations"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//First get all the associations
			foreach (var mdAssociation in magicDrawReader.allAssociations)
			{
				//Check if the association already exists
				if(!exists(mdAssociation.Key,mdAssociation.Value.source.endClassID, mdAssociation.Value.target.endClassID,"Association",mdAssociation.Value.stereotype))
				{
					//It it does not exist -> create
					var sourceElement = this.getElementByMDid(mdAssociation.Value.source.endClassID);
					var targetElement = this.getElementByMDid(mdAssociation.Value.target.endClassID);
					
					if (sourceElement != null && targetElement != null)
					{
						//create the actual association
						TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(sourceElement,string.Empty);	
						//set source end properties
						setEndProperties(newAssociation.sourceEnd, mdAssociation.Value.source);
						//set target end properties
						setEndProperties(newAssociation.targetEnd, mdAssociation.Value.target);
						//set the target end navigable by default --> always true?
						newAssociation.targetEnd.isNavigable = true;
						//set target class
						newAssociation.target = targetElement;
						//set the stereotype
						newAssociation.addStereotype(this.model.factory.createStereotype(newAssociation, mdAssociation.Value.stereotype));
						//save the new association
						newAssociation.save();
						//set the md_guid tagged value
						newAssociation.addTaggedValue("md_guid",mdAssociation.Key);
						
						//tell the user
						EAOutputLogger.log(this.model,this.outputName
		                   	,string.Format("{0} Corrected «"+mdAssociation.Value.stereotype+"» association between '{1}' and '{2}'"
		                  	,DateTime.Now.ToLongTimeString()
		                  	,newAssociation.source.name
		                  	,newAssociation.target.name)
					        ,((TSF_EA.ElementWrapper)newAssociation.source).id
		      			,LogTypeEnum.log);
					}
				}
				
			}
			
							EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the associations"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctControlFlows()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the controlFlows"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the controlFlows"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctDependencies()
		{
			
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the dependencies"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//First get al the dependencies
			foreach (var mdDependency in magicDrawReader.allDependencies)
			{
				
			}
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the dependencies"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
			
		}
		
		void correctExtensions()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the extensions"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the extensions"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctGeneralizations()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the generalizations"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the generalizations"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctInformationFlows()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the informationflows"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the informationflows"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctNoteLinks()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the notelinks"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the notelinks"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctObjectFlows()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the objectflows"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the objectflows"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctRealisations()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the realisations"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the realisations"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctSequences()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the sequences"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the sequences"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctStateFlows()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the stateflows"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the stateflows"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctUsages()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the usages"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the usages"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void correctUseCases()
		{
						EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for the usecases"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			
		
			EAOutputLogger.log(this.model,this.outputName
               ,string.Format("{0} Finished corrections for the usecases"
                              ,DateTime.Now.ToLongTimeString())
               ,0
              ,LogTypeEnum.log);
		}
		
		void setEndProperties (TSF_EA.AssociationEnd eaEnd, MDAssociationEnd mdEnd)
		{
			//set the aggregationKind
			if (mdEnd.aggregationKind == "shared")
			{
				eaEnd.aggregation = UML.Classes.Kernel.AggregationKind.shared;
			}
			else
			{
				eaEnd.aggregation = UML.Classes.Kernel.AggregationKind.none;
			}
			//set the name
			eaEnd.name = mdEnd.name;
			//set the multiplicity
			if (! string.IsNullOrEmpty(mdEnd.lowerBound)
			    && !string.IsNullOrEmpty(mdEnd.upperBound))
			{
				eaEnd.multiplicity = new TSF_EA.Multiplicity(mdEnd.lowerBound,mdEnd.upperBound);
			}
		}
		
		bool exists(string md_guid, string source, string target, string type, string stereotype)
		{
			//check if the relation already exists
			//first try to find it using the MD guid
			string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
								inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
											and tv.Property = 'md_guid'))
								where tv.VALUE = '"+md_guid+"'";
			
			var correspondingRelations = this.model.getRelationsByQuery(sqlGetExistingRelations).ToList();
			
			//if not found by mdGUID then find all associations between source and target
			if(!correspondingRelations.Any())
			{
				//find the corresponding association based on:
				//1.the source and target
				//2.the relation type
				//3.the stereotype
				
				sqlGetExistingRelations = @"select c.Connector_ID from ((((t_connector c
											inner join t_object obs on c.Start_Object_ID = obs.Object_ID)
											inner join t_objectproperties obstv on (obstv.Object_ID = obs.Object_ID
																					and obstv.Property = 'md_guid'))
											inner join t_object obe on c.End_Object_ID = obe.Object_ID)
											inner join t_objectproperties obetv on (obetv.Object_ID = obe.Object_ID
																					and obetv.Property = 'md_guid'))
											where
											c.Connector_Type = '"+ type +@"'
											and c.Stereotype = '"+ stereotype +@"'
											and obstv.Value = '"+ source +@"'
											and obetv.Value = '"+ target +"'";
				
				correspondingRelations = this.model.getRelationsByQuery(sqlGetExistingRelations).ToList();
			}
			if(!correspondingRelations.Any())
			{
				return false;
			}
			
			return true;
			
			
			
		}
	}
}
