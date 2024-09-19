using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class GoogleDriveUploader
{
    // Kullanıcı giriş yaptıktan sonra aldığınız accessToken burada olacak
    private string accessToken;

    public GoogleDriveUploader(string accessToken)
    {
        this.accessToken = accessToken;
    }

    public async Task UploadFileToGoogleDrive(string filePath,string dosyaID)
    {
        try
        {
            // Kullanıcıdan aldığınız yetkilendirme ile ilgili token bilgilerini alın
            var credential = await GetUserCredentialAsync();

            // Kullanıcının erişim jetonunu kullanarak yetkilendirme
            //var credential = GoogleCredential.FromAccessToken(accessToken);

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
                //Parents = new List<string> { "root" } // "root" Drive'ın ana dizinine yükler
                //Parents = new List<string> { "my-drive" } // "root" Drive'ın ana dizinine yükler
                //Parents= new List<string> { "1H62UmUsFSG5PY3TcXza6qSU_E7u0Z5At" }
                Parents= new List<string> { dosyaID }
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
                    // Hata mesajını ve durumu loga yazdırma
                    WriteToLog($"Yükleme sırasında bir hata oluştu. Status: {result.Status}, Hata mesajı: {result.Exception?.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            WriteToLog($"Google Drive yükleme hatası: {ex.Message}");
        }
    }
    public static async Task<UserCredential> GetUserCredentialAsync()
    {
        // Kullanıcıdan gerekli izinleri almak için yetki kapsamları
        string[] scopes = { DriveService.Scope.DriveFile }; // Sadece uygulamanızın yüklediği dosyalara erişir

        using (var stream = new FileStream("client_secret_443208802422-7ki16aca76sq48oq1f5ci6stavfvi8dn.apps.googleusercontent.com.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = "token.json";
            return await GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true));
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
