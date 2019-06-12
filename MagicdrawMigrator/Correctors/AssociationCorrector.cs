using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF.UmlToolingFramework.UML.Classes.Kernel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;
using System.Xml;

namespace MagicdrawMigrator
{
    /// <summary>
    /// Description of FixMultiplicities.
    /// </summary>
    public class AssociationCorrector : MagicDrawCorrector
    {
        public AssociationCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage) : base(magicDrawReader, model, mdPackage)
        {

        }

        public override void correct()
        {
            EAOutputLogger.log(this.model, this.outputName
                       , string.Format("{0} Starting to correct Associations"
                                      , DateTime.Now.ToLongTimeString())
                       , 0
                       , LogTypeEnum.log);

            // get all the associations
            foreach (MDAssociation mdAssociation in magicDrawReader.allAssociations.Values)
            {
                //first find the corresponding association in the EA model
                var eaAssociation = findCorrespondingAssociation(mdAssociation);

                if (eaAssociation != null)
                {

                    //fix navigability
                    this.fixNavigability(eaAssociation);
                    //set the sequencingKey
                    this.setSequenceKey(mdAssociation, eaAssociation);
                    //correct the "(Unspecified)..(Unspecified)" multiplicities in the database
                    string sqlCorrectUnspecified = "update t_connector set DestCard = null where DestCard like '%unspecified%'";
                    this.model.executeSQL(sqlCorrectUnspecified);
                    sqlCorrectUnspecified = "update t_connector set SourceCard = null where SourceCard like '%unspecified%'";
                    this.model.executeSQL(sqlCorrectUnspecified);
                    //reload the association
                    eaAssociation = model.getRelationByGUID(eaAssociation.uniqueID) as TSF_EA.Association;
                    //check first if the multiplicity is to be corrected
                    if (!mdAssociation.source.lowerBound.Equals(eaAssociation.sourceEnd.lower.ToString())
                        || !mdAssociation.source.upperBound.Equals(eaAssociation.sourceEnd.upper.ToString())
                        || !mdAssociation.target.lowerBound.Equals(eaAssociation.targetEnd.lower.ToString())
                        || !mdAssociation.target.upperBound.Equals(eaAssociation.targetEnd.upper.ToString()))
                    {
                        //actually fix the multiplicities
                        try
                        {
                            bool updatedMultiplicity = false;
                            //set source (only if not both empty)
                            if (!string.IsNullOrEmpty(mdAssociation.source.lowerBound)
                                && !string.IsNullOrEmpty(mdAssociation.source.upperBound))
                            {
                                var sourceMultiplicity = new TSF_EA.Multiplicity(mdAssociation.source.lowerBound, mdAssociation.source.upperBound);
                                eaAssociation.sourceEnd.multiplicity = sourceMultiplicity;
                                updatedMultiplicity = true;
                            }
                            //set target (only if not both empty)
                            if (!string.IsNullOrEmpty(mdAssociation.target.lowerBound)
                                && !string.IsNullOrEmpty(mdAssociation.target.upperBound))
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
                                EAOutputLogger.log(this.model, this.outputName
                                   , string.Format("{0} Corrected multiplicity of association between '{1}' and '{2}'"
                                                  , DateTime.Now.ToLongTimeString()
                                                 , eaAssociation.source.name
                                                 , eaAssociation.target.name)
                                  , ((TSF_EA.ElementWrapper)eaAssociation.source).id
                                  , LogTypeEnum.log);
                            }
                        }
                        catch (Exception e)
                        {
                            //tell the user we could not fix the multiplicity
                            EAOutputLogger.log(this.model, this.outputName
                               , string.Format("{0} Could not update multiplicities between '{1}' and '{2}' because of error '{3}'"
                                              , DateTime.Now.ToLongTimeString()
                                             , eaAssociation.source.name
                                             , eaAssociation.target.name
                                             , e.GetType().Name)
                              , ((TSF_EA.ElementWrapper)eaAssociation.source).id
                              , LogTypeEnum.error);
                            //also log the complete error
                            Logger.logError(string.Format("Exception '{0}' with message '{1}' occurred at stacktrace: {2}"
                                                          , e.GetType().Name
                                                          , e.Message
                                                          , e.StackTrace));
                        }
                    }
                }

            }

            //Log finished
            EAOutputLogger.log(this.model, this.outputName
                       , string.Format("{0} Finished correcting Associationss"
                                      , DateTime.Now.ToLongTimeString())
                       , 0
                      , LogTypeEnum.log);

        }

        private void setSequenceKey(MDAssociation mdAssociation, TSF_EA.Association eaAssociation)
        {
            //only if the association is an ASMA or ASBIE association
            if (eaAssociation.hasStereotype("ASMA")
                || eaAssociation.hasStereotype("ASBIE"))
            {
                //find the associationRole with the largest sequenceKey
                int sequenceKey = mdAssociation.source.sequenceKey > mdAssociation.target.sequenceKey ?
                                mdAssociation.source.sequenceKey : mdAssociation.target.sequenceKey;
                //set the tagged value on the association
                eaAssociation.addTaggedValue("sequencingKey", sequenceKey.ToString());
            }
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
                    EAOutputLogger.log(this.model, this.outputName
                       , string.Format("{0} Corrected navigability of association between '{1}' and '{2}'"
                                      , DateTime.Now.ToLongTimeString()
                                     , eaAssociation.source.name
                                     , eaAssociation.target.name)
                      , ((TSF_EA.ElementWrapper)eaAssociation.source).id
                      , LogTypeEnum.log);
                }
            }
        }
        private TSF_EA.Association findCorrespondingAssociation(MDAssociation mdAssociation)
        {
            //first try to find it using the MD guid
            string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
											inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
														and tv.Property = 'md_guid'))
											where tv.VALUE = '" + mdAssociation.md_guid + "'";

            TSF_EA.Association correspondingAssociation = this.model.getRelationsByQuery(sqlGetExistingRelations).FirstOrDefault() as TSF_EA.Association;
            //if not found then we look for associations that 
            // - are between the same two elements
            // - have the same rolenames
            if (correspondingAssociation != null) return correspondingAssociation;

            //not found with mdguid, search other associations
            string sqlGetCorrespondingAssociation = string.Empty;
            var sourceElement = getElementByMDid(mdAssociation.source.endClassID);
            var targetElement = getElementByMDid(mdAssociation.target.endClassID);

            if (sourceElement != null && targetElement != null)
            {
                //source -> target
                correspondingAssociation = GetCorrespondingAssociation(sourceElement, targetElement, mdAssociation.source.name, mdAssociation.target.name);
                //target -> source
                if (correspondingAssociation == null)
                    correspondingAssociation = GetCorrespondingAssociation(targetElement, sourceElement, mdAssociation.target.name, mdAssociation.source.name);
                //if still not found we create it new
                if (correspondingAssociation == null)
                    correspondingAssociation = createNewCorrespondingAssociation(sourceElement, targetElement, mdAssociation);
                //if we find the association we set the md_guid tagged value
                if (correspondingAssociation != null)
                {
                    correspondingAssociation.addTaggedValue("md_guid", mdAssociation.md_guid);
                }
            }

            return correspondingAssociation;
        }
        private TSF_EA.Association createNewCorrespondingAssociation(TSF_EA.ElementWrapper sourceElement, TSF_EA.ElementWrapper targetElement, MDAssociation mdAssociation)
        {
            //create the actual association
            TSF_EA.Association newAssociation = this.model.factory.createNewElement<TSF_EA.Association>(sourceElement, string.Empty);
            //set target class
            newAssociation.target = targetElement;
            //set source name
            newAssociation.sourceEnd.name = mdAssociation.source.name;
            newAssociation.sourceEnd.aggregation = parseAggregationKind(mdAssociation.source.aggregationKind);
            //set target name
            newAssociation.targetEnd.name = mdAssociation.target.name;
            newAssociation.targetEnd.aggregation = parseAggregationKind(mdAssociation.target.aggregationKind);
            //set the target end navigable by default
            newAssociation.targetEnd.isNavigable = true;
            if (mdAssociation.stereotype == "participates")
            {
                newAssociation.targetEnd.isNavigable = false;
            }
            //set the stereotype
            newAssociation.addStereotype(this.model.factory.createStereotype(newAssociation, mdAssociation.stereotype));
            //save the new association
            newAssociation.save();
            //return
            return newAssociation;
        }

        private AggregationKind parseAggregationKind(string aggregationKind)
        {
            switch (aggregationKind.ToLower())
            {
                case "composite":
                    return AggregationKind.composite;
                case "shared":
                    return AggregationKind.shared;
                default:
                    return AggregationKind.none;
            }
        }

        private TSF_EA.Association GetCorrespondingAssociation(TSF_EA.ElementWrapper sourceElement, TSF_EA.ElementWrapper targetElement, string sourceName, string targetName)
        {
            //first set the first part:
            string sqlGetCorrespondingAssociation =
                @"select c.Connector_ID from t_connector c
						where c.Connector_Type in ('Association', 'Aggregation')
						and c.Start_Object_ID = " + sourceElement.id + Environment.NewLine +
                "and c.End_Object_ID = " + targetElement.id;
            if (!string.IsNullOrEmpty(targetName))
            {
                //target role is filled in
                sqlGetCorrespondingAssociation += @" 
						and c.DestRole = '" + targetName + "'";
            }
            if (!string.IsNullOrEmpty(sourceName))
            {
                //source role is filled in
                sqlGetCorrespondingAssociation += @" 
						and c.SourceRole = '" + sourceName + "'";
            }
            //add the part checking for the md_guid
            sqlGetCorrespondingAssociation += @" 
					and not exists 
					(select tv.PropertyID from t_connectortag tv
					where tv.ElementID = c.Connector_ID
					and tv.Property = 'md_guid'
					and tv.VALUE is not null)";

            var correspondingAssociation = this.model.getRelationsByQuery(sqlGetCorrespondingAssociation).FirstOrDefault() as TSF_EA.Association;

            return correspondingAssociation;
        }


        void fixParticipation()
        {
            /*this method adds the stereotype <<participates>> to all the connectors between a 'Harmonized_Role'
			 * actor and a 'BusinessRealizationUseCase, bRealizationUC' use case
			 */

            string sqlGetAssociations = @"select con.Connector_ID, con.[Stereotype]
										from ((t_connector con
										inner join t_object ac
										on (con.[Start_Object_ID] = ac.[Object_ID] and ac.[Stereotype]= 'Harmonized_Role' ))
										inner join t_object uc
										on (con.[End_Object_ID] = uc.[Object_ID] and uc.[Stereotype] in ('BusinessRealizationUseCase','bRealizationUC')))";

            var associations = this.model.getRelationsByQuery(sqlGetAssociations);

            foreach (var association in associations)
            {
                association.addStereotype(this.model.factory.createStereotype(association, "participates"));
                association.save();
                //tell the user
                EAOutputLogger.log(this.model, this.outputName
                           , string.Format("{0} Corrected «participates» association'"
                          , DateTime.Now.ToLongTimeString()
                   )
                        , 0
                      , LogTypeEnum.log);
            }

        }





    }
}
