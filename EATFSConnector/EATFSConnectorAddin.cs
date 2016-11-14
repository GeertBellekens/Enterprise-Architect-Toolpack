
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UML=TSF.UmlToolingFramework.UML;
using TSF_EA=TSF.UmlToolingFramework.Wrappers.EA;
using EAAddinFramework;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;


namespace EATFSConnector
{
	/// <summary>
	/// Description of MyClass.
	/// </summary>
	public class EATFSConnectorAddin:EAAddinBase
	{
		// define menu constants
        const string menuName = "-&TFS Connector";
        const string menuSynchTFStoEA = "&Sync TFS to EA";
        const string menuSynchEAtoTFS = "&Sync EA to TFS";
        const string menuSettings = "&Settings";
        const string menuAbout = "&About";
        
        //private attributes
        private EATFSConnectorSettings settings = new EATFSConnectorSettings();
        private UML.Extended.UMLModel model;
		private TSF_EA.Model EAModel {get{return this.model as TSF_EA.Model;}}
		private bool fullyLoaded = false;
        /// <summary>
        /// constructor, set menu names
        /// </summary>
        public EATFSConnectorAddin():base()
        {
        	this.menuHeader = menuName;
			this.menuOptions = new string[]{menuSynchTFStoEA,menuSynchEAtoTFS, menuSettings, menuAbout};
        }
       	public override void EA_FileOpen(EA.Repository Repository)
		{
			// initialize the model
	        this.model = new TSF_EA.Model(Repository);
			// indicate that we are now fully loaded
	        this.fullyLoaded = true;
		}
       	/// <summary>
        /// Called when user makes a selection in the menu.
        /// This is your main exit point to the rest of your Add-in
        /// </summary>
        /// <param name="Repository">the repository</param>
        /// <param name="Location">the location of the menu</param>
        /// <param name="MenuName">the name of the menu</param>
        /// <param name="ItemName">the name of the selected menu item</param>
        public override void EA_MenuClick(EA.Repository Repository, string Location, string MenuName, string ItemName)
        {
            switch (ItemName)
            {
            	case menuSynchTFStoEA:
            		synchTFSToEA();
            		break;
                case menuSynchEAtoTFS:
					//TODO
                    break;
		        case menuAbout :
		            new AboutWindow().ShowDialog();
		            break;
	            case menuSettings:
		            new TFSConnectorSettingsForm(this.settings).ShowDialog();
	                break;
            }
        }
        private void synchTFSToEA()
        {
        	string TFSUrl;
        	//get the TFS location
        	if (this.settings.projectConnections.TryGetValue(this.EAModel.projectGUID,out TFSUrl))
        	{
        		//ok we have an URL for the TFS
        		//get a list of all workitems of a certain type
        		GetWorkItemsByWiql(TFSUrl);
        	}
        }
        public string GetWorkItemsByWiql(string TFSUrl)
        {
            // create wiql object
            var wiql = new
            {
                query = "Select [State], [Title] " +
                        "From WorkItems " +
                        "Where [Work Item Type] = 'Feature' " +
                        "Order By [State] Asc, [Changed Date] Desc"
            };

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(TFSUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // serialize the wiql object into a json string   
                var postValue = new StringContent(JsonConvert.SerializeObject(wiql), Encoding.UTF8, "application/json"); // mediaType needs to be application/json for a post call

                // set the httpmethod to PPOST
                var method = new HttpMethod("POST");

                // send the request               
                var httpRequestMessage = new HttpRequestMessage(method, TFSUrl + "_apis/wit/wiql?api-version=2.2") { Content = postValue };
                var httpResponseMessage = client.SendAsync(httpRequestMessage).Result;

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    WorkItemQueryResult workItemQueryResult = httpResponseMessage.Content.ReadAsAsync<WorkItemQueryResult>().Result;
                                     
                    // now that we have a bunch of work items, build a list of id's so we can get details
                    var builder = new System.Text.StringBuilder();
                    foreach (var item in workItemQueryResult.workItems)
                    {
                        builder.Append(item.id.ToString()).Append(",");
                    }

                    // clean up string of id's
                    string ids = builder.ToString().TrimEnd(new char[] { ',' });

                    HttpResponseMessage getWorkItemsHttpResponse = client.GetAsync("_apis/wit/workitems?ids=" + ids + "&fields=System.Id,System.Title,System.State&asOf=" + workItemQueryResult.asOf + "&api-version=2.2").Result;

                    if (getWorkItemsHttpResponse.IsSuccessStatusCode)
                    {
                        var result = getWorkItemsHttpResponse.Content.ReadAsStringAsync().Result;
                        return "success";
                    }

                    return "failed";               
                }

                return "failed";                               
            }
        }
	}
	public class QueryResult
    {
        public string id { get; set; }
        public string name { get; set; }
        public string path { get; set; }           
        public string url { get; set; }
    }

    public class WorkItemQueryResult 
    {
        public string queryType { get; set; }
        public string queryResultType { get; set; }
        public DateTime asOf { get; set; }
        public Column[] columns { get; set; }
        public Workitem[] workItems { get; set; }
    }   

    public class Workitem
    {
        public int id { get; set; }
        public string url { get; set; }
    }

    public class Column
    {
        public string referenceName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class AttachmentReference
    {
        public string id { get; set; }
        public string url { get; set; }
    }

    public class WorkItemFields 
    {
        public int count { get; set; }
        public WorkItemField[] value { get; set; }
    }

    public class WorkItemField
    {
        public string name { get; set; }
        public string referenceName { get; set; }
        public string type { get; set; }
        public bool readOnly { get; set; }        
        public string url { get; set; }
    }
}