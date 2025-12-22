using EAAddinFramework.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF.UmlToolingFramework.Wrappers.EA;
using YamlDotNet.RepresentationModel;
using TFS_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EADataContract
{
    public class ODCSLogicalType:ODCSItem
    {
        public static string stereotype => profile + "ODCS_LogicalType";
        public ODCSLogicalType() { }
        
        public ODCSLogicalType(string logicalTypeName, YamlMappingNode optionsNode, ODCSProperty owner):base(optionsNode, owner)
        {
            if (Enum.TryParse<ODCSLogicalTypeEnum>(logicalTypeName, out var typeEnumValue))
            {
                this.type = typeEnumValue;
            }
        }
        private DataType modelDataType => this.modelElement as DataType;
        private ODCSProperty ownerProperty => this.owner as ODCSProperty;
        public ODCSLogicalTypeEnum type { get; set; }
        private ODCSLogicalTypeOptions _options = null;
        public ODCSLogicalTypeOptions options 
        { 
            get
            {
                if (_options == null
                    && this.node != null)
                {
                    //_options = new ODCSLogicalTypeOptions(this.node, this);
                }
                return _options;
            }
        }

        public override void getModelElement(Element context)
        {
            var contextAttribute = context as TFS_EA.Attribute;
            if (contextAttribute == null)
            {
                throw new InvalidDataException("ODCS Logical Type must be imported with Attribute as context");
            }
            //check if the attribute already as a type that corresponds to the logical type
            var existingType = contextAttribute.type as DataType;
            if (existingType.getTaggedValue("type")?.eaStringValue == this.type.ToString())
            //TODO: add check for options as well
            {
                this.modelElement = existingType;
            }
            else
            {
                //create new data type
                var dataTypeName = $"{contextAttribute.name}_{this.type}_Type";
                var datatype = getExistingDataType(contextAttribute.getOwner<Package>(), dataTypeName);
                if (datatype != null)
                {
                    this.modelElement = datatype;
                }
                else
                {
                    var newDataType = contextAttribute.getOwner<Package>().addOwnedElement<DataType>(dataTypeName);
                    newDataType.fqStereotype = stereotype;
                    newDataType.save();
                    this.modelElement = newDataType;
                }
                //TODO: add options as tagged values
            }
        }
        public DataType getExistingDataType(Package package, string datatypeName)
        {
            //check if a datatype exists with the given name and the correct stereotype
            return package.ownedElementWrappers.OfType<DataType>()
                .FirstOrDefault(x => x.name == datatypeName
                                    && x.fqStereotype == stereotype);
        }

        public override void updateModelElement(int position)
        {
            EAOutputLogger.log($"Updating datatype: {this.modelDataType.name}"
                           , this.modelDataType.id
                           , LogTypeEnum.log);
            this.modelDataType.addTaggedValue("type", this.type.ToString());
            //update type of parent property
            this.ownerProperty.modelAttribute.type = this.modelDataType;
            this.ownerProperty.modelAttribute.save();
        }

        public override IEnumerable<ODCSItem> getChildItems()
        {
            var childItems = new List<ODCSItem>();
            if (this.options != null)
            {
                childItems.Add(this.options);
            }
            return childItems;
        }
    }
    public enum ODCSLogicalTypeEnum
    {
        @string,
        date,
        number,
        integer,
        @object,
        array,
        boolean
    }
}
