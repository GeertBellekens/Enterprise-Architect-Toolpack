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
	public class CorrectActorDependencies:MagicDrawCorrector
	{
		public CorrectActorDependencies(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		
		public override void correct()
		{
			
		}
	}
}
