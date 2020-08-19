using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Localization
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
            Application.Run(new Form1());
            saveVersion();

            var result = Updater.check();
            result = 2;
            switch (result)
            {
                case 1:
                    Application.Run(new FUpdater());
                    break;
                case 2:
                    Application.Run(new Form1());
                    break;
            }
        }
        private static void saveVersion()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
                string productVersion = fileVersionInfo.ProductVersion;
                File.WriteAllText(Updater.getVersionFilePath(), productVersion);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
