
using System;
using System.ComponentModel;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;
using System.Net.Http;
using System.IO;
using EAAddinFramework.Utilities;

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
		private EAImvertorSettings _settings;
		private BackgroundWorker _backgroundWorker;
		private DateTime _startDateTime;
		public EAImvertorSettings settings
		{
			get {return this._settings;}
		}
		private string reportUrl
		{
			get{return _settings.imvertorURL+ "imvertor-executor/report?pin=" + settings.defaultPIN + "&job=" + _jobID;}
		}
		public EAImvertorJob(UML.Classes.Kernel.Package package, EAImvertorSettings settings)
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
						this._status = "Queued";
						break;
					case 2:
						this._status = "In Progress";
						break;
					case 3:
						this._status = "Finished";
						break;
					default:
						this._status = "Error";
						break;
				}
			}
			else
			{
				this._status = jobStatus;
			}
			if (this._backgroundWorker != null && this._backgroundWorker.IsBusy)
			{
				this._backgroundWorker.ReportProgress(0,this);
			}
		}
		//public void startJob(string imvertorURL, string pincode,string processName ,string imvertorProperties,string imvertorPropertiesFilePath, string imvertorHistoryFilePath)
		public void startJob(EAImvertorSettings settings, BackgroundWorker backgroundWorker)
		{
			this._settings = settings;
			this._startDateTime = DateTime.Now;
			this._backgroundWorker = backgroundWorker;
			//create the specific properties for this job
			this.settings.defaultPropertiesFilePath = createSpecificPropertiesFile();
			string xmiFileName = Path.GetTempFileName();
			this.setStatus("Exporting Model");
			this.sourcePackage.getRootPackage().exportToXMI(xmiFileName);
			this.setStatus("Uploading Model");
			this._jobID = this.Upload(settings.imvertorURL+"/imvertor-executor/upload",settings.defaultPIN,settings.defaultProcessName,settings.defaultProperties
			                           ,xmiFileName,settings.defaultHistoryFilePath,settings.defaultPropertiesFilePath);

			Logger.log(this.reportUrl);
			this.setStatus("Upload Finished");
			getJobReport();
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
			if (File.Exists(this.settings.defaultPropertiesFilePath))
			{
				return File.ReadAllText(this.settings.defaultPropertiesFilePath);
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
		public void downloadResults()
		{
			if (! string.IsNullOrEmpty(this._zipUrl))
			{
				System.Diagnostics.Process.Start(this._zipUrl);
			}
		}
		public void viewReport()
		{
			System.Diagnostics.Process.Start(this.reportUrl);
		}
		private void getJobReport()
		{
			var xmlReport = getReport(this.reportUrl);
			if (xmlReport != null)
			{
				Logger.log ("report at try "  + tries.ToString() + " " + xmlReport.InnerXml);
				var statusNode = xmlReport.SelectSingleNode("//status");
				if (statusNode != null)
				{
					string jobStatus = statusNode.InnerText;
					//set the status
					this.setStatus(jobStatus);
					if (this.status == "Queued" || this.status == "In Progress" )//if status queued or in progress then try again
					{
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
							this.setStatus(this.status + " (Timed out)");
						}
					}
					//get the zip url
					else if (this.status == "Finished")
					{
						var zipNode = xmlReport.SelectSingleNode("//zip");
						if (zipNode != null)
						{
							this._zipUrl = this.settings.imvertorURL + zipNode.InnerText;
						}
					}
				}
			}
			else
			{
				Logger.log("xmlReport is null");
			}
		}
		private XmlDocument getReport(string reportURL)
		{
			using (var client = new HttpClient())
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
		    using (var client = new HttpClient())
		    using (var formData = new MultipartFormDataContent())
		    {
		        formData.Add(processNameContent, "procname");
		        formData.Add(propertiesContent, "properties");
		        if (modelFileContent != null) formData.Add(modelFileContent, "umlfile", "umlfile.xmi");
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
