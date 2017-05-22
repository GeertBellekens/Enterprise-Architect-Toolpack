using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Diagnostics;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Sets the structure of the model according to the original Magicdraw Model
	/// </summary>
	public class SetStructureCorrector:MagicDrawCorrector
	{
		public SetStructureCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
			//throw new NotImplementedException();
			//Debug.WriteLine(model.ToString() + " - " + mdPackage.ToString() );
			
			
			var package = (TSF_EA.Package)mdPackage.nestedPackages.FirstOrDefault();
			if (package != null)
			{
				foreach (TSF_EA.Package dataPackage in mdPackage.nestedPackages)
				{
					foreach (TSF_EA.Package workPackage in dataPackage.nestedPackages)
					{
						//checken of er al een package met die naam bestaat
						
						//als er zo een package bestaat -> inhoud van deze package naar die package verplaatsen
						
						//als die package nog niet bestaat, gehele package kopiëren
						workPackage.owningPackage = package;
						
					}
				}
			}
			
		
			
				
				
			
			
		}

		#endregion
	}
}
