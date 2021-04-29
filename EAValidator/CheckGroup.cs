using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    public class CheckGroup : CheckItem
    {
        private DirectoryInfo directory { get; set; }
        private EAValidatorSettings settings { get; set; }
        private TSF_EA.Model model { get; set; }
        public override CheckStatus Status 
        { 
            get
            {
                var tempStatus = CheckStatus.NotValidated;
                foreach(var check in this.GetAllChecks())
                {
                    if (check.Status == CheckStatus.Failed)
                    {
                        return CheckStatus.Failed;
                    }
                    else if(check.Status == CheckStatus.Passed)
                    {
                        tempStatus = CheckStatus.Passed;
                    }
                }
                return tempStatus;
            }
        }
        public override int? NumberOfElementsFound
        {
            get
            {
                int? numberFound = null;
                foreach (var check in this.GetAllChecks())
                {
                    if (check.NumberOfElementsFound.HasValue)
                    { 
                        if (!numberFound.HasValue)
                        {
                            numberFound = 0;
                        }
                        numberFound += check.NumberOfElementsFound;
                    }
                }
                return numberFound;
            }
        }

        public override int? NumberOfValidationResults 
        {
            get
            {
                int? numberFound = null;
                foreach (var check in this.GetAllChecks())
                {
                    if (check.NumberOfValidationResults.HasValue)
                    {
                        if (!numberFound.HasValue)
                        {
                            numberFound = 0;
                        }
                        numberFound += check.NumberOfValidationResults;
                    }
                }
                return numberFound;
            }
        }


        public CheckGroup(DirectoryInfo directory, EAValidatorSettings settings, TSF_EA.Model model)
        {
            this.directory = directory;
            this.settings = settings;
            this.model = model;
        }
        public string name => this.directory.Name;
        public override bool? selected
        {
            get
            {
                if (this.subItems.Any())
                {
                    if (this.subItems.All(x => x.selected == true))
                    {
                        return true;
                    }
                    if (this.subItems.All(x => x.selected == false))
                    {
                        return false;
                    }
                }
                //not all selected and not all not selected
                return null;
                
            }
            set
            {
                
                if (value != null)
                {
                    this.subItems.ToList().ForEach(x => x.selected = value);
                }
                else
                {
                    var currentValue = this.selected;
                    this.subItems.ToList().ForEach(x => x.selected = ! currentValue);
                }
            }
        }

        public List<CheckGroup> _subGroups;
        public IEnumerable<CheckGroup> subGroups
        {
            get
            {
                if (_subGroups == null)
                {
                    _subGroups = new List<CheckGroup>();
                    foreach (var subdirectory in this.directory.GetDirectories())
                    {
                        var newSubGroup = new CheckGroup(subdirectory, this.settings, this.model);
                        if (newSubGroup.subItems.Any())
                        {
                            _subGroups.Add(newSubGroup);
                        }
                    }
                }
                return _subGroups;
            }
        }
        private List<CheckItem> _subItems;
        public IEnumerable<CheckItem> subItems
        {
            get
            {
                if (_subItems == null)
                {
                    _subItems = new List<CheckItem>();
                    _subItems.AddRange(this.checks);
                    _subItems.AddRange(this.subGroups);
                }
                return _subItems;
            }
        }
        private List<Check> _checks;
        public IEnumerable<Check> checks
        {
            get
            {
                if (this._checks == null)
                {
                    this._checks = new List<Check>();
                    // Get files from given directory                
                    foreach (var file in this.directory.GetFiles("*.xml"))
                    {
                        // add new check
                        try
                        {
                            var check = new Check(file.FullName, this, this.settings, this.model);
                            this._checks.Add(check);
                            //order by checkID
                            this._checks = this._checks.OrderBy(x => x.CheckId).ToList();
                        }
                        catch (XmlSchemaValidationException e)
                        {
                            EAAddinFramework.Utilities.EAOutputLogger.log(this.model,this.settings.outputName, e.Message, 0, EAAddinFramework.Utilities.LogTypeEnum.error);
                        }
                    }
                }
                return this._checks;
            }
        }

        

        public IEnumerable<Check> GetAllChecks()
        {
            var allChecks = new List<Check>();
            //add these checks
            allChecks.AddRange(this.checks);
            //add checks of subgroups
            foreach (var subGroup in subGroups)
            {
                allChecks.AddRange(subGroup.GetAllChecks());
            }
            return allChecks;
        }
    }
}
