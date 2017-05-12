using System.Collections.Generic;
using System.Linq;
using System;
using System.Windows.Forms;

namespace MagicdrawMigrator
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			var MDMigratorController = new MDMigratorController();
			Application.Run(new MDMigratorForm(MDMigratorController));
		}
		
	}
}
