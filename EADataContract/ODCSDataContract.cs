using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace EADataContract
{
    public class ODCSDataContract
    {

        public ODCSDataContract(string filePath)
        {
            this.filePath = filePath;
            this.Parse(filePath);
        }
        public string filePath { get; set; }
        public ODCSSchema schema { get; set; }

        public void Parse(string filePath)
        {
            var yamlText = File.ReadAllText(filePath);

            var yaml = new YamlStream();
            yaml.Load(new StringReader(yamlText));

            var root = (YamlMappingNode)yaml.Documents[0]?.RootNode;

            if (!root.Children.TryGetValue("schema", out var schemaNode))
            {
                return;
            }

            //create schema
            this.schema = new ODCSSchema(schemaNode);
        }
    }
}
