using EAAddinFramework.Utilities;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;



namespace EADataContract
{
    public abstract class ODCSItem
    {
        public static string profile => "ODCS::";
        public string id { get; set; }
        protected ODCSItem() { }
        public ODCSItem(YamlNode node, ODCSItem owner)
        {
            this.node = node;
            this.owner = owner;
            this.id = getStringValue("id");
        }
        public TSF_EA.Element importToModel(TSF_EA.Element context, int position)
        {
            this.getModelElement(context);
            this.updateModelElement(position);
            int i = 0;
            foreach (var childItem in this.getChildItems())
            {
                childItem.importToModel(modelElement, i);
                i++;
            }
            return modelElement;
        }
        public abstract void getModelElement(TSF_EA.Element context);
        public abstract void updateModelElement(int position);
        public abstract IEnumerable<ODCSItem> getChildItems();
        public YamlNode node { get; protected set; }
        public ODCSItem owner { get; protected set; }

        public TSF_EA.Element modelElement { get; protected set; } = null;
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
