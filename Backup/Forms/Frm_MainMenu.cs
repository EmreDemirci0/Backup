using Backup.Class;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Oauth2.v2;
using Google.Apis.Oauth2.v2.Data;
using Google.Apis.Services;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup
{
    public partial class Frm_MainMenu : Form
    {
        ContextMenuStrip kaynakDropdownMenu = new ContextMenuStrip();
        ContextMenuStrip hedefDropdownMenu = new ContextMenuStrip();

        private List<string> kaynakSelectedItems = new List<string>(); // Kaynak Dosya ve klasörleri tutacak liste
        private List<string> hedefSelectedItems = new List<string>(); // Hedef Dosya ve klasörleri tutacak liste
        private string fileName;

        private Dictionary<string, string> driveFolders = new Dictionary<string, string>(); // Hedef Dosya ve klasörleri tutacak liste
        string DriveDosyaID;

        string servisSuresi;
        private string serviceName = "AAAAA_Backup";  // Servis adınızı buraya yazın

        public string accessToken;


        bool isSaveDrive = false;//burayı kontrol edeceğiz. Her an her şey olabilir//???////////////////////////////////
        bool isSession=false;
        LoginGoogle loginGoogle;

        //  Login _login;
        public Frm_MainMenu(/*Login login*/)
        {
            // _login = login;
            InitializeComponent();

            comboBox1.SelectedIndex = 0;
            lbl_dosyaAdi.Text = txt_dosyaAdi.Text;
            radio_rar.Checked = true;
            toolTip1.SetToolTip(btn_infoGoogle, "Drive'da bir klasör seçiniz.");
            loginGoogle = new LoginGoogle();
        }


        private void Frm_MainMenu_Load(object sender, EventArgs e)
        {
            ListViewIslemleri();
            KaynakEkleIslemleri();
            HedefEkleIslemleri();
            LoadXmlDatas();
            LoginWithSession();
        }
        private void LoadXmlDatas()
        {
            XmlDetaylar.LoadSettings(out fileName, out kaynakSelectedItems, out hedefSelectedItems, out isSaveDrive);



            listBox1.Items.Clear();
            listBox1.Items.AddRange(kaynakSelectedItems.ToArray());

            listBox2.Items.Clear();
            listBox2.Items.AddRange(hedefSelectedItems.ToArray());

            txt_dosyaAdi.Text = fileName;

            check_saveGoogle.Checked = isSaveDrive;
            panel_Google.Enabled = isSaveDrive;

        }
        private void SaveXmlDatas()
        {
            string compressionType;
            if (radio_rar.Checked)
                compressionType = "rar";
            else
                compressionType = "zip";

            fileName = txt_dosyaAdi.Text;
            XmlDetaylar.SaveSettings(kaynakSelectedItems, hedefSelectedItems, fileName, servisSuresi, compressionType, isSaveDrive);

        }
        private void SaveAccessTokenToXml()
        {
            //if (_login.accessToken == null)
            //{
            //    Logger.WriteToLog("Login giriş kodu bulunamadı:\n" + _login.accessToken);
            //}
            //XmlDetaylar.SaveAccessTokenToXml(_login.accessToken);

            if (accessToken == null)
            {
                Logger.WriteToLog("Login giriş kodu bulunamadı:" + accessToken);
            }
            XmlDetaylar.SaveAccessTokenToXml(accessToken);

            //Giriş işlemi sonrası accessToken alınması
            //string accessToken = await _login.GetAccessToken(_login.accessToken);

            //AccessToken'ı XML'e kaydet
        }
        private void Btn_kaynakSil_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null) // Eğer bir öğe seçiliyse
            {
                DialogResult result = MessageBox.Show("Seçili kaynaklar silinsin mi?", "Emre Backup ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) // Eğer kullanıcı "Evet" derse
                {
                    object selectedItem = listBox1.SelectedItem;
                    kaynakSelectedItems.Remove(selectedItem.ToString());
                    listBox1.Items.Remove(selectedItem.ToString());
                }
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir öğe seçin.");
            }
        }
        private void btn_servisBaslat_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceController service = new ServiceController(serviceName);

                // Eğer servis duruyorsa başlat
                if (service.Status == ServiceControllerStatus.Stopped)
                {
                    service.Start();
                    service.WaitForStatus(ServiceControllerStatus.Running); // Servis tamamen başlatılana kadar bekle
                    MessageBox.Show("Servis başarıyla başlatıldı.");
                }
                else
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped); // Servis tamamen durana kadar bekle
                    MessageBox.Show("Servis başarıyla durduruldu.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Eğer çalışmazsa Projeyi Yönetici olarak Çalıştır.\nServis başlatılırken bir hata oluştu: {ex.Message}");
            }
        }
        private void btn_HedefSil_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedItem != null) // Eğer bir öğe seçiliyse
            {
                DialogResult result = MessageBox.Show("Seçili Hedef silinsin mi?", "Emre Backup ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes) // Eğer kullanıcı "Evet" derse
                {
                    object selectedItem = listBox2.SelectedItem;
                    hedefSelectedItems.Remove(selectedItem.ToString());
                    listBox2.Items.Remove(selectedItem.ToString());
                }
            }
            else
            {
                MessageBox.Show("Lütfen listeden bir öğe seçin.");
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0) // Eğer bir öğe seçilmişse
            {
                // Seçilen öğeyi al
                int selectedIndex = listView1.SelectedItems[0].Index;

                // Seçilen öğeye göre görünürlük ayarları yap
                switch (selectedIndex)
                {
                    case 0: // Eğer 1. öğe seçildiyse (index 0)
                        groupBox_Genel.Visible = true;
                        groupBox_Dosya.Visible = false;
                        groupBox_Zamanlayici.Visible = false;
                        groupbox_drive.Visible = false;
                        break;

                    case 1: // Eğer 2. öğe seçildiyse (index 1)
                        groupBox_Genel.Visible = false;
                        groupBox_Dosya.Visible = true;
                        groupBox_Zamanlayici.Visible = false;
                        groupbox_drive.Visible = false;

                        break;

                    case 2: // Eğer 3. öğe seçildiyse (index 2)
                        groupBox_Genel.Visible = false;
                        groupBox_Dosya.Visible = false;
                        groupBox_Zamanlayici.Visible = true;
                        groupbox_drive.Visible = false;

                        break;
                    case 3: // Eğer 3. öğe seçildiyse (index 2)
                        groupBox_Genel.Visible = false;
                        groupBox_Dosya.Visible = false;
                        groupBox_Zamanlayici.Visible = false;
                        groupbox_drive.Visible = true;

                        break;


                }
            }
            else
            {
                groupBox_Genel.Visible = true;
                groupBox_Dosya.Visible = false;
                groupBox_Zamanlayici.Visible = false;
            }
        }

        private async void btn_sikistir_Click(object sender, EventArgs e)
        {

            if (listBox1.Items.Count == 0)
            {
                MessageBox.Show("Lütfen önce dosya veya klasör ekleyin.");
                return;
            }

            if (listBox2.Items.Count == 0)
            {
                MessageBox.Show("Lütfen hedef klasörleri ekleyin.");
                return;
            }

            SaveXmlDatas();


            string selectedCompressionType; //cmb_sikistirmaTuru.SelectedItem.ToString(); burayı değişmek gerek açarsak
            if (radio_rar.Checked)
                selectedCompressionType = "rar";
            else
                selectedCompressionType = "zip";

            foreach (string hedefFolderPath in listBox2.Items)
            {
                if (!Directory.Exists(hedefFolderPath))
                {
                    MessageBox.Show($"Hedef klasör bulunamadı: {hedefFolderPath}");
                    continue; // Geçerli olmayan hedef klasörü atla
                }

                string archiveFilePath = Path.Combine(hedefFolderPath, $"{txt_dosyaAdi.Text}.{selectedCompressionType.ToLower()}");

                // Aynı isimde dosya varsa numaralandır
                int count = 1;
                while (File.Exists(archiveFilePath))
                {
                    string tempFileName = $"{txt_dosyaAdi.Text}({count}).{selectedCompressionType.ToLower()}";
                    archiveFilePath = Path.Combine(hedefFolderPath, tempFileName);
                    count++;
                }

                if (selectedCompressionType == "zip")
                {
                    Compression.CompressWithZip(archiveFilePath, listBox1.Items.Cast<string>().ToList());
                }
                else if (selectedCompressionType == "rar")
                {
                    Compression.CompressWithRar(archiveFilePath, listBox1.Items.Cast<string>().ToList());

                }

                //    MessageBox.Show($"Dosyalar başarıyla sıkıştırıldı ve kaydedildi: {archiveFilePath}");
                //    // Google Drive'a yükleme
                //    //GoogleDriveUploader.UploadFileToGoogleDrive(archiveFilePath);
                //    //MessageBox.Show($"Dosya Google Drive'a yüklendi: {archiveFilePath}");
                // Alınan accessToken ile GoogleDriveUploader sınıfını başlatın
                //GoogleDriveUploader uploader = new GoogleDriveUploader(_login.accessToken);                
                GoogleDriveUploader uploader = new GoogleDriveUploader(accessToken);
                //MessageBox.Show("Test"+ archiveFilePath);
                // Dosyayı kullanıcının Drive'ına yükleyin
                if (isSession&&isSaveDrive)
                {
                    await uploader.UploadFileToGoogleDrive(archiveFilePath, DriveDosyaID);
                    MessageBox.Show($"Dosya Google Drive'a ve ilgili dizine yüklendi: {archiveFilePath}");
                }
                else
                {
                    MessageBox.Show("Dosya ilgili dizine yüklendi.Google Drive Giriş Yapmadığı veya Tercih etmediği için Drive Yüklenemedi.");
                }

            }


        }

        private void txt_dosyaAdi_TextChanged(object sender, EventArgs e)
        {
            lbl_dosyaAdi.Text = txt_dosyaAdi.Text;
        }
        private void btn_kaynakEkle_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Show(btn_kaynakEkle,0,btn_kaynakEkle.Height);
            kaynakDropdownMenu.Show(btn_kaynakEkle, 0, btn_kaynakEkle.Height);
        }
        private void btn_hedefEkle_Click(object sender, EventArgs e)
        {
            hedefDropdownMenu.Show(btn_hedefEkle, 0, btn_hedefEkle.Height);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
                servisSuresi = "300000";
            //servisSuresi = "20000";
            else if (comboBox1.SelectedIndex == 1)
                servisSuresi = "900000";
            else if (comboBox1.SelectedIndex == 2)
                servisSuresi = "1800000";
            else if (comboBox1.SelectedIndex == 3)
                servisSuresi = "3600000";
            else if (comboBox1.SelectedIndex == 4)
                servisSuresi = "10800000";
            else if (comboBox1.SelectedIndex == 5)
                servisSuresi = "216000000";
            else if (comboBox1.SelectedIndex == 6)
                servisSuresi = "432000000";
            else if (comboBox1.SelectedIndex == 7)
                servisSuresi = "864000000";
            else if (comboBox1.SelectedIndex == 8)
                servisSuresi = "1728000000";
            else if (comboBox1.SelectedIndex == 9)
                servisSuresi = "6048000000";


        }
        private void ListViewIslemleri()
        {
            ImageList imageList = new ImageList();
            string imagePath1 = Path.Combine(Application.StartupPath, "Foto", "general.png");
            string imagePath2 = Path.Combine(Application.StartupPath, "Foto", "folder.png");
            string imagePath3 = Path.Combine(Application.StartupPath, "Foto", "timer.png");
            string imagePath4 = Path.Combine(Application.StartupPath, "Foto", "drive.png");

            // ImageList'e görüntü ekle
            imageList.Images.Add(Image.FromFile(imagePath1)); // Görüntü 1
            imageList.Images.Add(Image.FromFile(imagePath2)); // Görüntü 2
            imageList.Images.Add(Image.FromFile(imagePath3)); // Görüntü 2
            imageList.Images.Add(Image.FromFile(imagePath4)); // Görüntü 2

            imageList.ImageSize = new Size(28, 28); // Görüntülerin boyutunu 32x32 olarak ayarla

            // ListView ayarları
            listView1.View = View.SmallIcon; // Görüntülerin boyutuna göre LargeIcon da kullanılabilir
            listView1.SmallImageList = imageList;

            // ListView'e öğe ekle
            ListViewItem item1 = new ListViewItem("Genel", 0); // 0, ImageList'teki ilk görüntüye işaret eder
            ListViewItem item2 = new ListViewItem("Dosya", 1); // 1, ikinci görüntü
            ListViewItem item3 = new ListViewItem("Detay", 2); // 1, ikinci görüntü
            ListViewItem item4 = new ListViewItem("Drive", 3); // 1, ikinci görüntü
            listView1.Items.Add(item1);
            listView1.Items.Add(item2);
            listView1.Items.Add(item3);
            listView1.Items.Add(item4);


            string bannerImagePath = Path.Combine(Application.StartupPath, "Foto", "banner.png");
            pictureBox1.Image = Image.FromFile(bannerImagePath);

            groupBox_Genel.Visible = true;
            groupBox_Dosya.Visible = false;
            groupBox_Zamanlayici.Visible = false;
            groupbox_drive.Visible = false;
        }
        private void KaynakEkleIslemleri()
        {
            string imagePath1 = Path.Combine(Application.StartupPath, "Foto", "ButtonFile2.png");
            string imagePath2 = Path.Combine(Application.StartupPath, "Foto", "folder.png");
            ToolStripMenuItem itemDosya = new ToolStripMenuItem("Dosya");
            itemDosya.Image = Image.FromFile(imagePath1); // Resim ekle
            itemDosya.Click += new EventHandler(ItemKaynakDosya_Click);

            // İkinci öğeye bir resim ekle
            ToolStripMenuItem itemKlasor = new ToolStripMenuItem("Klasör");
            itemKlasor.Image = Image.FromFile(imagePath2); // Resim ekle
            itemKlasor.Click += new EventHandler(ItemKaynakKlasor_Click);


            // Menüye öğeleri ekle
            kaynakDropdownMenu.Items.Add(itemDosya);
            kaynakDropdownMenu.Items.Add(itemKlasor);

        }
        private void HedefEkleIslemleri()
        {
            string imagePath2 = Path.Combine(Application.StartupPath, "Foto", "folder.png");

            // İkinci öğeye bir resim ekle
            ToolStripMenuItem itemKlasor = new ToolStripMenuItem("Klasör");
            itemKlasor.Image = Image.FromFile(imagePath2); // Resim ekle
            itemKlasor.Click += new EventHandler(ItemHedefKlasor_Click);


            hedefDropdownMenu.Items.Add(itemKlasor);

        }

        private void ItemKaynakDosya_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Bir dosya seçin",
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = "Tüm Dosyalar (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Eğer dosya zaten listede yoksa ekleyelim
                if (!kaynakSelectedItems.Contains(selectedFilePath))
                {
                    kaynakSelectedItems.Add(selectedFilePath);
                    listBox1.Items.Add(selectedFilePath); // ListBox'a ekleyelim
                }
                else
                {
                    MessageBox.Show("Bu dosya zaten eklenmiş.");
                }
            }
        }
        private void ItemKaynakKlasor_Click(object sender, EventArgs e)
        {

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Bir klasör seçin";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // Eğer klasör zaten listede yoksa ekleyelim
                    if (!kaynakSelectedItems.Contains(selectedFolderPath))
                    {
                        kaynakSelectedItems.Add(selectedFolderPath);
                        listBox1.Items.Add(selectedFolderPath); // ListBox'a ekleyelim
                    }
                    else
                    {
                        MessageBox.Show("Bu klasör zaten eklenmiş.");
                    }
                }
            }
        }
        private void ItemHedefKlasor_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Bir klasör seçin";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;

                    // Eğer klasör zaten listede yoksa ekleyelim
                    if (!hedefSelectedItems.Contains(selectedFolderPath))
                    {
                        hedefSelectedItems.Add(selectedFolderPath);
                        listBox2.Items.Add(selectedFolderPath); // ListBox'a ekleyelim
                    }
                    else
                    {
                        MessageBox.Show("Bu klasör zaten eklenmiş.");
                    }
                }
            }
        }

        private async void DriveLoadFolder()
        {
            //GoogleDriveUploader gdu = new GoogleDriveUploader(_login.accessToken);
            //var folders = await gdu.ListDriveFolders();

            accessToken = loginGoogle.LoadAccessTokenFromFile();
            GoogleDriveUploader gdu = new GoogleDriveUploader(accessToken);
            var folders = await gdu.ListDriveFolders();

            if (folders != null)
            {
                // ComboBox içindeki mevcut öğeleri temizle
                cmb_folders.Items.Clear();
                cmb_folders.Items.Add("Root");

                // Sözlüğü de temizle, böylece önceki veriler kalmaz
                driveFolders.Clear();
                driveFolders.Add("Root", "0");

                // Klasörleri ComboBox ve Dictionary içine ekle
                foreach (var folder in folders)
                {
                    // ComboBox'a ekle
                    cmb_folders.Items.Add(new { folder.Id, folder.Name });

                    // Sözlüğe ekle: Klasör Adı - Klasör ID'si
                    driveFolders.Add(folder.Name, folder.Id);

                }

                // Kullanıcının seçim yapmasını sağlamak için ComboBox'ı aktif et
                cmb_folders.DisplayMember = "Name";
                cmb_folders.ValueMember = "Id";
                cmb_folders.SelectedIndex = 0;  // İlk klasörü varsayılan olarak seç
            }
            else
            {
                MessageBox.Show("Drive klasörleri yüklenemedi.");
            }
        }

        private void DriveSelectFolder()
        {
            if (cmb_folders.SelectedIndex == 0)
            {
                DriveDosyaID = "";
            }
            else
            {
                // Seçilen klasörün adını alıyoruz
                string selectedFolderName = cmb_folders.Text;

                // Sözlükten klasör ID'sini alıyoruz
                if (driveFolders.ContainsKey(selectedFolderName))
                {
                    string folderId = driveFolders[selectedFolderName];

                    DriveDosyaID = folderId;
                    // Klasör adı ve ID'sini mesaj kutusunda gösteriyoruz
                }
                else
                {
                    MessageBox.Show("Klasör ID bulunamadı.");
                }
            }




        }

       async void LoginWithSession()
        {
            // Check if the token exists and is valid
            string savedToken = loginGoogle.LoadAccessTokenFromFile();
            if (savedToken != null && await loginGoogle.IsTokenValid(savedToken))
            {
                // Token is valid, no need to log in again
                Userinfo userInfo = await loginGoogle.GetUserInfo(savedToken);
                lbl_googleEmail.Text = userInfo.Email;
                lbl_googleName.Text = userInfo.Name;
                panel_googleDetaylar.Enabled = true;
                DriveLoadFolder();
                isSaveDrive = true;
                btn_googleGiris.Enabled = false;
                btn_googleCikis.Enabled = true;
                isSession = true;
                return;
            }
            else
            {
                btn_googleGiris.Enabled = true;
                btn_googleCikis.Enabled = false ;
                isSession = false;
            }
        }
        private async void btn_googleGiris_Click(object sender, EventArgs e)
        {

            

            try
            {
                string scope = "https://www.googleapis.com/auth/userinfo.profile " +
                               "https://www.googleapis.com/auth/userinfo.email " +
                               "https://www.googleapis.com/auth/drive.file " +
                               "https://www.googleapis.com/auth/drive";

                string authorizationUrl = $"https://accounts.google.com/o/oauth2/v2/auth?" +
                                          $"scope={scope}&" +
                                          $"access_type=offline&" +
                                          $"include_granted_scopes=true&" +
                                          $"response_type=code&" +
                                          $"redirect_uri={loginGoogle.redirectUri}&" +
                                          $"client_id={loginGoogle.clientId}";

                Process.Start(new ProcessStartInfo(authorizationUrl) { UseShellExecute = true });

                string authCode = Microsoft.VisualBasic.Interaction.InputBox("Google yetkilendirme kodunu girin:", "Yetkilendirme Kodu");
                accessToken = await loginGoogle.GetAccessToken(authCode);

                // Save the access token to file
                loginGoogle.SaveAccessTokenToFile(accessToken);

                Userinfo userInfo = await loginGoogle.GetUserInfo(accessToken);
                lbl_googleEmail.Text = userInfo.Email;
                lbl_googleName.Text = userInfo.Name;
                panel_googleDetaylar.Enabled = true;

                DriveLoadFolder();
                btn_googleGiris.Enabled = false;
                btn_googleCikis.Enabled = true;
                isSaveDrive = true;
                isSession = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Giriş başarısız: {ex.Message}");
            }
        }


        private void check_saveGoogle_CheckedChanged(object sender, EventArgs e)
        {
            isSaveDrive = check_saveGoogle.Checked;
            if (check_saveGoogle.Checked)
            {
                panel_Google.Enabled = true;
            }
            else
            {
                panel_Google.Enabled = false;

            }
        }

        private void cmb_folders_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriveSelectFolder();
        }

        private void btn_googleCikis_Click(object sender, EventArgs e)
        {
            // Delete the token file
            loginGoogle.DeleteAccessTokenFile();

            // Clear the UI
            lbl_googleEmail.Text = string.Empty;
            lbl_googleName.Text = string.Empty;
            panel_googleDetaylar.Enabled = false;
            btn_googleGiris.Enabled = true;
            btn_googleCikis.Enabled = false;
            isSession = false;
            MessageBox.Show("Başarıyla çıkış yaptınız.");
        }
    }
}

