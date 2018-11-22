using System;
using System.Collections.Generic;
using System.Linq;



using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{




    public abstract class GlossaryItem: IEDDItem
    {

        //TODO: figure out a way to get the actual values from the model
        public static List<string> statusValues { get { return new List<string> { "proposed", "approved", "rejected" }; } }

        public string GUID
        {
            get { return this.origin.uniqueID; }
        }

        public bool toBeDeleted;

        public string Name
        {
            get { return this.origin.name; }
            set { this.origin.name = value; }
        }

        public string Author
        {
            get { return this.origin.author; }
            set { this.origin.author = value; }
        }

        public string Version
        {
            get { return this.origin.version; }
            set { this.origin.version = value; }
        }

        public string Status
        {
            get { return this.origin.status; }
            set { this.origin.status = value; }
        }

        public List<string> Keywords
        {
            get { return this.origin.keywords; }
            set { this.origin.keywords = value; }
        }

        public DateTime CreateDate
        {
            get { return this.origin.created; }
        }

        public DateTime UpdateDate
        {
            get { return this.origin.modified; }
        }

        public string UpdatedBy
        {
            get { return this.origin.getTaggedValue("modifier")?.eaStringValue; }
            set { this.origin.addTaggedValue("modifier", value); }
        }

        public TSF_EA.ElementWrapper origin { get; set; }

        internal GlossaryManagerSettings settings { get; set; }
        public bool isDirty { get { return origin != null ? origin.isDirty : true; } }
        


        public static List<T> Load<T>(string file)
        where T : GlossaryItem
        {
            throw new NotImplementedException();
        }

        public static void Save<T>(string file, List<T> items)
          where T : GlossaryItem
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return string.Join(", ", new List<string> {
        this.GUID,
        this.Name,
        this.Author,
        this.Version,
        this.Status.ToString(),
        "[" + string.Join(",", this.Keywords) + "]",
        this.CreateDate.ToString(),
        this.UpdateDate.ToString(),
        this.UpdatedBy
      });
        }

        // EA support

        public abstract string Stereotype { get; }
        private Domain _domain = null;
        public Domain domain
        {
            get
            {
                if (_domain == null
                    && this.origin != null)
                {
                    _domain = Domain.getDomain(this.origin.owningPackage);
                }
                return _domain;
            }
            set
            {
                _domain = value;
                if (_domain != null)
                    setOwningPackage();
            }
        }
        public string domainPath
        {
            get
            {
                return this.domain?.domainPath;
            }
        }
        protected abstract void setOwningPackage();

        public UML.Classes.Kernel.Class AsClassIn(TSF_EA.Package package)
        {
            var clazz = package.EAModel.factory.createNewElement<UML.Classes.Kernel.Class>(
              package, this.Name
            );

            var stereotypes = new HashSet<UML.Profiles.Stereotype>();
            stereotypes.Add(new TSF_EA.Stereotype(
              package.EAModel, clazz as TSF_EA.Element, this.Stereotype
            ));
            clazz.stereotypes = stereotypes;

            this.origin = clazz as TSF_EA.ElementWrapper;
            this.update();

            return clazz;
        }
        public void reload()
        {
            this.origin.reload();
            this.reloadData();
        }
        protected abstract void reloadData();
        public void save()
        {
            if (this.origin == null)
            {
                // TODO assertion ?
                //      Can't Save without an Origin and should not be possible?!
                return;
            }
            this.update();
            //set updateDate and update user if dirty
            if (this.isDirty)
            {
                this.UpdatedBy = this.origin.EAModel.currentUser.login;
            }
            //save item
            this.origin.modified = this.UpdateDate;
            this.origin.addTaggedValue("modifier", this.UpdatedBy);
            this.origin.save();
        }
        public void delete()
        {
            this.origin?.delete();
        }

        protected abstract void update();
        

        public void selectInProjectBrowser()
        {
            this.origin.open();
        }
        public void openProperties()
        {
            this.origin.openProperties();
        }

    }
}


