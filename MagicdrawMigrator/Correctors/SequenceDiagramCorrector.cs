using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of CorrectSequenceDiagrams.
	/// </summary>
	public class SequenceDiagramCorrector:MagicDrawCorrector
	{
		public SequenceDiagramCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Starting corrections for Sequence Diagrams"
			          ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
			//set classifiers
			setClassifiersOnLifeLines();
			//add missing messages
			addMessages();
			//add missing fragments and update existing fragments
			addOrUpdateFragments();
			
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Finished corrections for Sequence Diagrams"
			          ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
		}

		void addOrUpdateFragments()
		{
			//add or update the fragments
			foreach (var mdFragment in magicDrawReader.allFragments) 
			{
				//check if the fragment already exists
				var eaFragment = this.getElementByMDid(mdFragment.mdID);
				//create new if is doesn't exist yet
				//get its owner
				var owner = this.getElementByMDid(mdFragment.ownerMdID);
				if (eaFragment == null
				    && owner != null)
				{
					//create the fragment under the owner
					eaFragment = this.model.factory.createNewElement<UML.Interactions.BasicInteractions.InteractionFragment>(owner,string.Empty) as TSF_EA.ElementWrapper;
				}
				if (eaFragment != null)
				{
					//set the type
					switch (mdFragment.fragmentType)
					{
						case "alt":
							eaFragment.subType = "0";
							break;
						case "opt":
							eaFragment.subType = "1";
							break;
						case "par":
							eaFragment.subType = "3";
							break;
						case "loop":
							eaFragment.subType = "4";
							break;
					}
					//update the partitions
					foreach (var guard in mdFragment.operandGuards)
					{
						global::EA.Partition partition = eaFragment.WrappedElement.Partitions.AddNew(guard,mdFragment.fragmentType) as global::EA.Partition;
						partition.Size = 100; //default value, later to be corrected in the DiagramLayoutCorrector
						eaFragment.isDirty = true; //make sure it gets saved
					}
					eaFragment.save();
					//add the md_guid tagged value if needed
					if (eaFragment != null) eaFragment.addTaggedValue("md_guid",mdFragment.mdID);
					//tell the user what we are doing
					EAOutputLogger.log(this.model,this.outputName
					,string.Format("{0} Updating fragment in '{1}'"
					          	,DateTime.Now.ToLongTimeString()
					          	, eaFragment.owner.name)
					 ,eaFragment.id
					,LogTypeEnum.log);
					//set the operands
//					string xrefDescription = this.getOperandDescription(mdFragment.operandGuards);
//					if (! string.IsNullOrEmpty(xrefDescription))
//					{
//						string updateXrefSQL = "";
//					}
					
				}
			}
		}
		void addMessages()
		{
			//add the messages
			foreach (MDMessage mdMessage in magicDrawReader.allMessages) 
			{
				var source = this.getElementByMDid(mdMessage.sourceID);
				var target = this.getElementByMDid(mdMessage.targetID);
				if (source != null
				    && target != null)
				{
					TSF_EA.Message eaMessage;
					//check if the message already exists
					string sqlGetExistingMessage = @"select c.Connector_ID from (t_connector c  
													inner join t_connectortag tv on (tv.ElementID = c.Connector_ID
																					and tv.Property = 'md_guid'))
													where c.Connector_Type = 'Sequence'
													and tv.VALUE = '"+mdMessage.messageID+"'";
					eaMessage = this.model.getRelationsByQuery(sqlGetExistingMessage).FirstOrDefault() as TSF_EA.Message;
					if (eaMessage == null)
					{
						//check if a message with the same name exists but without a md_guid tagged value
						string sqlGetMessageWithoutTV = @"select c.Connector_ID from t_connector c  	
														where c.Connector_Type = 'Sequence'
														and not exists (select tv.ea_guid from t_connectortag tv 
																		where tv.ElementID = c.Connector_ID
																			and tv.Property = 'md_guid')
														and c.Name = '"+mdMessage.messageName+"'";
						eaMessage = this.model.getRelationsByQuery(sqlGetExistingMessage).FirstOrDefault() as TSF_EA.Message;
					}
					if (eaMessage == null)
					{
						//create a new message
						eaMessage = this.model.factory.createNewElement<TSF_EA.Message>(source,mdMessage.messageName);
					}
					//set the properties
					if (eaMessage != null)
					{
						eaMessage.source = source;
						eaMessage.target = target;
						eaMessage.name = mdMessage.messageName;
						eaMessage.save();
						//changing messageSort is done directly in the database so the connector should already exist.
						eaMessage.messageSort = mdMessage.isAsynchronous ? UML.Interactions.BasicInteractions.MessageSort.asynchSignal : UML.Interactions.BasicInteractions.MessageSort.synchCall;
						//add the md_guid tagged value
						eaMessage.addTaggedValue("md_guid",mdMessage.messageID);
						//tell the user what we are doing
						EAOutputLogger.log(this.model,this.outputName
						,string.Format("{0} Creating message '{1}' in package '{2}'"
						          	,DateTime.Now.ToLongTimeString()
						         	, mdMessage.messageName
						        	, source.owningPackage.name)
						 ,source.id
						,LogTypeEnum.log);
					}
				}
				else
				{
					EAOutputLogger.log(this.model,this.outputName
					,string.Format("{0} Could not create message '{1}' between lifelineID '{2}' and lifeLineID '{3}' because at least one of the lifelines was not found"
					          	,DateTime.Now.ToLongTimeString()
					         	, mdMessage.messageName
					        	, mdMessage.sourceID
					        	, mdMessage.targetID)
					 ,source != null ? source.id : target != null ? target.id : 0
					,LogTypeEnum.error);
				}
			}
		}
		void setClassifiersOnLifeLines()
		{
			//set the classifiers on the lifelines
			foreach (var lifeLineID in magicDrawReader.allLifeLines.Keys) 
			{
				//get the lifeline and classifier
				var lifeLine = this.getElementByMDid(lifeLineID);
				var classifier = this.getElementByMDid(magicDrawReader.allLifeLines[lifeLineID]);
				if (lifeLine != null && classifier != null)
				{
					lifeLine.classifier = classifier;
					lifeLine.save();
					EAOutputLogger.log(this.model,this.outputName
					,string.Format("{0} Setting classifier {1} on lifeline in package {2}"
					          	,DateTime.Now.ToLongTimeString()
					         	, classifier.name
					        	, lifeLine.owningPackage.name)
					,lifeLine.id
					,LogTypeEnum.log);
				}
			}
		}
		string getOperandDescription(List<string> operandGuards)
		{
			string description = string.Empty;
			foreach (var guard in operandGuards) 
			{
				description += "@PAR;Name="+guard+";Size=100;GUID="+Guid.NewGuid().ToString("B")+";@ENDPAR;";
			}
			return description;
		}
	}
}
