using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Localization
{
    public partial class FUpdater : Form
    {
        private string programUrl = "https://github.com/aysenuryildiz/testrepo/blob/master/localization_setup.msi?raw=true";
        private string exeName = "switchpro.msi";
        private string downloadsPath = "";
        public FUpdater()
        {
            InitializeComponent();

            setText();

            downloadsPath = System.IO.Path.GetTempPath();
            this.Shown += FUpdater_Shown;
        }

        private void setText()
        {
            try
            {
                this.lblProgress.Text = Localization.lblProgress;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void FUpdater_Shown(object sender, EventArgs e)
        {
            try
            {
                downloadFile(programUrl, downloadsPath + exeName);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void downloadFile(string urlAddress, string location)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                    client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                    client.DownloadFileAsync(new Uri(urlAddress), location);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;

                progressBar.Value = int.Parse(Math.Truncate(percentage).ToString());

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                var msiFilePath = downloadsPath + exeName;
                var result = RunInstallMSI(msiFilePath, exeName);
                //System.Diagnostics.Process.Start(msiFilePath);

                Application.Exit();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
        public static bool RunInstallMSI(string path, string fileName)
        {
            try
            {
                Process install  = new Process();
                install.StartInfo.FileName = "msiexec";
                //install.StartInfo.Arguments = string.Format("/i \"{0}\"", path);

                install.StartInfo.Arguments = string.Format(" /qb /i \"{0}\" ALLUSERS=1", path);
                install.Start();
                install.WaitForExit();
                return true;
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
                return false;  //Return False if process ended unsuccessfully
            }
        }
    }
}
