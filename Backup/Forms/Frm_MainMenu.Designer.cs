﻿
namespace Backup
{
    partial class Frm_MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_MainMenu));
            this.klasörToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dosyaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox_Zamanlayici = new System.Windows.Forms.GroupBox();
            this.radio_zip = new System.Windows.Forms.RadioButton();
            this.radio_rar = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox_Genel = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_driveKlasorid = new System.Windows.Forms.TextBox();
            this.lbl_dosyaAdi = new System.Windows.Forms.Label();
            this.txt_dosyaAdi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listView1 = new System.Windows.Forms.ListView();
            this.groupBox_Dosya = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_servisAcKapa = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.picture_infoDrive = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_HedefSil = new System.Windows.Forms.Button();
            this.btn_hedefEkle = new System.Windows.Forms.Button();
            this.Btn_kaynakSil = new System.Windows.Forms.Button();
            this.btn_kaynakEkle = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox_Zamanlayici.SuspendLayout();
            this.groupBox_Genel.SuspendLayout();
            this.groupBox_Dosya.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_infoDrive)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // klasörToolStripMenuItem
            // 
            this.klasörToolStripMenuItem.Name = "klasörToolStripMenuItem";
            this.klasörToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.klasörToolStripMenuItem.Text = "Klasör";
            // 
            // dosyaToolStripMenuItem
            // 
            this.dosyaToolStripMenuItem.Name = "dosyaToolStripMenuItem";
            this.dosyaToolStripMenuItem.Size = new System.Drawing.Size(119, 24);
            this.dosyaToolStripMenuItem.Text = "Dosya";
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
            // groupBox_Zamanlayici
            // 
            this.groupBox_Zamanlayici.Controls.Add(this.pictureBox3);
            this.groupBox_Zamanlayici.Controls.Add(this.pictureBox2);
            this.groupBox_Zamanlayici.Controls.Add(this.radio_zip);
            this.groupBox_Zamanlayici.Controls.Add(this.radio_rar);
            this.groupBox_Zamanlayici.Controls.Add(this.label5);
            this.groupBox_Zamanlayici.Controls.Add(this.label4);
            this.groupBox_Zamanlayici.Controls.Add(this.comboBox1);
            this.groupBox_Zamanlayici.Location = new System.Drawing.Point(1089, 39);
            this.groupBox_Zamanlayici.Name = "groupBox_Zamanlayici";
            this.groupBox_Zamanlayici.Size = new System.Drawing.Size(317, 426);
            this.groupBox_Zamanlayici.TabIndex = 19;
            this.groupBox_Zamanlayici.TabStop = false;
            this.groupBox_Zamanlayici.Text = "Detaylar";
            // 
            // radio_zip
            // 
            this.radio_zip.AutoSize = true;
            this.radio_zip.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radio_zip.Location = new System.Drawing.Point(67, 316);
            this.radio_zip.Name = "radio_zip";
            this.radio_zip.Size = new System.Drawing.Size(67, 36);
            this.radio_zip.TabIndex = 13;
            this.radio_zip.TabStop = true;
            this.radio_zip.Text = "Zip";
            this.radio_zip.UseVisualStyleBackColor = true;
            // 
            // radio_rar
            // 
            this.radio_rar.AutoSize = true;
            this.radio_rar.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.radio_rar.Location = new System.Drawing.Point(67, 274);
            this.radio_rar.Name = "radio_rar";
            this.radio_rar.Size = new System.Drawing.Size(67, 36);
            this.radio_rar.TabIndex = 12;
            this.radio_rar.TabStop = true;
            this.radio_rar.Text = "Rar";
            this.radio_rar.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(20, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(228, 28);
            this.label5.TabIndex = 11;
            this.label5.Text = "Sıkıştırma Türünü Seçiniz";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(20, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(214, 28);
            this.label4.TabIndex = 9;
            this.label4.Text = "Tekrar Zamanını Seçiniz";
            // 
            // comboBox1
            // 
            this.comboBox1.AllowDrop = true;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
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
            this.comboBox1.Location = new System.Drawing.Point(20, 124);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(252, 40);
            this.comboBox1.TabIndex = 8;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // groupBox_Genel
            // 
            this.groupBox_Genel.Controls.Add(this.picture_infoDrive);
            this.groupBox_Genel.Controls.Add(this.label6);
            this.groupBox_Genel.Controls.Add(this.txt_driveKlasorid);
            this.groupBox_Genel.Controls.Add(this.lbl_dosyaAdi);
            this.groupBox_Genel.Controls.Add(this.pictureBox1);
            this.groupBox_Genel.Controls.Add(this.txt_dosyaAdi);
            this.groupBox_Genel.Controls.Add(this.label2);
            this.groupBox_Genel.Location = new System.Drawing.Point(251, 26);
            this.groupBox_Genel.Name = "groupBox_Genel";
            this.groupBox_Genel.Size = new System.Drawing.Size(350, 430);
            this.groupBox_Genel.TabIndex = 17;
            this.groupBox_Genel.TabStop = false;
            this.groupBox_Genel.Text = "Genel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(6, 280);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(293, 32);
            this.label6.TabIndex = 5;
            this.label6.Text = "Drive Klasör ID\'sini Giriniz.";
            // 
            // txt_driveKlasorid
            // 
            this.txt_driveKlasorid.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_driveKlasorid.Location = new System.Drawing.Point(12, 315);
            this.txt_driveKlasorid.Name = "txt_driveKlasorid";
            this.txt_driveKlasorid.Size = new System.Drawing.Size(325, 39);
            this.txt_driveKlasorid.TabIndex = 4;
            // 
            // lbl_dosyaAdi
            // 
            this.lbl_dosyaAdi.AutoSize = true;
            this.lbl_dosyaAdi.BackColor = System.Drawing.Color.Transparent;
            this.lbl_dosyaAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lbl_dosyaAdi.Location = new System.Drawing.Point(40, 71);
            this.lbl_dosyaAdi.Name = "lbl_dosyaAdi";
            this.lbl_dosyaAdi.Size = new System.Drawing.Size(0, 25);
            this.lbl_dosyaAdi.TabIndex = 3;
            // 
            // txt_dosyaAdi
            // 
            this.txt_dosyaAdi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_dosyaAdi.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_dosyaAdi.Location = new System.Drawing.Point(9, 186);
            this.txt_dosyaAdi.Name = "txt_dosyaAdi";
            this.txt_dosyaAdi.Size = new System.Drawing.Size(298, 35);
            this.txt_dosyaAdi.TabIndex = 0;
            this.txt_dosyaAdi.Text = "Yeni Görev";
            this.txt_dosyaAdi.TextChanged += new System.EventHandler(this.txt_dosyaAdi_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(5, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 29);
            this.label2.TabIndex = 1;
            this.label2.Text = "Görev Adı";
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Location = new System.Drawing.Point(18, 43);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(449, 106);
            this.listBox1.TabIndex = 9;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // listView1
            // 
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(25, 26);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(220, 430);
            this.listView1.TabIndex = 18;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // groupBox_Dosya
            // 
            this.groupBox_Dosya.Controls.Add(this.label3);
            this.groupBox_Dosya.Controls.Add(this.label1);
            this.groupBox_Dosya.Controls.Add(this.btn_HedefSil);
            this.groupBox_Dosya.Controls.Add(this.btn_hedefEkle);
            this.groupBox_Dosya.Controls.Add(this.listBox2);
            this.groupBox_Dosya.Controls.Add(this.Btn_kaynakSil);
            this.groupBox_Dosya.Controls.Add(this.btn_kaynakEkle);
            this.groupBox_Dosya.Controls.Add(this.listBox1);
            this.groupBox_Dosya.Location = new System.Drawing.Point(610, 30);
            this.groupBox_Dosya.Name = "groupBox_Dosya";
            this.groupBox_Dosya.Size = new System.Drawing.Size(473, 426);
            this.groupBox_Dosya.TabIndex = 16;
            this.groupBox_Dosya.TabStop = false;
            this.groupBox_Dosya.Text = "Dosya";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 19);
            this.label3.TabIndex = 16;
            this.label3.Text = "Hedef";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 15;
            this.label1.Text = "Kaynak";
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 17;
            this.listBox2.Location = new System.Drawing.Point(18, 232);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(449, 106);
            this.listBox2.TabIndex = 12;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(120, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 24);
            this.toolStripMenuItem1.Text = "Dosya";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(418, 476);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 48);
            this.button1.TabIndex = 20;
            this.button1.Text = "Ayarları Uygula";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btn_sikistir_Click);
            // 
            // btn_servisAcKapa
            // 
            this.btn_servisAcKapa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_servisAcKapa.Location = new System.Drawing.Point(25, 476);
            this.btn_servisAcKapa.Name = "btn_servisAcKapa";
            this.btn_servisAcKapa.Size = new System.Drawing.Size(220, 48);
            this.btn_servisAcKapa.TabIndex = 21;
            this.btn_servisAcKapa.Text = "Servisi Başlat/Bitir";
            this.btn_servisAcKapa.UseVisualStyleBackColor = true;
            this.btn_servisAcKapa.Click += new System.EventHandler(this.btn_servisBaslat_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 200;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Drive Klasör ID";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Backup.Properties.Resources.zip;
            this.pictureBox3.Location = new System.Drawing.Point(25, 316);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(36, 36);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 15;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Backup.Properties.Resources.winrar;
            this.pictureBox2.Location = new System.Drawing.Point(25, 274);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(36, 36);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // picture_infoDrive
            // 
            this.picture_infoDrive.Image = global::Backup.Properties.Resources.info;
            this.picture_infoDrive.Location = new System.Drawing.Point(305, 280);
            this.picture_infoDrive.Name = "picture_infoDrive";
            this.picture_infoDrive.Size = new System.Drawing.Size(32, 32);
            this.picture_infoDrive.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture_infoDrive.TabIndex = 7;
            this.picture_infoDrive.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(9, 37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(298, 84);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // btn_HedefSil
            // 
            this.btn_HedefSil.Image = global::Backup.Properties.Resources.delete1;
            this.btn_HedefSil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_HedefSil.Location = new System.Drawing.Point(127, 344);
            this.btn_HedefSil.Name = "btn_HedefSil";
            this.btn_HedefSil.Size = new System.Drawing.Size(103, 34);
            this.btn_HedefSil.TabIndex = 14;
            this.btn_HedefSil.Text = "Sil";
            this.btn_HedefSil.UseVisualStyleBackColor = true;
            this.btn_HedefSil.Click += new System.EventHandler(this.btn_HedefSil_Click);
            // 
            // btn_hedefEkle
            // 
            this.btn_hedefEkle.Image = global::Backup.Properties.Resources.icons8_file_48__1_;
            this.btn_hedefEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_hedefEkle.Location = new System.Drawing.Point(18, 344);
            this.btn_hedefEkle.Name = "btn_hedefEkle";
            this.btn_hedefEkle.Size = new System.Drawing.Size(103, 34);
            this.btn_hedefEkle.TabIndex = 13;
            this.btn_hedefEkle.Text = "Ekle";
            this.btn_hedefEkle.UseVisualStyleBackColor = true;
            this.btn_hedefEkle.Click += new System.EventHandler(this.btn_hedefEkle_Click);
            // 
            // Btn_kaynakSil
            // 
            this.Btn_kaynakSil.Image = global::Backup.Properties.Resources.delete1;
            this.Btn_kaynakSil.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Btn_kaynakSil.Location = new System.Drawing.Point(127, 155);
            this.Btn_kaynakSil.Name = "Btn_kaynakSil";
            this.Btn_kaynakSil.Size = new System.Drawing.Size(103, 34);
            this.Btn_kaynakSil.TabIndex = 11;
            this.Btn_kaynakSil.Text = "Sil";
            this.Btn_kaynakSil.UseVisualStyleBackColor = true;
            this.Btn_kaynakSil.Click += new System.EventHandler(this.Btn_kaynakSil_Click);
            // 
            // btn_kaynakEkle
            // 
            this.btn_kaynakEkle.Image = global::Backup.Properties.Resources.icons8_file_48__1_;
            this.btn_kaynakEkle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_kaynakEkle.Location = new System.Drawing.Point(18, 155);
            this.btn_kaynakEkle.Name = "btn_kaynakEkle";
            this.btn_kaynakEkle.Size = new System.Drawing.Size(103, 34);
            this.btn_kaynakEkle.TabIndex = 10;
            this.btn_kaynakEkle.Text = "Ekle";
            this.btn_kaynakEkle.UseVisualStyleBackColor = true;
            this.btn_kaynakEkle.Click += new System.EventHandler(this.btn_kaynakEkle_Click);
            // 
            // Frm_MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 547);
            this.Controls.Add(this.btn_servisAcKapa);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox_Zamanlayici);
            this.Controls.Add(this.groupBox_Genel);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.groupBox_Dosya);
            this.Font = new System.Drawing.Font("Segoe UI", 8.139131F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Frm_MainMenu";
            this.Text = "Backup Sayfası";
            this.Load += new System.EventHandler(this.Frm_MainMenu_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox_Zamanlayici.ResumeLayout(false);
            this.groupBox_Zamanlayici.PerformLayout();
            this.groupBox_Genel.ResumeLayout(false);
            this.groupBox_Genel.PerformLayout();
            this.groupBox_Dosya.ResumeLayout(false);
            this.groupBox_Dosya.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture_infoDrive)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem klasörToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dosyaToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox groupBox_Zamanlayici;
        private System.Windows.Forms.GroupBox groupBox_Genel;
        private System.Windows.Forms.Label lbl_dosyaAdi;
        private System.Windows.Forms.TextBox txt_dosyaAdi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Button btn_kaynakEkle;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.GroupBox groupBox_Dosya;
        private System.Windows.Forms.Button Btn_kaynakSil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_HedefSil;
        private System.Windows.Forms.Button btn_hedefEkle;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_servisAcKapa;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton radio_zip;
        private System.Windows.Forms.RadioButton radio_rar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_driveKlasorid;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox picture_infoDrive;
    }
}