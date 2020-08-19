using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        private string programUrl = "https://download-hr.utorrent.com/track/stable/endpoint/utorrent/os/windows";
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
                System.Diagnostics.Process.Start(msiFilePath);
                Application.Exit();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

        }
    }
}
