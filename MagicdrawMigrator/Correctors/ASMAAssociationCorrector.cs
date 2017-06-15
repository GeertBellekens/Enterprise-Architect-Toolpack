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
	/// Description of ASMAAssociationCorrector.
	/// </summary>
	public class ASMAAssociationCorrector:MagicDrawCorrector
	{
		public ASMAAssociationCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}


		#region implemented abstract members of MagicDrawCorrector
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting corrections for ASMA associations'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			foreach (MDAssociation mdAssociation in magicDrawReader.allASMAAssociations) 
			{
				//find the source class
				var sourceClass = this.getClassByMDid(mdAssociation.source.endClassID);
				//find the target class
				var targetClass = this.getClassByMDid(mdAssociation.target.endClassID);
				if (sourceClass != null && targetClass != null)
				{
					EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Creating ASMA association between '{1}' and '{2}'"
	                                  ,DateTime.Now.ToLongTimeString()
	                                 ,sourceClass.name
	                                 ,targetClass.name)
	                   ,sourceClass.id
	                  ,LogTypeEnum.log);
					//create the actual association
					TSF_EA.Association newAsmaAssociation = this.model.factory.createNewElement<TSF_EA.Association>(sourceClass,string.Empty);
					//set source end properties
					setEndProperties(newAsmaAssociation.sourceEnd, mdAssociation.source);
					//set target end properties
					setEndProperties(newAsmaAssociation.targetEnd, mdAssociation.target);
					//set target class
					newAsmaAssociation.target = targetClass;
					//set the stereotype
					newAsmaAssociation.addStereotype(this.model.factory.createStereotype(newAsmaAssociation,mdAssociation.stereotype));
					//save the new association
					newAsmaAssociation.save();
				}
				else
				{
						EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Could not create ASMA association classes with ID's '{1}' and '{2}'"
	                                  ,DateTime.Now.ToLongTimeString()
	                                 ,mdAssociation.source.endClassID
	                                 ,mdAssociation.target.endClassID)
	                   ,0
	                  ,LogTypeEnum.error);
				}

			}
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished corrections for ASMA associations'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
		TSF_EA.Class getClassByMDid(string mdID)
		{
			string getClassesSQL = @"select o.Object_ID from (t_object o
									inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
															and tv.Property = 'md_guid'))
									where o.Object_Type = 'Class'
									and tv.Value = '"+mdID+"'";
			return this.model.getElementWrappersByQuery(getClassesSQL).FirstOrDefault() as TSF_EA.Class;
		}
		void setEndProperties (TSF_EA.AssociationEnd eaEnd, MDAssociationEnd mdEnd)
		{
			//set the aggregationKind
			if (mdEnd.aggregationKind == "shared")
			{
				eaEnd.aggregation = UML.Classes.Kernel.AggregationKind.shared;
			}
			else
			{
				eaEnd.aggregation = UML.Classes.Kernel.AggregationKind.none;
			}
			//set the name
			eaEnd.name = mdEnd.name;
			//set the multiplicity
			if (! string.IsNullOrEmpty(mdEnd.lowerBound)
			    && !string.IsNullOrEmpty(mdEnd.upperBound))
			{
				eaEnd.multiplicity = new TSF_EA.Multiplicity(mdEnd.lowerBound,mdEnd.upperBound);
			}
		}
		#endregion
	}
}
