using System;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using System.Timers;
using System.Xml.Linq;
using System.IO.Compression;
using System.Collections.Generic;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Drive.v3;

namespace BackupService2
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;

        public Service1()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            WriteToLog("Servis Başladı: ");
            timer = new Timer(LoadServisSure()); // Her 1 dakikada bir çalışacak
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
        }

        protected override void OnStop()
        {
            WriteToLog("Servis Durduruldu: ");
            timer.Stop();
        }


        private  void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                // XML dosyasından ayarları yükle
                var (kaynaklar, hedefler, fileName, compressionType) = LoadSettings();

                foreach (var hedef in hedefler)
                {
                    string zipFilePath = Path.Combine(hedef, $"{fileName}.{compressionType}");

                    // Aynı isimde dosya varsa, numaralandırarak farklı isim oluştur
                    int count = 1;
                    while (File.Exists(zipFilePath))
                    {
                        string tempFileName = $"{fileName}({count}).{compressionType}";
                        zipFilePath = Path.Combine(hedef, tempFileName);
                        count++;
                    }

                    // Sıkıştırma türüne göre işlem yap
                    if (compressionType == "zip")
                    {
                        CompressWithZip(zipFilePath, kaynaklar);
                       
                    }
                    else if (compressionType == "rar")
                    {
                        CompressWithRar(zipFilePath, kaynaklar);
                    }
                    //Sıkıntı var parada
                    // Google Drive'a yükleme işlemi
                    //UploadFileToGoogleDrive(zipFilePath);
                    // Giriş işlemi sonrası accessToken alınması
                    //string accessToken = await GetAccessToken(authCode);

                    //// Alınan accessToken ile GoogleDriveUploader sınıfını başlatın
                    //GoogleDriveUploader uploader = new GoogleDriveUploader(accessToken);

                    //// Dosyayı kullanıcının Drive'ına yükleyin
                    //await uploader.UploadFileToGoogleDrive("path/to/your/file.zip");
                    //WriteToLog($"Dosya Google Drive'a yüklendi: {zipFilePath}");
                }
            }
            catch (Exception ex)
            {
                WriteToLog($"Hata oluştu: {ex.Message}");
            }
        }
        private void CompressWithZip(string zipFilePath, List<string> kaynaklar)
        {
            // Zip dosyasını oluştur
            using (ZipArchive zip = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
            {
                foreach (string kaynak in kaynaklar)
                {
                    if (Directory.Exists(kaynak)) // Eğer klasörse
                    {
                        foreach (string filePath in Directory.GetFiles(kaynak, "*", SearchOption.AllDirectories))
                        {
                            string entryName = GetRelativePath(kaynak, filePath);
                            zip.CreateEntryFromFile(filePath, entryName);
                        }
                    }
                    else if (File.Exists(kaynak)) // Eğer dosyaysa
                    {
                        zip.CreateEntryFromFile(kaynak, Path.GetFileName(kaynak));
                    }
                }
            }

            WriteToLog($"Sıkıştırma işlemi başarılı: {zipFilePath}");
        }
        private void CompressWithRar(string rarFilePath, List<string> kaynaklar)
        {
            try
            {
                string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string parentDirectory = Directory.GetParent(applicationDirectory).Parent.FullName;
                string parentDirectory2 = Directory.GetParent(parentDirectory).Parent.FullName;

                string settingsFilePath = Path.Combine(parentDirectory2, "Winrar", "Rar.exe");

                //System.Windows.Forms.MessageBox.Show("settingFilePath" + settingsFilePath);

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
                        throw new Exception($"RAR sıkıştırma işlemi sırasında hata oluştu: {error}");
                    }
                    WriteToLog($"RAR sıkıştırma işlemi tamamlandı: {rarFilePath}");
                }
            }
            catch (Exception ex)
            {
                WriteToLog($"RAR sıkıştırma hatası: {ex.Message}");
                //MessageBox.Show($"RAR sıkıştırma hatası: {ex.Message}");
            }
        }

        private double LoadServisSure()
        {
            // XML dosyasının proje klasöründeki yolu
            //string settingsFilePath = @"C:\Users\Lenovo\source\repos\Backup\BackupService2\bin\Debug\settings.xml";
            string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // XML dosyasının proje klasöründeki yolu (çalışma dizininde dinamik olarak ayarlanır)
            string settingsFilePath = Path.Combine(applicationDirectory, "settings.xml");


            if (File.Exists(settingsFilePath))
            {
                var settings = XElement.Load(settingsFilePath);

                // ServisSure değerini al
                string servisSureStr = settings.Element("ServisSure")?.Value;

                // ServisSure değerini double'a çevir ve geri döndür
                if (double.TryParse(servisSureStr, out double servisSure))
                {
                    return servisSure;
                }
                else
                {
                    //MessageBox.Show("Servis süresi geçerli bir sayı değil.");
                    return 0;
                }
            }
            else
            {
                //MessageBox.Show("Ayar dosyası bulunamadı.");
                return 0;
            }
        }
        private (List<string> kaynaklar, List<string> hedefler, string fileName, string compressionType) LoadSettings()
        {
            string settingsFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.xml");

            if (!File.Exists(settingsFilePath))
                throw new FileNotFoundException("Ayar dosyası bulunamadı.");

            var settings = XElement.Load(settingsFilePath);

            var kaynaklar = new List<string>(settings.Element("Kaynaklar")?.Value.Split(';'));
            var hedefler = new List<string>(settings.Element("Hedefler")?.Value.Split(';'));
            string fileName = settings.Element("FileName")?.Value;
            string compressionType = settings.Element("SikistirmaTuru")?.Value.ToLower(); // Sıkıştırma türünü küçük harflerle al

            return (kaynaklar, hedefler, fileName, compressionType);
        }
        public void UploadFileToGoogleDrive(string filePath)
        {
            try
            {
                string applicationDirectory = AppDomain.CurrentDomain.BaseDirectory;

                // XML dosyasının proje klasöründeki yolu (çalışma dizininde dinamik olarak ayarlanır)
                //string settingsFilePath = Path.Combine(applicationDirectory, "settings.xml");
                string serviceAccountCredentialFilePath = Path.Combine(applicationDirectory, "emre-projesi-79ceabaa8877.json");

                //string serviceAccountCredentialFilePath = @"C:\Users\Lenovo\source\repos\Backup\BackupService2\bin\Debug\emre-projesi-79ceabaa8877.json";

                var credential = GoogleCredential.FromFile(serviceAccountCredentialFilePath)
                    .CreateScoped(DriveService.ScopeConstants.DriveFile);


                var service = new DriveService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "BackupService",

                });



                var fileMetadata = new Google.Apis.Drive.v3.Data.File()
                {
                    Name = Path.GetFileName(filePath),
                    Parents = new List<string> { "1ItUaDyPe7xbTLPEa2gFhitoUS3kkabSy" }  // Buraya hedef klasörün ID'sini yazın
                };


                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var request = service.Files.Create(fileMetadata, stream, "application/zip");

                    request.Fields = "id";

                    var result = request.Upload();


                }

            }
            catch (Exception ex)
            {
                WriteToLog($"Google Drive yükleme hatası: {ex.Message}");
                throw;
            }
        }

        private string GetRelativePath(string relativeTo, string path)
        {
            Uri fromUri = new Uri(relativeTo);
            Uri toUri = new Uri(path);

            if (fromUri.Scheme != toUri.Scheme) { return path; }

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());
            return relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        private void WriteToLog(string message)
        {
            string logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log.txt");
            using (StreamWriter sw = new StreamWriter(logPath, true))
            {
                sw.WriteLine($"{message} - {DateTime.Now}");
            }
        }
    }
}
