
using System;
using UML=TSF.UmlToolingFramework.UML;
using UTF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using System.Linq;
using System.Net.Http;
using System.IO;

namespace EAImvertor
{
	/// <summary>
	/// Description of EAImvertorJob.
	/// </summary>
	public class EAImvertorJob
	{
		private UML.Classes.Kernel.Package sourcePackage;
		public EAImvertorJob(UML.Classes.Kernel.Package package)
		{
			this.sourcePackage = package;
		}
		public void startJob(string imvertorURL, string pincode,string processName ,string imvertorProperties,string imvertorPropertiesFilePath, string imvertorHistoryFilePath)
			//		
		{
			var response = this.Upload(imvertorURL +"/imvertor-executor/upload",pincode,processName,imvertorProperties
			                           ,@"C:\temp\SampleApplication.1.xmi",imvertorHistoryFilePath,imvertorPropertiesFilePath);
			// convert stream to string
			StreamReader reader = new StreamReader(response);
			string text = reader.ReadToEnd();
		}
		private Stream Upload(string actionUrl,string pincode, string processName, string imvertorProperties
		                                , string modelFilePath, string historyFilePath, string propertiesFilePath )
		{
		    HttpContent processNameContent = new StringContent(processName);
		    HttpContent propertiesContent = new StringContent(imvertorProperties);
		    HttpContent pincodeContent = new StringContent(pincode);
		    HttpContent ownerContent = new StringContent("Kadaster"); //TODO: remove once Arjan changed the code on the server
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
		        formData.Add(ownerContent, "owner");
		        var response = client.PostAsync(actionUrl, formData).Result;
		        if (!response.IsSuccessStatusCode)
		        {
		            return null;
		        }
		        return response.Content.ReadAsStreamAsync().Result;
		    }
		}
	}
}
