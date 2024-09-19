using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup
{
    public partial class Login : Form
    {
        private string clientId = "443208802422-7ki16aca76sq48oq1f5ci6stavfvi8dn.apps.googleusercontent.com"; // Google Cloud Console'dan alınacak
        private string clientSecret = "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"; // Google Cloud Console'dan alınacak
        private string redirectUri = "urn:ietf:wg:oauth:2.0:oob"; // Sabit geri dönüş URI

        public string accessToken;
        public Login()
        {
            InitializeComponent();
        }


        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                // Yetkilendirme URL'sini oluştur
                string authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                          $"scope=https://www.googleapis.com/auth/userinfo.profile " +
                                          $"https://www.googleapis.com/auth/userinfo.email&" + // email erişim yetkisi eklendi
                                          $"access_type=offline&" +
                                          $"include_granted_scopes=true&" +
                                          $"response_type=code&" +
                                          $"redirect_uri={redirectUri}&" +
                                          $"client_id={clientId}";

                // Tarayıcıyı açarak kullanıcıyı giriş ekranına yönlendir
                Process.Start(new ProcessStartInfo(authorizationUrl) { UseShellExecute = true });

                // Kullanıcıdan yetkilendirme kodunu al
                string authCode = Microsoft.VisualBasic.Interaction.InputBox("Google yetkilendirme kodunu girin:", "Yetkilendirme Kodu");

                // Yetkilendirme kodunu kullanarak erişim jetonunu alın
                 accessToken = await GetAccessToken(authCode);
                
                // Erişim jetonunu kullanarak kullanıcı bilgilerini al
                await GetUserInfo(accessToken);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş başarısız: {ex.Message}");
            }

        }

        public async Task<string> GetAccessToken(string authCode)
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

        private async Task GetUserInfo(string accessToken)
        {
            var oauthService = new Oauth2Service(new BaseClientService.Initializer
            {
                HttpClientInitializer = GoogleCredential.FromAccessToken(accessToken),
                ApplicationName = "GoogleLoginExample"
            });

            var userInfo = await oauthService.Userinfo.Get().ExecuteAsync();

            // Kullanıcı bilgilerini ekranda göster
            MessageBox.Show($"Giriş Başarılı!\nKullanıcı: {userInfo.Name}\nEmail: {userInfo.Email}"+accessToken);
            Frm_MainMenu frm_MainMenu = new Frm_MainMenu(this);
            frm_MainMenu.Show();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}
