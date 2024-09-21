using Backup.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup.Forms
{
    public partial class denemeForm : Form
    {
        string fileName = "";
        List<string> kaynaklar = new List<string>();
        List<string> hedefler = new List<string>();
        string servisSure;
        bool isSaveDrive = false;
        CompressionType compressionType = CompressionType.rar;
        string DosyaID = "";
        string mail = "";
        public denemeForm()
        {
            InitializeComponent();
        }
       

        private async void denemeForm_Load(object sender, EventArgs e)
        {
            try
            {
                XmlDetaylar.LoadSettings(out fileName, out kaynaklar, out hedefler, out servisSure, out isSaveDrive, out DosyaID, out compressionType, out mail);

                foreach (string hedefFolderPath in hedefler)
                {
                    Logger.WriteToLog("Hedef Yüklüyorrrrrrrrrrrrrrrrrrrrrrr");
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
                Logger.WriteToLog($"Drive'a Yüklendi Yeeey" + XmlDetaylar.GetAccessTokenForService());
                GoogleDriveUploader uploader = new GoogleDriveUploader(XmlDetaylar.GetAccessTokenForService());
                Logger.WriteToLog("Google Yüklüyorrrrrrrrrrrrrrrrrrrrrrr");

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
                Logger.WriteToLog($"Drive'a Yüklenecekti yeeey");
                //MessageBox.Show("Dosya ilgili dizine yüklendi.Google Drive Giriş Yapmadığı veya Tercih etmediği için Drive Yüklenemedi.");
            }
        }


    }
}
