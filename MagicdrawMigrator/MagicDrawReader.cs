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
				ZipFile.ExtractToDirectory(fileName,Path.Combine(tempDirectory.FullName,Path.GetFileName(fileName)));
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
			var constraints = new List<MDConstraint>();
			foreach (var sourceFile in this.sourceFiles.Values) 
			{
				foreach (XmlNode constraintElementNode in sourceFile.SelectNodes("//constrainedElement[xmi:idref='"+MDElementID + "']"))
				{
					string name = constraintElementNode.ParentNode.Attributes.GetNamedItem("name").InnerText;
					string body = string.Empty;
					string language = string.Empty;
					foreach (XmlNode childNode in constraintElementNode.ParentNode.ChildNodes) 
					{
;
						if (childNode.Name == "specification")
						{
							//get the body
							var bodyNode = childNode.SelectSingleNode("/body");
							if (bodyNode != null)
							{
								body = bodyNode.InnerText;
							}
						}
						else if (childNode.Name == "language")
						{
							language = childNode.InnerText;
						}
					}
					//check if everything is filled in
					if (! string.IsNullOrEmpty(name)
					    && ! string.IsNullOrEmpty(body)
					    && ! string.IsNullOrEmpty(language))
					{
						//create new MDConstraint
						constraints.Add(new MDConstraint(name, body, language));
					}
				} 
			}
			return constraints;
		}
	}
}
