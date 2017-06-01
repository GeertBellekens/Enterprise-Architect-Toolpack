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
	/// Description of DiagramLayoutCorrector.
	/// </summary>
	public class DiagramLayoutCorrector:MagicDrawCorrector
	{
		public DiagramLayoutCorrector(MagicDrawReader magicDrawReader, TSF_EA.Model model, TSF_EA.Package mdPackage ):base(magicDrawReader,model,mdPackage)
		{
		}
		public override void correct()
		{
			//Loop each diagram
			//find the corresponding diagram in eA
			//get the binary-file containing the layout of the diagram
			//for each of the elements on the diagram create the diagramobject with the appropriate link to the elemment and geometry.
		}
	}
}
