using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Backup
{
    public partial class Frm_MainMenu : Form
    {
        private string saveFolderPath = string.Empty;
        ContextMenuStrip kaynakDropdownMenu = new ContextMenuStrip();
        ContextMenuStrip hedefDropdownMenu = new ContextMenuStrip();

        private List<string> kaynakSelectedItems = new List<string>(); // Kaynak Dosya ve klasörleri tutacak liste
        private List<string> hedefSelectedItems = new List<string>(); // Hedef Dosya ve klasörleri tutacak liste
        private string fileName;
        string servisSuresi;
        private string serviceName = "AAAAA_Backup";  // Servis adınızı buraya yazın

        Login _login;
        public Frm_MainMenu(Login login)
        {
            _login = login;

            InitializeComponent();
            comboBox1.SelectedIndex = 0;

            
        }
        

        private  void Frm_MainMenu_Load(object sender, EventArgs e)
        {
            ListViewIslemleri();
            KaynakEkleIslemleri();
            HedefEkleIslemleri();
            lbl_dosyaAdi.Text = txt_dosyaAdi.Text;
            radio_rar.Checked = true;
            //cmb_sikistirmaTuru.SelectedIndex = 0;
            // Ayarları yükle

            LoadXmlDatas();



            toolTip1.SetToolTip(picture_infoDrive, "Drive'da bir klasör oluşturun ve https://drive.google.com/drive/u/0/folders/{FOLDER_ID} kısmındaki folder_ID yi aşağı yazınız.\nAynı zamanda bu klasorü erişim iznini klasör özellikleri / paylaş / genel erişim kısmından Bağlantıya Sahip olan herkes ve rol olarak da düzenleyen verilmesi gerekir.");
            Start();
        }
        void LoadXmlDatas()
        {
            XmlDetaylar.LoadSettings(out fileName, out kaynakSelectedItems, out hedefSelectedItems);



            listBox1.Items.Clear();
            listBox1.Items.AddRange(kaynakSelectedItems.ToArray());

            listBox2.Items.Clear();
            listBox2.Items.AddRange(hedefSelectedItems.ToArray());

            txt_dosyaAdi.Text = fileName;

        }
        void SaveXmlDatas()
        {
            string compressionType;
            if (radio_rar.Checked)
                compressionType = "rar";
            else
                compressionType = "zip";

            fileName = txt_dosyaAdi.Text;
            XmlDetaylar.SaveSettings(kaynakSelectedItems,hedefSelectedItems,fileName,servisSuresi,compressionType);

        }
        void Start()
        {
            if (_login != null)
            {
                MessageBox.Show("Login var:\n" + _login.accessToken);
            }
            //Giriş işlemi sonrası accessToken alınması
            //string accessToken = await _login.GetAccessToken(_login.accessToken);

            //AccessToken'ı XML'e kaydet
            XmlDetaylar.SaveAccessTokenToXml(_login.accessToken);
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
                        break;

                    case 1: // Eğer 2. öğe seçildiyse (index 1)
                        groupBox_Genel.Visible = false;
                        groupBox_Dosya.Visible = true;
                        groupBox_Zamanlayici.Visible = false;
                        break;

                    case 2: // Eğer 3. öğe seçildiyse (index 2)
                        groupBox_Genel.Visible = false;
                        groupBox_Dosya.Visible = false;
                        groupBox_Zamanlayici.Visible = true;
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
                GoogleDriveUploader uploader = new GoogleDriveUploader(_login.accessToken);
                string dosyaID = txt_driveKlasorid.Text;
                //MessageBox.Show("Test"+ archiveFilePath);
                // Dosyayı kullanıcının Drive'ına yükleyin
                await uploader.UploadFileToGoogleDrive(archiveFilePath, dosyaID);
                MessageBox.Show($"Dosya Google Drive'a yüklendi: {archiveFilePath}");
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


              private void KlasorSikistir()
        {
            if (string.IsNullOrEmpty(saveFolderPath))
            {
                MessageBox.Show("Lütfen önce kaydedilecek yeri seçin.");
                return;
            }

            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Bir klasör seçin";
                folderBrowserDialog.ShowNewFolderButton = true;

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string selectedFolderPath = folderBrowserDialog.SelectedPath;
                    string newFileName = !string.IsNullOrEmpty(txt_dosyaAdi.Text) ? txt_dosyaAdi.Text : Path.GetFileName(selectedFolderPath);

                    // Ayarları kaydet
                    //SaveSettings(selectedFolderPath, saveFolderPath, newFileName);
                }
            }
        }
        private void DosyaSikistir()
        {
            if (string.IsNullOrEmpty(saveFolderPath))
            {
                MessageBox.Show("Lütfen önce kaydedilecek yeri seçin.");
                return;
            }

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
                string newFileName = !string.IsNullOrEmpty(txt_dosyaAdi.Text) ? txt_dosyaAdi.Text : Path.GetFileNameWithoutExtension(selectedFilePath);

                // Ayarları kaydet
                //SaveSettings(selectedFilePath, saveFolderPath, newFileName);
            }
        }
        private void ListViewIslemleri()
        {
            ImageList imageList = new ImageList();
            string imagePath1 = Path.Combine(Application.StartupPath, "Foto", "general.png");
            string imagePath2 = Path.Combine(Application.StartupPath, "Foto", "folder.png");
            string imagePath3 = Path.Combine(Application.StartupPath, "Foto", "timer.png");

            // ImageList'e görüntü ekle
            imageList.Images.Add(Image.FromFile(imagePath1)); // Görüntü 1
            imageList.Images.Add(Image.FromFile(imagePath2)); // Görüntü 2
            imageList.Images.Add(Image.FromFile(imagePath3)); // Görüntü 2

            imageList.ImageSize = new Size(28, 28); // Görüntülerin boyutunu 32x32 olarak ayarla

            // ListView ayarları
            listView1.View = View.SmallIcon; // Görüntülerin boyutuna göre LargeIcon da kullanılabilir
            listView1.SmallImageList = imageList;

            // ListView'e öğe ekle
            ListViewItem item1 = new ListViewItem("Genel", 0); // 0, ImageList'teki ilk görüntüye işaret eder
            ListViewItem item2 = new ListViewItem("Dosya", 1); // 1, ikinci görüntü
            ListViewItem item3 = new ListViewItem("Detay", 2); // 1, ikinci görüntü
            listView1.Items.Add(item1);
            listView1.Items.Add(item2);
            listView1.Items.Add(item3);


            string bannerImagePath = Path.Combine(Application.StartupPath, "Foto", "banner.png");
            pictureBox1.Image = Image.FromFile(bannerImagePath);

            groupBox_Genel.Visible = true;
            groupBox_Dosya.Visible = false;
            groupBox_Zamanlayici.Visible = false;
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

        
    }
}

