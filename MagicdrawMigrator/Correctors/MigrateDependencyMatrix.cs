using System.Collections;
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
	/// Description of MigrateDependencyMatrix.
	/// </summary>
	public class MigrateDependencyMatrix:MagicDrawCorrector
	{
		public MigrateDependencyMatrix(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
				/* Check how the Depencency matrix information can be transferred to EA. 
 				Since this seems to be linking attributes with other attributes or associations we 
				will probably need to use tagged values to create the links and specific SQL Searches 
				to get the information out of EA as well. 
				(to be check with Thibault if the information is present or not)*/
				
				EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections for relationship matrixes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
				
				
				foreach(var mdDependency in magicDrawReader.allAttDependencies)
				{			
					//get the corresponding attributes in EA
					//source element
					var sourceElement = (TSF_EA.Class)getElementByMDid(mdDependency.sourceParentGuid);
					
					//target element
					var targetElement = (TSF_EA.Class)getElementByMDid(mdDependency.targetParentGuid);
					
					if (sourceElement != null & targetElement != null)
					{
						var sourceAttribute = (TSF_EA.Attribute)sourceElement.attributes.FirstOrDefault( x => x.name == mdDependency.sourceName);
						mdDependency.sourceGuid = sourceAttribute != null? sourceAttribute.guid: string.Empty;
						
						var targetAttribute = (TSF_EA.Attribute)targetElement.attributes.FirstOrDefault( x => x.name == mdDependency.targetName);
						mdDependency.targetGuid = targetAttribute != null? targetAttribute.guid: string.Empty;
						
						if (sourceAttribute!= null && targetAttribute != null)
						{
                            //add duplicates here to allow for 
							sourceAttribute.addTaggedValue("sourceAttribute", mdDependency.targetGuid,null,true);
												
							EAOutputLogger.log(this.model,this.outputName
							                   	,string.Format("{0} Set sourceAttribute '{1}' for '{2}'"
			                                  	,DateTime.Now.ToLongTimeString()
			                                 	,mdDependency.targetName
			                                	,mdDependency.sourceName)
			                   ,0
			                  ,LogTypeEnum.log);
						}
					}
					
				}
				
				
				EAOutputLogger.log(this.model,this.outputName
           ,string.Format("{0} Finished Corrections for Dependency Matrix'"
                          ,DateTime.Now.ToLongTimeString())
           ,0
          ,LogTypeEnum.log);
				

		}
	}
}
