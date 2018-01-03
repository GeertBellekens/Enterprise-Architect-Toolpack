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
        public Domain(UML.Classes.Kernel.Package wrappedPackage)
        {
            this.wrappedPackage = wrappedPackage;
        }
        public Domain(UML.Classes.Kernel.Package wrappedPackage, Domain parentDomain):this(wrappedPackage)
        {
            this._parentDomain = parentDomain;
        }
        public string name { get{ return this.wrappedPackage.name; } }
        private List<Domain> _subDomains;
        public List<Domain> subDomains
        {
            get
            {
                if (_subDomains == null)
                {
                    _subDomains = new List<Domain>();
                    foreach(var subPackage in wrappedPackage.ownedElements.OfType<UML.Classes.Kernel.Package>())
                    {
                        _subDomains.Add(new Domain(subPackage));
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
                    if (this.wrappedPackage.owningPackage != null)
                    {
                        this._parentDomain = new Domain(this.wrappedPackage.owningPackage);
                    }
                }
                return _parentDomain;
            }
        }
    }
}
