using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml;
using System.IO;
using System.IO.Compression;
using EAAddinFramework.Utilities;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


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
		Dictionary<string,string> _allLinkedAssociationTables;
		Dictionary<string, MDDiagram> _allDiagrams;
		Dictionary<string, string> _allObjects;
		Dictionary<string,List<MDConstraint>> allConstraints;
		Dictionary<string,XmlDocument> _sourceFiles;
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
			this.outputName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
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
							if (bodyNode != null) body = bodyNode.InnerText;
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
									//get the elementID
									var elementIDNode = diagramObjectNode.SelectSingleNode(".//elementID");
									if (elementIDNode != null)
									{
									
										//first theck the href attribute
										XmlAttribute hrefAttribute = elementIDNode.Attributes["href"];
										string elementID = string.Empty;
										if (hrefAttribute != null)
										{
											string fullHrefString = elementIDNode.Attributes["href"].Value;
											int seperatorIndex = fullHrefString.IndexOf('#');
											if (seperatorIndex >= 0 )
											{
												elementID =  fullHrefString.Substring(seperatorIndex +1);
											}
										}
										else
										{
											//check the "xmi:idref attribute
											XmlAttribute idRefAttribute = elementIDNode.Attributes["xmi:idref"];
											if (idRefAttribute != null) elementID = idRefAttribute.Value;
										}
										
										if (!string.IsNullOrEmpty(elementID))
										{
											//get the geometry
											var geometryNode = diagramObjectNode.SelectSingleNode(".//geometry");
											if (geometryNode != null
											    && ! string.IsNullOrEmpty(geometryNode.InnerText))
											{
												if (currentDiagram ==null) currentDiagram = new MDDiagram(diagramName);
												var diagramObject = new MDDiagramObject(elementID,geometryNode.InnerText);
												currentDiagram.addDiagramObject(diagramObject);
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
					if (!string.IsNullOrEmpty(objectId) & !string.IsNullOrEmpty(inState) & !string.IsNullOrEmpty(objectState))
					{
						foundObjects.Add(objectId,objectState);
					}
				
				}
				
			}
			_allObjects = foundObjects;
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
