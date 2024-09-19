using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Backup
{
    public partial class Frm_YeniGorev : Form
    {
        private string saveFolderPath = string.Empty;
       
        public Frm_YeniGorev()
        {
            InitializeComponent();
            radioButton1.Checked = true;
        }

        private void btn_klasor_Click(object sender, EventArgs e)
        {
            // Klasör sıkıştırma işlemi
            KlasorSikistir();
        }

        private void btn_dosya_Click(object sender, EventArgs e)
        {
            // Dosya sıkıştırma işlemi
            DosyaSikistir();
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

        // Ayarları XML dosyasına kaydeder
        private void SaveSettings(string pathToCompress, string destinationPath, string fileName)
        {
            // XML dosyasının proje klasöründeki yolu
            string settingsFilePath = @"C:\Users\Lenovo\source\repos\Backup\BackupService2\bin\Debug\settings.xml";
            if (string.IsNullOrEmpty( settingsFilePath))
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
        void KlasorSikistir()
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
                    string newFileName = !string.IsNullOrEmpty(textBox1.Text) ? textBox1.Text : Path.GetFileName(selectedFolderPath);

                    // Ayarları kaydet
                    SaveSettings(selectedFolderPath, saveFolderPath, newFileName);
                }
            }
        }

        void DosyaSikistir()
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
                string newFileName = !string.IsNullOrEmpty(textBox1.Text) ? textBox1.Text : Path.GetFileNameWithoutExtension(selectedFilePath);

                // Ayarları kaydet
                SaveSettings(selectedFilePath, saveFolderPath, newFileName);
            }
        }

        private void Frm_YeniGorev_Load(object sender, EventArgs e)
        {
            ImageList imageList = new ImageList();
            string imagePath1 = Path.Combine(Application.StartupPath, "Foto", "general.png");
            string imagePath2 = Path.Combine(Application.StartupPath, "Foto", "folder.png");
            string imagePath3 = Path.Combine(Application.StartupPath, "Foto", "timer.png");
            string imagePath4 = Path.Combine(Application.StartupPath, "Foto", "banner.png");

            // ImageList'e görüntü ekle
            imageList.Images.Add(Image.FromFile(imagePath1)); // Görüntü 1
            imageList.Images.Add(Image.FromFile(imagePath2)); // Görüntü 2
            imageList.Images.Add(Image.FromFile(imagePath3)); // Görüntü 2

            imageList.ImageSize = new Size(28,28); // Görüntülerin boyutunu 32x32 olarak ayarla

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

            pictureBox1.Image = Image.FromFile(imagePath4);
        }
    }
}
