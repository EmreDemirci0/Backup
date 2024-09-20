using Backup;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

    public async Task UploadFileToGoogleDrive(string filePath, string dosyaID)
    {
        try
        {
            ////Kullanıcıdan aldığınız yetkilendirme ile ilgili token bilgilerini alın
            //var credential = await GetUserCredentialAsync();

            //Kullanıcının erişim jetonunu kullanarak yetkilendirme
            var credential = GoogleCredential.
                 FromAccessToken(accessToken).
                 CreateScoped(new[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile });


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
                //Parents= new List<string> { dosyaID }
            };


            if (dosyaID != "")
            {
                fileMetadata.Parents = new List<string> { dosyaID };
            }



            // Dosyayı Google Drive'a yükle
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, "application/zip");
                request.Fields = "id";
                var result = await request.UploadAsync();

                if (result.Status == Google.Apis.Upload.UploadStatus.Completed)
                {
                    Logger.WriteToLog("Dosya Drive'a başarıyla yüklendi.");
                }
                else
                {
                    // Hata mesajını ve durumu loga yazdırma
                    Logger.WriteToLog($"Yükleme sırasında bir hata oluştu. Status: {result.Status}, Hata mesajı: {result.Exception?.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Logger.WriteToLog($"Google Drive yükleme hatası: {ex.Message}");
        }
    }

    public async Task<List<Google.Apis.Drive.v3.Data.File>> ListDriveFolders()
    {
        try
        {

            var credential = GoogleCredential.FromAccessToken(accessToken);
            var driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Drive API",
            });

            var request = driveService.Files.List();
            request.Q = "mimeType='application/vnd.google-apps.folder'";
            request.Fields = "files(id, name)";
            var result = await request.ExecuteAsync();

            return result.Files.ToList();
        }
        catch (Exception ex)
        {
            Logger.WriteToLog($"Klasörleri alırken hata oluştu: {ex.Message} - {DateTime.Now}");
            return null;
        }
    }
    public async Task<bool> IsTokenValid(string accessToken)
    {
        try
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?access_token={accessToken}");
                Logger.WriteToLog($"Token Basarıyla doğrulandı");
                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            Logger.WriteToLog($"Token doğrulama hatası: {ex.Message}");
            return false;
        }
    }
}
