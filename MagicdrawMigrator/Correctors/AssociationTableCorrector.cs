using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using System.Windows.Forms;
using EAAddinFramework.Utilities;
using TSF_EA =TSF.UmlToolingFramework.Wrappers.EA;
using UML = TSF.UmlToolingFramework.UML;
using Excel = Microsoft.Office.Interop.Excel;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Description of AssociationTableCorrector.
	/// </summary>
	public class AssociationTableCorrector:MagicDrawCorrector
	{
		public AssociationTableCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}

		#region implemented abstract members of MagicDrawCorrector

		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
				                   ,string.Format("{0} Starting Corrections for Association Tables'"
				                                  ,DateTime.Now.ToLongTimeString())
				                   ,0
				                  ,LogTypeEnum.log);
			//loop all association tables
			foreach (var mdID in magicDrawReader.allLinkedAssociationTables.Keys) 
			{
				//find the associated element
				string sqlGetElements = @"select tv.Object_ID from t_objectproperties tv
										where tv.Property = 'md_guid'
										and tv.Value = '"+mdID +"'";
				var associatedElements = this.model.getElementWrappersByQuery(sqlGetElements);
				foreach (var associatedElement in associatedElements) 
				{
					if (associatedElement != null)
					{
						EAOutputLogger.log(this.model,this.outputName
					                   ,string.Format("{0} Adding Association table for '{1}' in package '{2}'"
					                                  ,DateTime.Now.ToLongTimeString()
					                                  ,associatedElement.name
					                                  ,associatedElement.owningPackage.name)
					                   ,associatedElement.id
					                  ,LogTypeEnum.log);
						//convert the html into RTF
						string rtfContent = this.convertHTMLToRTF(magicDrawReader.allLinkedAssociationTables[mdID]);
						//create linked document on the element
						associatedElement.linkedDocument = rtfContent;
					}
				}
				
			}
			EAOutputLogger.log(this.model,this.outputName
				                   ,string.Format("{0} Finished Corrections for Association Tables'"
				                                  ,DateTime.Now.ToLongTimeString())
				                   ,0
				                  ,LogTypeEnum.log);
		}
		/// <summary>
		/// subOptimal way to convert the HTML to RTF
		/// TODO: find a better way that doesn't involve using the clipboard
		/// </summary>
		/// <param name="HTMLstring"></param>
		/// <returns></returns>
		private string convertHTMLToRTF(string HTMLstring)
		{
			RichTextBox rtbTemp= new RichTextBox();
			WebBrowser wb = new WebBrowser();
			wb.Navigate("about:blank");
			
			wb.Document.Write(HTMLstring);
			wb.Document.ExecCommand("SelectAll", false, null);
			wb.Document.ExecCommand("Copy", false, null);
			
			rtbTemp.SelectAll();
			rtbTemp.Paste();
			return rtbTemp.Rtf;

		}

		#endregion
	}
}
