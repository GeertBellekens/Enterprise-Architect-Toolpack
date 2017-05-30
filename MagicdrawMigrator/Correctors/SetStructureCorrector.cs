using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
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
			//Log start
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting Corrections the Package Structure'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			// Data package aanmaken dat zal gebruikt worden om alles onder te plaatsen
			TSF_EA.Package newData = this.model.factory.createNewElement<TSF_EA.Package>(mdPackage,"Data");
			newData.save();
			var importPackage = (TSF_EA.Package)mdPackage.nestedPackages.FirstOrDefault();
			if (importPackage != null)
			{
				foreach (TSF_EA.Package dataPackage in mdPackage.nestedPackages) // Doorloopt de Magicdraw Import
				{	
					//tell the user which package we are dealing  with
					EAOutputLogger.log(this.model,this.outputName
					                   	,string.Format("{0} Correcting package '{1}' with GUID '{2}'"
	                                  	,DateTime.Now.ToLongTimeString()
	                                 	,dataPackage.name
	                                	,dataPackage.guid)
	                   ,0
	                  ,LogTypeEnum.log);
					if (dataPackage.isEmpty)
					{
						dataPackage.delete(); //lege packages deleten
					}
					else
					{
						foreach (TSF_EA.Package workPackage in dataPackage.nestedPackages)
						{
							
							if (workPackage.isEmpty) 
							{
								workPackage.delete(); //lege packages deleten
							}
							else 
							{
								if(hasPackage(workPackage,newData)) 
								{
									
								  // inhoud moven naar desbetreffende package
								  moveContent(workPackage,getPackage(workPackage,newData));
								}
								else 
								{
									workPackage.owningPackage = newData;
									workPackage.save();
								}
								
							}
						
						}
						foreach (TSF_EA.Diagram diagram in dataPackage.ownedDiagrams)
						{
							diagram.owningPackage = newData;
							diagram.save();
						}
						
					}
					
					//Delete the unneeded packages
					if(dataPackage.guid != newData.guid)
					{
						dataPackage.delete();
					}
				}

				mdPackage.refresh();
				//Log Finished
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished Corrections the Package Structure'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			}
		}
		
		public void moveContent(TSF_EA.Package subPackage, TSF_EA.Package newData)
		{
			if (newData.name == "Belgian Energy Distribution Market") // NULLPOINTER!!!
			{
					Debug.WriteLine("here");
			}
			
			foreach (TSF_EA.Package p in subPackage.nestedPackages) // de packages onder 'Data' package
			{				
				p.owningPackage = newData;
				p.save();
			}
			foreach (TSF_EA.Diagram d in subPackage.ownedDiagrams) // de diagrams onder 'Data' package
			{
				d.owningPackage = newData;
				d.save();	
			}
			
			foreach (TSF_EA.Element e in subPackage.ownedElements.Where(x => !(x is UML.Classes.Kernel.Package))) // de elementen onder 'Data' package
			{		
				e.owningPackage = newData;
				e.save();
			}
		}
		
		
		
		public bool hasDiagram(string diagramName, TSF_EA.Package importPackage)
		{
			bool contains = false;
			foreach(var res in importPackage.ownedDiagrams)
			{
				if(res.name == diagramName)
				{
					contains = true;
				}
				
			}
			return contains;
		}
		
		public bool hasElement(string elementName, TSF_EA.Package importPackage)
		{
			bool contains = false;
			foreach(var res in importPackage.ownedElements)
			{
				if(res.name == elementName)
				{
					contains = true;
				}
				
			}
			return contains;
		}
		
		public bool hasPackage(TSF_EA.Package package, TSF_EA.Package importPackage) // Checks if Data has already this package, with another ID
		{
			bool contains = false;
			
			foreach(TSF_EA.Package res in importPackage.nestedPackages)
			{
				if(res.name == package.name && res.guid != package.guid )
				{
					contains = true;
				}				
			}
			return contains;		
		}
		
		public TSF_EA.Package getPackage(TSF_EA.Package package, TSF_EA.Package inDataPackage)
		{
			TSF_EA.Package target = null;
			foreach(TSF_EA.Package res in inDataPackage.nestedPackages)
			{
				if(res.name == package.name && res.guid != package.guid)
				{
					target = res;
				}	
			}
			return target;			
		}

		#endregion
	}
}
