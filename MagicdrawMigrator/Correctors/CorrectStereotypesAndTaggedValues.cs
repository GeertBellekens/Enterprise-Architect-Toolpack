using System.Collections;
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
	/// Description of CorrectStereotypesAndTaggedValues.
	/// </summary>
	public class CorrectStereotypesAndTaggedValues:MagicDrawCorrector
	{
		public CorrectStereotypesAndTaggedValues(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
		}
		
		public override void correct()
		{
			
			//Log start
				EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting correct stereotypes and tagged values'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
			// uniqueIdentifier -> uniqueID
			this.model.executeSQL(@"update t_objectproperties 
									set [Property] = 'uniqueID'
									where [Property] = 'uniqueIdentifier'");
			
			this.model.executeSQL(@"update t_attributetag
									set [Property] = 'uniqueID'
									where [Property] = 'uniqueIdentifier'");
			
			this.model.executeSQL(@"update t_connectortag
									set [Property] = 'uniqueID'
									where [Property] = 'uniqueIdentifier'");
			
			// versionIdentifier -> versionID
			this.model.executeSQL(@"update t_objectproperties 
									set [Property] = 'versionID'
									where [Property] = 'versionIdentifier'");
			
			this.model.executeSQL(@"update t_attributetag
									set [Property] = 'versionID'
									where [Property] = 'versionIdentifier'");
			
			this.model.executeSQL(@"update t_connectortag
									set [Property] = 'versionID'
									where [Property] = 'versionIdentifier'");
												
			// businessTerm -> businessTermName
			this.model.executeSQL(@"update t_objectproperties 
									set [Property] = 'businessTermName'
									where [Property] = 'businessTerm'");
			
			this.model.executeSQL(@"update t_attributetag
									set [Property] = 'businessTermName'
									where [Property] = 'businessTerm'");
			
			this.model.executeSQL(@"update t_connectortag
									set [Property] = 'businessTermName'
									where [Property] = 'businessTerm'");
			
			
			// synchronize stereotypes
			synchronizeStereotypes();
			
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished correct stereotypes and tagged values'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
		public void synchronizeStereotypes()
		{
			string profile;
			ArrayList stereotypes = new ArrayList();
			
			profile = "UPCC3 - BIELibrary Abstract Syntax";
			stereotypes.Add("ABIE");
			stereotypes.Add("BBIE");
			stereotypes.Add("ASBIE");
			
			foreach (var stereotype in stereotypes)
			{
				model.wrappedModel.CustomCommand("Repository", "SynchProfile", "Profile=" + profile + ";Stereotype="+ stereotype + ";");
			}
			EAOutputLogger.log(this.model,this.outputName
				                   	,string.Format("{0} Finished profile '{1}'"
                                  	,DateTime.Now.ToLongTimeString()
                                	,profile)
                   ,0
                  ,LogTypeEnum.log);
			
			profile = "UPCC3 - BDTLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("BDT");
			stereotypes.Add("CON");
			stereotypes.Add("SUP");
			foreach (var stereotype in stereotypes)
			{
				model.wrappedModel.CustomCommand("Repository", "SynchProfile", "Profile=" + profile + ";Stereotype="+ stereotype + ";");
			}
			EAOutputLogger.log(this.model,this.outputName
				                   	,string.Format("{0} Finished profile '{1}'"
                                  	,DateTime.Now.ToLongTimeString()
                                	,profile)
                   ,0
                  ,LogTypeEnum.log);
			
			profile = "UPCC3 - Model Management Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("BDTLibrary");
			stereotypes.Add("BIELibrary");
			stereotypes.Add("DOCLibrary");
		
			foreach (var stereotype in stereotypes)
			{
				model.wrappedModel.CustomCommand("Repository", "SynchProfile", "Profile=" + profile + ";Stereotype="+ stereotype + ";");
			}
			EAOutputLogger.log(this.model,this.outputName
				                   	,string.Format("{0} Finished profile '{1}'"
                                  	,DateTime.Now.ToLongTimeString()
                                	,profile)
                   ,0
                  ,LogTypeEnum.log);
			
			
			profile = "UPCC3 - PRIMLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("PRIM");
			stereotypes.Add("ValueDomain");
		
			foreach (var stereotype in stereotypes)
			{
				model.wrappedModel.CustomCommand("Repository", "SynchProfile", "Profile=" + profile + ";Stereotype="+ stereotype + ";");
			}
			EAOutputLogger.log(this.model,this.outputName
				                   	,string.Format("{0} Finished profile '{1}'"
                                  	,DateTime.Now.ToLongTimeString()
                                	,profile)
                   ,0
                  ,LogTypeEnum.log);
		}
	}
}
