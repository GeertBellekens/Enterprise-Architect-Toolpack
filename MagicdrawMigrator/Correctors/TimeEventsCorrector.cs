using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of TimeEventsCorrector.
	/// </summary>
	public class TimeEventsCorrector:MagicDrawCorrector
	{
		public TimeEventsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		
		public override void correct()
		{
				EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Starting corrections for the Time Events"
	                          ,DateTime.Now.ToLongTimeString())
	           ,0
	          ,LogTypeEnum.log);

			//First get all the Time Events
			foreach (var mdTimeEvent in magicDrawReader.allTimeEvents)
			{	
				TSF_EA.Action TimeEvent = getElementByMDid(mdTimeEvent.Key) as TSF_EA.Action;
				
				if (TimeEvent != null)
				{
					
					//change type
					TimeEvent.kind = TSF_EA.ActionKind.AcceptEventTimer;
					
					//set name = value
					TimeEvent.name = mdTimeEvent.Value.value;
					
					TimeEvent.save();
					
					
					//let the user know
							EAOutputLogger.log(this.model,this.outputName
			                   ,string.Format("{0} Corrected time event '{1}'"
			                                  ,DateTime.Now.ToLongTimeString()
			                                 ,TimeEvent.name)
							  ,TimeEvent.id
			                  ,LogTypeEnum.log);
				}
			}
			
			
			
			
			//Finished
				EAOutputLogger.log(this.model,this.outputName
           ,string.Format("{0} Finished corrections for the Time Events"
                          ,DateTime.Now.ToLongTimeString())
           ,0
          ,LogTypeEnum.log);

		}
	}
}
