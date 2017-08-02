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
			
			// codeListAgencyIdentifier -> codeListAgencyID
			this.model.executeSQL(@"update t_objectproperties 
									set [Property] = 'codeListAgencyID'
									where [Property] = 'codeListAgencyIdentifier'");
			
			this.model.executeSQL(@"update t_attributetag
									set [Property] = 'codeListAgencyID'
									where [Property] = 'codeListAgencyIdentifier'");
			
			this.model.executeSQL(@"update t_connectortag
									set [Property] = 'codeListAgencyID'
									where [Property] = 'codeListAgencyIdentifier'");
			
			
			// codeListIdentifier -> codeListID
			this.model.executeSQL(@"update t_objectproperties 
									set [Property] = 'codeListID'
									where [Property] = 'codeListIdentifier'");
			
			this.model.executeSQL(@"update t_attributetag
									set [Property] = 'codeListID'
									where [Property] = 'codeListIdentifier'");
			
			this.model.executeSQL(@"update t_connectortag
									set [Property] = 'codeListID'
									where [Property] = 'codeListIdentifier'");
			

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
			var stereotypes = new ArrayList();
			
			//UPCC3 - Model Management Abstract Syntax
			profile = "UPCC3 - Model Management Abstract Syntax";
			stereotypes.Add("UPCCLibrary");
			stereotypes.Add("DOCLibrary");
			stereotypes.Add("bLibrary");
			stereotypes.Add("BIELibrary");
			stereotypes.Add("BDTLibrary");
			stereotypes.Add("CCLibrary");
			stereotypes.Add("CDTLibrary");
			stereotypes.Add("ENUMLibrary");
			stereotypes.Add("PRIMLibrary");
			stereotypes.Add("UsageRule");
			stereotypes.Add("basedOn");
			stereotypes.Add("equivalentTo");			
			
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
			
			//UPCC3 - DOCLibrary Abstract Syntax
			profile = "UPCC3 - DOCLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("MA");
			stereotypes.Add("ASMA");
			
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
			
			
			//UPCC3 - BIELibrary Abstract Syntax
			profile = "UPCC3 - BIELibrary Abstract Syntax";
			stereotypes.Clear();
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
			
			
			//UPCC3 - BDTLibrary Abstract Syntax
			profile = "UPCC3 - BDTLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("BDT");
			stereotypes.Add("CON");
			stereotypes.Add("SCBVD");
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
			
			//UPCC3 - CCLibrary Abstract Syntax
			profile = "UPCC3 - CCLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("ACC");
			stereotypes.Add("BCC");
			stereotypes.Add("ASCC");
			
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
			
			
			//UPCC3 - CDTLibrary Abstract Syntax
			profile = "UPCC3 - CDTLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("CDT");
			stereotypes.Add("CON");
			stereotypes.Add("SUP");
			stereotypes.Add("CDTProperty");
			
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
			
			
			//UPCC3 - ENUMLibrary Abstract Syntax
			profile = "UPCC3 - ENUMLibrary Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("IDSCHEME");
			stereotypes.Add("ValueDomain");
			stereotypes.Add("ENUM");
			stereotypes.Add("CodelistEntry");
			
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
			
			
			//UPCC3 - PRIMLibrary Abstract Syntax
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
			
			
			
			//UPCC3 - BusinessContext Abstract Syntax
			profile = "UPCC3 - BusinessContext Abstract Syntax";
			stereotypes.Clear();
			stereotypes.Add("BusinessContext");
			stereotypes.Add("BusinessProcessContextValue");
			stereotypes.Add("BusinessProcessRoleContextValue");
			stereotypes.Add("ClassificationScheme");
			stereotypes.Add("GeopoliticalContextValue");
			stereotypes.Add("IndustryClassificationContextValue");
			stereotypes.Add("OfficialConstraintsContextValue");
			stereotypes.Add("ProductClassificationContextValue");
			stereotypes.Add("SupportingRoleContextValue");
			stereotypes.Add("SystemCapabilitiesContextValue");
			stereotypes.Add("ContextValue");
		
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
