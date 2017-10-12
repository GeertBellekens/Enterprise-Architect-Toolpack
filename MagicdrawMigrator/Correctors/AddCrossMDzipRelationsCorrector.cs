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
	/// Description of AddCrossMDzipRelationsCorrector.
	/// </summary>
	public class AddCrossMDzipRelationsCorrector:MagicDrawCorrector
	{
		public AddCrossMDzipRelationsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
				EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Starting corrections for cross MDzip relations"
	                          ,DateTime.Now.ToLongTimeString())
	           ,0
	          ,LogTypeEnum.log);

			//loop all simple element to element cross mdzip relations
			foreach (var crossRelation in magicDrawReader.allCrossMDzipRelations) 
			{
				//check if the relation doesn't exist yet
				string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
												inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
															and tv.Property = 'md_guid'))
												where tv.VALUE = '"+crossRelation.Key+"'";
				var newRelation = this.model.getRelationsByQuery(sqlGetExistingRelations).FirstOrDefault();
				
			
				
				if (newRelation == null)
				{
					MDElementRelation relation = crossRelation.Value;
					//find source
					var source = this.getElementByMDid(relation.sourceMDGUID);
					//find target
					var target = this.getElementByMDid(relation.targetMDGUID);
					//create relation
					if (source != null 
					    && target != null)
					{
						//create the actual relation
						newRelation = this.model.factory.createNewElement(source,relation.name,relation.relationType) as TSF_EA.ConnectorWrapper;
						if (newRelation != null)
						{
							newRelation.target = target;
							newRelation.save();
							//save md_guid tag
							newRelation.addTaggedValue("md_guid",crossRelation.Key);
							//tell the user what is happening
							EAOutputLogger.log(this.model,this.outputName
							,string.Format("{0} Created relation of type {1} between '{2}' and '{3}'"
							              ,DateTime.Now.ToLongTimeString()
							              ,relation.relationType
							              ,source.name
							              ,target.name)
							,source.id
							,LogTypeEnum.log);
							
						}
						else
						{
							//report the fact that we could not create the relation
							EAOutputLogger.log(this.model,this.outputName
							,string.Format("{0} Could not create relation of type {1} between '{2}' and '{3}'"
							              ,DateTime.Now.ToLongTimeString()
							              ,relation.relationType
							              ,source.name
							              ,target.name)
							,source.id
							,LogTypeEnum.error);
						}
					}
				}
				//save md_guid tag
				if (newRelation != null) newRelation.addTaggedValue("md_guid",crossRelation.Key);
			}
			

			//loop all cross MDzip Associations
			foreach (var mdCrossAssocation in magicDrawReader.allCrossMDzipAssociations) 
			{
				//check if the relation doesn't exist yet
				string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
												inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
															and tv.Property = 'md_guid'))
												where tv.VALUE = '"+mdCrossAssocation.Key+"'";

				TSF_EA.Association newCrossAssociation = this.model.getRelationsByQuery(sqlGetExistingRelations).FirstOrDefault() as TSF_EA.Association;
				if (newCrossAssociation == null)
				{
					var mdAssociation = mdCrossAssocation.Value;
					//get the source element
					var source = this.getElementByMDid(mdAssociation.source.endClassID);
					//get the target element
					var target = this.getElementByMDid(mdAssociation.target.endClassID);
					if (source != null 
					    && target != null)
					{
						//create the actual association
						newCrossAssociation = this.model.factory.createNewElement<TSF_EA.Association>(source,string.Empty);
						//set source end properties
						setEndProperties(newCrossAssociation.sourceEnd, mdAssociation.source);
						//set target end properties
						setEndProperties(newCrossAssociation.targetEnd, mdAssociation.target);
						//set target class
						newCrossAssociation.target = target;
						//set navigability
						newCrossAssociation.targetEnd.isNavigable = true;
						//save the association						
						newCrossAssociation.save();
						//tell the user what is happening
						EAOutputLogger.log(this.model,this.outputName
						,string.Format("{0} Created association between '{1}' and '{2}'"
						              ,DateTime.Now.ToLongTimeString()
						              ,source.name
						              ,target.name)
						,source.id
						,LogTypeEnum.log);
					}
					else
					{
						//report the fact that we could not create the relation
						EAOutputLogger.log(this.model,this.outputName
						,string.Format("{0} Could not create association between '{1}' and '{2}'"
						              ,DateTime.Now.ToLongTimeString()
						              ,source.name
						              ,target.name)
						,source.id
						,LogTypeEnum.error);
					}
				}
				//set the md_guid tagged value, also on existing associations
				if (newCrossAssociation != null) newCrossAssociation.addTaggedValue("md_guid",mdCrossAssocation.Key);
			}
			EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Finished corrections for cross MDzip relations"
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

		#endregion
	}
}
