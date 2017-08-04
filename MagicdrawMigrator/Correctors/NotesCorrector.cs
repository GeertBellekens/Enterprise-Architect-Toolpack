using System.Collections.Generic;
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
	/// Description of NotesCorrector.
	/// </summary>
	public class NotesCorrector:MagicDrawCorrector
	{
		string packageTreeIDString;
		public NotesCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage):base(magicDrawReader,model,mdPackage)
		{
			packageTreeIDString = mdPackage.getPackageTreeIDString();
		}
		
		
		public override void correct()
		{
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Starting fix notes'"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                   ,LogTypeEnum.log);
			
			// get al the diagrams
			foreach (var mdDiagramKeyValue in magicDrawReader.allDiagrams) 
			{
				var mdDiagram = mdDiagramKeyValue.Value;
				var ownerID = magicDrawReader.getDiagramOnwerID(mdDiagramKeyValue.Key);
				var parentElement = getElementByMDid(ownerID);
				
				
				
				foreach (var mdDiagramNote in mdDiagram.diagramNotes)
				{
					if (parentElement != null)
					{
						TSF_EA.NoteComment newNote = this.model.factory.createNewElement<TSF_EA.NoteComment>(parentElement, string.Empty);
						newNote.ownedComments.FirstOrDefault().body = mdDiagramNote.text;
						newNote.save();
						
						var linkedElement = getElementByMDid(mdDiagramNote.linkedElement);
						if (linkedElement != null)
						{
							TSF_EA.ConnectorWrapper noteLink = newNote.addOwnedElement<TSF_EA.ConnectorWrapper>(string.Empty, "NoteLink");
							noteLink.target = linkedElement;
							noteLink.save();
							EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Create note '{1}' for '{2}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,newNote.ownedComments.FirstOrDefault().body
		                                 ,getElementByMDid(mdDiagramNote.linkedElement).name)
		                   ,newNote.id
		                  ,LogTypeEnum.log);
						}
						else
						{
							EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Create note '{1}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,newNote.ownedComments.FirstOrDefault().body)
		                             
		                   ,newNote.id
		                  ,LogTypeEnum.log);
						}
					}
					else
					{
						EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} No parent for note '{1}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,mdDiagramNote.text)
		                             
		                   ,0
		                  ,LogTypeEnum.log);
						
						
					}

				}
			}
			
			
			//Log finished
			EAOutputLogger.log(this.model,this.outputName
	                   ,string.Format("{0} Finished fix notes"
	                                  ,DateTime.Now.ToLongTimeString())
	                   ,0
	                  ,LogTypeEnum.log);
			
		}
	}
}
