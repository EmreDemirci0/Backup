using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Backup
{
    public partial class Frm_YeniGorevim : Form
    {
        private string saveFolderPath = string.Empty;
        ContextMenuStrip dropdownMenu = new ContextMenuStrip();

        public Frm_YeniGorevim()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }
        private void Frm_YeniGorevim_Load(object sender, EventArgs e)
        {
            ListViewIslemleri();
            KaynakEkleIslemleri();
            lbl_dosyaAdi.Text = txt_dosyaAdi.Text;
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
        private void btn_KaydedilecekYer_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.Description = "Zip dosyasını kaydedeceğiniz klasörü seçin";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    saveFolderPath = folderBrowserDialog.SelectedPath;
                    textBox2.Text = "Seçili Yol: " + saveFolderPath;
                }
            }
        }

        private void btn_sikistir_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                KlasorSikistir();
            }
            else
            {
                DosyaSikistir();
            }
        }
        private void txt_dosyaAdi_TextChanged(object sender, EventArgs e)
        {
            lbl_dosyaAdi.Text = txt_dosyaAdi.Text;
        }
        private void btn_kaynakEkle_Click(object sender, EventArgs e)
        {
            //contextMenuStrip1.Show(btn_kaynakEkle,0,btn_kaynakEkle.Height);
            dropdownMenu.Show(btn_kaynakEkle, 0, btn_kaynakEkle.Height);
        }

        private void SaveSettings(string pathToCompress, string destinationPath, string fileName) // Ayarları XML dosyasına kaydeder
        {
            // XML dosyasının proje klasöründeki yolu
            string settingsFilePath = @"C:\Users\Lenovo\source\repos\Backup\BackupService2\bin\Debug\settings.xml";
            if (string.IsNullOrEmpty(settingsFilePath))
            {
                MessageBox.Show("FilePath boş");
                return;
            }
            MessageBox.Show("XML dosyası şu konumda oluşturulacak: " + settingsFilePath);
            var settings = new XElement("Settings",
                new XElement("PathToCompress", pathToCompress),
                new XElement("DestinationPath", destinationPath),
                new XElement("FileName", fileName)
            );

            // Ayarları XML dosyasına kaydet
            settings.Save(settingsFilePath);

            MessageBox.Show("Ayarlar kaydedildi.");
        }
        // Dosya/Klasör sıkıştırma işlemi
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
                    SaveSettings(selectedFolderPath, saveFolderPath, newFileName);
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
                SaveSettings(selectedFilePath, saveFolderPath, newFileName);
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
            ListViewItem item3 = new ListViewItem("Zamanlayıcı", 2); // 1, ikinci görüntü
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
            ToolStripMenuItem item1 = new ToolStripMenuItem("Dosya");
            item1.Image = Image.FromFile(imagePath1); // Resim ekle
            item1.Click += new EventHandler(Item1_Click);

            // İkinci öğeye bir resim ekle
            ToolStripMenuItem item2 = new ToolStripMenuItem("Klasör");
            item2.Image = Image.FromFile(imagePath2); // Resim ekle
            item2.Click += new EventHandler(Item2_Click);


            // Menüye öğeleri ekle
            dropdownMenu.Items.Add(item1);
            dropdownMenu.Items.Add(item2);

        }

        private void Item1_Click(object sender, EventArgs e)
        {
            KlasorSikistir();
        }
        private void Item2_Click(object sender, EventArgs e)
        {
            DosyaSikistir();
        }

       

        
    }
}

