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

    public async Task UploadFileToGoogleDrive(string filePath, string dosyaID, bool isDirectory)
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
            };
            if (!string.IsNullOrEmpty(dosyaID))
            {
                fileMetadata.Parents = new List<string> { dosyaID };
                Logger.WriteToMailLog($"Dosya ilgili klasöre yükleniyor. Klasör ID: {dosyaID}");
            }
            else
            {
                Logger.WriteToMailLog("Dosya Google Drive root dizinine yükleniyor.");
            }


            if (isDirectory)//Klasör ise
            {
                string driveFolderId = "";
                if (!string.IsNullOrEmpty(dosyaID))
                    driveFolderId = CreateFolder(service, fileMetadata.Name, dosyaID);
                else
                    driveFolderId = CreateFolder(service, fileMetadata.Name);
                UploadFolder(service, filePath, driveFolderId);
            }
            else//Rar Zip ise
            {
                // Dosyayı Google Drive'a yükle
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var request = service.Files.Create(fileMetadata, stream, "application/zip");
                    request.Fields = "id";
                    var result = await request.UploadAsync();

                    if (result.Status == Google.Apis.Upload.UploadStatus.Completed)
                    {
                        //Logger.WriteToLog("Dosya Drive'a başarıyla yüklendi.");
                    }
                    else
                    {
                        // Hata mesajını ve durumu loga yazdırma
                        Logger.WriteToMailLog($"Yükleme sırasında bir hata oluştu. Status: {result.Status}, Hata mesajı: {result.Exception?.Message}");
                    }
                }
            }

        }
        catch (Exception ex)
        {
            Logger.WriteToMailLog($"Google Drive yükleme hatası: {ex.Message}");
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
            Logger.WriteToMailLog($"Klasörleri alırken hata oluştu: {ex.Message} - {DateTime.Now}");
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
                //Logger.WriteToLog($"Token Basarıyla doğrulandı");
                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            Logger.WriteToMailLog($"Token doğrulama hatası: {ex.Message}");
            return false;
        }
    }

    static string CreateFolder(DriveService service, string folderName, string parentId = null)
    {
        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = folderName,
            MimeType = "application/vnd.google-apps.folder",
            Parents = parentId != null ? new List<string> { parentId } : null
        };

        var request = service.Files.Create(fileMetadata);
        request.Fields = "id";
        var file = request.Execute();
        return file.Id;
    }
    static void UploadFolder(DriveService service, string localFolderPath, string parentId)
    {
        foreach (var directory in Directory.GetDirectories(localFolderPath))
        {
            string folderName = Path.GetFileName(directory);
            string folderId = CreateFolder(service, folderName, parentId);
            UploadFolder(service, directory, folderId); // Alt klasörleri yükle
        }

        foreach (var filePath in Directory.GetFiles(localFolderPath))
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = Path.GetFileName(filePath),
                Parents = new List<string> { parentId }
            };

            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                var request = service.Files.Create(fileMetadata, stream, GetMimeType(filePath));
                request.Fields = "id";
                request.Upload();
                var file = request.ResponseBody;
            }
        }
    }
    static string GetMimeType(string filePath)
    {
        var mimeType = "application/octet-stream";
        var ext = Path.GetExtension(filePath).ToLower();
        switch (ext)
        {
            case ".jpg": mimeType = "image/jpeg"; break;
            case ".jpeg": mimeType = "image/jpeg"; break;
            case ".png": mimeType = "image/png"; break;
            case ".gif": mimeType = "image/gif"; break;
            case ".pdf": mimeType = "application/pdf"; break;
                // Diğer MIME türlerini ekleyebilirsiniz.
        }
        return mimeType;
    }
}
