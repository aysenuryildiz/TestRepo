using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Localization
{
    public static class Updater
    {
        private static string programUrl = "https://raw.githubusercontent.com/aysenuryildiz/testrepo/master/newVer";
        public static int check()
        {
            try
            {
                string version = getVersion();
                var result = checkVersion(version); //surumu kontrol et
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }



        private static int checkVersion(string version)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    var localVersion = version.Split('.');

                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    int localMajor = Convert.ToInt32(localVersion[0]);
                    int localMinor = Convert.ToInt32(localVersion[1]);
                    int localRelease = Convert.ToInt32(localVersion[2]);
                    int localBuild = Convert.ToInt32(localVersion[3]);
                    int newMajor;
                    int newMinor;
                    int newRelease;
                    int newBuild;

                    string strNewVersion = client.DownloadString(programUrl); //remote txt içeriğine eriş
                    strNewVersion = strNewVersion.Replace("\n", String.Empty);
                    var newVersion = strNewVersion.Split('.');
                    newMajor = Convert.ToInt32(newVersion[0]);
                    newMinor = Convert.ToInt32(newVersion[1]);
                    var released = newVersion[2].Split(';');
                    newRelease = Convert.ToInt32(released[0]);
                    newBuild = Convert.ToInt32(released[1]);
                    var isUpdateRequired = Convert.ToInt32(newBuild);




                    var majorAyniMi = newMajor.Equals(localMajor);
                    var minorAyniMi = newMinor.Equals(localMinor);
                    var releaseAyniMi = newRelease.Equals(localRelease);
                    var buildAyniMi = newBuild.Equals(localBuild);

                    if (majorAyniMi)
                    {
                        //major aynı büyük değişiklik yok 
                        if (newBuild == 1 && localBuild == 0)
                        {
                            return 1;
                        }
                        else
                        {
                            if (!minorAyniMi)
                            {
                                return 1;
                            }

                            else if (!releaseAyniMi)
                            {
                                return 1;
                            }

                        }

                    }

                    if (!majorAyniMi)
                    {
                        return 1;
                    }

                }

                return 2;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
        private static string getVersion()
        {
            try
            {
                string version = "";
                var versionFilePath = getVersionFilePath();
                using (System.IO.StreamReader file = new System.IO.StreamReader(versionFilePath))
                    version = file.ReadToEnd();

                return version;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public static string getVersionFilePath()
        {
            try
            {
                var versionFilePath = Application.StartupPath + "\\version.txt";
                return versionFilePath;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
