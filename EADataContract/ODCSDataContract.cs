using EAAddinFramework.Utilities;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSDataContract : ODCSItem
    {
        public static string stereotype => profile + "ODCS_DataContract";
        private YamlMappingNode root => this.node as YamlMappingNode;
        private Package modelPackage => this.modelElement as Package;
        public ODCSDataContract(string filePath, YamlMappingNode node) : base(node, null)
        {
            this.filePath = filePath;
            this.name = getStringValue("name");
            this.version = getStringValue("version");
            this.ID = getStringValue("id");

            if (root.Children.TryGetValue("schema", out var schemaNode))
            {
                //create schema
                this.schema = new ODCSSchema(schemaNode, this);
            }
        }
        public void importContract(Package package)
        {
            this.importToModel(package, 0);
            EAOutputLogger.log("Importing Relationships" , 0, LogTypeEnum.log);
            this.importRelationships(0);
        }
        public string filePath { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string ID { get; set; }

        public ODCSSchema schema { get; set; }

        public static ODCSDataContract Parse(string filePath)
        {
            var yamlText = File.ReadAllText(filePath);
            var yaml = new YamlStream();
            yaml.Load(new StringReader(yamlText));

            var root = (YamlMappingNode)yaml.Documents[0]?.RootNode;
            var dataContract = new ODCSDataContract(filePath, root);
            return dataContract;
        }
        public static ODCSDataContract getUserSelectedContract()
        {
            ODCSDataContract dataContract = null;
            //Let the user select a .yaml file
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a Datacontract file",
                Filter = "Datacontract files (*.yaml;*.yml)|*.yaml;*.yml|All files (*.*)|*.*",
                FilterIndex = 1,
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = openFileDialog.FileName;
                dataContract = Parse(filePath);
            }
            return dataContract;
        }

        public override void getModelElement(Element context)
        {
            //context must always be a package
            var contextPackage = context as Package;
            if (contextPackage == null)
            {
                contextPackage = context?.getOwner<Package>();
            }
            if (contextPackage == null)
            {
                throw new InvalidDataException("ODCS Data Contract must be imported into a package or element owned by a package");
            }

            //look for package with correct name and stereotype under context package
            var existingPackage = contextPackage.getNestedPackageTree(true)
                                   .OfType<Package>()
                                   .FirstOrDefault(x => x.name == this.name 
                                                    && x.fqStereotype == stereotype);
            if (existingPackage != null)
            {
                this.modelElement = existingPackage;
            }
            else
            {
                //create new package
                var newPackage = contextPackage.addOwnedElement<Package>(this.name ?? this.ID); //if name not filled in, use ID instead
                newPackage.fqStereotype = stereotype;
                newPackage.save();
                this.modelElement = newPackage;
            }
        }

        public override void updateModelElement(int position)
        {
            this.modelPackage.name = this.name;
            this.modelPackage.version = this.version;
            this.modelPackage.addTaggedValue("ID", this.ID);
            this.modelPackage.save();
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>() { this.schema };
        }
    }
}
