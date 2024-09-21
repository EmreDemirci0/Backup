using Backup.Class;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backup
{
    public static class XmlDetaylar
    {
        private static string GetFilePath()
        {
            //string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //string settingsFilePath = Path.Combine(applicationDirectory, "settings.xml");
            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string parentDirectory = Directory.GetParent(applicationDirectory).Parent.FullName;
            string parentDirectory2 = Directory.GetParent(parentDirectory).Parent.FullName;

            string settingsFilePath = Path.Combine(parentDirectory2, "Backup", "bin", "Debug", "settings.xml");

            return settingsFilePath;
        }
        public static void LoadSettings(out string fileName, out List<string> kaynakSelectedItems, out List<string> hedefSelectedItems, out string servisSuresi, out bool isSaveDrive, out string DosyaID, out CompressionType compressionType, out string mail) // XML dosyasından ayarları yükler
        {
            string settingsFilePath = GetFilePath();

            fileName = string.Empty;
            kaynakSelectedItems = new List<string>();
            hedefSelectedItems = new List<string>();
            isSaveDrive = false;
            compressionType = new CompressionType();
            DosyaID = "";
            mail = "";
            servisSuresi = "";

            if (File.Exists(settingsFilePath))
            {
                var settings = XElement.Load(settingsFilePath);

                // Kaynaklar ve Hedefler listelerini doldur
                kaynakSelectedItems = new List<string>(settings.Element("Kaynaklar")?.Value.Split(';'));
                hedefSelectedItems = new List<string>(settings.Element("Hedefler")?.Value.Split(';'));
                fileName = settings.Element("FileName")?.Value;
                mail = settings.Element("mail")?.Value;
                DosyaID = settings.Element("DosyaID")?.Value;
                servisSuresi = settings.Element("ServisSure")?.Value;

                string sikistirmaTuru = settings.Element("SikistirmaTuru")?.Value;
                if (sikistirmaTuru == "rar")
                {
                    compressionType = CompressionType.rar;
                }
                else if (sikistirmaTuru == "zip")
                {
                    compressionType = CompressionType.zip;
                }
                else if (sikistirmaTuru == "folder")
                {
                    compressionType = CompressionType.folder;
                }

                if (settings.Element("DriveKaydedilecekMi")?.Value == "true")
                    isSaveDrive = true;
                else
                    isSaveDrive = false;

            }
            else
            {
                Logger.WriteToLog("Load için XML dosyası bulunamadı...");
            }
        }
        public static void SaveSettings(string fileName, List<string> kaynakSelectedItems, List<string> hedefSelectedItems, string servisSuresi, bool isSaveDrive, string DosyaID, CompressionType compressionType, string mail) // Ayarları XML dosyasına kaydeder
        {
            string settingsFilePath = GetFilePath();

            var settings = new XElement("Settings",
                new XElement("FileName", fileName),
                new XElement("Kaynaklar", string.Join(";", kaynakSelectedItems)),
                new XElement("Hedefler", string.Join(";", hedefSelectedItems)),
                new XElement("ServisSure", servisSuresi),
                new XElement("DriveKaydedilecekMi", isSaveDrive),
                new XElement("DosyaID", DosyaID),
                new XElement("SikistirmaTuru", compressionType.ToString()),
                new XElement("mail", mail)
            );

            // Ayarları XML dosyasına kaydet
            settings.Save(settingsFilePath);
            //MessageBox.Show("Ayarlar başarıyla kaydedildi.");
        }

        public static string GetMail()
        {
            string settingsFilePath = GetFilePath(); // XML dosyanızın yolunu buradan alıyorsunuz.

            try
            {
                // XML dosyasını yükle
                XDocument xmlDoc = XDocument.Load(settingsFilePath);

                // 'mail' elementini çek
                var mailElement = xmlDoc.Root.Element("mail");
                if (mailElement != null)
                {
                    string email = mailElement.Value;

                    // Mail formatını kontrol et
                    if (IsValidEmail(email))
                    {
                        return email; // Mail formatı uygunsa döndür

                    }
                    else
                    {
                        Logger.WriteToLog("Hata: E-posta adresi geçersiz bir formata sahip.");
                        return string.Empty;
                    }
                }
                else
                {
                    throw new Exception("Mail adresi XML dosyasında bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog($"Hata: {ex.Message}");
                return string.Empty;
            }
        }

        // E-posta formatını doğrulayan fonksiyon
        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Email formatını kontrol etmek için Regex kullanıyoruz
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string GetAccessTokenForService()
        {
            try
            {
                Logger.WriteToLog("1");
                string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string parentDirectory = Directory.GetParent(applicationDirectory).Parent.FullName;
                string parentDirectory2 = Directory.GetParent(parentDirectory).Parent.FullName;

                // token.xml dosyasının tam yolu
                string settingsFilePath = Path.Combine(parentDirectory2, "Backup", "bin", "Debug", "token.xml");
                Logger.WriteToLog("2" + settingsFilePath);

                // token.xml dosyasını yükleyin
                XDocument xmlDoc = XDocument.Load(settingsFilePath);

                Logger.WriteToLog("3" + settingsFilePath);

                // Token değerini çekin (XML dosyasındaki formatına göre bu işlemi uyarlayabilirsiniz)
                string token = xmlDoc.Root.Value.Trim();
                Logger.WriteToLog("4Token" + token);
                // Token'ı döndür
                return token;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog($"Hata: {ex.Message}");
                Logger.WriteToLog("5");

                return string.Empty;
            }
        }
        public static void SaveAccessTokenToFileForService(string token,string tokenFilePath)
        {
            // XML formatında token'ı kaydet
            XDocument xmlDoc = new XDocument(
                new XElement("Token", token)
            );

            // XML dosyasını kaydet
            xmlDoc.Save(tokenFilePath);
        }
        public static string LoadAccessTokenFromFile(string filePath)
        {
            //string filePath = @"C:\Users\Lenovo\Emre\GitHub\Backup\Backup\bin\Debug\token.xml";

            XDocument doc = XDocument.Load(filePath);
            XElement tokenElement = doc.Root;

            if (tokenElement != null)
            {
                return tokenElement.Value; // Return the token value from the XML file
            }

            return null; // Handle case if Token element is not found
        }
    }
}
