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
	///Types of actions (always callbehaviour), Forks and Joins, action pins and States on objects. 
	///(we might have to find another solution for the state on objects issue)
	/// </summary>
	public class SetStatesOnObjects:MagicDrawCorrector
	{
		public SetStatesOnObjects(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
			//Log start
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting changing type from CentralBufferNode to Object'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			//Look for all the CentralBufferNodes under an Activity and change the type to Object
			this.model.executeSQL(@"update t_object
									set [Object_Type] = 'Object'
									where Object_ID in
									(select o1.[Object_ID]
									from t_object o1
									inner join t_object o2
									on o1.[ParentID] = o2.[Object_ID]
									where o2.[Object_Type] = 'Activity'
									and o1.[Object_Type] = 'CentralBufferNode')");
			
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished changing type from CentralBufferNode to Object'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting changing the status of the Objects'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			//Loop each object and set the state to the corresponding state in the source file
			foreach (var mdObject in magicDrawReader.allObjects) 
			{
				this.model.executeSQL(@"update t_object
										set [StateFlags] = '" + mdObject.Value + @"'
										where Object_ID in
										(select o.[Object_ID]
										from t_object o
										inner join [t_objectproperties] op
										on o.[Object_ID] = op.[Object_ID]
										where op.[Property] = 'md_guid'
										and op.[value] = '" + mdObject.Key + "')");
				
				EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Set state for object '{1}' to '{2}'"
                  	,DateTime.Now.ToLongTimeString()
                  	,mdObject.Key
                  	,mdObject.Value)
                    	
       			,0
      			,LogTypeEnum.log);
				
			}
			
			EAOutputLogger.log(this.model,this.outputName
                   	,string.Format("{0} Finished set states on objects'"
                  	,DateTime.Now.ToLongTimeString())
       			,0
      			,LogTypeEnum.log);
		}
	}
}
