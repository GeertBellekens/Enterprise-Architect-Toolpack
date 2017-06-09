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
	///Some parts of the Activity Diagrams are missing such as classifiers from partitions, 
	/// </summary>
	public class AddClassifiersToPartitions:MagicDrawCorrector
	{
		public AddClassifiersToPartitions(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
				//Log start
				EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting add classifiers to partitions'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
				
				foreach(var mdPartition in magicDrawReader.allPartitions)
				{
					//key = partition, value = represents
					//partitionID: '_17_0_2_b9402f1_1366196749027_793557_54194'	
					//representsID: '_17_0_2_3_38a017f_1374238063627_212930_92600'	
					KeyValuePair<string,string> objectIdGuid = this.model.getObjectIdAndGuid(@"select o.[Object_ID], o.[ea_guid] 
																							from t_object o
																			    			inner join [t_objectproperties] op
																			    			on o.[Object_ID] = op.[Object_ID]
																			    			where op.[Property] = 'md_guid'
																			    			and op.[value] = '"+ mdPartition.Value +"'");
					
					
					
					this.model.executeSQL(@"update t_object 
											set [Classifier] = '" + objectIdGuid.Key + @"',
											[Classifier_guid] = '" + objectIdGuid.Value + @"'
											where Object_ID in
											(select o.[Object_ID]
											from t_object o
											inner join [t_objectproperties] op
											on o.[Object_ID] = op.[Object_ID]
											where op.[Property] = 'md_guid'
											and op.[value] = '"+ mdPartition.Key +"')");
					

				//logging	                               
				EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Add the classifier '{1}' - '{2}' for partition '{3}'"
                  	,DateTime.Now.ToLongTimeString()
                  	,objectIdGuid.Key
                  	,objectIdGuid.Value
                  	,mdPartition.Key)
                    	
       			,0
      			,LogTypeEnum.log);
					
					
				}
			
				
				EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Finished add classifiers to partitions'"
                  	,DateTime.Now.ToLongTimeString())
       			,0
      			,LogTypeEnum.log);
				

		}
	}
}
