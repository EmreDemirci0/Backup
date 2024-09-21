using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Backup.Class
{
    public class LoginGoogle
    {
        public string clientId = "443208802422-7ki16aca76sq48oq1f5ci6stavfvi8dn.apps.googleusercontent.com"; // Google Cloud Console'dan alınacak
        public string clientSecret = "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"; // Google Cloud Console'dan alınacak
        public string redirectUri = "urn:ietf:wg:oauth:2.0:oob"; // Sabit geri dönüş URI
        public Userinfo userInfo;

        private string tokenFilePath = "token.xml"; // Path where you save the token

        public async Task<string> GetAccessToken(string authCode)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var requestBody = new FormUrlEncodedContent(new[]
                    {
                    new KeyValuePair<string, string>("code", authCode),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("redirect_uri", redirectUri),
                    new KeyValuePair<string, string>("grant_type", "authorization_code"),
                });

                    var response = await client.PostAsync("https://oauth2.googleapis.com/token", requestBody);
                    response.EnsureSuccessStatusCode();

                    var responseString = await response.Content.ReadAsStringAsync();
                    var jsonResponse = JObject.Parse(responseString);

                    return jsonResponse["access_token"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToMailLog($"Token alma sırasında hata: {ex.Message}");
                throw;
            }
        }

        public async Task<Userinfo> GetUserInfo(string accessToken)
        {
            var oauthService = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(accessToken),
                ApplicationName = "GoogleLoginExample"
            });
            
            userInfo = await oauthService.Userinfo.Get().ExecuteAsync();

            return userInfo;
            // Kullanıcı bilgilerini ekranda göster
            //MessageBox.Show($"Giriş Başarılı!\nKullanıcı: {userInfo.Name}\nEmail: {userInfo.Email}\n" + accessToken);
            //Frm_MainMenu frm_MainMenu = new Frm_MainMenu(this);
            //frm_MainMenu.Show();
        }

        
        public async Task<bool> IsTokenValid(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync($"https://oauth2.googleapis.com/tokeninfo?access_token={token}");
                    return response.IsSuccessStatusCode; // Token is valid if the request is successful
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveAccessTokenToFile(string token)
        {
            XmlDetaylar.SaveAccessTokenToFileForService( token, tokenFilePath);
        }


        public string LoadAccessTokenFromFile()
        {
           return XmlDetaylar.LoadAccessTokenFromFile(tokenFilePath);
        }


        public void DeleteAccessTokenFile()
        {
            if (File.Exists(tokenFilePath))
            {
                File.Delete(tokenFilePath);
            }
        }

    }
}
