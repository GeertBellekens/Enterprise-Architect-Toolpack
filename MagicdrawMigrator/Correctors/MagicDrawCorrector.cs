using System.Collections.Generic;
using System.Linq;
using System;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of MagicDrawCorrector.
	/// </summary>
	public abstract class MagicDrawCorrector
	{
		protected TSF_EA.Model model {get;set;}
		protected MagicDrawReader magicDrawReader {get;set;}
		protected TSF_EA.Package mdPackage{get;set;}
		public string outputName {get;private set;}

		protected MagicDrawCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage)
		{
			this.magicDrawReader = magicDrawReader;
			this.model = model;
			this.mdPackage = mdPackage;
			this.outputName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
		}
		public abstract void correct();
		
		public TSF_EA.ElementWrapper getElementByMDid(string mdID)
		{
			string getClassesSQL = @"select o.Object_ID from (t_object o
									inner join t_objectproperties tv on (tv.Object_ID = o.Object_ID
															and tv.Property = 'md_guid'))
									where tv.Value = '"+mdID+"'";
			return this.model.getElementWrappersByQuery(getClassesSQL).FirstOrDefault();
		}
		
		
	}
}
