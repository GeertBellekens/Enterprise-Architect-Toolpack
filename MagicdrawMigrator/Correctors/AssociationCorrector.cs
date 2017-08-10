using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF.UmlToolingFramework.UML.Classes.Kernel;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;
using System.Xml;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of FixMultiplicities.
	/// </summary>
	public class AssociationCorrector:MagicDrawCorrector
	{
		public AssociationCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		
		}
			
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting to correct Associations"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			// get all the associations
			foreach (MDAssociation mdAssociation in magicDrawReader.allAssociations.Values) 
			{
				//first find the corresponding association in the EA model
				var eaAssociation = findCorrespondingAssociation(mdAssociation);
				
				if (eaAssociation != null)
				{
					//fix navigability
					this.fixNavigability(eaAssociation);
					//check first if the multiplicity is to be corrected
					if (!mdAssociation.source.lowerBound.Equals(eaAssociation.sourceEnd.lower.ToString())
					    || !mdAssociation.source.upperBound.Equals(eaAssociation.sourceEnd.upper.ToString())
					    || !mdAssociation.target.lowerBound.Equals(eaAssociation.targetEnd.lower.ToString())
					    || !mdAssociation.target.upperBound.Equals(eaAssociation.targetEnd.upper.ToString()))

					//actually fix the multiplicities
					try
					{
						bool updatedMultiplicity = false;
						//set source (only if not both empty)
						if (! string.IsNullOrEmpty(mdAssociation.source.lowerBound)
						    && ! string.IsNullOrEmpty(mdAssociation.source.upperBound))
						{
							var sourceMultiplicity = new TSF_EA.Multiplicity(mdAssociation.source.lowerBound, mdAssociation.source.upperBound);
							eaAssociation.sourceEnd.multiplicity = sourceMultiplicity;
							updatedMultiplicity = true;
						}
						//set target (only if not both empty)
						if (! string.IsNullOrEmpty(mdAssociation.target.lowerBound)
						    && ! string.IsNullOrEmpty(mdAssociation.target.upperBound))
						{
							var targetMultiplicity = new TSF_EA.Multiplicity(mdAssociation.target.lowerBound, mdAssociation.target.upperBound);
							eaAssociation.targetEnd.multiplicity = targetMultiplicity;
							updatedMultiplicity = true;
						}
						//tell the user we have updated the multiplicities
						if (updatedMultiplicity)
						{
							//save the association
							eaAssociation.save();
							//let the user know
							EAOutputLogger.log(this.model,this.outputName
			                   ,string.Format("{0} Corrected multiplicity of association between '{1}' and '{2}'"
			                                  ,DateTime.Now.ToLongTimeString()
			                                 ,eaAssociation.source.name
			                                 ,eaAssociation.target.name)
							  ,((TSF_EA.ElementWrapper)eaAssociation.source).id
			                  ,LogTypeEnum.log);
						}
					}
					catch (Exception e)
					{
						//tell the user we could not fix the multiplicity
						EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Could not update multiplicities between '{1}' and '{2}' because of error '{3}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                 ,eaAssociation.source.name
		                                 ,eaAssociation.target.name
		                                 , e.GetType().Name)
						  ,((TSF_EA.ElementWrapper)eaAssociation.source).id
		                  ,LogTypeEnum.error);
						//also log the complete error
						Logger.logError(string.Format("Exception '{0}' with message '{1}' occurred at stacktrace: {2}"
						                              ,e.GetType().Name
						                              ,e.Message
						                              ,e.StackTrace));
					}
				}
				
			}
								
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting to correct Associationss"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
		
		/// <summary>
		/// if the association is between two classes, and one of the end is an aggregation or composite then the other side should be navigable
		/// </summary>
		/// <param name="eaAssociation">the association to change</param>
		void fixNavigability(TSF_EA.Association eaAssociation)
		{
			var sourceEnd = eaAssociation.sourceEnd;
			var targetEnd = eaAssociation.targetEnd;
			bool navigabilityUpdated = false;
			if (sourceEnd.type is UML.Classes.Kernel.Class
			   && targetEnd.type is UML.Classes.Kernel.Class)
			{
				if (sourceEnd.aggregation != AggregationKind.none)
				{
					if (!targetEnd.isNavigable)
					{
						targetEnd.isNavigable = true;
						navigabilityUpdated = true;
					}
					if (sourceEnd.isNavigable)
					{
						sourceEnd.isNavigable = false;
						navigabilityUpdated = true;
					}
				}
				if (targetEnd.aggregation != AggregationKind.none)
				{
					if (!sourceEnd.isNavigable)
					{
						sourceEnd.isNavigable = true;
						navigabilityUpdated = true;
					}
					if (targetEnd.isNavigable)
					{
						targetEnd.isNavigable = false;
						navigabilityUpdated = true;
					}
				}
				//save if needed
				if (navigabilityUpdated)
				{
					//save the association
					eaAssociation.save();
					//let the user know
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Corrected navigability of association between '{1}' and '{2}'"
	                                  ,DateTime.Now.ToLongTimeString()
	                                 ,eaAssociation.source.name
	                                 ,eaAssociation.target.name)
					  ,((TSF_EA.ElementWrapper)eaAssociation.source).id
	                  ,LogTypeEnum.log);
				}
			}
		}
		private TSF_EA.Association findCorrespondingAssociation(MDAssociation mdAssociation)
		{
			//first try to find it using the MD guid
			string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
											inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
														and tv.Property = 'md_guid'))
											where tv.VALUE = '"+mdAssociation.md_guid+"'";

			TSF_EA.Association correspondingAssociation = this.model.getRelationsByQuery(sqlGetExistingRelations).FirstOrDefault() as TSF_EA.Association;
			//if not found then we look for associations that 
			// - are between the same two elements
			// - have the same rolenames
			if (correspondingAssociation == null)
			{	
				var sourceElement = getElementByMDid(mdAssociation.source.endClassID);
				var targetElement = getElementByMDid(mdAssociation.target.endClassID);
				if (sourceElement != null && targetElement != null)
				{
					//first set the first part:
					string sqlGetCorrespondingAssociation = 
						@"select c.Connector_ID from t_connector c
						where c.Connector_Type in ('Association', 'Aggregation')
						and c.Start_Object_ID = " + sourceElement.id + Environment.NewLine +
						"and c.End_Object_ID = " + targetElement.id ;
					if (!string.IsNullOrEmpty(mdAssociation.target.name))
					{
						//target role is filled in
						sqlGetCorrespondingAssociation += @" 
						and c.DestRole = '"+ mdAssociation.target.name +"'";
					}
					if (!string.IsNullOrEmpty(mdAssociation.source.name))
					{
						//source role is filled in
						sqlGetCorrespondingAssociation += @" 
						and c.SourceRole = '"+ mdAssociation.source.name +"'";
					}
					//add the part checking for the md_guid
					sqlGetCorrespondingAssociation += @" 
					and not exists 
					(select tv.PropertyID from t_connectortag tv
					where tv.ElementID = c.Connector_ID
					and tv.Property = 'md_guid'
					and tv.VALUE is not null)";
					correspondingAssociation = this.model.getRelationsByQuery(sqlGetCorrespondingAssociation).FirstOrDefault() as TSF_EA.Association;
					//if we find the association we set the md_guid tagged value
					if (correspondingAssociation != null) correspondingAssociation.addTaggedValue("md_guid",mdAssociation.md_guid);					
				}
			}
			return correspondingAssociation;
		}
		
		

		
	}
}
