namespace Document_Classify
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_choose = new System.Windows.Forms.Button();
            this.folderbrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.test_results = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.grpbx_calculatedvalues = new System.Windows.Forms.GroupBox();
            this.txt_oran = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.spor = new System.Windows.Forms.RichTextBox();
            this.saglik = new System.Windows.Forms.RichTextBox();
            this.magazin = new System.Windows.Forms.RichTextBox();
            this.ekonomi = new System.Windows.Forms.RichTextBox();
            this.siyasi = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpbx_calculatedvalues.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_choose
            // 
            this.btn_choose.Location = new System.Drawing.Point(12, 266);
            this.btn_choose.Name = "btn_choose";
            this.btn_choose.Size = new System.Drawing.Size(124, 27);
            this.btn_choose.TabIndex = 0;
            this.btn_choose.Text = "Döküman Seç";
            this.btn_choose.UseVisualStyleBackColor = true;
            this.btn_choose.Click += new System.EventHandler(this.btn_choose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(445, 248);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // test_results
            // 
            this.test_results.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.test_results.FormattingEnabled = true;
            this.test_results.ItemHeight = 15;
            this.test_results.Location = new System.Drawing.Point(10, 39);
            this.test_results.Name = "test_results";
            this.test_results.Size = new System.Drawing.Size(182, 379);
            this.test_results.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(186, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Test Kümesi Tahmini Kategoriler";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(222, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(232, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Precision, Recall Ve F-Measure Değerleri";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(222, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ekonomi";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(222, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "Magazin";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(222, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Sağlık";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(222, 349);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "Spor";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(222, 280);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "Siyasi";
            // 
            // grpbx_calculatedvalues
            // 
            this.grpbx_calculatedvalues.Controls.Add(this.siyasi);
            this.grpbx_calculatedvalues.Controls.Add(this.txt_oran);
            this.grpbx_calculatedvalues.Controls.Add(this.label8);
            this.grpbx_calculatedvalues.Controls.Add(this.spor);
            this.grpbx_calculatedvalues.Controls.Add(this.saglik);
            this.grpbx_calculatedvalues.Controls.Add(this.magazin);
            this.grpbx_calculatedvalues.Controls.Add(this.ekonomi);
            this.grpbx_calculatedvalues.Controls.Add(this.label2);
            this.grpbx_calculatedvalues.Controls.Add(this.test_results);
            this.grpbx_calculatedvalues.Controls.Add(this.label1);
            this.grpbx_calculatedvalues.Controls.Add(this.label6);
            this.grpbx_calculatedvalues.Controls.Add(this.label7);
            this.grpbx_calculatedvalues.Controls.Add(this.label3);
            this.grpbx_calculatedvalues.Controls.Add(this.label4);
            this.grpbx_calculatedvalues.Controls.Add(this.label5);
            this.grpbx_calculatedvalues.Location = new System.Drawing.Point(463, 12);
            this.grpbx_calculatedvalues.Name = "grpbx_calculatedvalues";
            this.grpbx_calculatedvalues.Size = new System.Drawing.Size(498, 436);
            this.grpbx_calculatedvalues.TabIndex = 10;
            this.grpbx_calculatedvalues.TabStop = false;
            this.grpbx_calculatedvalues.Text = "Hesaplanan Değerler";
            this.grpbx_calculatedvalues.Visible = false;
            // 
            // txt_oran
            // 
            this.txt_oran.Location = new System.Drawing.Point(343, 398);
            this.txt_oran.Name = "txt_oran";
            this.txt_oran.Size = new System.Drawing.Size(100, 20);
            this.txt_oran.TabIndex = 11;
            this.txt_oran.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(198, 398);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(139, 20);
            this.label8.TabIndex = 15;
            this.label8.Text = "Bayes Hesaplama Oranı";
            // 
            // spor
            // 
            this.spor.Location = new System.Drawing.Point(310, 337);
            this.spor.Name = "spor";
            this.spor.Size = new System.Drawing.Size(182, 45);
            this.spor.TabIndex = 14;
            this.spor.Text = "";
            // 
            // saglik
            // 
            this.saglik.Location = new System.Drawing.Point(310, 203);
            this.saglik.Name = "saglik";
            this.saglik.Size = new System.Drawing.Size(182, 45);
            this.saglik.TabIndex = 12;
            this.saglik.Text = "";
            // 
            // magazin
            // 
            this.magazin.Location = new System.Drawing.Point(310, 133);
            this.magazin.Name = "magazin";
            this.magazin.Size = new System.Drawing.Size(182, 45);
            this.magazin.TabIndex = 11;
            this.magazin.Text = "";
            // 
            // ekonomi
            // 
            this.ekonomi.Location = new System.Drawing.Point(310, 54);
            this.ekonomi.Name = "ekonomi";
            this.ekonomi.Size = new System.Drawing.Size(182, 45);
            this.ekonomi.TabIndex = 10;
            this.ekonomi.Text = "";
            // 
            // siyasi
            // 
            this.siyasi.Location = new System.Drawing.Point(310, 268);
            this.siyasi.Name = "siyasi";
            this.siyasi.Size = new System.Drawing.Size(182, 45);
            this.siyasi.TabIndex = 16;
            this.siyasi.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1150, 458);
            this.Controls.Add(this.grpbx_calculatedvalues);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_choose);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "News_Classifier";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpbx_calculatedvalues.ResumeLayout(false);
            this.grpbx_calculatedvalues.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_choose;
        private System.Windows.Forms.FolderBrowserDialog folderbrowser;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox test_results;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox grpbx_calculatedvalues;
        private System.Windows.Forms.RichTextBox spor;
        private System.Windows.Forms.RichTextBox saglik;
        private System.Windows.Forms.RichTextBox magazin;
        private System.Windows.Forms.RichTextBox ekonomi;
        private System.Windows.Forms.TextBox txt_oran;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox siyasi;
    }
}

