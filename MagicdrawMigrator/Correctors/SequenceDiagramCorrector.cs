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
	/// Description of CorrectSequenceDiagrams.
	/// </summary>
	public class SequenceDiagramCorrector:MagicDrawCorrector
	{
		public SequenceDiagramCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Starting corrections for Sequence Diagrams"
			          ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
			foreach (var lifeLineID in magicDrawReader.allLifeLines.Keys) 
			{
				//get the lifeline and classifier
				var lifeLine = this.getElementByMDid(lifeLineID);
				var classifier = this.getElementByMDid(magicDrawReader.allLifeLines[lifeLineID]);
				if (lifeLine != null && classifier != null)
				{
					lifeLine.classifier = classifier;
					lifeLine.save();
					EAOutputLogger.log(this.model,this.outputName
					,string.Format("{0} Setting classifier {1} on lifeline in package {2}"
					          	,DateTime.Now.ToLongTimeString()
					         	, classifier.name
					        	, lifeLine.owningPackage.name)
					,lifeLine.id
					,LogTypeEnum.log);
				}
				//TODO: Messages, nested fragments, fragment info
			}
			EAOutputLogger.log(this.model,this.outputName
			,string.Format("{0} Finished corrections for Sequence Diagrams"
			          ,DateTime.Now.ToLongTimeString())
			,0
			,LogTypeEnum.log);
		}
		
	}
}
