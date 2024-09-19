using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backup
{
    public static class XmlDetaylar
    {
        private static string GetFilePath()
        {
            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Directory.GetParent(applicationDirectory).Parent.FullName;
            string parentDirectory2 = Directory.GetParent(parentDirectory).Parent.FullName;

            string settingsFilePath = Path.Combine(parentDirectory2, "BackupService2", "bin", "Debug", "settings.xml");
            return settingsFilePath;
        }
        public static void LoadSettings(out string fileName, out List<string> kaynakSelectedItems, out List<string> hedefSelectedItems) // XML dosyasından ayarları yükler
        {
            string settingsFilePath = GetFilePath();

            kaynakSelectedItems = new List<string>();
            hedefSelectedItems = new List<string>();
            fileName = string.Empty;
            if (File.Exists(settingsFilePath))
            {
                var settings = XElement.Load(settingsFilePath);

                // Kaynaklar ve Hedefler listelerini doldur
                kaynakSelectedItems = new List<string>(settings.Element("Kaynaklar")?.Value.Split(';'));
                hedefSelectedItems = new List<string>(settings.Element("Hedefler")?.Value.Split(';'));
                fileName = settings.Element("FileName")?.Value;
            }
            else
            {
                Logger.WriteToLog("Load için XML dosyası bulunamadı...");
            }
        }
        public static void SaveSettings(List<string> kaynakSelectedItems, List<string> hedefSelectedItems, string fileName, string servisSuresi, string CompressionType) // Ayarları XML dosyasına kaydeder
        {
            string settingsFilePath = GetFilePath();

            var settings = new XElement("Settings",
                new XElement("Kaynaklar", string.Join(";", kaynakSelectedItems)),
                new XElement("Hedefler", string.Join(";", hedefSelectedItems)),
                new XElement("FileName", fileName),
                new XElement("ServisSure", servisSuresi),
                new XElement("SikistirmaTuru", CompressionType)
            );

            // Ayarları XML dosyasına kaydet
            settings.Save(settingsFilePath);
            //MessageBox.Show("Ayarlar başarıyla kaydedildi.");
        }
        public static void SaveAccessTokenToXml(string accessToken)
        {
            
            string settingsFilePath = GetFilePath();

            // Eğer XML dosyası yoksa oluştur
            XElement settings;
            if (File.Exists(settingsFilePath))
            {
                settings = XElement.Load(settingsFilePath);
                var accessTokenElement = settings.Element("AccessToken");

                if (accessTokenElement != null)
                {
                    accessTokenElement.Value = accessToken;
                }
                else
                {
                    settings.Add(new XElement("AccessToken", accessToken));
                }
            }
            else
            {
                settings = new XElement("Settings",
                    new XElement("AccessToken", accessToken)
                );
            }

            settings.Save(settingsFilePath);
            Logger.WriteToLog("AccessToken başarıyla kaydedildi.");
        }

    }
}
