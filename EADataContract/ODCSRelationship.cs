using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using TSF.UmlToolingFramework.UML.CommonBehaviors.BasicBehaviors;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class ODCSRelationship: ODCSItem
    {
        public static string stereotype => profile + "ODCS_Relationship";
        private Association modelAssociation => this.modelElement as Association;
        public string from { get; set; }
        public string to { get; set; }  
        public string type { get; set; }
        public ODCSRelationship(YamlMappingNode node, ODCSItem owner) : base(node, owner)
        {
            this.from = getStringValue("from");
            this.to = getStringValue("to");
            this.type = getStringValue("type");
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            return new List<ODCSItem>(); //no child items
        }

        public override void getModelElement(Element context)
        {
            Attribute fromAttribute;
            if (context is Attribute)
            {
                fromAttribute = (Attribute)context; 
            }
            else
            {
                fromAttribute = getAttributeFromString(context, this.from);
            }
            var toAttribute = getAttributeFromString(context, this.to);
            // we need both from attribute as toattribute to continue
            if (fromAttribute == null || toAttribute == null) return;
            //create association if not already present
            var sqlGetData = $@"select c.Connector_ID from t_connector c
                            inner join t_object o on o.Object_ID = c.Start_Object_ID
                            where c.StyleEx like '%LFSP={fromAttribute.uniqueID}%'
                            and c.StyleEx like '%LFEP={toAttribute.uniqueID}%'";
            this.modelElement = context.EAModel.getRelationsByQuery(sqlGetData).FirstOrDefault();
            
            if (this.modelElement == null)
            {
                //create new relationship
                this.modelElement = fromAttribute.model.factory.createNewElement<Association>(fromAttribute.getOwner<Class>(), string.Empty);
                this.modelAssociation.source = fromAttribute;
                this.modelAssociation.target = toAttribute;
                this.modelAssociation.targetEnd.isNavigable = true;
                this.modelAssociation.addStereotype(stereotype);
                this.modelAssociation.save();
            }
        }

        public override void updateModelElement(int position)
        {
            //no properties to update on the relationship
        }
        private Attribute getAttributeFromString (Element context, string attributeString)
        {
            
            //attribute string is of the form ClassName.AttributeName
            var parts = attributeString.Split('.');
            string className;
            string attributeName;

            if (parts.Length == 2)
            {
                //get class name(id) and atrribute name (id)
                className = parts[0];
                attributeName = parts[1];
            }
            else
            {
                //try fully qualified string, select only the last two
                // a fully qualified string is of the form of schema/{className}/properties/{attributeName}
                // so we ned the second and 4th parts
                var fqParts = attributeString.Split('/');
                if (fqParts.Length != 4) return null; //skip further processing. We can't parse the name
                className = fqParts[1];
                attributeName = fqParts[3];
            }

            var parentPackage = context.getOwner<Package>();
            var sqlGetData = $@"select a.ea_guid 
                        from t_attribute a
                        left join t_attributetag tva on tva.ElementID = a.ID
							                        and tva.Property = 'id'
                        inner join t_object o on o.Object_ID = a.Object_ID
                        left join t_objectproperties tvo on tvo.Object_ID = o.Object_ID
						                        and tvo.Property = 'id'
                        where case when tva.VALUE = '' then a.Name else coalesce(tva.Value, a.Name) end = '{attributeName}'
                        and case when tvo.VALUE = '' then o.Name else coalesce(tvo.Value, o.Name) end = '{className}'
                        and o.Package_ID = {parentPackage.packageID}";
            return context.EAModel.getAttributesByQuery(sqlGetData).FirstOrDefault();
        }
    }
}
