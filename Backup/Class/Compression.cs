using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backup
{
    public class Compression
    {
        public static void CompressWithRar(string rarFilePath, List<string> kaynaklar)
        {
            try
            {
                string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string parentDirectory = Directory.GetParent(applicationDirectory).Parent.FullName;
                string parentDirectory2 = Directory.GetParent(parentDirectory).Parent.FullName;

                string settingsFilePath = Path.Combine(parentDirectory2, "Winrar", "Rar.exe");

               
                var startInfo = new ProcessStartInfo
                {

                    FileName = settingsFilePath, // WinRAR'ın tam yolu
                    Arguments = $"a \"{rarFilePath}\" \"{string.Join("\" \"", kaynaklar)}\" -r",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                };

                using (var process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Logger.WriteToLog($"RAR sıkıştırma işlemi sırasında hata oluştu: {error}");
                    }
                    Logger.WriteToLog($"RAR sıkıştırma işlemi tamamlandı: {rarFilePath}");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"RAR sıkıştırma hatası: {ex.Message}");
                Logger.WriteToLog($"RAR sıkıştırma hatası: {ex.Message}");
            }
        }
        public static void CompressWithZip(string rarFilePath, List<string> kaynaklar)
        {
            // Zip sıkıştırma kodu
            using (ZipArchive zip = ZipFile.Open(rarFilePath, ZipArchiveMode.Create))
            {
                foreach (string item in kaynaklar)
                {
                    if (Directory.Exists(item)) // Eğer klasörse
                    {
                        foreach (string filePath in Directory.GetFiles(item, "*", SearchOption.AllDirectories))
                        {
                            string entryName = GetRelativePath(item, filePath); // Klasör yapısını korumak için göreceli yol
                            zip.CreateEntryFromFile(filePath, entryName);
                        }
                    }
                    else if (File.Exists(item)) // Eğer dosyaysa
                    {
                        zip.CreateEntryFromFile(item, Path.GetFileName(item));
                    }
                }
            }
            Logger.WriteToLog($"Dosyalar zip ile başarıyla sıkıştırıldı ve kaydedildi: {rarFilePath}");

        }
        static string GetRelativePath(string relativeTo, string path)
        {
            Uri fromUri = new Uri(relativeTo);
            Uri toUri = new Uri(path);

            if (fromUri.Scheme != toUri.Scheme) { return path; } // path can't be made relative.

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }
    }
}
