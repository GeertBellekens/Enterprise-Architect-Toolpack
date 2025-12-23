using EAAddinFramework.Utilities;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Helpers;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;



namespace EADataContract
{
    public abstract class ODCSItem
    {
        public static string profile => "ODCS::";
        public string id { get; set; }
        public List<ODCSRelationship> relationships { get; } = new List<ODCSRelationship>();
        protected ODCSItem() { }
        public ODCSItem(YamlNode node, ODCSItem owner)
        {
            this.node = node;
            this.owner = owner;
            this.id = getStringValue("id");
        }
        protected TSF_EA.Element importToModel(TSF_EA.Element context, int position)
        {
            this.getModelElement(context);
            this.updateModelElement(position);
            this.getRelationships();
            int i = 0;
            foreach (var childItem in this.getChildItems())
            {
                childItem.importToModel(modelElement, i);
                i++;
            }
            return modelElement;
        }
        protected void importRelationships(int position)
        {
            foreach (var relationship in this.relationships)
            {
                relationship.importToModel(this.modelElement, position);
            }
            //process relationships of child items
            int i = 0;
            foreach (var childItem in this.getChildItems())
            {
                childItem.importRelationships(i);
                i++;
            }
        }
        protected void getRelationships()
        {
            YamlNode relationshipsNode = null;
            if (this.node is YamlMappingNode)
            { 
                var children = ((YamlMappingNode)this.node).Children;
                if (children.TryGetValue("relationships", out relationshipsNode))
                {
                    //check if sequence node
                    if (relationshipsNode is YamlSequenceNode)
                    {
                        var relationshipsSeqNode = relationshipsNode as YamlSequenceNode;
                        if (relationshipsSeqNode == null) return;
                        foreach (var relationshipNode in relationshipsSeqNode.Children.OfType<YamlMappingNode>())
                        {
                            var odcsRelationship = new ODCSRelationship(relationshipNode, this);
                            this.relationships.Add(odcsRelationship);
                        }
                    }
                }
            }
            
        }
        public abstract void getModelElement(TSF_EA.Element context);
        public abstract void updateModelElement(int position);
        public abstract IEnumerable<ODCSItem> getChildItems();
        public YamlNode node { get; protected set; }
        public ODCSItem owner { get; protected set; }

        public TSF_EA.Element modelElement { get; protected set; } = null;
        public string getYamlString()
        {

            return new SerializerBuilder()
                .Build().Serialize(this.node).Trim();
            //var stream = new YamlStream(new YamlDocument(this.node));
            //using (var writer = new StringWriter())
            //{
            //    stream.Save(writer, assignAnchors: false);
            //    return writer.ToString();
            //}
        }
        protected string getStringValue(string key)
        {
            if (this.node is YamlMappingNode mappingNode)
            {
                if (mappingNode.Children.TryGetValue(key, out var valueNode)
                    && valueNode is YamlScalarNode)
                {
                    return ((YamlScalarNode)valueNode).Value;
                }
            }
            return null;
        }
        protected bool? getBooleanValue(string key)
        {
            if (bool.TryParse(getStringValue(key), out var booleanValue))
            {
                return booleanValue;
            }
            else
            {
                return null;
            }
        }
        protected int? getIntValue(string key)
        {
            if (int.TryParse(getStringValue(key), out var intValue))
            {
                return intValue;
            }
            else
            {
                return null;
            }
        }
        protected decimal? getDecimalValue(string key)
        {
            if (decimal.TryParse(getStringValue(key), out var decimalValue))
            {
                return decimalValue;
            }
            else
            {
                return null;
            }
        }
        public override string ToString()
        {
            return this.node?.ToString();
        }
    }
}
