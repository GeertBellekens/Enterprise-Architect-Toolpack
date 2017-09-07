using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;
using System.Xml;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of LinksAttributesEnumsCorrector.
	/// </summary>
	public class LinksAttributesEnumsCorrector:MagicDrawCorrector
	{
		public LinksAttributesEnumsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting create links between attributes and elements'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			
			
			//get al the classes with stereotype BusinessEntity
			var classes = this.model.getElementWrappersByQuery(@"select distinct o.[Object_ID]
																from t_object o
																left join t_xref x on (o.[ea_guid] = x.[Client])
																where o.[Stereotype] like 'BusinessEntity' or o.[StereoType] like 'bEntity'
																or (x.[Name] = 'StereoTypes' and x.[Description] = 'BusinessEntity' or x.[Description] = 'bEntity')");
			
			//loop the attribues in the classes, filter the enums out
			foreach (var businessEntity in classes) 
			{
				foreach (var attribute in businessEntity.attributes.Where(x => x.classifier is UML.Classes.Kernel.Enumeration))
				{
					if(!attribute.relationships.Any())
					{
						EAOutputLogger.log(this.model,this.outputName
						                   	,string.Format("{0} Create <<usage>> link from '{1}.{2}' to '{3}'"
		                                  	,DateTime.Now.ToLongTimeString()
		                                 	,businessEntity.name
		                                	,attribute.name
		                                	,attribute.classifier)
		                   ,businessEntity.id
		                  ,LogTypeEnum.log);
						
						// create a 'usage' link from the enum attributes to the enum entities
						TSF_EA.Usage usage = this.model.factory.createNewElement<TSF_EA.Usage>(attribute.owner, string.Empty);
						usage.source = attribute;
						usage.target = (TSF_EA.Element)attribute.classifier;
						usage.targetEnd.isNavigable = true;
						usage.save();
					}
						
				
					
				}											
			}
		
			
			
			
		
			
			
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished create links between attributes and elements'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
		}
	}
}
