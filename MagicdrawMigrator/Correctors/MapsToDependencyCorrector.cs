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
	public class MapsToDependencyCorrector:MagicDrawCorrector
	{
		public MapsToDependencyCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections for «mapsTo» dependencies"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			//Get all the dependencies
			foreach(var mdDependency in magicDrawReader.allMapsToDependencies)
			{
				//check if the dependency already exists
				//first try to find it using the MD guid
				string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
												inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
															and tv.Property = 'md_guid'))
												where tv.VALUE = '"+mdDependency.md_guid+"'";
				//Ook testen of er nog geen relatie met dat stereotype bestaat.
	
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
					//tell the user
					EAOutputLogger.log(this.model,this.outputName
		                   	,string.Format("{0} Corrected «mapsTo» dependency between '{1}' and '{2}'"
		                  	,DateTime.Now.ToLongTimeString()
		                  	,dependency.source.name
		                  	,dependency.target.name)
					        ,((TSF_EA.ElementWrapper)dependency.source).id
		      			,LogTypeEnum.error);
				}

			}

			//Log Finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished corrections for «mapsTo» dependencies"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		
		}
	}
}
