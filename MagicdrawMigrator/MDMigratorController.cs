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
		public string outputName {get;private set;}
		public MDMigratorController()
		{
			correctors = new List<MagicDrawCorrector>();
			model = new TSF_EA.Model();
			this.outputName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
		}
		
		public void startCorrections(string mdzipPath, List<MagicDrawCorrector> correctionsToStart)
		{
			//clear the log
			EAOutputLogger.clearLog(this.model, this.outputName);
			//tell the user what is happening
			EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Starting corrections for Magicdraw import"
	                          ,DateTime.Now.ToLongTimeString())
	           ,0
	          ,LogTypeEnum.log);
			//start correcting
			foreach (var corrector in correctionsToStart) 
			{
				corrector.correct();
			}
			//tell the user what is happening
			EAOutputLogger.log(this.model,this.outputName
	           ,string.Format("{0} Finished corrections for Magicdraw import"
	                          ,DateTime.Now.ToLongTimeString())
	           ,0
	          ,LogTypeEnum.log);
		}
		public List<MagicDrawCorrector> createCorrectors(string mdzipPath, TSF_EA.Package mdPackage)
		{
			//var mdPackage = getMagicDrawPackage();
			var magicDrawReader = new MagicDrawReader(mdzipPath,this.model);
			if (mdPackage !=null)
			{	
				correctors.Add(new SetStructureCorrector(magicDrawReader,model, mdPackage));	
				correctors.Add(new NotesCorrector(magicDrawReader, model, mdPackage));
				correctors.Add(new RelationsCorrector(magicDrawReader, model, mdPackage));
                correctors.Add(new AssociationCorrector(magicDrawReader, model, mdPackage));
                correctors.Add(new CrossMDzipAttributeCorrector(magicDrawReader, model, mdPackage));
				correctors.Add(new OCLConstraintsCorrector(magicDrawReader,model,mdPackage));
				correctors.Add(new AssociationTableCorrector(magicDrawReader,model,mdPackage));
				correctors.Add(new FixCallBehaviorActionCorrector(magicDrawReader,model,mdPackage));
				correctors.Add(new ConvertPropertiesToAttributes(magicDrawReader,model,mdPackage));
				correctors.Add(new SetStatesOnObjects(magicDrawReader,model, mdPackage));
                correctors.Add(new AttributeSequenceCorrector(magicDrawReader, model, mdPackage));
				correctors.Add(new AddClassifiersToPartitions(magicDrawReader,model, mdPackage));
				correctors.Add(new SequenceDiagramCorrector(magicDrawReader,model, mdPackage));
				correctors.Add(new DiagramLayoutCorrector(magicDrawReader,model, mdPackage));
				correctors.Add(new CorrectStereotypesAndTaggedValues(magicDrawReader,model, mdPackage));
				correctors.Add(new MigrateDependencyMatrix(magicDrawReader,model, mdPackage));				
				correctors.Add(new AddGuardConditions(magicDrawReader, model, mdPackage));
				correctors.Add(new TimeEventsCorrector(magicDrawReader,model,mdPackage));
			}
            return correctors;
		}
		public TSF_EA.Package getMagicDrawPackage()
		{
			return this.model.getUserSelectedPackage() as TSF_EA.Package;
		}

	}
}
