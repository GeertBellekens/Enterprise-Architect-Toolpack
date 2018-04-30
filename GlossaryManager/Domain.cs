using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{
    public class Domain
    {
        internal UML.Classes.Kernel.Package businessItemsPackage { get; private set; }
        private UML.Classes.Kernel.Package businessItemsTopLevelPackage;
        internal UML.Classes.Kernel.Package dataItemsPackage { get; private set; }
        private UML.Classes.Kernel.Package dataItemsTopLevelPackage;
        private static Dictionary<string, Domain> alldomains;
        public Domain(UML.Classes.Kernel.Package businessItemsPackage, UML.Classes.Kernel.Package businessItemsTopLevelPackage
                      , UML.Classes.Kernel.Package dataItemsPackage, UML.Classes.Kernel.Package dataItemsTopLevelPackage)
        {
            this.businessItemsPackage = businessItemsPackage;
            this.businessItemsTopLevelPackage = businessItemsTopLevelPackage;
            this.dataItemsPackage = dataItemsPackage;
            this.dataItemsTopLevelPackage = dataItemsTopLevelPackage;
        }
        public Domain(UML.Classes.Kernel.Package businessItemsPackage, UML.Classes.Kernel.Package businessItemsTopLevelPackage
            , UML.Classes.Kernel.Package dataItemsPackage, UML.Classes.Kernel.Package dataItemsTopLevelPackage
            , Domain parentDomain) : this(businessItemsPackage, businessItemsTopLevelPackage, dataItemsPackage, dataItemsTopLevelPackage)
        {
            this.parentDomain = parentDomain;
        }
        public static List<Domain> getAllDomains(UML.Classes.Kernel.Package businessItemsTopLevelPackage, UML.Classes.Kernel.Package dataItemsTopLevelPackage)
        {
            if (alldomains == null) alldomains = new Dictionary<string, Domain>();
            //clear domains
            alldomains.Clear();
            //recreate them
            if (businessItemsTopLevelPackage != null)
            {
                foreach (var domainPackage in businessItemsTopLevelPackage.nestedPackages)
                {
                    var currentDomain = new Domain(domainPackage, businessItemsTopLevelPackage, null, dataItemsTopLevelPackage);
                    alldomains[currentDomain.uniqueID] = currentDomain;
                    currentDomain.getAllSubdomains().ForEach(x => alldomains[x.uniqueID] = x);
                }
            }
            if (dataItemsTopLevelPackage != null)
            {
                foreach (var domainPackage in dataItemsTopLevelPackage.nestedPackages)
                {
                    //check if domain like this exists
                    var currentDomain = new Domain(null, businessItemsTopLevelPackage,domainPackage, dataItemsTopLevelPackage);
                    var correspondingDomain = alldomains.Values.FirstOrDefault(x => x.domainPath == currentDomain.domainPath);
                    if (correspondingDomain != null )
                    {
                        correspondingDomain.dataItemsPackage = domainPackage;
                    }
                    else
                    {
                        alldomains[currentDomain.uniqueID] = currentDomain;
                    }
                    foreach(var subdomain in currentDomain.getAllSubdomains())
                    {
                        var correspondingSubDomain = alldomains.Values.FirstOrDefault(x => x.domainPath == subdomain.domainPath);
                        if (correspondingSubDomain != null)
                        {
                            correspondingSubDomain.dataItemsPackage = subdomain.dataItemsPackage;
                        }
                        else
                        {
                            alldomains[subdomain.uniqueID] = subdomain;
                        }
                    }
                }
            }
            return alldomains.Values.ToList() ;
        }
        public static Domain getDomain(UML.Classes.Kernel.Package package)
        {
            Domain foundDomain = null;
            if (package != null)
            {
                //first look on unique id
                foundDomain = alldomains.ContainsKey(package.uniqueID) ? alldomains[package.uniqueID] : null;
                // then check the dataItemsPackage
                if (foundDomain == null)
                {
                    foundDomain = alldomains.Values.FirstOrDefault(x => x.dataItemsPackage?.uniqueID == package?.uniqueID);
                }
            }
            return foundDomain;
        }
        public string domainPath
        {
            get
            {
                return string.Join(".", this.parentDomains.Select(x => x.name));
            }
        }
        public List<Domain> parentDomains
        {
            get
            {
                List<Domain> domains;
                if (this.parentDomain == null)
                {
                    domains = new List<Domain>();
                }
                else
                {
                    domains = this.parentDomain.parentDomains;
                }
                domains.Add(this);
                return domains;
            }
        }
        public string uniqueID
        {
            get { return this.businessItemsPackage != null ? this.businessItemsPackage.uniqueID : this.dataItemsPackage?.uniqueID ; }
        }
        public List<Domain> getAllSubdomains()
        {
            var allSubDomains = new List<Domain>();
            foreach (var subDomain in this.subDomains)
            {
                allSubDomains.Add(subDomain);
                allSubDomains.AddRange(subDomain.getAllSubdomains());
            }
            return allSubDomains;
        }

        public void createMissingPackage()
        {
            if (this.dataItemsPackage == null)
            {
                TSF_EA.Package parentPackage = null;
                if (this.parentDomain != null)
                {
                    if (this.parentDomain.dataItemsPackage == null) this.parentDomain.createMissingPackage();
                    parentPackage = (TSF_EA.Package)this.parentDomain.dataItemsPackage;
                }
                else
                {
                    parentPackage = (TSF_EA.Package)this.dataItemsTopLevelPackage;
                }
                this.dataItemsPackage = parentPackage.addOwnedElement<TSF_EA.Package>(this.name);
                this.dataItemsPackage.save();
            }
            else if (this.businessItemsPackage == null)
            {
                TSF_EA.Package parentPackage = null;
                if (this.parentDomain != null)
                {
                    if (this.parentDomain.businessItemsPackage == null) this.parentDomain.createMissingPackage();
                    parentPackage = (TSF_EA.Package)this.parentDomain.businessItemsPackage;
                }
                else
                {
                    parentPackage = (TSF_EA.Package)this.businessItemsTopLevelPackage;
                }
                this.businessItemsPackage = parentPackage.addOwnedElement<TSF_EA.Package>(this.name);
                this.businessItemsPackage.save();
            }
        }

        public string name { get{ return this.businessItemsPackage != null ? this.businessItemsPackage.name : this.dataItemsPackage.name; } }
        private int level
        {
            get
            {
                return this.parentDomain != null ? this.parentDomain.level + 1 : 0;
            }
        }
        /// <summary>
        /// returns an indented name adding 4 spaces for each level
        /// </summary>
        public string displayName
        {
            get { return name.PadLeft(this.name.Length + this.level * 4); }
        }
        
        private List<Domain> _subDomains;
        public List<Domain> subDomains
        {
            get
            {
                if (_subDomains == null)
                {
                    _subDomains = new List<Domain>();
                    if (businessItemsPackage != null)
                    {
                        foreach (var businessSubPackage in businessItemsPackage.nestedPackages)
                        {
                            _subDomains.Add(new Domain(businessSubPackage, this.businessItemsTopLevelPackage, null, this.dataItemsTopLevelPackage, this));
                        }
                    }
                    if (dataItemsPackage != null)
                    {
                        foreach (var dataSubPackage in dataItemsPackage.nestedPackages)
                        {
                            var dataDomain = new Domain(null, this.businessItemsTopLevelPackage, dataSubPackage,this.dataItemsTopLevelPackage, this);
                            var correspondingDomain = _subDomains.FirstOrDefault(x => x.domainPath == dataDomain.domainPath);
                            if (correspondingDomain != null)
                            {
                                correspondingDomain.dataItemsPackage = dataSubPackage;
                            }
                            else
                            {
                                _subDomains.Add(dataDomain);
                            }
                        }
                    }
                }
                return _subDomains;
            }
        }

        public Domain parentDomain { get; private set; }
    }
}
