using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml;
using System.IO;
using System.IO.Compression;
using EAAddinFramework.Utilities;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MagicDrawReader.
	/// </summary>
	public class MagicDrawReader
	{
		string mdzipPath {get;set;}
		protected TSF_EA.Model model {get;set;}
		public string outputName {get;private set;}
		Dictionary<string,string> _allClasses;
		Dictionary<string,MDAssociation> _allCrossMDzipAssociations;
		Dictionary<string,MDAssociation> _allAssociations;
		Dictionary<string,MDDependency> _allDependencies;
		Dictionary<string,MDAssociationEnd> _allAttributeAssociationRoles;
		Dictionary<string,MDTimeEvent> _allTimeEvents;
		List<MDAssociation> _allASMAAssociations;
		List<MDAssociation> _allParticipatesAssociations;
		Dictionary<string,string> _allLinkedAssociationTables;
		Dictionary<string, MDDiagram> _allDiagrams;
		Dictionary<string, string> _allObjects;
		Dictionary<string, string> _allPartitions;
		List<MDElementRelation> _allMapsToDependencies;
		Dictionary<string,string> _allLifeLines;
		List<MDFragment> _allFragments;
		List<MDMessage> _allMessages;
		List<MDAttribute> _allAttributes;
		List<MDDependency> _allAttDependencies;
		Dictionary<string,List<MDConstraint>> allConstraints;
		Dictionary<string, MDElementRelation> _allCrossMDzipRelations;
		Dictionary<string, MDElementRelation> _allMDElementRelations;
		Dictionary<string, MDElementRelation> _allDirectMDElementRelations;
		Dictionary<string,XmlDocument> _sourceFiles;
		List<MDGuard> _allGuards;
		
	
		Dictionary<string,XmlDocument> sourceFiles
		{
			get
			{
				if (_sourceFiles == null)
				{
					_sourceFiles = new Dictionary<string, XmlDocument>();
					readMDSourceFiles();
				}
				return _sourceFiles;
			}
		}
		public MagicDrawReader(string mdzipPath,TSF_EA.Model model)
		{
			this.mdzipPath = mdzipPath;
			this.outputName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
			this.model = model;
		}
		private void readMDSourceFiles()
		{
			//create a new temp directory
			var tempDirectory = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(),"TmpMDZip"));
			//loop the files in the mdzip directory
			foreach (var fileName in Directory.GetFiles(mdzipPath,"*.mdzip")) 
			{
				//unzip all the mdzip file to their own subdirectory
				//check if the directory already exists and if so delete it
				string targetDirectory = Path.Combine(tempDirectory.FullName,Path.GetFileName(fileName));
				if (Directory.Exists(targetDirectory))
				{
					Directory.Delete(targetDirectory,true);
				}
				ZipFile.ExtractToDirectory(fileName,targetDirectory);
			}
			//loop all created subdirectories and read com.nomagic.magicdraw.uml_model.model and com.nomagic.magicdraw.uml_model.shared_model files as xml files
			foreach (var subDirectory in tempDirectory.GetDirectories())
			{
				var modelFile = subDirectory.GetFiles("com.nomagic.magicdraw.uml_model.model").FirstOrDefault();
				if (modelFile != null
				    && ! string.IsNullOrEmpty(modelFile.FullName))
				{
					var xmlModel =new XmlDocument();
					xmlModel.Load(modelFile.FullName);
					//add the xml documents to the dictionary of source files
					this._sourceFiles.Add(subDirectory.Name + "_Model",xmlModel);
				}
				var sharedModelFile = subDirectory.GetFiles("com.nomagic.magicdraw.uml_model.shared_model").FirstOrDefault();
				if (sharedModelFile != null
				    && ! string.IsNullOrEmpty(sharedModelFile.FullName))
				{
					var xmlModel =new XmlDocument();
					xmlModel.Load(sharedModelFile.FullName);
					//add the xml documents to the dictionary of source files
					this._sourceFiles.Add(subDirectory.Name + "_SharedModel",xmlModel);
				}
			}
		}
		public Dictionary<string,MDAssociationEnd> allAttributeAssociationRoles
		{
			get
			{
				if (_allAttributeAssociationRoles == null)
				{
					this.getAllAttributeAssociationRoles();
				}
				return _allAttributeAssociationRoles;
			}
		}
		public Dictionary<string, MDAssociation> allAssociations
		{
			get
			{
				if (_allAssociations == null)
				{
					this.getAllAssociations();
				}
				return _allAssociations;
			}
		}
		
		public Dictionary<string, MDTimeEvent> allTimeEvents
		{
			get
			{
				if (_allTimeEvents == null)
				{
					this.getAllTimeEvents();
				}
				return _allTimeEvents;
			}
		}
		
		public List<MDGuard> allGuards
		{
			get
			{
				if (_allGuards == null)
				{
					this.getAllGuards();
				}
				return _allGuards;
			}
		}
		
		public Dictionary<string,MDAssociation> allCrossMDzipAssociations
		{
			get
			{
				if (_allCrossMDzipAssociations == null)
				{
					this.getAllCrossMDzipAssociations();
				}
				return _allCrossMDzipAssociations;
			}
		}
		public Dictionary<string, MDElementRelation> allCrossMDzipRelations
		{
			get
			{
				if (_allCrossMDzipRelations == null)
				{
					this.getAllCrossMDZipRelations();
				}
				return _allCrossMDzipRelations;
			}
		}
		public Dictionary<string, MDElementRelation> allMDElementRelations
		{
			get
			{
				if (_allMDElementRelations == null)
				{
					this.getAllMDElementRelations();
				}
				return _allMDElementRelations;
			}
		}
		
				public Dictionary<string, MDElementRelation> allDirectMDElementRelations
		{
			get
			{
				if (_allDirectMDElementRelations == null)
				{
					this.getAllDirectMDElementRelations();
				}
				return _allDirectMDElementRelations;
			}
		}
		
		
		
		
		public List<MDDependency> allAttDependencies
		{
			get
			{
				if (_allAttDependencies == null)
				{
					this.getAllAttDependencies();
				}
				return _allAttDependencies;
			}
		}
		
		public List<MDAttribute> allAttributes
		{
			get
			{
				if (_allAttributes == null)
				{
					this.getAllAttributes();
				}
				return _allAttributes;
			}
		}
		public List<MDFragment> allFragments
		{
			get
			{
				if (_allFragments == null)
				{
					this.getAllFragments();
				}
				return _allFragments;
			}
		}
		public List<MDMessage> allMessages
		{
			get
			{
				if (_allMessages == null)
				{
					this.getAllMessages();
				}
				return _allMessages;
			}
		}
		public Dictionary<string, string> allLifeLines 
		{
			get 
			{
				if (_allLifeLines == null)
				{
					this.getAllLifeLines();
				}
				return _allLifeLines;
			}
		}

		public Dictionary<string, MDDiagram> allDiagrams
		{
			get
			{
				if (_allDiagrams == null)
				{
					this.getAllDiagrams();
				}
				return _allDiagrams;
			}
		}
		public Dictionary<string, string> allObjects
		{
			get
			{
				if (_allObjects == null)
				{
					this.getAllObjects();
				}
				return _allObjects;
			}
		}
		public Dictionary<string, string> allPartitions
		{
			get
			{
				if (_allPartitions == null)
				{
					this.getAllPartitions();
				}
				return _allPartitions;
			}
		}
		public List<MDElementRelation> allMapsToDependencies {
			get {
				if (_allMapsToDependencies == null)
				{
					this.getAllMapsToDependencies();
				}
				
				return _allMapsToDependencies;
			}
		}
		
		public Dictionary<string,string> allClasses
		{
			get
			{
				if (_allClasses == null)
				{
					this.getAllClasses();
				}
				return _allClasses;
			}
		}
		public Dictionary<string,string> allLinkedAssociationTables
		{
			get
			{
				if (_allLinkedAssociationTables == null)
				{
					this.getAllAssociationTables();
				}
				return _allLinkedAssociationTables;
			}
		}
		public List<MDAssociation> allASMAAssociations
		{
			get
			{
				if (_allASMAAssociations == null)
				{
					this.getAllASMAAssociations();
				}
				return _allASMAAssociations;
			}
		}
		
		
		public List<MDAssociation> allParticipatesAssociations
		{
			get
			{
				if (_allParticipatesAssociations == null)
				{
					this.getParticipatesAssociations();
				}
				return _allParticipatesAssociations;
			}
		}
		
		public List<MDConstraint> getContraints(string MDElementID)
		{
			//check if the constraints were already read
			if (allConstraints == null)
			{
				allConstraints = getAllConstraints();
			}
			//check if list of constraints exists for the given id
			if (allConstraints.ContainsKey(MDElementID))
			{
				return allConstraints[MDElementID];
			}
			else
			{
				//return empty list if not found
				return new List<MDConstraint>();
			}
			
		}
		/// <summary>
		/// gets all simple relations between elements that go between different MDzip files.K
		/// This can be recognized by the fact that the supplier has a href= attribute instead of a xmi:idref attribute
		/// </summary>
		void getAllCrossMDZipRelations()
		{
			var foundElementRelations = new Dictionary<string,MDElementRelation>();
			//loop the source files to find the cross mdzip relations
			foreach (var relation in this.allMDElementRelations.Where(
				x => x.Value.isCrossMDZip))
			{
				foundElementRelations.Add(relation.Key, relation.Value);
			}
			
			//loop the source files to find the direct cross mdzip relations
			foreach (var relation in this.allDirectMDElementRelations.Where(
				x => x.Value.isCrossMDZip))
			{
				foundElementRelations.Add(relation.Key, relation.Value);
			}
			
			
			//set the found relations
			_allCrossMDzipRelations = foundElementRelations;
		}
		

		
		void getAllDirectMDElementRelations()
		{
			var foundDirectMDElementRelations = new Dictionary<string, MDElementRelation>();
			//loop the source files to find the cross mdzip relations
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				foreach (XmlNode generalNode in sourceFile.SelectNodes("//general",nsMgr))
				{
					//general node = supplier = target
					//the packaged element above = client = source
					bool isCrossMdZip = this.isForeign(generalNode);
					//get the parent Node id
					XmlNode generalizationNode = generalNode.ParentNode;
					
					if (generalizationNode != null)
					{
						//get the relationType
						XmlAttribute relationTypeAttribute = generalizationNode.Attributes["xmi:type"];
						string relationType = relationTypeAttribute != null ? relationTypeAttribute.Value : string.Empty;
						var relationParts = relationType.Split(':');
						if (relationParts.Count() == 2)
						{
							relationType = relationParts[1];
						}
						//get the relation name
						XmlAttribute relationNameAttribute = generalizationNode.Attributes["name"];
						string relationName = relationNameAttribute != null ? relationNameAttribute.Value : string.Empty;
						string relationID = getID(generalizationNode);
						//get the client node
						XmlNode sourceNode = generalizationNode.ParentNode;
						//check if crossMDZip
						if (sourceNode != null && ! isCrossMdZip) isCrossMdZip = this.isForeign(sourceNode);
						//get the ID's
						string sourceID = getID(sourceNode);
						string targetID = getID(generalNode);
						//add the relation
						if (! string.IsNullOrEmpty(relationID)
						    && ! string.IsNullOrEmpty(sourceID)
						    && ! string.IsNullOrEmpty(targetID)
						    && ! string.IsNullOrEmpty(relationType)
						    && ! foundDirectMDElementRelations.ContainsKey(relationID))
						{
							var newRelation = new MDElementRelation(sourceID, targetID,relationType,relationID);
							newRelation.name = relationName;
							newRelation.isCrossMDZip = isCrossMdZip;

							//add the new relation to the list of found relations
							foundDirectMDElementRelations.Add(relationID,newRelation);
							
							
						}	
					}
				}
			}
			//set the found relations
			_allDirectMDElementRelations = foundDirectMDElementRelations;
		}

		void getAllMDElementRelations()
		{
			var foundElementRelations = new Dictionary<string,MDElementRelation>();
			//loop the source files to find the cross mdzip relations
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				foreach (XmlNode supplierNode in sourceFile.SelectNodes("//supplier",nsMgr))
				{
					bool isCrossMdZip = this.isForeign(supplierNode);
					//get the parent Node id
					XmlNode relationNode = supplierNode.ParentNode;				
					if (relationNode != null)
					{
						//get the relationType
						XmlAttribute relationTypeAttribute = relationNode.Attributes["xmi:type"];
						string relationType = relationTypeAttribute != null ? relationTypeAttribute.Value : string.Empty;
						//the relationType is often described as uml:Dependency. In these cases we only want to store "Dependency"
						var relationParts = relationType.Split(':');
						if (relationParts.Count() == 2)
						{
							relationType = relationParts[1];
						}
						//get the relation name
						XmlAttribute relationNameAttribute = relationNode.Attributes["name"];
						string relationName = relationNameAttribute != null ? relationNameAttribute.Value : string.Empty;
						string relationID = getID(relationNode);
						//get the client node
						XmlNode sourceNode = relationNode.SelectSingleNode("./client");
						//check if crossMDZip
						if (sourceNode != null && ! isCrossMdZip) isCrossMdZip = this.isForeign(sourceNode);
						//get the ID's
						string sourceID = getID(sourceNode);
						string targetID = getID(supplierNode);
						//add the relation
						if (! string.IsNullOrEmpty(relationID)
						    && ! string.IsNullOrEmpty(sourceID)
						    && ! string.IsNullOrEmpty(targetID)
						    && ! string.IsNullOrEmpty(relationType)
						    && ! foundElementRelations.ContainsKey(relationID))
						{
							
							var newRelation = new MDElementRelation(sourceID, targetID,relationType,relationID);
							newRelation.name = relationName;
							newRelation.isCrossMDZip = isCrossMdZip;
							
							//get the stereotype, search for base_Dependency, base_Realization, 
							//dependencies, usages and realisation
							
							XmlNode stereotypeNode = sourceFile.SelectSingleNode("//*[@base_Dependency='"+newRelation.md_guid+"' or @base_Realization='"+newRelation.md_guid+"']", nsMgr);
							if(stereotypeNode != null)
							{
								if(!string.IsNullOrEmpty(stereotypeNode.LocalName))
								{
									newRelation.stereotype = stereotypeNode.LocalName;								
								}
							}

							//add the new relation to the list of found relations
							foundElementRelations.Add(relationID,newRelation);
						}
					}
				}
			}
			//set the found relations
			_allMDElementRelations = foundElementRelations;
		}

		void getAllFragments()
		{
			var foundFragments = new List<MDFragment>();
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				//start with all xml nodes with name packagedElement and xmi:type='uml:Interaction'
				foreach (XmlNode interactionNode in sourceFile.SelectNodes("//packagedElement[@xmi:type='uml:Interaction']",nsMgr))
				{
					
					XmlAttribute interactionIDAttribute = interactionNode.Attributes["xmi:id"];
					if (interactionIDAttribute != null 
					    && !string.IsNullOrEmpty(interactionIDAttribute.Value))
					{
						string ownerID = interactionIDAttribute.Value;
						//get all xml nodes of with name fragment that have as xmi:type='uml:CombinedFragment'
						foreach (XmlNode fragmentNode in interactionNode.SelectNodes(".//fragment[@xmi:type='uml:CombinedFragment']",nsMgr)) 
						{
							//get the fragment mdID
							XmlAttribute fragmentIDAttribute = fragmentNode.Attributes["xmi:id"];
							string fragmentID = fragmentIDAttribute != null ? fragmentIDAttribute.Value:string.Empty;
							//get the fragment type
							XmlAttribute fragmentTypeAttribute = fragmentNode.Attributes["interactionOperator"];
							string fragmentType = fragmentTypeAttribute != null ? fragmentTypeAttribute.Value:string.Empty;
							if (! string.IsNullOrEmpty(fragmentID))
							{
								//create new MDFragment
								var mdFragment = new MDFragment(ownerID,fragmentID,fragmentType);
								foreach (XmlNode operandNode in fragmentNode.SelectNodes("./operand",nsMgr)) 
								{
									XmlNode guardNode = operandNode.SelectSingleNode("./guard/specification",nsMgr);
									//get the guard text
									XmlAttribute guardValueAttribute = guardNode != null ? guardNode.Attributes["value"] : null;
									//if the guard is empty then the default is "else"
									string operandGuard = guardValueAttribute != null ? guardValueAttribute.Value:"else";
									if (! string.IsNullOrEmpty(operandGuard))
									{
										//add it to the fragment
										mdFragment.operandGuards.Add(operandGuard);
									}
								}
								//add the fragment to the list of found fragments
								foundFragments.Add(mdFragment);
							}
						} 
					}
				}
			}
			//set the fragments
			_allFragments = foundFragments;
		}
		
		
		void getParticipatesAssociations()
		{
			var foundAssociations = new List<MDAssociation>();
			
			foreach(var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				nsMgr.AddNamespace("UMM_2_0_Foundation_Module___Long","http://www.magicdraw.com/schemas/UMM_2_0_Foundation_Module___Long.xmi");
				
				//get the associations that are missing by looking at the tags with UMM_2_0_Foundation_Module___Long:participates
				foreach (XmlNode participatesNode in sourceFile.SelectNodes("//*[local-name() = 'participates']",nsMgr)) 
				{
					MDAssociationEnd mdTargetEnd = null;
					MDAssociationEnd mdSourceEnd = null;
					
					// and getting the ID of their attribute base_Association
					XmlAttribute baseAssociationAttribute = participatesNode.Attributes["base_Association"];
					if (baseAssociationAttribute != null)
					{
						string associationID = baseAssociationAttribute.Value;
						//get the association
						if (this.allAssociations.ContainsKey(associationID))
						{
							var newParticipatesAssocation = allAssociations[associationID];
							newParticipatesAssocation.stereotype = "participates";
							foundAssociations.Add(newParticipatesAssocation);
						}
					}
				}
				
			}
			//set the collection to the found associations
			_allParticipatesAssociations = foundAssociations;
		}
		
		
			
		
		void getAllASMAAssociations()
		{
			var foundAssociations = new List<MDAssociation>();
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				nsMgr.AddNamespace("Business_Document_Library", "http://www.magicdraw.com/schemas/Business_Document_Library.xmi");
				//get the associations that are missing by looking at the tags with Business_Document_Library:ASMA
				foreach (XmlNode asmaNode in sourceFile.SelectNodes("//*[local-name() = 'ASMA']",nsMgr)) 
				{
					MDAssociationEnd mdTargetEnd = null;
					MDAssociationEnd mdSourceEnd = null;
					// and getting the ID of their attribute base_Association
					XmlAttribute baseAssociationAttribute = asmaNode.Attributes["base_Association"];
					if (baseAssociationAttribute != null)
					{
						string associationID = baseAssociationAttribute.Value;
						//get the association
						if (this.allAssociations.ContainsKey(associationID))
						{
							var newASMAAssocation = allAssociations[associationID];
							newASMAAssocation.stereotype = "ASMA";
							foundAssociations.Add(newASMAAssocation);
						}
					}
				} 
				
			}
			//set the collection to the found associations
			_allASMAAssociations = foundAssociations;
		}
		
		
		void getAllAssociations()
		{
			var foundAssociations = new Dictionary<string,MDAssociation>();
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				// then get the association itself by looking at the nodes <packagedElement> with attribute  xmi:type='uml:Association' and xmi:id= the id of the association
				foreach (XmlNode associationNode in sourceFile.SelectNodes("//packagedElement[@xmi:type='uml:Association']",nsMgr))					                                                           
				{
					//initialize
					MDAssociationEnd mdTargetEnd = null;
					MDAssociationEnd mdSourceEnd = null;
					bool isCrossMDZip = false;
					//get the association ID
					string associationID = this.getID(associationNode);
					
					//loop membernodes
					foreach (XmlNode memberNode in associationNode.SelectNodes("./memberEnd"))
					{
						MDAssociationEnd associationEnd = null;
						//check if memberNode is foreign
						if (this.isForeign(memberNode)) isCrossMDZip = true;
						//check if the membernode is an ownedEnd of the association
						string memberID = this.getID(memberNode);
						
						//we look for the ownedAttribute. In that case we found the target end
						if (this.allAttributeAssociationRoles.ContainsKey(memberID))
						{
							associationEnd = allAttributeAssociationRoles[memberID];
							mdTargetEnd = associationEnd;
						}
						else			
						{
							//if not found as ownedAttribute we look for OwnedEnd on the association
							XmlNode ownedEndNode = associationNode.SelectSingleNode("./ownedEnd[@xmi:id='"+memberID+"']",nsMgr);
							if (ownedEndNode != null) associationEnd = this.createAssociationEnd(ownedEndNode);
						}
						//set the crossMDzip value
						if (associationEnd != null)
						{
							if (associationEnd.hasForeignType) isCrossMDZip = true;
							if (mdTargetEnd != null )
						    {
								//target is already filled in so we found the source
						    	if (mdSourceEnd == null && mdTargetEnd != associationEnd)
						    	{
						    		mdSourceEnd = associationEnd;
						    	}
							}
							else
							{
								//target not yet filled in so we check the source first
								if (mdSourceEnd == null)
						    	{
						    		mdSourceEnd = associationEnd;
								}
								else
								{
									//only if the source is already filled in we fill in the target
									mdTargetEnd = associationEnd;
								}
							}
						}
					}
					//create the association
					if (mdSourceEnd != null 
					    && mdTargetEnd != null)
					{
						//switch aggregationKind from target to source
						var tempAggregation = mdTargetEnd.aggregationKind;
						mdTargetEnd.aggregationKind = mdSourceEnd.aggregationKind;
						mdSourceEnd.aggregationKind = tempAggregation;
						//set the navigability default to the target end 
						mdTargetEnd.isNavigable = true; //set the target navigable per default
						//create the association
						var newAssociation = new MDAssociation(mdSourceEnd,mdTargetEnd,associationID);
						newAssociation.isCrossMDZip = isCrossMDZip;
						
						
						//get the stereotype, search for base_Association with the md_guid
						XmlNode stereotypeNode = sourceFile.SelectSingleNode("//*[@base_Association='"+newAssociation.md_guid+"']", nsMgr);
						if(stereotypeNode != null)
						{
							if (!string.IsNullOrEmpty(stereotypeNode.LocalName))
							{
								newAssociation.stereotype = stereotypeNode.LocalName;
							}
						}

						foundAssociations.Add(associationID,newAssociation);
					}
					
					
				}
			}
			this._allAssociations = foundAssociations;
		}
		void getAllCrossMDzipAssociations()
		{
			var foundAssociations = new Dictionary<string,MDAssociation>();
			foreach (var crossAssociationKeyValue in this.allAssociations.Where(
											x => x.Value.isCrossMDZip ))
			{
				foundAssociations.Add(crossAssociationKeyValue.Key, crossAssociationKeyValue.Value);
			}
			this._allCrossMDzipAssociations = foundAssociations;
		}
		void getAllMessages()
		{
			var foundMessages = new List<MDMessage>();
			//first find all the Lifeline nodes
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				foreach (XmlNode messageNode in sourceFile.SelectNodes("//message")) 
				{
					//get the messageID
					XmlAttribute messageIDAttribute = messageNode.Attributes["xmi:id"];
				 	string messageID = messageIDAttribute != null ? messageIDAttribute.Value: string.Empty;
					//get the source lifelineID
					string sourceID = getLifeLineID(messageNode,true,nsMgr);
				 	//get the target lifelineID
				 	string targetID = getLifeLineID(messageNode,false,nsMgr);
				 	//get the name of the message
				 	XmlAttribute nameAttribute = messageNode.Attributes["name"];
				 	string messageName = nameAttribute != null ? nameAttribute.Value: string.Empty;
				 	//get the synchronous/asynchronous attribute
				 	XmlAttribute messageSortAttribute = messageNode.Attributes["messageSort"];
				 	bool asynchronousMessage = messageSortAttribute != null && "asynchSignal".Equals(messageSortAttribute.Value, StringComparison.InvariantCulture);
				 	//create message
				 	if (! string.IsNullOrEmpty(messageID)
				 		&& ! string.IsNullOrEmpty(sourceID)
				 	    && ! string.IsNullOrEmpty(targetID))
				 	{
				 		var mdMessage = new MDMessage(messageID,sourceID,targetID,messageName,asynchronousMessage);
				 		foundMessages.Add(mdMessage);
				 	}
				}
			}
			//set the list to the found messages
			_allMessages = foundMessages;
		}
		private string getLifeLineID(XmlNode messageNode, bool source,XmlNamespaceManager nsMgr)
		{
			//first get the occurenceID
			XmlAttribute occurenceIDAttribute = source ? messageNode.Attributes["sendEvent"] : messageNode.Attributes["receiveEvent"];
			if (occurenceIDAttribute != null)
			{
				string occurenceID = occurenceIDAttribute.Value;
				if (! string.IsNullOrEmpty(occurenceID))
				{
					//get the messageOccurenceNode
					XmlNode occurenceNode = messageNode.SelectSingleNode("..//fragment[@xmi:id='"+occurenceID+"']",nsMgr);
					if (occurenceNode != null)
					{
						//get the covered node
						XmlNode coveredNode = occurenceNode.SelectSingleNode("covered");
						if (coveredNode != null)
						{
							XmlAttribute idRefAttribute = coveredNode.Attributes["xmi:idref"];
							return idRefAttribute != null ? idRefAttribute.Value : string.Empty;
						}
					}
				}
			}
			//if not found then return empty string
			return string.Empty;
		}
		void getAllLifeLines()
		{
			var foundLifeLines = new Dictionary<string, string>();
			//first find all the Lifeline nodes
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				foreach (XmlNode lifeLineNode in sourceFile.SelectNodes("//lifeline")) 
				{
					//get the ID of the lifeline
					XmlAttribute idAttribute = lifeLineNode.Attributes["xmi:id"];
					if (idAttribute != null)
					{
						string lifelineID = idAttribute.Value;
						//get the represents attribute
						XmlAttribute representsAttribute = lifeLineNode.Attributes["represents"];
						if (representsAttribute != null)
						{
							string ownedAttributeID = representsAttribute.Value;
							//get the ownedAttribute that represents this lifeline
							XmlNode ownedAttributeNode = sourceFile.SelectSingleNode("//ownedAttribute[@xmi:id='"+ownedAttributeID+"']",nsMgr);
							if (ownedAttributeNode != null)
							{
								//get the type attribute
								XmlAttribute typeAttribute = ownedAttributeNode.Attributes["type"];
								if (typeAttribute != null)
								{
									string typeID = typeAttribute.Value;
									//add the lifeline to the list
									if (! string.IsNullOrEmpty(lifelineID)
									    && ! string.IsNullOrEmpty(typeID)
									    && ! foundLifeLines.ContainsKey(lifelineID))
									{
										foundLifeLines.Add(lifelineID,typeID);
									}
								}
							}
						}
					}
				}
			}
			_allLifeLines = foundLifeLines;
		}

		void getAllClasses()
		{
			var foundClasses = new Dictionary<string, string>();
			//first find all the class nodes
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				foreach (XmlNode classNode in sourceFile.SelectNodes("//packagedElement [@xmi:type='uml:Class']")) 
				{
					//get the ID attribute
					XmlAttribute idAttribute = classNode.Attributes["xmi:id"];
					string classID = idAttribute!= null ? idAttribute.Value: string.Empty;
					//get the name ID
					XmlAttribute nameAttribute = classNode.Attributes["name"];
					string className = nameAttribute!= null ? nameAttribute.Value: string.Empty;
					if (idAttribute != null
					    && ! foundClasses.Keys.Contains(classID))
					{
						foundClasses.Add(classID,className);
					}
						
				} 
			}
			//set the found classes
			_allClasses = foundClasses;
		}

		/// <summary>
		/// returns a dictionary of all constraints found in the Magicdraw source files
		/// </summary>
		/// <returns></returns>
		private Dictionary<string,List<MDConstraint>> getAllConstraints()
		{
			Dictionary<string,List<MDConstraint>> foundConstraints = new Dictionary<string, List<MDConstraint>>();
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				foreach (XmlNode constraintNode in sourceFile.SelectNodes("//ownedRule"))
				{
					//get the parent node
					var parentNode = constraintNode.ParentNode;
					//loop the attributes to get he type and id of the parent 
					string parentType = string.Empty;
					string parentID = string.Empty;
					foreach (XmlAttribute parentAttribute in parentNode.Attributes) 
					{
						switch (parentAttribute.Name) 
						{
							case "xmi:type":
								parentType = parentAttribute.Value;
								break;
							case "xmi:id":
								parentID = parentAttribute.Value;
								break;
						}
					}
					//only interested in constraints on classes
					if (parentType == "uml:Class"
					    && !string.IsNullOrEmpty(parentID))
					{
						List<MDConstraint> constraints;
						//check if the dictioary already contains our parent element
						if (foundConstraints.ContainsKey(parentID))
					    {
							constraints = foundConstraints[parentID];
					    }
						else
						{
							//not found, so create new one
							constraints = new List<MDConstraint>();
							foundConstraints.Add(parentID,constraints);
						}
						//create the MDConstraint
						
						try
						{
							string name = constraintNode.Attributes["name"].Value;
							string body = string.Empty;
							XmlNode bodyNode = constraintNode.SelectSingleNode("./specification/body");
							if (bodyNode != null) body = bodyNode.InnerText.Replace("\n","\r\n"); //replace LF with CRLF
							string language = string.Empty;
							XmlNode languageNode = constraintNode.SelectSingleNode("./specification/language");
							if (languageNode != null) language = languageNode.InnerText;
							//check if everything is filled in
							if (! string.IsNullOrEmpty(name)
							    && ! string.IsNullOrEmpty(body)
							    && ! string.IsNullOrEmpty(language))
							{
								//create new MDConstraint and add it to the list
								constraints.Add(new MDConstraint(name, body, language));
							}
						}
						catch(NullReferenceException)
						{
							//do nothing, constraints without name cannor be created
						}
					}
				} 
			}
			return foundConstraints;
		}
		
		void getAllGuards()
		{
			// object MDGuard aanmaken
			var foundGuards = new List<MDGuard>();
			
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				foreach (XmlNode behaviourNode in sourceFile.SelectNodes("//ownedBehavior [@xmi:type='uml:Activity']", nsMgr))
				{
					// <ownedDiagram xmi:type='uml:Diagram'
					foreach (XmlNode diagramNode in behaviourNode.SelectNodes(".//ownedDiagram"))
					{
						XmlNode binaryObjectNode = diagramNode.SelectSingleNode(".//binaryObject");
						if (binaryObjectNode != null)
						{
							try
							{
								string diagramContentFileName = binaryObjectNode.Attributes["streamContentID"].Value;
								string sourceDirectory = Path.GetDirectoryName(sourceFile.BaseURI.Substring(8));
								string diagramFileName = Path.Combine(sourceDirectory,diagramContentFileName);
								
								if (File.Exists(diagramFileName))
								{
									var xmlDiagram  =new XmlDocument();
									XmlReaderSettings settings = new XmlReaderSettings { NameTable = new NameTable() };
									XmlNamespaceManager xmlns = new XmlNamespaceManager(settings.NameTable);
									xmlns.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
									XmlParserContext context = new XmlParserContext(null, xmlns, "", XmlSpace.Default);
									XmlReader reader = XmlReader.Create(diagramFileName, settings, context);
									xmlDiagram.Load(reader);
									
									
									foreach (XmlNode controlFlowNode in xmlDiagram.SelectNodes(".//mdElement [@elementClass='ControlFlow']")) 
									{
										
										// check guard condition node exists
										var guardConditionNode = controlFlowNode.SelectSingleNode(".//text");
										if (guardConditionNode != null)
										{
											//get the guard condition text
											string guardCondition = string.Empty;
											guardCondition = guardConditionNode.InnerText;
											
											//get the end object
											string linkFirstEndID = string.Empty;
											var endObjectNode = controlFlowNode.SelectSingleNode(".//linkFirstEndID");
											if (endObjectNode != null)
											{
												XmlAttribute linkFirstEndIdAttribute = endObjectNode.Attributes["xmi:idref"];
												linkFirstEndID = linkFirstEndIdAttribute != null ? linkFirstEndIdAttribute.Value : string.Empty;
											}
											
											//get the start object 
											string linkSecondEndID = string.Empty;
											var startObjectNode = controlFlowNode.SelectSingleNode(".//linkSecondEndID");
											if (startObjectNode != null)
											{
												XmlAttribute linkSecondEndIdAttribute = startObjectNode.Attributes["xmi:idref"];
												linkSecondEndID = linkSecondEndID != null ? linkSecondEndIdAttribute.Value : string.Empty;
											}									
										
										
											// source MDelement by ID
											var sourceElementNode = xmlDiagram.SelectSingleNode(".//mdElement [@xmi:id='"+ linkSecondEndID +"']", nsMgr);
											var sourceIdNode = sourceElementNode.SelectSingleNode(".//elementID");
											string sourceID = string.Empty;
											sourceID = getID(sourceIdNode);
											
											// target MDelement by ID
											var targetElementNode = xmlDiagram.SelectSingleNode(".//mdElement [@xmi:id='"+ linkFirstEndID +"']", nsMgr);
											var targetIdNode = targetElementNode.SelectSingleNode(".//elementID");
											string targetID = string.Empty;
											targetID = getID(targetIdNode);	
											
											
											var mdGuard = new MDGuard(guardCondition,sourceID, targetID);
											foundGuards.Add(mdGuard);
											
										}
										
								
								
										
										
										
									}
								}
							}
							catch(NullReferenceException)
							{
							//do nothing, we can't do anything with bynary object nodes withotu a streamContentID
							}
						}
					}
				}
			}
			_allGuards = foundGuards;
		}
		

		void getAllDiagrams()
		{
			var foundDiagrams = new Dictionary<string, MDDiagram>();
			//first find all the diagram nodes
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				foreach (XmlNode diagramNode in sourceFile.SelectNodes("//ownedDiagram"))
				{
					string diagramName = diagramNode.Attributes["name"].Value;
					//get the ID of the diagram. this is a combination of the ID of the owner + the name of the diagram
					string diagramID = diagramNode.Attributes["ownerOfDiagram"].Value +""+ diagramName ;
					//get the streamcontentID like <binaryObject streamContentID=BINARY-f9279de7-2e1e-4644-98ca-e1e496b72a22 
					// because that is the file we need to read and use to figure out the diagramObjects
					XmlNode binaryObjectNode = diagramNode.SelectSingleNode(".//binaryObject");
					if (binaryObjectNode != null)
					{
						MDDiagram currentDiagram = null;
						try
						{
							string diagramContentFileName = binaryObjectNode.Attributes["streamContentID"].Value;
							
							//get the file with the given name
							//get the directory of the source file
							string sourceDirectory = Path.GetDirectoryName(sourceFile.BaseURI.Substring(8));
							string diagramFileName = Path.Combine(sourceDirectory,diagramContentFileName);
							if (File.Exists(diagramFileName))
							{
								//all this workaround is needed because xmi is not defined as prefix in the binary files of MD
								var xmlDiagram  =new XmlDocument();
								XmlReaderSettings settings = new XmlReaderSettings { NameTable = new NameTable() };
								XmlNamespaceManager xmlns = new XmlNamespaceManager(settings.NameTable);
								xmlns.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
								XmlParserContext context = new XmlParserContext(null, xmlns, "", XmlSpace.Default);
								XmlReader reader = XmlReader.Create(diagramFileName, settings, context);
								xmlDiagram.Load(reader);
								
								//xmlDiagram.Load(diagramFileName);
								foreach (XmlNode diagramObjectNode in xmlDiagram.SelectNodes(".//mdElement")) 
								{
									//create the object
									MDDiagramObject diagramObject;
									//get the elementID
									string elementID = getElementID(diagramObjectNode);
									//get the umlType of the elementNode
									XmlAttribute umlTypeAttribute = diagramObjectNode.Attributes["elementClass"];
									string umlType = umlTypeAttribute != null ? umlTypeAttribute.Value : string.Empty;
									
									if (!string.IsNullOrEmpty(elementID)
									   || umlType == "Split")
									{
										//get the geometry
										var geometryNode = diagramObjectNode.SelectSingleNode(".//geometry");
										if (geometryNode != null
										    && ! string.IsNullOrEmpty(geometryNode.InnerText))
										{
											if (currentDiagram ==null) currentDiagram = new MDDiagram(diagramName);
											
											currentDiagram.id = elementID;
											
											//handle the notes
											if(umlType == "Note")
											{
												//get the ID
												XmlAttribute noteIdAttribute = diagramObjectNode.Attributes["xmi:id"];
												string noteId = noteIdAttribute != null ? noteIdAttribute.Value : string.Empty;
												
												//get the text
												string text = string.Empty;
												var textNode = diagramObjectNode.SelectSingleNode(".//text");
												if (textNode != null
										    	&& ! string.IsNullOrEmpty(textNode.InnerText))
												{
													text = textNode.InnerText;
												}
												
												//get the linked element
												string linkedElement = string.Empty;
												var linkedElementNode = diagramObjectNode.SelectSingleNode(".//elementID");
												if (linkedElementNode != null)
												{
													XmlAttribute linkedElementAttribute = linkedElementNode.Attributes["href"];
													string fullHrefValue = linkedElementAttribute != null ? linkedElementAttribute.Value : string.Empty;
													//get the part after the # sign
													var splittedHref = fullHrefValue.Split('#');
													if (splittedHref.Count() == 2)
													{
														linkedElement = splittedHref[1];
													}
												}


												var note = new MDNote(noteId,text,linkedElement);
												currentDiagram.addDiagramNote(note);
												diagramObject = new MDDiagramObject(noteId,geometryNode.InnerText,umlType);
											}
											else
											{
												diagramObject = new MDDiagramObject(elementID,geometryNode.InnerText,umlType);
											}

												currentDiagram.addDiagramObject(diagramObject);
												
												
												if (umlType == "Split")
												{
													XmlNode fragmentNode = diagramObjectNode.ParentNode.ParentNode;
													if (fragmentNode.Name == "mdElement")
													{
														//get the id of the fragment
														string fragmentID = getElementID(fragmentNode);
														if (! string.IsNullOrEmpty(fragmentID))
														{
															//find the corresponding diagramObject from the current diagram
															var fragmentDiagramObject = currentDiagram.diagramObjects.FirstOrDefault (x => x.mdID.Equals(fragmentID,StringComparison.CurrentCultureIgnoreCase));
															if (fragmentDiagramObject != null)
															{
																fragmentDiagramObject.ownedSplits.Add(diagramObject);
															}
														}
													}
												}
											

											

										}
									}
								}
								//add the diagram to the list
								if (currentDiagram != null 
								    && ! foundDiagrams.ContainsKey(diagramID))
								{
									foundDiagrams.Add(diagramID,currentDiagram);
								}
							}
						}
						catch(NullReferenceException)
						{
							//do nothing, we can't do anything with bynary object nodes withotu a streamContentID
						}
					}
				}
			}
			_allDiagrams = foundDiagrams;
		}
		
		/// <summary>
		/// checks if a node is defined in another mdzip file
		/// </summary>
		/// <param name="node">the node to check</param>
		/// <returns>true if the attribute href is found indicating that this node is defined in another file</returns>
		bool isForeign(XmlNode node)
		{
			//if it has a href attribute then it is foreign (and defined in another mdzip file
			XmlAttribute hrefAttribute = node.Attributes["href"];
			return hrefAttribute != null;
		}
		string getID (string idString)
		{
			string elementID = string.Empty;
			int seperatorIndex = idString.IndexOf('#');
			if (seperatorIndex >= 0 )
			{
				elementID =  idString.Substring(seperatorIndex +1);
			}
			else
			{
				elementID = idString;
			}
			return elementID;
		}
		bool isForeign(string idString)
		{
			return idString.IndexOf('#') >= 0;
		}
		string getID(XmlNode node)
		{
			string elementID = string.Empty;
			
			if (node != null)
			{
				//first theck the href attribute
				XmlAttribute hrefAttribute = node.Attributes["href"];
				
				if (hrefAttribute != null)
				{
					string fullHrefString = node.Attributes["href"].Value;
					int seperatorIndex = fullHrefString.IndexOf('#');
					if (seperatorIndex >= 0 )
					{
						elementID =  fullHrefString.Substring(seperatorIndex +1);
					}
				}
				else
				{
					//check the "xmi:idref attribute
					XmlAttribute idRefAttribute = node.Attributes["xmi:idref"];
					if (idRefAttribute == null)
					{
						//check the xmi:id attribute
						idRefAttribute = node.Attributes["xmi:id"];
					}
					if (idRefAttribute != null) 
					{
						elementID = idRefAttribute.Value;
					}
				}
			}
			
			return elementID;
		}
		
		string getElementID(XmlNode diagramObjectNode)
		{		
			//get the elementID
			var node = diagramObjectNode.SelectSingleNode(".//elementID");
			return getID(node);
		}
		
		void getAllObjects()
		{
			var foundObjects = new Dictionary<string, string>();
			//first find all the object nodes
			
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				string objectId = "", inState = "", objectState = "";
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				//[@xmi:type='uml:CentralBufferNode']"
				
				foreach (XmlNode objectNode in sourceFile.SelectNodes("//node[@xmi:type='uml:CentralBufferNode']", nsMgr))
				{
					try
					{
						var id = objectNode.Attributes["xmi:id"].Value;
						if (id != null)
						{
							objectId = id; //md_guid
							EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Getting objectNode with ObjectID: '{1}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                  	,objectId)
	                                	
	                   		,0
	                  		,LogTypeEnum.log);
						}
						
						XmlNode inStateNode = objectNode.SelectSingleNode(".//inState");
						if(inStateNode != null)
						{
							var idref = inStateNode.Attributes["xmi:idref"].Value;
							if (idref != null)
							{
								inState = idref;
								EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Getting inState value '{1}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                  	,inState)
	                                	
	                   		,0
	                  		,LogTypeEnum.log);
							}			
						}
					
						XmlNode stateNode = sourceFile.SelectSingleNode("//subvertex[@xmi:type='uml:State' and @xmi:id='"+ inState +"']", nsMgr);
						if(stateNode != null)
						{
							var name = stateNode.Attributes["name"].Value;
							if (name != null)
							{
								objectState = name;
								EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Getting object state '{1}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                  	,objectState)
	                                	
	                   			,0
	                  			,LogTypeEnum.log);
							}	
						}
					}
					catch (NullReferenceException)
					{
			
						
					}	
					if (!string.IsNullOrEmpty(objectId) 
					    && !string.IsNullOrEmpty(inState) 
					    && !string.IsNullOrEmpty(objectState)
					    && !foundObjects.ContainsKey(objectId))
					{
						foundObjects.Add(objectId,objectState);
					}
				
				}
				
			}
			_allObjects = foundObjects;
		}
		
		
		void getAllMapsToDependencies()
		{
			var foundDependencies = new List<MDElementRelation>();
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				foreach (XmlNode mapsToNode in sourceFile.SelectNodes("//*[local-name() = 'mapsTo']",nsMgr)) 
				{
					
					XmlAttribute dependencyAttribute = mapsToNode.Attributes["base_Dependency"];
					
					if (dependencyAttribute != null)
					{
						string dependencyID = dependencyAttribute.Value;
						if (this.allMDElementRelations.ContainsKey(dependencyID))
						{
							foundDependencies.Add(allMDElementRelations[dependencyID]);
						}						
					}				
				}
			}
			_allMapsToDependencies = foundDependencies;
		}
		
		void getAllAttDependencies()
		{
			var foundDependencies = new List<MDDependency>();
			
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				foreach (XmlNode dependencyNode in sourceFile.SelectNodes("//packagedElement[@xmi:type='uml:Dependency']", nsMgr))
				{
						//<client xmi:idref='_18_2_b9402f1_1467202520981_286657_153229'/>
						//<supplier xmi:idref='_17_0_2_2_eac0340_1366370215876_17265_33738'/>
						//select client node, attribute idref
						string source = "", target = "";
						XmlNode clientNode = dependencyNode.SelectSingleNode("./client");
						if (clientNode != null)
						{
							source = getID(clientNode);
							
							// _17_0_2_eac0340_1354547327504_709036_28090   //Balance Responsible Party ID
							
						}
						XmlNode supplierNode = dependencyNode.SelectSingleNode("./supplier");
						if (supplierNode != null)
						{
							target = getID(supplierNode);
						}
						
						if (!string.IsNullOrEmpty(target) && !string.IsNullOrEmpty(source))
						{
							var mdDependency = new MDDependency();
							mdDependency.sourceGuid = source;
						 	mdDependency.targetGuid = target;
							//first check if both id's are in the list of attributes
							if (containsDependencies(mdDependency))
							{
								foundDependencies.Add(mdDependency);
							}
							
						}
				}
			}
			_allAttDependencies = foundDependencies;
		}
		
		bool containsDependencies(MDDependency dependency)
		{
			var sourceAttribute = allAttributes.FirstOrDefault( x => x.mdGuid == dependency.sourceGuid);
			var targetAttribute = allAttributes.FirstOrDefault( x => x.mdGuid == dependency.targetGuid);
			bool contains = sourceAttribute != null 
						&& targetAttribute != null;
			if (contains)
			{
				dependency.sourceName = sourceAttribute.name;
				dependency.targetName = targetAttribute.name;
				dependency.sourceParentGuid = sourceAttribute.mdParentGuid;
				dependency.targetParentGuid = targetAttribute.mdParentGuid;
			}
			
			return contains;
		}
		
		void getAllAttributeAssociationRoles()
		{
			var foundAttributeAssociationRoles = new Dictionary<string,MDAssociationEnd>();
			foreach (var sourceFile in this.sourceFiles.Values)
			{
		
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");	
				
				foreach (XmlNode attributeRoleNode in sourceFile.SelectNodes("//ownedAttribute[@xmi:type='uml:Property' and @association]", nsMgr))
				{
					//get the id from the ownedAttributeNode
					string roleMDGuid = this.getID(attributeRoleNode);
					var newAssociationEnd = this.createAssociationEnd(attributeRoleNode);
					if (! string.IsNullOrEmpty(roleMDGuid) && newAssociationEnd != null)
					{
						newAssociationEnd.isNavigable = true;//per default ownedAttributes are always navigable
						foundAttributeAssociationRoles.Add(roleMDGuid,newAssociationEnd);		
					}
				
				}				
			
			}
			_allAttributeAssociationRoles = foundAttributeAssociationRoles;
	
		}
		
		MDAssociationEnd createAssociationEnd(XmlNode roleNode)
		{
			string name = string.Empty;
			string lowerBound = string.Empty;
			string upperBound = string.Empty;
			string targetTypeID = string.Empty;
			string aggregationType = string.Empty;
			bool hasForeignType = false;
			//get the name from the attribute name
			XmlAttribute nameAttribute = roleNode.Attributes["name"];
			name = nameAttribute != null ? nameAttribute.Value: string.Empty;
			//get the targeTypeID from attribute href (after the # sign) in the subnode type
			XmlNode targetTypeNode = roleNode.SelectSingleNode("./type");
			if (targetTypeNode != null)
			{
				//type is stored as xmlNode
				hasForeignType = this.isForeign(targetTypeNode);
				targetTypeID = getID(targetTypeNode);
			}
			else
			{
				//type is stored as an attribute
				XmlAttribute typeAttribute = roleNode.Attributes["type"];
				if (typeAttribute != null)
				{
					hasForeignType = this.isForeign(typeAttribute.Value);
					targetTypeID = getID(typeAttribute.Value);
				}
			}
			//get the lowerBound
			XmlNode lowerBoundNode = roleNode.SelectSingleNode(".//lowerValue");
			if (lowerBoundNode != null)
			{
				XmlAttribute valueAttribute = lowerBoundNode.Attributes["value"];
				lowerBound = valueAttribute != null ? valueAttribute.Value: "0";
			}
			//get the upperBound
			XmlNode upperBoundNode = roleNode.SelectSingleNode(".//upperValue");
			if (upperBoundNode != null)
			{
				XmlAttribute valueAttribute = upperBoundNode.Attributes["value"];
				upperBound = valueAttribute != null ? valueAttribute.Value: string.Empty;
			}
			//get the aggregationKind
			XmlAttribute aggregationAttribute = roleNode.Attributes["aggregation"];
            aggregationType = aggregationAttribute != null ? aggregationAttribute.Value : string.Empty;
            
            //get the association order
            int associationOrder = getOwnedAttributeOrder(roleNode);

            //create the association end
            if (! string.IsNullOrEmpty(targetTypeID))
			{
				var newAssociationEnd = new MDAssociationEnd();
				newAssociationEnd.aggregationKind = aggregationType;
				newAssociationEnd.endClassID = targetTypeID;
				newAssociationEnd.lowerBound = lowerBound;
				newAssociationEnd.upperBound = upperBound;
				newAssociationEnd.name = name;
				newAssociationEnd.hasForeignType = hasForeignType;
                newAssociationEnd.sequenceKey = associationOrder;
				return newAssociationEnd;
			}
			//return null if target type ID not found
			return null;
		}
        /// <summary>
        /// get the order in which this ownedAttribute occurs within its owning elemnet.
        /// Each attribute counts for 100, each association for 5
        /// </summary>
        /// <param name="propertyNode">the propertyNode to get the order for</param>
        /// <returns>the order relative to the attributes and associations</returns>
        int getOwnedAttributeOrder(XmlNode propertyNode)
        {
            int sequenceKey = 0;
            //get the owning elemnet
            foreach (XmlNode ownedAttributeNode in propertyNode.ParentNode.SelectNodes(".//ownedAttribute"))
            {
                sequenceKey++;
                if (getID(propertyNode) == getID(ownedAttributeNode)) break;
            }
            return sequenceKey;
        }
		void getAllAttributes()
		{
			var foundAttributes = new List<MDAttribute>();
			
			foreach (var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");	
				
				foreach (XmlNode elementNode in sourceFile.SelectNodes(".//packagedElement[@xmi:type='uml:Class']", nsMgr))
				{
					//get the element mdID
					XmlAttribute elementIDAttribute = elementNode.Attributes["xmi:id"];
					string elementID = elementIDAttribute != null ? elementIDAttribute.Value:string.Empty;
					
					//Debug.WriteLine("Class: " + elementNode.Attributes["name"].Value);
					
					
					foreach (XmlNode attributeNode in elementNode.SelectNodes(".//ownedAttribute[@xmi:type='uml:Property' and not(@association) and not(@aggregation)]", nsMgr))
					{
						
						//get the attribute mdID
						string attributeMDGuid = getID(attributeNode);
						
						//get the attribute name
						XmlAttribute nameAttribute = attributeNode.Attributes["name"];
						string attributeName = nameAttribute != null ? nameAttribute.Value:string.Empty;
						
						//get the attribute type
						XmlNode typeNode = attributeNode.SelectSingleNode("./type");
						string typeID = getID(typeNode);
						bool isForeignType = typeNode != null && this.isForeign(typeNode);

                        var attributeSequence = getOwnedAttributeOrder(attributeNode);

                        if (! string.IsNullOrEmpty(attributeMDGuid) && ! string.IsNullOrEmpty(attributeName))
						{
							var mdAttribute = new MDAttribute();
							mdAttribute.mdGuid = attributeMDGuid;
							mdAttribute.name = attributeName;
							mdAttribute.mdParentGuid = elementID;
							mdAttribute.isCrossMDZip = isForeignType;
							mdAttribute.typeMDGuid = typeID;
                            mdAttribute.sequencingKey = attributeSequence;
							foundAttributes.Add(mdAttribute);
							
						}
					}
					
				}
				
				
			}
			_allAttributes = foundAttributes;
		}
		
		void getAllTimeEvents()
		{
			var foundEvents = new Dictionary<string,MDTimeEvent>();
			
			foreach(var sourceFile in this.sourceFiles.Values)
			{
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				foreach (XmlNode actionNode in sourceFile.SelectNodes("//node[@xmi:type='uml:AcceptEventAction']", nsMgr))
				{
					//get the element mdID from the AcceptEventAction node
					string elementID = getID(actionNode); //mdGuid from the object in EA
					string eventID = string.Empty;
					
					//get the triggernode and event
					XmlNode triggerNode = actionNode.SelectSingleNode(".//trigger", nsMgr);
					if (triggerNode != null)
					{
						XmlAttribute eventAttribute = triggerNode.Attributes["event"];
						eventID = eventAttribute != null ? eventAttribute.Value:string.Empty;
					}
					
					//get the TimeEvent and the value
					
					if(!string.IsNullOrEmpty(eventID))
					{
						XmlNode timeEventNode = sourceFile.SelectSingleNode(".//packagedElement[@xmi:id='"+ eventID +"']", nsMgr);
						if (timeEventNode != null)
						{
							XmlNode expressionNode = timeEventNode.SelectSingleNode(".//expr", nsMgr);
							
							if (expressionNode != null)
							{
								XmlAttribute valueAttribute = expressionNode.Attributes["value"];
								string value = valueAttribute != null ? valueAttribute.Value:string.Empty;
								
								
								if (!string.IsNullOrEmpty(elementID) && !string.IsNullOrEmpty(value))
								{
									var mdTimeEvent = new MDTimeEvent(elementID,value);
									foundEvents.Add(elementID,mdTimeEvent);
								}	
							}
							
						}
					}
					

					
					

				}
			}
			_allTimeEvents = foundEvents;
		}
		
		void getAllPartitions()
		{
			var foundPartitions = new Dictionary<string, string>();
			
			//first find all the object nodes
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				string partitionID = "", representsID = "";
				XmlNamespaceManager nsMgr = new XmlNamespaceManager(sourceFile.NameTable);
				nsMgr.AddNamespace("xmi", "http://www.omg.org/spec/XMI/20131001");
				nsMgr.AddNamespace("uml", "http://www.omg.org/spec/UML/20131001");
				
				foreach (XmlNode partitionNode in sourceFile.SelectNodes("//group[@xmi:type='uml:ActivityPartition']", nsMgr))
				{
					//Look for the <group> node based on the md_guid, md_guid = xmi:id in the group node
					try
					{
						//Get the partition id
						var partition = partitionNode.Attributes["xmi:id"].Value;
						if (partition != null)
						{
							partitionID = partition;
							EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Getting partitionID: '{1}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                  	,partitionID)
	                                	
	                   		,0
	                  		,LogTypeEnum.log);
						}
						
						//Get the represents id
						var represents = partitionNode.Attributes["represents"].Value;
						if (represents != null)
						{
							representsID = represents;
							EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Getting representsID: '{1}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                  	,representsID)
	                                	
	                   		,0
	                  		,LogTypeEnum.log);
						}
					}
					catch (NullReferenceException)
					{
			
						
					}	
					if(!string.IsNullOrEmpty(partitionID) 
					   && !string.IsNullOrEmpty(representsID)
					   && !foundPartitions.ContainsKey(partitionID))
					{
						foundPartitions.Add(partitionID, representsID);
					}
				}
				
			}
			_allPartitions = foundPartitions;
		}
		
		public string getDiagramOnwerID(string diagramID)
		{
			return diagramID.Substring(0,diagramID.IndexOf(""));
		}
		
		private void getAllAssociationTables()
		{
			var foundTables = new Dictionary<string,string>();
			//find the comment nodes containing the association tables.
			//these are on the diagrams in MagicDraw.
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				foreach (XmlNode constraintNode in sourceFile.SelectNodes("//ownedDiagram/ownedComment"))
				{
					
					//get the body attribute and check if it starts with "'&lt;" (html content)
					try
					{
						XmlAttribute bodyAttribute = constraintNode.Attributes["body"];
						if (bodyAttribute.Value.StartsWith("<html>"))
					    {
					    	//we have html comments
					    	//get the parentPackage node
					    	XmlNode packageNode = constraintNode.ParentNode.ParentNode.ParentNode.ParentNode;
					    	if (packageNode.Name == "packagedElement" && packageNode.Attributes["xmi:type"].Value == "uml:Package" )
					    	{
					    		//OK we have the package node. Now check if it contains a class with the same name as the diagram
						    	//check if there is a class in the owning package with the same name
						    	foreach (XmlNode ownedElementNode in packageNode.ChildNodes) 
						    	{
						    		if (ownedElementNode.Name == "packagedElement" 
						    		    && ownedElementNode.Attributes["xmi:type"].Value == "uml:Class" //is of type class
						    		    && ownedElementNode.Attributes["name"].Value.Equals(constraintNode.ParentNode.Attributes["name"].Value,StringComparison.InvariantCultureIgnoreCase)) //name corresponds to the diagram
						    		{
						    			string tableID = ownedElementNode.Attributes["xmi:id"].Value;
						    			if (! foundTables.ContainsKey(tableID))
						    			{
						    				//actually add the association table to the list
						    				foundTables.Add(tableID,bodyAttribute.Value);
						    			}
						    		}
						    	}
					    	}
					    }
					}
					catch(NullReferenceException)
					{
						//do nothing, constraints without name cannor be created
					}				
				}
			}
			//save the association tables
			_allLinkedAssociationTables = foundTables; 
			
		}
	}
}
