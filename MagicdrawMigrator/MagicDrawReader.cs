using System.Collections.Generic;
using System.Linq;
using System;
using System.Xml;
using System.IO;
using System.IO.Compression;
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
		public MagicDrawReader(string mdzipPath)
		{
			this.mdzipPath = mdzipPath;
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
	}
}
