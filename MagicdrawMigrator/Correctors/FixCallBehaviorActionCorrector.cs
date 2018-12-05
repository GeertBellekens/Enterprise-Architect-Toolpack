using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of FixCallBehaviorActionCorrector.
	/// </summary>
	public class FixCallBehaviorActionCorrector:MagicDrawCorrector
	{
		public FixCallBehaviorActionCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
			
		}
		#region implemented abstract members of MagicDrawCorrector
		
		public override void correct()
		{
			//Log start
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting fixing the CallBehavior action'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			

			string packageString = mdPackage.packageTreeIDString;
			this.model.executeSQL(@"delete from t_xref
			                      where [XrefID] in
									(select x.[XrefID] from (t_xref x 
									inner join t_object o on o.[ea_guid] = x.[Client])
									where x.name = 'CustomProperties'
									and o.[Classifier] = 0
									and x.[Description] like '@PROP=@NAME=kind@ENDNAME;@TYPE=ActionKind@ENDTYPE;@VALU=CallBehavior@ENDVALU;@PRMT=@ENDPRMT;@ENDPROP;'
									and o.[Package_ID] in ("+ packageString +"))");
			
			//Log Finished
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished fixing the CallBehavior action'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
		#endregion
	}
}
