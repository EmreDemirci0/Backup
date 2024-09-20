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
using Backup;
using Backup.Class;
using System.Threading.Tasks;

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
            Logger.WriteToLog("Servis Başladı: ");
            timer = new Timer(Convert.ToDouble(servisSure));
            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Start();
        }

        protected override void OnStop()
        {
            Logger.WriteToLog("Servis Durduruldu: ");
            timer.Stop();
        }
        string fileName = "";
        List<string> kaynaklar = new List<string>();
        List<string> hedefler = new List<string>();
        string servisSure;
        bool isSaveDrive = false;
        CompressionType compressionType = CompressionType.rar;
        string DosyaID = "";
        string mail = "";

        private async void OnElapsedTime(object sender, ElapsedEventArgs e)
        {
            try
            {
                XmlDetaylar.LoadSettings(out fileName, out kaynaklar, out hedefler, out servisSure, out isSaveDrive, out DosyaID, out compressionType, out mail);

                foreach (string hedefFolderPath in hedefler)
                {
                    if (!Directory.Exists(hedefFolderPath))
                    {
                        //MessageBox.Show($"Hedef klasör bulunamadı: {hedefFolderPath}");
                        continue; // Geçerli olmayan hedef klasörü atla
                    }

                    string archiveFilePath = "";
                    if (compressionType != CompressionType.folder)
                    {
                        archiveFilePath = Path.Combine(hedefFolderPath, $"{fileName}.{compressionType.ToString().ToLower()}");
                    }
                    else
                    {
                        archiveFilePath = Path.Combine(hedefFolderPath, $"{fileName}");
                    }

                    // Aynı isimde dosya varsa numaralandır
                    int count = 1;
                    while (File.Exists(archiveFilePath) || (compressionType == CompressionType.folder && Directory.Exists(archiveFilePath)))
                    {
                        string tempFileName = "";
                        if (compressionType != CompressionType.folder)
                        {
                            tempFileName = $"{fileName}({count}).{compressionType.ToString().ToLower()}";
                        }
                        else
                        {
                            tempFileName = $"{fileName}({count})";
                        }

                        archiveFilePath = Path.Combine(hedefFolderPath, tempFileName);
                        count++;
                    }

                    if (compressionType == CompressionType.zip)
                    {
                        Compression.CompressWithZip(archiveFilePath, kaynaklar);
                    }
                    else if (compressionType == CompressionType.rar)
                    {
                        Compression.CompressWithRar(archiveFilePath, kaynaklar);
                    }
                    else if (compressionType == CompressionType.folder)
                    {
                        Compression.CompressWithFile(archiveFilePath, kaynaklar);

                    }
                    await SaveDrive(archiveFilePath, DosyaID);
                    Logger.WriteToLog($"Drive'a Yüklendi Yeeey");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog($"Hata oluştu: {ex.Message}");
            }
        }
        public async Task SaveDrive(string archiveFilePath, string DriveDosyaID)
        {
            if (isSaveDrive)
            {
                GoogleDriveUploader uploader = new GoogleDriveUploader(XmlDetaylar.GetAccessTokenForService());
                Logger.WriteToLog(archiveFilePath);

                if (compressionType == CompressionType.folder)
                    await uploader.UploadFileToGoogleDrive(archiveFilePath, DriveDosyaID, true);
                else
                {
                    await uploader.UploadFileToGoogleDrive(archiveFilePath, DriveDosyaID, false);
                }
                //MessageBox.Show($"Dosya Google Drive'a ve ilgili dizine yüklendi: {archiveFilePath}");
            }
            else
            {
                //MessageBox.Show("Dosya ilgili dizine yüklendi.Google Drive Giriş Yapmadığı veya Tercih etmediği için Drive Yüklenemedi.");
            }
        }
        
    }
}
