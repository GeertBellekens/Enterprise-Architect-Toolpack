using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using EAValidator;
using TSF.UmlToolingFramework.Wrappers.EA;

namespace EAValidatorApp
{
    static class Program
    {
        // reference to currently opened EA repository
        internal static EA.Repository eaRepository;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            eaRepository = getOpenedModel();
            if (eaRepository != null)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var controller = new EAValidatorController(new Model(eaRepository), new EAValidatorSettings());
                Application.Run(new frmEAValidator(controller));
            }
        }

        private static EA.Repository getOpenedModel()
        {
            try
            {

                return ((EA.App)Marshal.GetActiveObject("EA.App")).Repository;

            }
            catch (COMException)
            {
                DialogResult result = MessageBox.Show("Could not find running instance of EA.\nStart EA and try again"
                                   , "EA not running", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Retry)
                {
                    return getOpenedModel();
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
