using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EHR_Monitor_Test
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// We aren't doing anything at startup except launching the application window.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}
