
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
			this._backgroundWorker.ReportProgress(0,this);
		}
		//public void startJob(string imvertorURL, string pincode,string processName ,string imvertorProperties,string imvertorPropertiesFilePath, string imvertorHistoryFilePath)
		public void startJob(EAImvertorSettings settings, BackgroundWorker backgroundWorker)
		{
			this._settings = settings;
			this._backgroundWorker = backgroundWorker;
			string xmiFileName = Path.GetTempFileName();
			this.setStatus("Exporting Model");
			this.sourcePackage.getRootPackage().exportToXMI(xmiFileName);
			this.setStatus("Uploading Model");
			this._jobID = this.Upload(settings.imvertorURL+"/imvertor-executor/upload",settings.defaultPIN,settings.defaultProcessName,settings.defaultProperties
			                           ,xmiFileName,settings.defaultHistoryFilePath,settings.defaultPropertiesFilePath);

			Logger.log(this.reportUrl);
			this.setStatus("Upload Finished");
			getJobReport(_settings.imvertorURL, this.settings.defaultPIN,0);
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
		private void getJobReport(string imvertorURL, string pincode, int tries)
		{
			if (tries < 10) //try ten times
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
						if (this.status == "Queued" || this.status == "In Progress" ) //if status queued or in progress then try again
						{
							//wait ten seconds
							Thread.Sleep(new TimeSpan(0,0,10));
							//then try again
							tries++;
							getJobReport(imvertorURL, pincode, tries);
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
			else
			{
				Logger.log("tried to get report 10 times");
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
