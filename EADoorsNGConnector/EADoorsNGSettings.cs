using System;
using System.Collections.Generic;
using System.Linq;
using EAAddinFramework.Utilities;
using EAAddinFramework.Requirements.DoorsNG;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADoorsNGConnector
{
    internal class EADoorsNGSettings : AddinSettings, DoorsNGSettings
    {
        protected override string configSubPath => @"\Bellekens\EADoorsNGConnector\";
        protected override string defaultConfigFilePath => System.Reflection.Assembly.GetExecutingAssembly().Location;

        public Dictionary<string, string> projectConnections
        {
            get
            {
                return getDictionaryValue("projectConnections");
            }
            set
            {
                this.setDictionaryValue("projectConnections", value);
            }
        }

        public Dictionary<string, string> requirementMappings
        {
            get
            {
                return getDictionaryValue("requirementMappings");
            }
            set
            {
                this.setDictionaryValue("requirementMappings", value);
            }
        }
        public string defaultProject
        {
            get
            {
                return this.getValue("defaultProject");
            }
            set
            {
                this.setValue("defaultProject", value);
            }
        }
        public string defaultUserName
        {
            get
            {
                return this.getValue("defaultUserName");
            }
            set
            {
                this.setValue("defaultUserName", value);
            }
        }
        public string defaultPassword { get; set; } //do not persist default password
        public string defaultStatus
        {
            get
            {
                return this.getValue("defaultStatus");
            }
            set
            {
                this.setValue("defaultStatus", value);
            }
        }

        public List<string> mappedElementTypes
        {
            get
            {
                return this.requirementMappings.Keys.Where(x => !x.Contains("::")).ToList();
            }
        }
        public List<string> mappedStereotypes
        {
            get
            {
                var stereotypes = new List<string>();
                foreach (var fullStereotype in this.requirementMappings.Keys.Where(x => x.Contains("::")))
                {
                    string simpleStereotype = fullStereotype.Split(new string[] { "::" }, StringSplitOptions.None)[1];
                    if (simpleStereotype.Length > 0) stereotypes.Add(simpleStereotype);
                }
                return stereotypes;
            }
        }
        public string getURL(TSF_EA.Model model)
        {
            string url;
            //get the DoorsNG Url
            projectConnections.TryGetValue(model.projectGUID, out url);
            return url;
        }

    }
}