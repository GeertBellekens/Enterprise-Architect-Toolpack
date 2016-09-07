
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;
using System.Net.Http;
using System.IO;
using EAAddinFramework.Utilities;
using System.IO.Compression;

namespace EAImvertor
{
	/// <summary>
	/// Description of EAImvertorJob.
	/// </summary>
	public class EAImvertorJob
	{
		private UML.Classes.Kernel.Package _sourcePackage;
		private string _jobID;
		private string _status;
		private string _zipUrl;
		private string _docUrl;
		private EAImvertorJobSettings _settings;
		private BackgroundWorker _backgroundWorker;
		private DateTime _startDateTime;
		private bool _timedOut = false;
		private string _message;
		private List<EAImvertorException> _warnings = new List<EAImvertorException>();
		private List<EAImvertorException> _errors = new List<EAImvertorException>();
		private string _downloadPath = string.Empty;
		private bool _startRequested = false;

		public string downloadPath {
			get {
				return _downloadPath;
			}
		}
		public string message 
		{
			get {return _message;}
		}
		public List<EAImvertorException> warnings 
		{
			get {return _warnings;}
		}
		public List<EAImvertorException> errors 
		{
			get {return _errors;}
		}
		public bool timedOut
		{
			get {return _timedOut;}
		}
		public EAImvertorJobSettings settings
		{
			get {return this._settings;}
		}
		public void showReport()
		{
			if (! string.IsNullOrEmpty(this._docUrl))
			{
				System.Diagnostics.Process.Start(this._docUrl);
			}
		}
		private string reportUrl
		{
			get{return _settings.imvertorURL+ this.settings.urlPostFix + "report?pin=" + settings.PIN + "&job=" + _jobID;}
		}
		private string startUrl
		{
			get{return _settings.imvertorURL+ this.settings.urlPostFix + "execute?pin=" + settings.PIN + "&job=" + _jobID;}
		}
		public EAImvertorJob(UML.Classes.Kernel.Package package, EAImvertorJobSettings settings)
		{
			this._sourcePackage = package;
			this._settings = settings;
			this._status = "Created";
		}
		//public properties
		public UML.Classes.Kernel.Package sourcePackage
		{
			get { return this._sourcePackage; }
		}
		public string jobID
		{
			get { return this._jobID; }
		}
		public string status
		{
			get { return this._status; }
		}
		public int tries {get;set;}

		private void setStatus(string jobStatus )
		{
			int jobStatusInt;
			if (int.TryParse(jobStatus, out jobStatusInt))
			{
				switch (jobStatusInt) 
				{
					case 1:
						setStatus("Queued");
						break;
					case 2:
						setStatus( "In Progress");
						break;
					case 3:
						setStatus( "Compiled");
						break;
					default:
						setStatus("Error");
						break;
				}
			}
			else
			{
				if (this._status != jobStatus)
				{
					//reset tries
					this.tries = 0;
				}
				this._status = jobStatus;
				if (this._backgroundWorker != null && this._backgroundWorker.IsBusy)
				{
					this._backgroundWorker.ReportProgress(0,this);
				}
			}
		}
		private static string CompressFile(string sourceFileName)
	    {
	        using (ZipArchive archive = ZipFile.Open(Path.ChangeExtension(sourceFileName, ".zip"), ZipArchiveMode.Create))
	        {
	            archive.CreateEntryFromFile(sourceFileName, Path.GetFileName(sourceFileName));
	        }
	        return Path.ChangeExtension(sourceFileName, ".zip");
	    }
		//public void startJob(string imvertorURL, string pincode,string processName ,string imvertorProperties,string imvertorPropertiesFilePath, string imvertorHistoryFilePath)
		public void startJob( BackgroundWorker backgroundWorker)
		{
			this._startDateTime = DateTime.Now;
			this._backgroundWorker = backgroundWorker;
			//create the specific properties for this job
			this.settings.PropertiesFilePath = createSpecificPropertiesFile();
			string xmiFileName = Path.ChangeExtension(Path.GetTempFileName(),".xmi");
			this.setStatus("Exporting Model");
			this.sourcePackage.getRootPackage().exportToXMI(xmiFileName);
			this.setStatus("Compressing File");
			if (File.Exists(xmiFileName))
			{
				xmiFileName = CompressFile(xmiFileName);
				this.setStatus("Uploading Model");
				try {
					this._jobID = this.Upload(settings.imvertorURL+settings.urlPostFix +"upload",settings.PIN,settings.ProcessName,settings.Properties
				                           ,xmiFileName,settings.HistoryFilePath,settings.PropertiesFilePath);
	
				this.setStatus("Upload Finished");
				getJobReport();
					
				} catch (Exception e) 
				{
					this.setStatus("Error");
					Logger.logError("Error in StartJob: " + e.Message + " stacktrace: "+ e.StackTrace);
				}
			}else
			{
				this.setStatus("Cancelled");
			}
		}
		private string createSpecificPropertiesFile()
		{
			UML.Classes.Kernel.Package projectPackage = getProjectPackage(this.sourcePackage);
			string propertiesContent = this.getDefaultPropertiesFileContent();
			//add application name
			propertiesContent += Environment.NewLine + "application = " + this.sourcePackage.name;
			if (projectPackage != null)
			{
				var nameparts = projectPackage.name.Split(':');
				if (nameparts.Count() >= 2)
				{
					string ownerName = nameparts[0].Trim();
					string projectName = nameparts[1].Trim();
					//add owner name
					if (ownerName.Length > 0 ) propertiesContent += Environment.NewLine + "owner = " + ownerName;
					if (projectName.Length > 0 ) propertiesContent += Environment.NewLine + "project = " + projectName;											
				}
			}
			//create file
			string tempFilePath = Path.GetTempFileName();
			File.WriteAllText(tempFilePath,propertiesContent);
			return tempFilePath;
		}
		private string getDefaultPropertiesFileContent()
		{
			if (File.Exists(this.settings.PropertiesFilePath))
			{
				return File.ReadAllText(this.settings.PropertiesFilePath);
			}
			return string.Empty;
		}
		private UML.Classes.Kernel.Package getProjectPackage(UML.Classes.Kernel.Package startingPackage)
		{
			if (startingPackage.owningPackage == null) return null;
			if (startingPackage.owningPackage.stereotypes.Any(x => x.name.Equals("project", StringComparison.InvariantCultureIgnoreCase)))
			{
				return startingPackage.owningPackage;
			}
			else
			{
				return getProjectPackage(startingPackage.owningPackage);
			}
		}
		public void refreshStatus()
		{
			//set timeout to 1 second to only try once
			this._settings.timeOutInSeconds = 1;
			this.getJobReport();
		}
		public void openResults()
		{
			if (! string.IsNullOrEmpty(this._downloadPath))
			{
				System.Diagnostics.Process.Start(this._downloadPath);
			}
		}
		public void viewWarnings()
		{
			 var outputItems = new List<UML.Extended.UMLModelOutPutItem>();
			 List<EAImvertorException> exceptions = new List<EAImvertorException>(this.warnings);
			 exceptions.AddRange(this.errors);
			foreach (var exception in exceptions) 
			{
				var outputItem = ((UTF_EA.Package)this._sourcePackage).model.getItemFromGUID(exception.guid);
				string outputItemName = "-";
				if (outputItem != null) outputItemName = outputItem.name;
				outputItems.Add( new UML.Extended.UMLModelOutPutItem(outputItem, 
				                                                     new List<string>(new []{outputItemName,exception.exceptionType,exception.message,exception.construct})));
			}
			//create the search output
			var searchOutPut = new EASearchOutput("Imvertor Messages"
			                                      ,new List<string>(new string[] {"Name","Message Type","Message","Path"})
			                                      ,outputItems
			                                      ,((UTF_EA.Package)this._sourcePackage).model);
			//show the output
			searchOutPut.show();
			
		}
		private void getJobReport()
		{
			var xmlReport = getReport(this.reportUrl);
			if (xmlReport != null)
			{
				var statusNode = xmlReport.SelectSingleNode("//status");
				if (statusNode != null)
				{
					string jobStatus = statusNode.InnerText;
					//set the status
					this.setStatus(jobStatus);
					if (this.status == "Queued" || this.status == "In Progress" )//if status queued or in progress then try again
					{
						if (this.status == "Queued" 
						    && ! string.IsNullOrEmpty(this._jobID)
						    && ! this._startRequested)
						{
							requestStart();
						}
						if((DateTime.Now - this._startDateTime).Seconds < _settings.timeOutInSeconds ) //if not timed out yet)
						{
							//wait the interval
							Thread.Sleep(new TimeSpan(0,0,_settings.retryInterval));
							//then try again
							this.tries++;
							getJobReport();
						}
						else
						{
							this._timedOut = true;
							this.setStatus(this.status);
						}
					}
					
					else if (this.status == "Compiled")
					{
						//get the zip url
						var zipNode = xmlReport.SelectSingleNode("//zip");
						if (zipNode != null)
						{
							this._zipUrl = this.settings.imvertorURL + zipNode.InnerText;
							this.setStatus("Getting Results");
							this.downloadZip();
						}
						//get the report doc url
						var docNode = xmlReport.SelectSingleNode("//doc");
						if (docNode != null)
						{
							this._docUrl = this.settings.imvertorURL + docNode.InnerText;
						}
						this.setStatus("Finished");
					}
					//get the message, the warnings and errors
					if (this.status == "Finished" || this.status == "Error")
					{
						//message
						var messageNode = xmlReport.SelectSingleNode("//message");
						if (messageNode != null) this._message = messageNode.InnerText;
						//warnings
						foreach (XmlNode warningNode in xmlReport.SelectNodes("//warning")) 
						{	
							this._warnings.Add(createImvertorException(warningNode));						
						}
						//warnings
						foreach (XmlNode errorNode in xmlReport.SelectNodes("//error")) 
						{	
							this._errors.Add(createImvertorException(errorNode));						
						}
						
					}
				}
			}
			else
			{
				this.setStatus("Error");
				Logger.logError(string.Format("Cannot get report from {0}",this.reportUrl));
			}
		}

		void requestStart()
		{
			var client = settings.getHttpClient();
			client.GetAsync(this.startUrl);
			this._startRequested = true;
		}
		void downloadZip()
		{
			    
			HttpClient httpclient = settings.getHttpClient();
			
			byte[] bytArray = httpclient.GetByteArrayAsync(_zipUrl).Result;
			//get the file name from the zipurl
			string filename = _zipUrl.Split('/').Last();
			string downloadFile = this.settings.resultsPath +@"\"+ filename;
			//creat the directory if needed
			Directory.CreateDirectory(this.settings.resultsPath);
			//save the file
			File.WriteAllBytes(downloadFile,bytArray);
			this._downloadPath = downloadFile;
			}
		

		private EAImvertorException createImvertorException(XmlNode exceptionNode)
		{
			//get guid
			string guid = string.Empty;
			var idAttribue = exceptionNode.Attributes.GetNamedItem("id") as XmlAttribute;
			if (idAttribue != null) guid = idAttribue.Value;
			
			//get step
			string step = string.Empty;
			var stepNode = exceptionNode.SelectSingleNode("./step");
			if (stepNode != null) step = stepNode.InnerText;
			
			//get construct
			string construct = string.Empty;
			var constructNode = exceptionNode.SelectSingleNode("./construct");
			if (constructNode != null) construct = constructNode.InnerText;
			
			//get text
			string text = string.Empty;
			var textNode = exceptionNode.SelectSingleNode("./text");
			if (textNode != null) text = textNode.InnerText;
			return new EAImvertorException(((UTF_EA.Package)this._sourcePackage).model,exceptionNode.Name,guid,step,construct,text);
		}
		
		private XmlDocument getReport(string reportURL)
		{
			using (var client = settings.getHttpClient())
			{
				var response = client.GetAsync(reportURL).Result;
				if (!response.IsSuccessStatusCode)
		        {
		            return null;
		        }
		        StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
				string responseText = reader.ReadToEnd();
				XmlDocument xmlResponse = new XmlDocument();
				xmlResponse.LoadXml(responseText);
				return xmlResponse;
			}
		}
		
		private string Upload(string actionUrl,string pincode, string processName, string imvertorProperties
		                                , string modelFilePath, string historyFilePath, string propertiesFilePath )
		{
		    HttpContent processNameContent = new StringContent(processName);
		    HttpContent propertiesContent = new StringContent(imvertorProperties);
		    HttpContent pincodeContent = new StringContent(pincode);
		    HttpContent modelFileContent = null;
			if (File.Exists(modelFilePath)) modelFileContent = new StreamContent(File.OpenRead(modelFilePath));
		    HttpContent historyFileContent = null;
		    if (File.Exists(historyFilePath)) historyFileContent = new StreamContent(File.OpenRead(historyFilePath));
		    HttpContent propertiesFileContent = null;
		    if (File.Exists(propertiesFilePath)) propertiesFileContent = new StreamContent(File.OpenRead(propertiesFilePath));
		    using (var client = settings.getHttpClient())
		    {
		    	//this might solve an issue with the proxy then uploading content
		    	client.DefaultRequestHeaders.ExpectContinue = false;
			    using (var formData = new MultipartFormDataContent())
			    {
			        formData.Add(processNameContent, "procname");
			        formData.Add(propertiesContent, "properties");
			        if (modelFileContent != null) formData.Add(modelFileContent, "umlfile", "umlfile.zip");
			        if (historyFileContent != null) formData.Add(historyFileContent, "hisfile", "hisfile");
			        if (propertiesFileContent != null) formData.Add(propertiesFileContent, "propfile", "propfile.properties");
			        formData.Add(pincodeContent, "pin");
			        var response = client.PostAsync(actionUrl, formData).Result;
			        if (!response.IsSuccessStatusCode)
			        {
			            return string.Empty;
			        }
			        StreamReader reader = new StreamReader(response.Content.ReadAsStreamAsync().Result);
					string responseText = reader.ReadToEnd();
					XmlDocument xmlResponse = new XmlDocument();
					xmlResponse.LoadXml(responseText);
					var jobIDNode = xmlResponse.SelectSingleNode("//jobid");
					if (jobIDNode != null)
					{
						return jobIDNode.InnerText;
					}
					else return string.Empty;
			    }
		    }
		}
	}
}
