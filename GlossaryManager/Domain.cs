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
        internal UML.Classes.Kernel.Package wrappedPackage { get; private set; }
        private UML.Classes.Kernel.Package topLevelPackage;
        private static Dictionary<string, Domain> alldomains = new Dictionary<string, Domain>();
        public Domain(UML.Classes.Kernel.Package wrappedPackage, UML.Classes.Kernel.Package topLevelPackage)
        {
            this.wrappedPackage = wrappedPackage;
            this.topLevelPackage = topLevelPackage;
        }
        public Domain(UML.Classes.Kernel.Package wrappedPackage, Domain parentDomain, UML.Classes.Kernel.Package topLevelPackage) :this(wrappedPackage,topLevelPackage)
        {
            this._parentDomain = parentDomain;
        }
        public static List<Domain> getAllDomains(UML.Classes.Kernel.Package topLevelPackage)
        {
            if (topLevelPackage != null)
            {
                //clear domains
                alldomains.Clear();
                //recreate them
                foreach (var domainPackage in topLevelPackage.nestedPackages)
                {
                    var currentDomain = new Domain(domainPackage, topLevelPackage);
                    alldomains[currentDomain.uniqueID] = currentDomain;
                    currentDomain.getAllSubdomains().ForEach(x => alldomains[x.uniqueID] = x);
                }
            }
            return alldomains.Values.ToList() ;
        }
        public static Domain getDomain(UML.Classes.Kernel.Package package)
        {
            return alldomains.ContainsKey(package.uniqueID) ? alldomains[package.uniqueID] : null;
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
            get { return this.wrappedPackage.uniqueID; }
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

        public string name { get{ return this.wrappedPackage.name; } }
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
                    foreach(var subPackage in wrappedPackage.nestedPackages)
                    {
                        _subDomains.Add(new Domain(subPackage, this.topLevelPackage));
                    }
                }
                return _subDomains;
            }
        }
        private Domain _parentDomain;
        public Domain parentDomain
        {
            get
            {
                if (_parentDomain == null)
                {
                    if (this.wrappedPackage.owningPackage != null
                        && ! this.wrappedPackage.owningPackage.Equals(this.topLevelPackage))
                    {
                        this._parentDomain = new Domain(this.wrappedPackage.owningPackage, this.topLevelPackage);
                    }
                }
                return _parentDomain;
            }
        }
    }
}
