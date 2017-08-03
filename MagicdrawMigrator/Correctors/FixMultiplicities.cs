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
	/// Description of FixMultiplicities.
	/// </summary>
	public class FixMultiplicities:MagicDrawCorrector
	{
		public FixMultiplicities(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		
		}
			
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting fix multiplicities'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			// get all the associations
			foreach (MDAssociation mdAssociation in magicDrawReader.allAssociations) 
			{
				//find the source class
				var sourceClass = this.getClassByMDid(mdAssociation.source.endClassID);
				//find the target class
				var targetClass = this.getClassByMDid(mdAssociation.target.endClassID);
				
				if (sourceClass != null && targetClass != null)
				{
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Correcting association between '{1}' and '{2}'"
	                                  ,DateTime.Now.ToLongTimeString()
	                                 ,sourceClass.name
	                                 ,targetClass.name)
	                   ,sourceClass.id
	                  ,LogTypeEnum.log);
					
				}
				
			}
			
		
			
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished fix multiplicities"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
		
		
		
		TSF_EA.Class getClassByMDid(string mdID)
		{
			string getClassesSQL = @"select o.Object_ID from (t_object o
									inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
															and tv.Property = 'md_guid'))
									where o.Object_Type = 'Class'
									and tv.Value = '"+mdID+"'";
			return this.model.getElementWrappersByQuery(getClassesSQL).FirstOrDefault() as TSF_EA.Class;
		}
		
	}
}
