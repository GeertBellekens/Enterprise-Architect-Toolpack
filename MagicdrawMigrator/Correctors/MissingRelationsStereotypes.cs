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
	/// Description of MissingRelationsStereotypes.
	/// </summary>
	public class MissingRelationsStereotypes:MagicDrawCorrector
	{
		public MissingRelationsStereotypes(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			//fix the represents stereotypes
			//this.fixRepresents();
			
			//fix the use stereotypes
			//this.fixUses();
			
			//fix the participates stereotypes
			this.fixParticipates();
				
			//fix the mapsTo stereotypes
			this.fixMapsTos();
			
			//fix the extends stereotypes
			
			//fix asma stereotypes
			this.fixAsmas();
			
		}
		
		void fixRepresents()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for «represents» dependencies"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
		}
		
		void fixUses()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for «use» dependencies"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
		}
		
		void fixParticipates()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for «participates» associations"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//Get all the participates associations from the sourcefiles
			foreach(var participate in magicDrawReader.allParticipatesAssociations)
			{
				//check if the association already exists
				//first try to find it using the MD guid
				string sqlGetExistingAssociations = @"select c.Connector_ID from (t_connector c
								inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
											and tv.Property = 'md_guid'))
								where tv.VALUE = '"+participate.md_guid+"'";
				
				var correspondingAssociations = this.model.getRelationsByQuery(sqlGetExistingAssociations).OfType<TSF_EA.Association>().ToList();
				//if not found by mdGUID then find all associations between source and target
				if(!correspondingAssociations.Any())
				{
					//find the corresponding association based on:
					//1.the source & target md_guids
					//2.the connector type
					//3.the stereotype
					sqlGetExistingAssociations = @"select c.Connector_ID from ((((t_connector c
											inner join t_object obs on c.Start_Object_ID = obs.Object_ID)
											inner join t_objectproperties obstv on (obstv.Object_ID = obs.Object_ID
																					and obstv.Property = 'md_guid'))
											inner join t_object obe on c.End_Object_ID = obe.Object_ID)
											inner join t_objectproperties obetv on (obetv.Object_ID = obe.Object_ID
																					and obetv.Property = 'md_guid'))
											where
											c.Connector_Type = 'Association'
											and c.Stereotype = 'participates'
											and obstv.Value = '"+participate.source.endClassID +@"'
											and obetv.Value = '"+participate.target.endClassID +"'";
					
					correspondingAssociations = this.model.getRelationsByQuery(sqlGetExistingAssociations).OfType<TSF_EA.Association>().ToList();
				}
				if(!correspondingAssociations.Any())
				{
					//doesn't exists, we have to create it
					var sourceElement = this.getElementByMDid(participate.source.endClassID);
					var targetElement = this.getElementByMDid(participate.target.endClassID);
					if (sourceElement != null && targetElement != null)
					{
						//create the actual association
						TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(sourceElement,string.Empty);	
						//set source end properties
						setEndProperties(newAssociation.sourceEnd, participate.source);
						//set target end properties
						setEndProperties(newAssociation.targetEnd, participate.target);
						//set the target end navigable by default
						newAssociation.targetEnd.isNavigable = true;
						//set target class
						newAssociation.target = targetElement;
						correspondingAssociations.Add(newAssociation);
					}
					
					
				}
				//add the stereotype and md_guid tagged value
				foreach (var association in correspondingAssociations)
				{
						//set the stereotype
						association.addStereotype(this.model.factory.createStereotype(association,participate.stereotype));
						//save the new association
						association.save();
						//set the md_guid tagged value
						association.addTaggedValue("md_guid",participate.md_guid);
						
						
						//tell the user
						EAOutputLogger.log(this.model,this.outputName
		                   	,string.Format("{0} Corrected «participate» association between '{1}' and '{2}'"
		                  	,DateTime.Now.ToLongTimeString()
		                  	,association.source.name
		                  	,association.target.name)
					        ,((TSF_EA.ElementWrapper)association.source).id
		      			,LogTypeEnum.log);
				}

				
				
			}
		}
		
		void fixAsmas()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for «ASMA» associations"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//Get all the ASMA associations from the sourcefiles
			foreach (MDAssociation mdAssociation in magicDrawReader.allASMAAssociations) 
			{
				//check if the association already exists
				//firt try to find it using the MD guid
				string sqlGetExistingAssociations = @"select c.Connector_ID from (t_connector c
								inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
											and tv.Property = 'md_guid'))
								where tv.VALUE = '"+mdAssociation.md_guid+"'";
				
				var correspondingAssociations = this.model.getRelationsByQuery(sqlGetExistingAssociations).OfType<TSF_EA.Association>().ToList();
				//if not found by mdGUID then find all associations between source and target
				if(!correspondingAssociations.Any())
				{
					//find the corresponding association based on:
					//1.the source & target md_guids
					//2.the connector type
					//3.the stereotype
					sqlGetExistingAssociations = @"select c.Connector_ID from ((((t_connector c
											inner join t_object obs on c.Start_Object_ID = obs.Object_ID)
											inner join t_objectproperties obstv on (obstv.Object_ID = obs.Object_ID
																					and obstv.Property = 'md_guid'))
											inner join t_object obe on c.End_Object_ID = obe.Object_ID)
											inner join t_objectproperties obetv on (obetv.Object_ID = obe.Object_ID
																					and obetv.Property = 'md_guid'))
											where
											c.Connector_Type = 'Association'
											and c.Stereotype = 'ASMA'
											and obstv.Value = '"+mdAssociation.source.endClassID +@"'
											and obetv.Value = '"+mdAssociation.target.endClassID +"'";
					
					correspondingAssociations = this.model.getRelationsByQuery(sqlGetExistingAssociations).OfType<TSF_EA.Association>().ToList();
				}
				if(!correspondingAssociations.Any())
				{
					//doesn't exists, we have to create it
					var sourceElement = this.getElementByMDid(mdAssociation.source.endClassID);
					var targetElement = this.getElementByMDid(mdAssociation.target.endClassID);
					if (sourceElement != null && targetElement != null)
					{
						//create the actual association
						TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(sourceElement,string.Empty);	
						//set source end properties
						setEndProperties(newAssociation.sourceEnd, mdAssociation.source);
						//set target end properties
						setEndProperties(newAssociation.targetEnd, mdAssociation.target);
						//set the target end navigable by default
						newAssociation.targetEnd.isNavigable = true;
						//set target class
						newAssociation.target = targetElement;
						correspondingAssociations.Add(newAssociation);
					}
					
					
				}
				//add the stereotype and md_guid tagged value
				foreach (var association in correspondingAssociations)
				{
						//set the stereotype
						association.addStereotype(this.model.factory.createStereotype(association,mdAssociation.stereotype));
						//save the new association
						association.save();
						//set the md_guid tagged value
						association.addTaggedValue("md_guid",mdAssociation.md_guid);
						
						
						//tell the user
						EAOutputLogger.log(this.model,this.outputName
		                   	,string.Format("{0} Corrected «ASMA» association between '{1}' and '{2}'"
		                  	,DateTime.Now.ToLongTimeString()
		                  	,association.source.name
		                  	,association.target.name)
					        ,((TSF_EA.ElementWrapper)association.source).id
		      			,LogTypeEnum.log);
				}
			
			}
		}
		
		void fixMapsTos()
		{
			EAOutputLogger.log(this.model,this.outputName
                   ,string.Format("{0} Starting corrections for «mapsTo» dependencies"
                                  ,DateTime.Now.ToLongTimeString())
                   ,0
                  ,LogTypeEnum.log);
			
			//Get all the mapsTo dependencies from the sourcefiles
			foreach(var mdDependency in magicDrawReader.allMapsToDependencies)
			{
				//check if the dependency already exists
				//first try to find it using the MD guid
				string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
												inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
															and tv.Property = 'md_guid'))
												where tv.VALUE = '"+mdDependency.md_guid+"'";
			
	
				var correspondingDependencies = this.model.getRelationsByQuery(sqlGetExistingRelations).OfType<TSF_EA.Dependency>().ToList();
				//if not found by mdGUID then to find all dependencies between source and target
				if (! correspondingDependencies.Any())
				{
					//find the corresponding dependency based on the source and target md_guid's
					sqlGetExistingRelations = @"select c.Connector_ID from ((((t_connector c
											inner join t_object obs on c.Start_Object_ID = obs.Object_ID)
											inner join t_objectproperties obstv on (obstv.Object_ID = obs.Object_ID
																					and obstv.Property = 'md_guid'))
											inner join t_object obe on c.End_Object_ID = obe.Object_ID)
											inner join t_objectproperties obetv on (obetv.Object_ID = obe.Object_ID
																					and obetv.Property = 'md_guid'))
											where
											c.Connector_Type = 'Dependency'
											and c.Stereotype = 'mapsTo'
											and obstv.Value = '"+mdDependency.sourceMDGUID +@"'
											and obetv.Value = '"+mdDependency.targetMDGUID+"'";
					correspondingDependencies = this.model.getRelationsByQuery(sqlGetExistingRelations).OfType<TSF_EA.Dependency>().ToList();
				}
				if (!correspondingDependencies.Any())
				{
					//doesn't exists, we have to create it
					var sourceElement = this.getElementByMDid(mdDependency.sourceMDGUID);
					var targetElement = this.getElementByMDid(mdDependency.targetMDGUID);
					if (sourceElement != null 
					    && targetElement != null)
					{
						TSF_EA.Dependency newDependency = this.model.factory.createNewElement<TSF_EA.Dependency>(sourceElement, string.Empty);
						newDependency.target = targetElement;
						newDependency.targetEnd.isNavigable = true;
						correspondingDependencies.Add(newDependency);
					}
				}
				//add the stereotype and save			 	
				foreach (var dependency in correspondingDependencies) 
				{
					dependency.addStereotype(this.model.factory.createStereotype(dependency,"mapsTo"));
					dependency.save();
					
					//set the md_guid tagged value
					dependency.addTaggedValue("md_guid",mdDependency.md_guid);
					
					//tell the user
					EAOutputLogger.log(this.model,this.outputName
		                   	,string.Format("{0} Corrected «mapsTo» dependency between '{1}' and '{2}'"
		                  	,DateTime.Now.ToLongTimeString()
		                  	,dependency.source.name
		                  	,dependency.target.name)
					        ,((TSF_EA.ElementWrapper)dependency.source).id
		      			,LogTypeEnum.log);
				}

			}

			//Log Finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished corrections for «mapsTo» dependencies"
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
	}
}
