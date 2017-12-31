using System;
using System.Collections.Generic;
using System.Linq;

using FileHelpers;

using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace GlossaryManager
{




    public abstract class GlossaryItem
    {

        //TODO: figure out a way to get the actual values from the model
        public static List<string> statusValues { get { return new List<string> { "proposed", "approved", "rejected" }; } }

        [FieldOrder(0)]
        [FieldNullValue(typeof(string), "")]
        public string GUID;

        [FieldOrder(1)]
        [FieldConverter(typeof(BoolConverter))]
        public bool Delete;

        [FieldOrder(2)]
        public string Name;

        [FieldOrder(3)]
        [FieldNullValue(typeof(string), "")]
        public string Author;

        [FieldOrder(4)]
        [FieldNullValue(typeof(string), "")]
        public string Version;

        [FieldOrder(5)]
        [FieldNullValue(typeof(string), "Approved")]
        public string Status;

        [FieldOrder(6)]
        [FieldConverter(typeof(StringListConverter))]
        public List<string> Keywords;

        [FieldOrder(7)]
        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        [FieldNullValue(typeof(DateTime), "1900-01-01")]
        public DateTime CreateDate;

        [FieldOrder(8)]
        [FieldConverter(ConverterKind.Date, "dd/MM/yyyy")]
        [FieldNullValue(typeof(DateTime), "1900-01-01")]
        public DateTime UpdateDate;

        [FieldOrder(9)]
        [FieldNullValue(typeof(string), "")]
        public string UpdatedBy;


        [FieldValueDiscarded]
        [FieldHidden]
        private TSF_EA.ElementWrapper origin = null;
        public TSF_EA.ElementWrapper Origin
        {
            get
            {
                return this.origin;
            }
            set
            {
                this.origin = value;
                this.GUID = this.origin.guid;
                this.Name = this.Origin.name;
                this.Author = this.Origin.author;
                this.Version = this.Origin.version;
                this.Status = this.Origin.status;
                this.Keywords = this.Origin.keywords;
                this.CreateDate = this.Origin.created;
                this.UpdateDate = this.Origin.modified;
                this.UpdatedBy = getTaggedValueString("modifier");
                //set the other origin values
                setOriginValues();
            }
        }
        protected abstract void setOriginValues();

        protected string getTaggedValueString(string tagName)
        {
            TSF_EA.TaggedValue tv = null;
            if (this.Origin != null)
                tv = this.Origin.getTaggedValue(tagName);
            return tv != null ? tv.eaStringValue : string.Empty;
        }

        public static List<T> Load<T>(string file)
        where T : GlossaryItem
        {
            var engine = new FileHelperEngine<T>();
            engine.HeaderText = engine.GetFileHeader();
            return engine.ReadFile(file).ToList();
        }

        public static void Save<T>(string file, List<T> items)
          where T : GlossaryItem
        {
            var engine = new FileHelperEngine<T>();
            engine.HeaderText = engine.GetFileHeader();
            engine.WriteFile(file, items);
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

            this.Origin = clazz as TSF_EA.ElementWrapper;
            this.Update(clazz);

            return clazz;
        }

        public void Save()
        {
            if (this.Origin == null)
            {
                // TODO assertion ?
                //      Can't Save without an Origin and should not be possible?!
                return;
            }
            this.Update(this.Origin as UML.Classes.Kernel.Class);
        }

        public virtual void Update(UML.Classes.Kernel.Class clazz)
        {
            var eaClass = clazz as TSF_EA.ElementWrapper;
            eaClass.name = this.Name;
            eaClass.author = this.Author;
            eaClass.version = this.Version;
            eaClass.status = this.Status;
            eaClass.keywords = this.Keywords;
            eaClass.created = this.CreateDate;
            eaClass.modified = this.UpdateDate;
            this.origin.addTaggedValue("modifier", this.UpdatedBy);

            eaClass.save();
        }

        public void SelectInProjectBrowser()
        {
            this.Origin.EAModel.selectedItem = this.Origin;
        }

    }
}

public class StringListConverter : ConverterBase
{
    public override object StringToField(string from)
    {
        if (from == null)
        {
            return new List<string>();
        }
        return from.Split(',').ToList();
    }

    public override string FieldToString(object fieldValue)
    {
        return string.Join(",", (List<string>)fieldValue);
    }

    // we want to handle null values
    protected override bool CustomNullHandling { get { return true; } }
}

// anything except nothing is true ;-)
public class BoolConverter : ConverterBase
{
    public override object StringToField(string delete)
    {
        if (delete != null && delete.Length > 0) { return true; }
        return false;
    }

    public override string FieldToString(object fieldValue)
    {
        return "";
    }

    // we want to handle null values
    protected override bool CustomNullHandling { get { return true; } }
}