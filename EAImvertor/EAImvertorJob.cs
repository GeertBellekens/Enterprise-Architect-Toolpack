
using System;
using System.Threading;
using System.Xml;
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
		private UML.Classes.Kernel.Package sourcePackage;
		private string jobID;
		public EAImvertorJob(UML.Classes.Kernel.Package package)
		{
			this.sourcePackage = package;
		}
		public void startJob(string imvertorURL, string pincode,string processName ,string imvertorProperties,string imvertorPropertiesFilePath, string imvertorHistoryFilePath)
			//		
		{
			this.jobID = this.Upload(imvertorURL +"/imvertor-executor/upload",pincode,processName,imvertorProperties
			                           ,@"C:\temp\SampleApplication.1.xmi",imvertorHistoryFilePath,imvertorPropertiesFilePath);

			Logger.log(imvertorURL + "imvertor-executor/report?pin=" + pincode + "&job=" + jobID);
			getReportJob(imvertorURL, pincode,0);
		}
		private void getReportJob(string imvertorURL, string pincode, int tries)
		{
			if (tries < 10) //try ten times
			{
				var xmlReport = getReport(imvertorURL + "imvertor-executor/report?pin=" + pincode + "&job=" + jobID);
				if (xmlReport != null)
				{
					Logger.log ("report at try "  + tries.ToString() + " " + xmlReport.InnerXml);
					var statusNode = xmlReport.SelectSingleNode("//status");
					if (statusNode != null)
					{
						string jobStatus = statusNode.InnerText;
						if (jobStatus == "1" || jobStatus == "2") //if status queued or in progress then try again
						{
							//wait ten seconds
							Thread.Sleep(new TimeSpan(0,0,10));
							//then try again
							tries++;
							getReportJob(imvertorURL, pincode, tries);
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
