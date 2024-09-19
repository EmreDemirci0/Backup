using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class GoogleDriveUploader
{
    // Kullanıcı giriş yaptıktan sonra aldığınız accessToken burada olacak
    private string accessToken;

    public GoogleDriveUploader(string accessToken)
    {
        this.accessToken = accessToken;
    }

    public async Task UploadFileToGoogleDrive(string filePath)
    {
        try
        {
            // Kullanıcının erişim jetonunu kullanarak yetkilendirme
            var credential = GoogleCredential.FromAccessToken(accessToken);

            // Google Drive API servisinin başlatılması
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "BackupService",
            });

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                // İsteğe bağlı olarak, hedef klasör ID'sini belirleyebilirsiniz. Burada boş bırakıyoruz.
                Parents = new List<string> { "root" } // "root" Drive'ın ana dizinine yükler
            };

            // Dosyayı Google Drive'a yükle
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, "application/zip");
                request.Fields = "id";
                var result = await request.UploadAsync();

                if (result.Status == Google.Apis.Upload.UploadStatus.Completed)
                {
                    WriteToLog("Dosya başarıyla yüklendi.");
                }
                else
                {
                    WriteToLog("Yükleme sırasında bir hata oluştu.");
                }
            }
        }
        catch (Exception ex)
        {
            WriteToLog($"Google Drive yükleme hatası: {ex.Message}");
        }
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
