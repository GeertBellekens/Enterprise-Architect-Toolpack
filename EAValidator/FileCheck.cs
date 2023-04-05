using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidator
{
    internal class FileCheck: Check
    {
        public string checkfile { get; }

        protected override string xmlString => File.ReadAllText(this.checkfile);

        protected override string checkName => new FileInfo(checkfile).Name;

        public FileCheck(string checkFile, CheckGroup group, EAValidatorSettings settings, TSF_EA.Model model):base(group, settings, model)
        {
            this.checkfile = checkFile;
            //load the contents from the the xml file
            this.loadXml();
        }

        public override void save()
        {
            this.xdoc.Save(this.checkfile);
        }
        public virtual string helpUrl
        {
            get
            {
                if (string.IsNullOrEmpty(this.helpUrlText))
                {
                    var helpPdf = Path.GetDirectoryName(this.checkfile)
                                  + "\\"
                                  + Path.GetFileNameWithoutExtension(this.checkfile) + ".pdf";

                    if (File.Exists(helpPdf))
                    {
                        return helpPdf;
                    }
                }
                return this.helpUrlText;
            }
        }
    }
}
