using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                        Logger.WriteToMailLog($"RAR sıkıştırma işlemi sırasında hata oluştu: {error}");
                    }
                    Logger.WriteToLog($"RAR sıkıştırma işlemi tamamlandı: {rarFilePath}");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"RAR sıkıştırma hatası: {ex.Message}");
                Logger.WriteToMailLog($"RAR sıkıştırma hatası: {ex.Message}");
            }
        }
        public static void CompressWithZip(string zipFilePath, List<string> kaynaklar)
        {
            try
            {
                using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
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
                Logger.WriteToLog($"Dosyalar zip ile başarıyla sıkıştırıldı ve kaydedildi: {zipFilePath}");
            }
            catch (Exception ex)
            {
                Logger.WriteToMailLog($"Bir hata oluştu: {ex.Message}");
            }
            // Zip sıkıştırma kodu


        }
        public static void CompressWithFile(string destinationFolderPath, List<string> kaynaklar)
        {
            try
            {
                // Ensure the destination folder exists
                Directory.CreateDirectory(destinationFolderPath);

                // Loop through each item in the kaynaklar list (could be file or directory)
                foreach (string item in kaynaklar)
                {
                    string destinationPath = Path.Combine(destinationFolderPath, Path.GetFileName(item));

                    if (Directory.Exists(item)) // If the item is a directory
                    {
                        // Copy the entire directory and preserve the folder structure
                        CopyDirectory(item, destinationPath);
                    }
                    else if (File.Exists(item)) // If the item is a file
                    {
                        // Copy the file directly into the destination folder
                        File.Copy(item, destinationPath, true); // Overwrite if the file exists
                    }
                    else
                    {
                        // Log if the source is neither a file nor a directory
                        Logger.WriteToLog($"Geçersiz dosya veya dizin: {item}");
                    }
                }

                Logger.WriteToLog($"Dosyalar sıkıştırılmadan başarıyla klasöre kopyalandı: {destinationFolderPath}");
            }
            catch (Exception ex)
            {
                Logger.WriteToMailLog($"Dosya kopyalama hatası: {ex.Message}");
            }
        }

        public static void CopyDirectory(string sourceDir, string destinationDir)
        {
            Directory.CreateDirectory(destinationDir); // Ensure destination directory exists

            // Copy all files
            foreach (string file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
            {
                string relativePath = GetRelativePath(sourceDir, file); // Retain folder structure
                string destinationFilePath = Path.Combine(destinationDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(destinationFilePath)); // Ensure subdirectories exist
                File.Copy(file, destinationFilePath, true); // Copy file, overwrite if exists
            }
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
