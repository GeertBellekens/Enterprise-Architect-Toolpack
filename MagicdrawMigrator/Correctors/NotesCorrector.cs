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
			packageTreeIDString = mdPackage.packageTreeIDString;
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
						//create a new note
						TSF_EA.NoteComment newNote = this.model.factory.createNewElement<TSF_EA.NoteComment>(parentElement, string.Empty);
                        //add the comments to the note
                        newNote.ownedComments.FirstOrDefault().body = model.convertToEANotes(mdDiagramNote.text, "HTML");
						//save the note
						newNote.save();
						//add the tagged value md_guid
						newNote.addTaggedValue("md_guid", mdDiagramNote.note_Id);
						
						//links
						var linkedElement = getElementByMDid(mdDiagramNote.linkedElement);
						if (linkedElement != null)
						{
							TSF_EA.ConnectorWrapper noteLink = newNote.addOwnedElement<TSF_EA.ConnectorWrapper>(string.Empty, "NoteLink");
							noteLink.target = linkedElement;
							noteLink.save();
						}
						
							EAOutputLogger.log(this.model,this.outputName
		                   ,string.Format("{0} Create new note '{1}'"
		                                  ,DateTime.Now.ToLongTimeString()
		                                  ,newNote.ownedComments.FirstOrDefault().body)
		                                 
							             ,parentElement.id
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
