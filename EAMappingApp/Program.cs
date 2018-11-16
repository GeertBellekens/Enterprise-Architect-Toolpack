using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TSF_EA = TSF.UmlToolingFramework.Wrappers.EA;

namespace EAMappingApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var model = new TSF_EA.Model();
            var mappingAddin = new EAMapping.EAMappingAddin();
            mappingAddin.EA_FileOpen(model.wrappedModel);
            var mappingForm = new MappingForm();
            mappingAddin.mappingControl = mappingForm.mappingControlGUI;
            mappingAddin.selectAndLoadNewMappingSource(false);


            Application.Run(mappingForm);
        }
    }
}
