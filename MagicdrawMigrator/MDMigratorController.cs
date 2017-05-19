using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using System.Windows.Forms;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MDMigratorController.
	/// </summary>
	public class MDMigratorController
	{
		List<MagicDrawCorrector> correctors {get;set;}
		TSF_EA.Model model {get;set;}
		public MDMigratorController()
		{
			correctors = new List<MagicDrawCorrector>();
			model = new TSF_EA.Model();
		}
		
		public void startCorrections(string mdzipPath)
		{
			//create correctors
			createCorrectors(mdzipPath);
			//clear the log
			EAOutputLogger.clearLog(this.model, correctors[0].outputName);
			//start correcting
			foreach (var corrector in this.correctors) 
			{
				corrector.correct();
			}
		}
		public void createCorrectors(string mdzipPath)
		{
			var mdPackage = getMagicDrawPackage();
			var magicDrawReader = new MagicDrawReader(mdzipPath);
			if (mdPackage !=null)
			{
				correctors.Add(new SetStructureCorrector(magicDrawReader,model,mdPackage));
				correctors.Add(new OCLConstraintsCorrector(magicDrawReader,model,mdPackage));
			}
		}
		public TSF_EA.Package getMagicDrawPackage()
		{
			MessageBox.Show("Please select the package containing the imported Magicdraw Model","Select Magicdraw Import Package");
			return this.model.getUserSelectedPackage() as TSF_EA.Package;
		}

	}
}
