using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;

namespace MagicdrawMigrator
{
    /// <summary>
    /// This corrector corrects all the types of relations, cross mdzip or not
    /// </summary>
    public class RelationsCorrector : MagicDrawCorrector
    {
        public RelationsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage) : base(magicDrawReader, model, mdPackage)
        {
        }


        public override void correct()
        {
            EAOutputLogger.log(this.model, this.outputName
           , string.Format("{0} Starting corrections for all the relations"
                          , DateTime.Now.ToLongTimeString())
           , 0
          , LogTypeEnum.log);

            //Dependencies - Usage - Realisation
            this.correctElementRelations();

            //Generalizations
            this.correctGeneralizations();

            //Finished
            EAOutputLogger.log(this.model, this.outputName
                           , string.Format("{0} Finished corrections for the all the relations"
                                          , DateTime.Now.ToLongTimeString())
                           , 0
                           , LogTypeEnum.log);

        }

        
        void correctElementRelations()
        {

            EAOutputLogger.log(this.model, this.outputName
                   , string.Format("{0} Starting corrections for the dependencies, usages and realisations"
                                  , DateTime.Now.ToLongTimeString())
                   , 0
                  , LogTypeEnum.log);

            //First get al the dependencies, usages and realisations
            foreach (var mdRelation in magicDrawReader.allMDElementRelations)
            {
                //check if the relation already exists
                if (!relationExists(mdRelation.Key, mdRelation.Value.sourceMDGUID, mdRelation.Value.targetMDGUID, mdRelation.Value.relationType, mdRelation.Value.stereotype))
                {
                    var sourceElement = this.getElementByMDid(mdRelation.Value.sourceMDGUID);
                    var targetElement = this.getElementByMDid(mdRelation.Value.targetMDGUID);

                    if (mdRelation.Value.relationType == "Usage" && sourceElement == null && targetElement != null)
                    {
                        //get the attribute with mdGuid = sourceMdGuid of the relation
                        foreach (var mdAttribute in magicDrawReader.allAttributes.Where(x => x.mdGuid == mdRelation.Value.sourceMDGUID))
                        {

                            //mdAttribute = source attribute
                            var attributeParent = this.getElementByMDid(mdAttribute.mdParentGuid);
                            if (attributeParent != null)
                            {
                                // create a 'usage' link from the enum attributes to the enum entities
                                TSF_EA.Usage usage = this.model.factory.createNewElement<TSF_EA.Usage>(attributeParent, string.Empty);

                                //check all atributes in the parent for an attribute with the same name as the mdAttribute
                                foreach (var attribute in attributeParent.attributes.Where(x => x.name == mdAttribute.name))
                                {
                                    usage.source = attribute;
                                    usage.target = targetElement;
                                    usage.targetEnd.isNavigable = true;
                                    usage.save();


                                    EAOutputLogger.log(this.model, this.outputName
                                                   , string.Format("{0} Created <<usage>> link from '{1}.{2}' to '{3}'"
                                                  , DateTime.Now.ToLongTimeString()
                                                 , attributeParent.name
                                                , attribute.name
                                                , targetElement.name)
                                       , attributeParent.id
                                      , LogTypeEnum.log);
                                }
                            }
                        }
                    }

                    if (sourceElement != null && targetElement != null)
                    {
                        //create the actual relation
                        var newRelation = this.model.factory.createNewElement(sourceElement, mdRelation.Value.name, mdRelation.Value.relationType) as TSF_EA.ConnectorWrapper;

                        //set target
                        newRelation.target = targetElement;

                        //set the target end navigable by default
                        newRelation.targetEnd.isNavigable = true;

                        //set the stereotype
                        newRelation.addStereotype(this.model.factory.createStereotype(newRelation, mdRelation.Value.stereotype));

                        //save the new relation
                        newRelation.save();

                        //set the md_guid tagged value
                        newRelation.addTaggedValue("md_guid", mdRelation.Key);

                        //tell the user
                        EAOutputLogger.log(this.model, this.outputName
                            , string.Format("{0} Created relation of type {1} between '{2}' and '{3}'"
                                          , DateTime.Now.ToLongTimeString()
                                          , mdRelation.Value.relationType
                                          , sourceElement.name
                                          , targetElement.name)
                            , sourceElement.id
                            , LogTypeEnum.log);

                    }
                }
            }

            EAOutputLogger.log(this.model, this.outputName
               , string.Format("{0} Finished corrections for the dependencies, usages and realisations"
                              , DateTime.Now.ToLongTimeString())
               , 0
              , LogTypeEnum.log);

        }

        void correctGeneralizations()
        {
            EAOutputLogger.log(this.model, this.outputName
               , string.Format("{0} Starting corrections for the generalizations"
                              , DateTime.Now.ToLongTimeString())
               , 0
              , LogTypeEnum.log);

            //First get all the generalizations
            foreach (var mdGeneralization in magicDrawReader.allDirectMDElementRelations)
            {
                string md_guid = mdGeneralization.Key;
                string source_id = mdGeneralization.Value.sourceMDGUID;
                string target_id = mdGeneralization.Value.targetMDGUID;
                string relationType = mdGeneralization.Value.relationType;
                string name = mdGeneralization.Value.name;
                string stereotype = string.Empty;

                //check if the relation already exists
                if (!relationExists(md_guid, source_id, target_id, relationType, stereotype))
                {
                    var sourceElement = this.getElementByMDid(source_id);
                    var targetElement = this.getElementByMDid(target_id);

                    if (sourceElement != null && targetElement != null)
                    {
                        //create the actual generalization
                        var newGeneralization = this.model.factory.createNewElement(sourceElement, name, relationType) as TSF_EA.Generalization;

                        //set target
                        newGeneralization.target = targetElement;

                        //set the target end navigable by default --> always true?
                        newGeneralization.targetEnd.isNavigable = true;

                        //save the new generalization
                        newGeneralization.save();

                        //set the md_guid tagged value
                        newGeneralization.addTaggedValue("md_guid", md_guid);

                        //tell the user
                        EAOutputLogger.log(this.model, this.outputName
                            , string.Format("{0} Created generalization between '{1}' and '{2}'"
                                          , DateTime.Now.ToLongTimeString()
                                          , sourceElement.name
                                          , targetElement.name)
                            , sourceElement.id
                            , LogTypeEnum.log);
                    }


                }

            }

            EAOutputLogger.log(this.model, this.outputName
               , string.Format("{0} Finished corrections for the generalizations"
                              , DateTime.Now.ToLongTimeString())
               , 0
              , LogTypeEnum.log);
        }




        bool relationExists(string md_guid, string source, string target, string type, string stereotype)
        {
            if (type == "Realization")
            {
                type = "Realisation";
            }
            //check if the relation already exists
            //first try to find it using the MD guid
            string sqlGetExistingRelations = @"select c.Connector_ID from (t_connector c
								inner join t_connectortag tv on( c.Connector_ID = tv.ElementID
											and tv.Property = 'md_guid'))
								where tv.VALUE = '" + md_guid + "'";

            var correspondingRelations = this.model.getRelationsByQuery(sqlGetExistingRelations);

            if (correspondingRelations.Any()) return true;
            //special check for participates
            if (stereotype == "participates")
            {
                sqlGetExistingRelations = @"select c.Connector_ID from ((((t_connector c
											inner join t_object obs on c.Start_Object_ID = obs.Object_ID)
											inner join t_objectproperties obstv on (obstv.Object_ID = obs.Object_ID
																					and obstv.Property = 'md_guid'))
											inner join t_object obe on c.End_Object_ID = obe.Object_ID)
											inner join t_objectproperties obetv on (obetv.Object_ID = obe.Object_ID
																					and obetv.Property = 'md_guid'))
											where
											c.Connector_Type = '" + type + @"'
											and c.Stereotype = '" + stereotype + @"'
											and obstv.Value in ('" + source + "','" + target + @"')
											and obetv.Value in ('" + target + "','" + source + "')";

                var correspondingParticipates = this.model.getRelationsByQuery(sqlGetExistingRelations).ToList();
                if (correspondingParticipates.Any()) // als er iets gevonden is
                {
                    return true; // bestaat al
                }
            }

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
										c.Connector_Type = '" + type + @"'
										and c.Stereotype = '" + stereotype + @"'
										and obstv.Value = '" + source + @"'
										and obetv.Value = '" + target + "'";

            correspondingRelations = this.model.getRelationsByQuery(sqlGetExistingRelations).ToList();
            if (correspondingRelations.Any())
            {
                return true; //exists
            }

            return false; //does not exist		
        }
    }
}
