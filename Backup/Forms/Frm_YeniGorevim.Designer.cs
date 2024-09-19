
namespace Backup
{
    partial class Frm_YeniGorevim
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btn_KaydedilecekYer = new System.Windows.Forms.Button();
            this.btn_sikistir = new System.Windows.Forms.Button();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox_Dosya = new System.Windows.Forms.GroupBox();
            this.btn_kaynakEkle = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.txt_dosyaAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_dosyaAdi = new System.Windows.Forms.Label();
            this.groupBox_Genel = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox_Zamanlayici = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.klasörToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox_Dosya.SuspendLayout();
            this.groupBox_Genel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox_Zamanlayici.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(251, 401);
            this.listView1.TabIndex = 14;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_KaydedilecekYer
            // 
            this.btn_KaydedilecekYer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_KaydedilecekYer.Location = new System.Drawing.Point(33, 356);
            this.btn_KaydedilecekYer.Name = "btn_KaydedilecekYer";
            this.btn_KaydedilecekYer.Size = new System.Drawing.Size(311, 62);
            this.btn_KaydedilecekYer.TabIndex = 4;
            this.btn_KaydedilecekYer.Text = "Kayıt Edilecek Yeri Seçiniz";
            this.btn_KaydedilecekYer.UseVisualStyleBackColor = true;
            this.btn_KaydedilecekYer.Click += new System.EventHandler(this.btn_KaydedilecekYer_Click);
            // 
            // btn_sikistir
            // 
            this.btn_sikistir.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_sikistir.Location = new System.Drawing.Point(33, 535);
            this.btn_sikistir.Name = "btn_sikistir";
            this.btn_sikistir.Size = new System.Drawing.Size(311, 56);
            this.btn_sikistir.TabIndex = 8;
            this.btn_sikistir.Text = "Sıkıştır";
            this.btn_sikistir.UseVisualStyleBackColor = true;
            this.btn_sikistir.Click += new System.EventHandler(this.btn_sikistir_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radioButton2.Location = new System.Drawing.Point(162, 471);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(98, 33);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Dosya";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radioButton1.Location = new System.Drawing.Point(33, 471);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(100, 33);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Klasör";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBox2.Location = new System.Drawing.Point(33, 424);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(311, 29);
            this.textBox2.TabIndex = 5;
            // 
            // groupBox_Dosya
            // 
            this.groupBox_Dosya.Controls.Add(this.btn_kaynakEkle);
            this.groupBox_Dosya.Controls.Add(this.listBox1);
            this.groupBox_Dosya.Controls.Add(this.btn_KaydedilecekYer);
            this.groupBox_Dosya.Controls.Add(this.btn_sikistir);
            this.groupBox_Dosya.Controls.Add(this.radioButton2);
            this.groupBox_Dosya.Controls.Add(this.radioButton1);
            this.groupBox_Dosya.Controls.Add(this.textBox2);
            this.groupBox_Dosya.Location = new System.Drawing.Point(639, 12);
            this.groupBox_Dosya.Name = "groupBox_Dosya";
            this.groupBox_Dosya.Size = new System.Drawing.Size(362, 626);
            this.groupBox_Dosya.TabIndex = 12;
            this.groupBox_Dosya.TabStop = false;
            this.groupBox_Dosya.Text = "Dosya";
            // 
            // btn_kaynakEkle
            // 
            this.btn_kaynakEkle.Image = global::Backup.Properties.Resources.icons8_file_48__1_;
            this.btn_kaynakEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_kaynakEkle.Location = new System.Drawing.Point(33, 248);
            this.btn_kaynakEkle.Name = "btn_kaynakEkle";
            this.btn_kaynakEkle.Size = new System.Drawing.Size(118, 32);
            this.btn_kaynakEkle.TabIndex = 10;
            this.btn_kaynakEkle.Text = "Ekle";
            this.btn_kaynakEkle.UseVisualStyleBackColor = true;
            this.btn_kaynakEkle.Click += new System.EventHandler(this.btn_kaynakEkle_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(33, 35);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(311, 196);
            this.listBox1.TabIndex = 9;
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // txt_dosyaAdi
            // 
            this.txt_dosyaAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dosyaAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_dosyaAdi.Location = new System.Drawing.Point(10, 175);
            this.txt_dosyaAdi.Name = "txt_dosyaAdi";
            this.txt_dosyaAdi.Size = new System.Drawing.Size(340, 35);
            this.txt_dosyaAdi.TabIndex = 0;
            this.txt_dosyaAdi.Text = "Yeni Görev";
            this.txt_dosyaAdi.TextChanged += new System.EventHandler(this.txt_dosyaAdi_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(6, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Görev Adı";
            // 
            // lbl_dosyaAdi
            // 
            this.lbl_dosyaAdi.AutoSize = true;
            this.lbl_dosyaAdi.BackColor = System.Drawing.Color.Transparent;
            this.lbl_dosyaAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_dosyaAdi.Location = new System.Drawing.Point(125, 67);
            this.lbl_dosyaAdi.Name = "lbl_dosyaAdi";
            this.lbl_dosyaAdi.Size = new System.Drawing.Size(0, 25);
            this.lbl_dosyaAdi.TabIndex = 3;
            // 
            // groupBox_Genel
            // 
            this.groupBox_Genel.Controls.Add(this.lbl_dosyaAdi);
            this.groupBox_Genel.Controls.Add(this.pictureBox1);
            this.groupBox_Genel.Controls.Add(this.txt_dosyaAdi);
            this.groupBox_Genel.Controls.Add(this.label2);
            this.groupBox_Genel.Location = new System.Drawing.Point(271, 12);
            this.groupBox_Genel.Name = "groupBox_Genel";
            this.groupBox_Genel.Size = new System.Drawing.Size(362, 401);
            this.groupBox_Genel.TabIndex = 13;
            this.groupBox_Genel.TabStop = false;
            this.groupBox_Genel.Text = "Genel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(10, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(340, 79);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox_Zamanlayici
            // 
            this.groupBox_Zamanlayici.Controls.Add(this.comboBox1);
            this.groupBox_Zamanlayici.Location = new System.Drawing.Point(1017, 12);
            this.groupBox_Zamanlayici.Name = "groupBox_Zamanlayici";
            this.groupBox_Zamanlayici.Size = new System.Drawing.Size(362, 401);
            this.groupBox_Zamanlayici.TabIndex = 15;
            this.groupBox_Zamanlayici.TabStop = false;
            this.groupBox_Zamanlayici.Text = "Zamanlayıcı";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaToolStripMenuItem,
            this.klasörToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(120, 52);
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.dosyaToolStripMenuItem.Text = "Dosya";
            // 
            // klasörToolStripMenuItem
            // 
            this.klasörToolStripMenuItem.Name = "klasörToolStripMenuItem";
            this.klasörToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.klasörToolStripMenuItem.Text = "Klasör";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "5 dk\'da 1",
            "15 dk\'da 1",
            "30 dk\'da 1",
            "1 Saatte 1",
            "3 Saatte 1",
            "6 Saatte 1",
            "12 Saatte 1",
            "1 Günde 1",
            "2 Günde 1",
            "7 Günde 1"});
            this.comboBox1.Location = new System.Drawing.Point(18, 67);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(303, 24);
            this.comboBox1.TabIndex = 0;
            // 
            // Frm_YeniGorevim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 650);
            this.Controls.Add(this.groupBox_Zamanlayici);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox_Dosya);
            this.Controls.Add(this.groupBox_Genel);
            this.Name = "Frm_YeniGorevim";
            this.Text = "Frm_YeniGorevim";
            this.Load += new System.EventHandler(this.Frm_YeniGorevim_Load);
            this.groupBox_Dosya.ResumeLayout(false);
            this.groupBox_Dosya.PerformLayout();
            this.groupBox_Genel.ResumeLayout(false);
            this.groupBox_Genel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox_Zamanlayici.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btn_KaydedilecekYer;
        private System.Windows.Forms.Button btn_sikistir;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox_Dosya;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txt_dosyaAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_dosyaAdi;
        private System.Windows.Forms.GroupBox groupBox_Genel;
        private System.Windows.Forms.GroupBox groupBox_Zamanlayici;
        private System.Windows.Forms.Button btn_kaynakEkle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem klasörToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}