using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using Newtonsoft.Json.Schema;
using UML = TSF.UmlToolingFramework.UML;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using EAAddinFramework.Utilities;

namespace EAJSON
{
    public class EAJSONSchema
    {
        //stereotypeNames
        public const string profileName = "JSON";
        public const string schemaStereotype = "JSON_Schema";
        public const string attributeStereotype = "JSON_Attribute";
        public const string datatypeStereotype = "JSON_Datatype";
        public const string elementStereotype = "JSON_Element";
        public const string schemaFileTagName = "schemaFileName";

        //(facet) tagged value names
        const string tv_minlength = "minlength";
        const string tv_maxlength = "maxlength";
        const string tv_pattern = "pattern";
        const string tv_format = "format";
        const string tv_enum = "enum";
        const string tv_minimum = "minimum";
        const string tv_exclusiveminimum = "exclusiveminimum";
        const string tv_maximum = "maximum";
        const string tv_exclusivemaximum = "exclusivemaximum";
        const string tv_multipleof = "multipleof";

        private TSF_EA.ElementWrapper _rootElement;

        private TSF_EA.ElementWrapper rootElement
        {
            get => _rootElement;
            set
            {
                if (value != null && 
                    value.stereotypes.Any(x => x.name.Equals(schemaStereotype, StringComparison.InvariantCultureIgnoreCase)))
                {
                    _rootElement = value;
                }
                else
                {
                    throw new ArgumentException($"The root element should have the «{schemaStereotype}» stereotype");
                }
            }
        }
        public EAJSONSchema(TSF_EA.ElementWrapper rootElement)
        {
            this.rootElement = rootElement;
        }
        private Uri _schemaId;
        public Uri schemaId
        {
            get
            {
                if (this._schemaId == null)
                {
                    var idTag = this.rootElement.taggedValues.FirstOrDefault(x => x.name.Equals("id", StringComparison.InvariantCultureIgnoreCase));
                    try
                    {
                        this._schemaId = new Uri(idTag?.tagValue.ToString());
                    }
                    catch(System.UriFormatException e)
                    {
                        var errorMessage = $"Fill the property 'id' of the element '{this.rootElement.name}' with a valid URL";
                        Logger.logError(errorMessage);
                        throw new Exception(errorMessage, e);
                    }
                }
                return this._schemaId;
            }
        }
        private Uri _schemaVersion;
        public Uri schemaVersion
        {
            get
            {
                if (this._schemaVersion == null)
                {
                    var schemaTag = this.rootElement.taggedValues.FirstOrDefault(x => x.name.Equals("schema", StringComparison.InvariantCultureIgnoreCase));
                    try
                    {
                        this._schemaVersion = new Uri(schemaTag?.tagValue.ToString());
                    }
                    catch (System.UriFormatException e)
                    {
                        var errorMessage = $"Fill the property 'schema' of the element '{this.rootElement.name}' with a valid URL";
                        Logger.logError(errorMessage);
                        throw new Exception(errorMessage, e);
                    }

                }
                return this._schemaVersion;
            }
        }
        private JSchema _schema;
        public JSchema schema
        {
            get
            {
                if (_schema == null)
                {
                    _schema = this.generateSchema();
                }
                return _schema;
            }
        }
        private string _schemaFileName;
        public string schemaFileName
        {
            get
            {
                if (string.IsNullOrEmpty(this._schemaFileName))
                {
                    var fileNameTag = this.rootElement.taggedValues.FirstOrDefault(x => x.name.Equals(schemaFileTagName, StringComparison.InvariantCultureIgnoreCase));
                    if (fileNameTag != null
                        && !string.IsNullOrWhiteSpace(fileNameTag.tagValue.ToString()))
                    {
                        this._schemaFileName = fileNameTag.tagValue.ToString();
                    }
                    else
                    {
                        //let the user select a file
                        var browseSchemaFile = new SaveFileDialog();
                        browseSchemaFile.Title = $"Save schema for {this.rootElement.name}";
                        browseSchemaFile.Filter = "JSON Schema files|*.json;";
                        browseSchemaFile.FilterIndex = 1;
                        var dialogResult = browseSchemaFile.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            //save the schema file name
                            this.rootElement.addTaggedValue(schemaFileTagName, browseSchemaFile.FileName);
                            //set the filename
                            this._schemaFileName = browseSchemaFile.FileName;
                        }
                    }
                }
                return this._schemaFileName;
            }
        }
        /// <summary>
        /// print this json schema to the schema filename
        /// </summary>
        public void print()
        {
            if (! string.IsNullOrEmpty(this.schemaFileName))
            {
                System.IO.File.WriteAllText(this.schemaFileName, this.schema.ToString());
            }
        }
        private JObject _definitions;
        private JObject definitions
        {
            get
            {
                if (_definitions == null)
                {
                    _definitions = new JObject();
                }
                return _definitions;
            }
        }

        private JSchema generateSchema()
        {

            //create schema
            var generatedSchema = createSchemaForElement(this.rootElement as UML.Classes.Kernel.Type);
            //set version and Id
            generatedSchema.SchemaVersion = this.schemaVersion;
            generatedSchema.Id = this.schemaId;
            generatedSchema.Title = this.rootElement.name;
            generatedSchema.Description = $"Version: {this.rootElement.version}" +
                                          Environment.NewLine + this.rootElement.EAModel.convertFromEANotes(this.rootElement.notes,"TXT");
            generatedSchema.ExtensionData.Add("definitions", this.definitions);

            return generatedSchema;
        }
        private JSchema createSchemaForElement(UML.Classes.Kernel.Type type)
        {
            var elementSchema = new JSchema();
            //set id 
            //some tools seem to have issues with the references to these type of ids.
            //elementSchema.Id = new Uri("#" + type.name, UriKind.Relative);
            //set description
            elementSchema.Description = ((TSF_EA.Element)type).EAModel.convertFromEANotes(type.ownedComments .FirstOrDefault()?.body, "TXT");
            //set schema type
            setSchemaType(type, elementSchema);
            //add properties
            var element = type as TSF_EA.ElementWrapper;
            if (element != null)
            {
                addProperties(elementSchema, element);
            }
            return elementSchema;
        }

        private void addProperties(JSchema schema, TSF_EA.ElementWrapper element)
        {
            //don't do anything if there are no attributes
            if (!element.attributes.Any()) return;
            //loop attributes
            foreach (var attribute in element.attributes)
            {
                //get the type of the attribute
                schema.Properties.Add(attribute.name, getPropertySchema(attribute));
                //add to required list if mandatory
                if (attribute.lower > 0 )
                {
                    schema.Required.Add(attribute.name);
                }
            }
            //don't allow additional properties
            schema.AllowAdditionalProperties = false;
        }
        private JSchema getPropertySchema(UML.Classes.Kernel.Property attribute)
        {
            var typeSchema = new JSchema();
            //set description
            typeSchema.Description = attribute.ownedComments.FirstOrDefault()?.body;
            //check if it is an array
            if (attribute.upper.isUnlimited || attribute.upper.integerValue > 1)
            {
                typeSchema.Type = JSchemaType.Array;
                //set lower value
                if (attribute.lower > 0)
                {
                    typeSchema.MinimumItems = attribute.lower;
                }
                //set upper value
                if (!attribute.upper.isUnlimited)
                {
                    typeSchema.MaximumItems = attribute.upper.integerValue;
                }
                JSchema itemsType;
                if (attribute.type is UML.Classes.Kernel.Class)
                {
                    itemsType = this.addDefinition(attribute.type);
                }
                else
                {
                    //set the type of the items
                    itemsType = createSchemaForElement(attribute.type);
                }
                //set the isUnique property
                if (attribute.isUnique)
                {
                    typeSchema.UniqueItems = true;
                }
                typeSchema.Items.Add(itemsType);
            }
            else if(attribute.type is UML.Classes.Kernel.Class)
            {
                //add schema to definitions if of type object
                typeSchema = this.addDefinition( attribute.type);
            }
            else
            {
                setSchemaType(attribute.type, typeSchema);
            }
            //process facets on the attribute
            processFacets(attribute, typeSchema);
            
            return typeSchema;
        }
        private JSchema addDefinition(UML.Classes.Kernel.Type type)
        {
            JToken definitionSchema; 
            //check if the definition doesn't exist yet
            if (!this.definitions.TryGetValue(type.name, out definitionSchema))
            {
                //create the schema
                definitionSchema = createSchemaForElement(type);
                //add the definition
                this.definitions.Add(type.name, definitionSchema);
            }
            //return
            return (JSchema) definitionSchema;
        }

        private void setSchemaType(UML.Classes.Kernel.Type type, JSchema typeSchema)
        {
            //TODO find a better, non hardcoded way to determine the type properties
            if (type is UML.Classes.Kernel.Class)
            {
                typeSchema.Type = JSchemaType.Object;
            }
            else if (type is UML.Classes.Kernel.Enumeration)
            {
                typeSchema.Type = JSchemaType.String;
                foreach(var enumValue in ((UML.Classes.Kernel.Enumeration)type).ownedLiterals.OfType<TSF_EA.EnumerationLiteral>())
                {
                    //TODO: make configurable in setting
                    var valueToUse = string.IsNullOrEmpty(enumValue.alias)
                                    ? enumValue.name
                                    : enumValue.alias;
                    typeSchema.Enum.Add(JValue.CreateString(valueToUse));
                }   
            }
            else if (type is UML.Classes.Kernel.DataType)
            {
                switch (type.name.ToLower())
                {
                    //check if the name is in the list of JSON types
                    case "string":
                        typeSchema.Type = JSchemaType.String;
                        break;
                    case "integer":
                        typeSchema.Type = JSchemaType.Integer;
                        break;
                    case "number":
                        typeSchema.Type = JSchemaType.Number;
                        break;
                    case "boolean":
                        typeSchema.Type = JSchemaType.Boolean;
                        break;
                }
                //do base types
                if (!typeSchema.Type.HasValue)
                {
                    var dataType = type as TSF_EA.DataType;
                    if (dataType!= null && dataType.superClasses.Any())
                    {
                        setSchemaType(((TSF_EA.DataType)type).superClasses.First(), typeSchema);
                    }
                }
                //process facets (of the datatype
                processFacets(type, typeSchema);
            }

        }
        private static void processFacets(UML.Classes.Kernel.Element element, JSchema typeSchema)
        {
            foreach (var tag in element.taggedValues
                                .Where(x => !string.IsNullOrEmpty(x.tagValue.ToString())))
            {
                //get string value
                var stringValue = tag.tagValue.ToString();
                //get long value
                long tempLong;
                long? longValue = null;
                if (long.TryParse(stringValue, out tempLong))
                {
                    longValue = tempLong;
                }
                //get double value
                double tempDouble;
                double? doubleValue = null;
                if (double.TryParse(stringValue, out tempDouble))
                {
                    doubleValue = tempDouble;
                }

                switch (tag.name.ToLower())
                {
                    
                    //string facets
                    case tv_minlength:
                        typeSchema.MinimumLength = longValue;
                        break;
                    case tv_maxlength:
                        typeSchema.MaximumLength = longValue;
                        break;
                    case tv_pattern:
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            //initialize
                            typeSchema.Pattern = string.Empty;
                            //add start indicator
                            if (!stringValue.StartsWith("^"))
                            {
                                typeSchema.Pattern = "^";
                            }
                            //add pattern
                            typeSchema.Pattern = typeSchema.Pattern + stringValue;
                            //add end indicator
                            if (!stringValue.EndsWith("$"))
                            {
                                typeSchema.Pattern = typeSchema.Pattern + "$";
                            }
                        }
                        break;
                    case tv_format:
                        if (stringValue.Equals("<none>", StringComparison.InvariantCultureIgnoreCase))
                        {
                            typeSchema.Format = null;
                        }
                        else
                        {
                            typeSchema.Format = stringValue;
                        }
                        break;
                    case tv_enum:
                        foreach(var enumValue in stringValue.Split(','))
                        {
                            typeSchema.Enum.Add(JValue.CreateString(enumValue));
                        }
                        break;
                    //numeric facets
                    case tv_minimum:
                        typeSchema.Minimum = longValue;
                        typeSchema.ExclusiveMinimum = false;
                        break;
                    case tv_exclusiveminimum:
                        typeSchema.Minimum = longValue;
                        typeSchema.ExclusiveMinimum = true;
                        break;
                    case tv_maximum:
                        typeSchema.Maximum = longValue;
                        typeSchema.ExclusiveMaximum = false;
                        break;
                    case tv_exclusivemaximum:
                        typeSchema.Maximum = longValue;
                        typeSchema.ExclusiveMaximum = true;
                        break;
                    case tv_multipleof:
                        typeSchema.MultipleOf = doubleValue;
                        break;
                }
            }
        }
        public static void transformPackage(UML.Classes.Kernel.Package package, UML.Classes.Kernel.Class rootClass)
        {
            //transform the rest of the package
            if (package != null)
            {
                foreach (var element in package.ownedElements.OfType<UML.Classes.Kernel.Classifier>())
                {
                    transformElement(element as TSF_EA.ElementWrapper, rootClass);
                }
            }
            //loop subPackages
            foreach (var subPackage in package.nestedPackages)
            {
                transformPackage(subPackage, rootClass);
            }
        }
        private static void transformElement(TSF_EA.ElementWrapper element, UML.Classes.Kernel.Class rootClass)
        {
            //set stereotype (not for rootclass)
            if (element.uniqueID != rootClass?.uniqueID)
            {
                if (element is TSF_EA.DataType)
                {
                    if (! element.hasStereotype(datatypeStereotype))
                    {
                        element.addStereotype(profileName + "::" + datatypeStereotype);
                        element.save();
                        //transform from XML datatypes
                        transformFromXmlDatatypes((TSF_EA.DataType)element);
                    }
                }
                else if (element is UML.Classes.Kernel.Class)
                {
                    if (!element.hasStereotype(elementStereotype))
                    {
                        element.addStereotype(profileName + "::" + elementStereotype);
                        element.save();
                    }
                }
            }
            else
            {
                if (!element.hasStereotype(schemaStereotype))
                {
                    //transform rootClass
                    element.addStereotype(profileName + "::" + schemaStereotype);
                    element.save();
                }
            }
            //loop attributes
            foreach (var attribute in element.attributes.OfType<TSF_EA.Attribute>())
            {
                attribute.addStereotype(profileName + "::" + attributeStereotype);
                attribute.save();
            }
        }
        private static void transformFromXmlDatatypes(TSF_EA.DataType dataType)
        {
            switch (dataType.name.ToLower())
            {
                //check if the name is one of the known XML types
                case "decimal":
                case "float":
                case "double":
                    dataType.name = "number";
                    
                    break;
                case "duration":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^-?P((([0-9]+Y([0-9]+M)?([0-9]+D)?|([0-9]+M)([0-9]+D)?|([0-9]+D))(T(([0-9]+H)([0-9]+M)?([0-9]+(\.[0-9]+)?S)?|([0-9]+M)([0-9]+(\.[0-9]+)?S)?|([0-9]+(\.[0-9]+)?S)))?)|(T(([0-9]+H)([0-9]+M)?([0-9]+(\.[0-9]+)?S)?|([0-9]+M)([0-9]+(\.[0-9]+)?S)?|([0-9]+(\.[0-9]+)?S))))$");
                    break;
                case "datetime":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_format, "date-time");
                    break;
                //TODO: find better way
                case "time":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_format, "time"); //only valid from draft 07
                    break;
                case "date":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_format, "date"); //only valid from draft 07
                    break;
                case "gyearmonth":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^-?([1-9][0-9]{3,}|0[0-9]{3})-(0[1-9]|1[0-2])(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?$");
                    break;
                case "gyear":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^-?([1-9][0-9]{3,}|0[0-9]{3})(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?$");
                    break;
                case "gmonthday":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^--(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?$");
                    break;   
                case "gday":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^---(0[1-9]|[12][0-9]|3[01])(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?$");
                    break;
                case "gmonth":
                    dataType.name = "string";
                    dataType.addTaggedValue(tv_pattern, @"^--(0[1-9]|1[0-2])(Z|(\+|-)((0[0-9]|1[0-3]):[0-5][0-9]|14:00))?$");
                    break;
                //TODO
                //case "hexbinary":
                //case "base64binary":
                //case "anyuri":
                //case "qname":
                //case "notation":
                //case "normalizedstring":
                //case "token":
                //case "language":
                //case "nmtoken":
                //case "nmtokens":
                //case "name":
                //case "ncname":
                //case "id":
                //case "idref":
                //case "idrefs":
                //case "entity":
                //case "entities":
                case "nonpositiveinteger":
                    dataType.name = "integer";
                    dataType.addTaggedValue(tv_maximum, "0");
                    break;
                case "negativeinteger":
                    dataType.name = "integer";
                    dataType.addTaggedValue(tv_exclusivemaximum, "0");
                    break;
                case "long":
                case "int":
                case "short":
                case "byte":
                    dataType.name = "integer";
                    break;
                case "nonnegativeinteger":
                case "unsignedlong":
                case "unsignedint":
                case "unsignedshort":
                case "unsignedbyte":
                case "positiveinteger":
                    dataType.name = "integer";
                    dataType.addTaggedValue(tv_minimum, "0");
                    break;
                    //TODO
                    //case "yearmonthduration":
                    //case "daytimeduration":
                    //case "datetimestamp":
            }
            //process the xml facet tagged values
            processXMLFacets(dataType);
            //save the datatype
            dataType.save();
        }
        private static void processXMLFacets(TSF_EA.Element element)
        {
            double? totalDigits = null;
            double? fractionDigits = null;
            bool minimumSet = false;
            bool maximumSet = false;
            bool multipleOfSet = false;
            foreach (var tag in element.taggedValues
                               .Where(x => !string.IsNullOrEmpty(x.tagValue.ToString())))
            {
                //get string value
                var stringValue = tag.tagValue.ToString();
                //get long value
                long tempLong;
                long? longValue = null;
                if (long.TryParse(stringValue, out tempLong))
                {
                    longValue = tempLong;
                }
                //get double value
                double tempDouble;
                double? doubleValue = null;
                if (double.TryParse(stringValue, out tempDouble))
                {
                    doubleValue = tempDouble;
                }

                switch (tag.name.ToLower())
                {
                    //xsd facets
                    case "fractiondigits":
                        fractionDigits = doubleValue;
                        break;
                    case "length":
                        element.addTaggedValue(tv_minlength, longValue.ToString());
                        element.addTaggedValue(tv_maxlength, longValue.ToString());
                        break;
                    case "maxexclusive":
                        element.addTaggedValue(tv_exclusivemaximum, longValue.ToString());
                        maximumSet = true;
                        break;
                    case "maxinclusive":
                        element.addTaggedValue(tv_maximum, longValue.ToString());
                        maximumSet = true;
                        break;
                    case "minexclusive":
                        element.addTaggedValue(tv_exclusiveminimum, longValue.ToString());
                        minimumSet = true;
                        break;
                    case "mininclusive":
                        element.addTaggedValue(tv_minimum, longValue.ToString());
                        minimumSet = true;
                        break;
                    case "totaldigits":
                        totalDigits = doubleValue;
                        break;
                }
            }
            //set minimum and maximum and multipleOf based on fractiondigits and totalDigits
            //but only if minimum, maximum and multipleOf are not yet set
            if (totalDigits.HasValue)
            {
                if (!fractionDigits.HasValue)
                {
                    fractionDigits = 0;
                }
                if (!maximumSet)
                {
                    element.addTaggedValue(tv_exclusivemaximum, Math.Pow(10, totalDigits.Value - fractionDigits.Value).ToString());
                }
                if (!multipleOfSet && fractionDigits > 0)
                {
                    element.addTaggedValue(tv_multipleof, Math.Pow(10, (fractionDigits.Value * -1)).ToString());
                }
                if (!minimumSet)
                {
                    element.addTaggedValue(tv_exclusiveminimum, (Math.Pow(10, totalDigits.Value - fractionDigits.Value) * -1).ToString());
                }
            }
        }    
    }
}
