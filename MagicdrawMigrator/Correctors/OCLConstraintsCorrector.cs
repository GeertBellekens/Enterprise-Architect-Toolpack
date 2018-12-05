using System.Collections.Generic;
using System.Linq;
using System;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;


namespace MagicdrawMigrator
{
	/// <summary>
	/// Sets the structure of the model according to the original Magicdraw Model
	/// </summary>
	public class OCLConstraintsCorrector:MagicDrawCorrector
	{
		public OCLConstraintsCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting Corrections for OCL Constraints'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			//loop all classes that have MA as stereotype
			string getMessageAssembliesSQL = "select o.Object_ID from t_object o"
											+ " where o.Stereotype = 'MA'"
											+ " and o.Object_Type = 'Class'"
				+ " and o.Package_ID in (" + mdPackage.packageTreeIDString + ")";
			var messageAssemblies = this.model.getElementWrappersByQuery(getMessageAssembliesSQL);
			foreach (var messageAssembly in messageAssemblies) 
			{
				EAOutputLogger.log(this.model,this.outputName
				                   ,string.Format("{0} Correcting OCL Constraints for '{1}' in package '{2}'"
				                                  ,DateTime.Now.ToLongTimeString()
				                                  ,messageAssembly.name
				                                  ,messageAssembly.owningPackage.name)
				                   ,messageAssembly.id
				                  ,LogTypeEnum.log);
				var mdIDTag = messageAssembly.taggedValues.FirstOrDefault( x => x.name == "md_guid");
				if (mdIDTag != null)
				{
					string mdID = mdIDTag.tagValue.ToString();
					//get the MDConstraints for this element
					var mdConstraints =  magicDrawReader.getContraints(mdID);
					if (mdConstraints.Any())
					{
						//first delete all existing constraints
						foreach (var existingContraint in messageAssembly.constraints) 
						{
							existingContraint.delete();
						}
						//add the new constraints
						foreach (var mdConstraint in mdConstraints) 
						{
							var newConstraint =  this.model.factory.createNewElement<TSF_EA.Constraint>(messageAssembly,mdConstraint.name);
							newConstraint.specification = new TSF_EA.OpaqueExpression(mdConstraint.body,mdConstraint.language);
							newConstraint.save();
						}
					}
					
				}
			}
			EAOutputLogger.log(this.model,this.outputName
           ,string.Format("{0} Finished Corrections for OCL Constraints'"
                          ,DateTime.Now.ToLongTimeString())
           ,0
          ,LogTypeEnum.log);
		}

		#endregion
	}
}
