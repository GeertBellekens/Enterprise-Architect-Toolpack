using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of CrossMDzipAttributeCorrector.
	/// </summary>
	public class CrossMDzipAttributeCorrector:MagicDrawCorrector
	{
		public CrossMDzipAttributeCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		
		#region implemented abstract members of MagicDrawCorrector
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Starting corrections for cross MDzip Attributes"
			              ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
			//get all attributes that have a foreign type
			foreach (var crossAttribute in magicDrawReader.allAttributes.Where(
										x => x.isCrossMDZip 
										&& !string.IsNullOrEmpty(x.typeMDGuid)))
			{
				//get the type element from EA
				var typeElement = this.getElementByMDid(crossAttribute.typeMDGuid);
				//make sure the element is a Type
				var typeAsType = typeElement as UML.Classes.Kernel.Type;
				if (typeAsType != null)
				{
					//get the corresponding attribute in EA that doesn't have the correct type yet
					string sqlGetCorrespondingAttribute = @"select a.ea_guid from (( t_attribute a 
															inner join t_object o on a.Object_ID = o.Object_ID)
															inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
																								and tv.Property = 'md_guid'))
															where
															tv.Value = '"+crossAttribute.mdParentGuid+ @"' 
															and a.Name = '"+crossAttribute.name+ @"'
															and (a.classifier is null 
															or a.Classifier <> '"+typeElement.id+"')";
					var correspondingAttributes = this.model.getAttributesByQuery(sqlGetCorrespondingAttribute);
					//set the type of the attribute in EA
					foreach (var attribute in correspondingAttributes) 
					{
						attribute.type = typeAsType;
						attribute.save();
						//set the md_guid tagged value
						attribute.addTaggedValue("md_guid",crossAttribute.mdGuid);
						//tell the user
						EAOutputLogger.log(this.model,this.outputName
						,string.Format("{0} Setting type '{1}' on attribute '{2}.{3}'"
						              ,DateTime.Now.ToLongTimeString()
						              , typeAsType.name
						              , attribute.owner.name
						              , attribute.name)
						,((TSF_EA.ElementWrapper)attribute.owner).id
						,LogTypeEnum.log);
					}
				}
			}
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Finished corrections for cross MDzip Attributes"
			              ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
		}
		#endregion
	}
}
