using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EAAddinFramework.Utilities;
using TSF.UmlToolingFramework.UML.Classes.Kernel;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
    public class AttributeSequenceCorrector : MagicDrawCorrector
    {
        public AttributeSequenceCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage) : base(magicDrawReader, model, mdPackage)
        {

        }
        public override void correct()
        {
            //tell the user we are starting
            EAOutputLogger.log(this.model, this.outputName
                   , string.Format("{0} Starting to correct Attribute Sequence"
                                  , DateTime.Now.ToLongTimeString())
                   , 0
                   , LogTypeEnum.log);
            //loop all attributes
            foreach (var mdAttribute in magicDrawReader.allAttributes)
            {
                //get the corresponding attribute in EA that doesn't have the correct type yet
                string sqlGetCorrespondingAttribute = @"select a.ea_guid from (( t_attribute a 
															inner join t_object o on a.Object_ID = o.Object_ID)
															inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
																								and tv.Property = 'md_guid'))
															where
															tv.Value = '" + mdAttribute.mdParentGuid + @"' 
															and a.Name = '" + mdAttribute.name + "'";
                var correspondingAttributes = this.model.getAttributesByQuery(sqlGetCorrespondingAttribute);
                //set the type of the attribute in EA
                foreach (var attribute in correspondingAttributes)
                {
                    //only for BBIE attributes
                    if (attribute.stereotypeNames.Contains("BBIE"))
                    {
                        //tell the user we are starting
                        EAOutputLogger.log(this.model, this.outputName
                               , string.Format("{0} Setting {1} as sequence for attribute {2}"
                                              , DateTime.Now.ToLongTimeString()
                                              , mdAttribute.sequencingKey
                                              , attribute.owner.name + "." + attribute.name)
                               , 0
                               , LogTypeEnum.log);
                        attribute.addTaggedValue("sequencingKey", mdAttribute.sequencingKey.ToString());
                        //set the md_guid tagged value
                        attribute.addTaggedValue("md_guid", mdAttribute.mdGuid);
                    }
                }
            }
            //tell the user we are finished
            EAOutputLogger.log(this.model, this.outputName
                   , string.Format("{0} Starting to correct Attribute Sequence"
                                  , DateTime.Now.ToLongTimeString())
                   , 0
                   , LogTypeEnum.log);

        }
    }
}
