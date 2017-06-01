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
	/// Description of ConvertPropertiesToAttributes.
	/// </summary>
	public class ConvertPropertiesToAttributes:MagicDrawCorrector
	{
		public ConvertPropertiesToAttributes(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			//Log start
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting converting embedded properties to attributes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			//Start converting
			List<TSF_EA.ElementWrapper> properties = this.model.getElementWrappersByQuery(@"select o1.[Object_ID] from [t_object] o1 where o1.[Object_Type] = 'Part'");
			
			foreach (TSF_EA.Property property in properties)
			{
				
				//Tell the user which property we are dealing  with
					EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Correcting property '{1}' with GUID '{2}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                 	,property.name
	                                	,property.guid)
	                   ,0
	                  ,LogTypeEnum.log);
				
				
				
				if(property.name != null)
				{
					//Create attribute with the name of the property
					TSF_EA.Attribute newAttribute = this.model.factory.createNewElement<TSF_EA.Attribute>(property.owner,property.name);
					
					if(property.classifier != null)
					{
						newAttribute.type = property.classifier;
					}
					newAttribute.multiplicity = property.multiplicity;
					newAttribute.save();
					
				}
				else
				{
					//Create attribute with the name of the classifier
					TSF_EA.Attribute newAttribute = this.model.factory.createNewElement<TSF_EA.Attribute>(property.owner,property.classifier.name);
				
					newAttribute.type = property.classifier;
					newAttribute.multiplicity = property.multiplicity;
					newAttribute.save();
				}
				
				
				//Delete property 
			}
			
		
			
			
			
			//Log Finished
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} converting embedded properties to attributes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
		}
	}
}
