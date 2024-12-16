namespace SymulacjaGryTaktycznejWinFormTEB
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnPojedynek;
        private System.Windows.Forms.TextBox txtWynik;
        private System.Windows.Forms.Button btnWalka;
        private System.Windows.Forms.Button btnWojna;
        private System.Windows.Forms.PictureBox picWojownik;
        private System.Windows.Forms.PictureBox picMag;
        private System.Windows.Forms.Panel pnlMap;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnPojedynek = new System.Windows.Forms.Button();
            this.txtWynik = new System.Windows.Forms.TextBox();
            this.btnWalka = new System.Windows.Forms.Button();
            this.btnWojna = new System.Windows.Forms.Button();
            this.picWojownik = new System.Windows.Forms.PictureBox();
            this.picMag = new System.Windows.Forms.PictureBox();
            this.pnlMap = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picWojownik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMag)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPojedynek
            // 
            this.btnPojedynek.Location = new System.Drawing.Point(12, 12);
            this.btnPojedynek.Name = "btnPojedynek";
            this.btnPojedynek.Size = new System.Drawing.Size(75, 23);
            this.btnPojedynek.TabIndex = 0;
            this.btnPojedynek.Text = "Pojedynek";
            this.btnPojedynek.UseVisualStyleBackColor = true;
            this.btnPojedynek.Click += new System.EventHandler(this.btnPojedynek_Click);
            // 
            // txtWynik
            // 
            this.txtWynik.Location = new System.Drawing.Point(12, 41);
            this.txtWynik.Multiline = true;
            this.txtWynik.Name = "txtWynik";
            this.txtWynik.Size = new System.Drawing.Size(776, 100);
            this.txtWynik.TabIndex = 1;
            // 
            // btnWalka
            // 
            this.btnWalka.Location = new System.Drawing.Point(93, 12);
            this.btnWalka.Name = "btnWalka";
            this.btnWalka.Size = new System.Drawing.Size(75, 23);
            this.btnWalka.TabIndex = 2;
            this.btnWalka.Text = "Walka";
            this.btnWalka.UseVisualStyleBackColor = true;
            this.btnWalka.Click += new System.EventHandler(this.btnWalka_Click);
            // 
            // btnWojna
            // 
            this.btnWojna.Location = new System.Drawing.Point(174, 12);
            this.btnWojna.Name = "btnWojna";
            this.btnWojna.Size = new System.Drawing.Size(75, 23);
            this.btnWojna.TabIndex = 3;
            this.btnWojna.Text = "Wojna";
            this.btnWojna.UseVisualStyleBackColor = true;
            this.btnWojna.Click += new System.EventHandler(this.btnWojna_Click);
            // 
            // picWojownik
            // 
            this.picWojownik.Location = new System.Drawing.Point(12, 147);
            this.picWojownik.Name = "picWojownik";
            this.picWojownik.Size = new System.Drawing.Size(100, 100);
            this.picWojownik.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWojownik.TabIndex = 4;
            this.picWojownik.TabStop = false;
            // 
            // picMag
            // 
            this.picMag.Location = new System.Drawing.Point(688, 147);
            this.picMag.Name = "picMag";
            this.picMag.Size = new System.Drawing.Size(100, 100);
            this.picMag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMag.TabIndex = 5;
            this.picMag.TabStop = false;
            // 
            // pnlMap
            // 
            this.pnlMap.Location = new System.Drawing.Point(118, 147);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(564, 291);
            this.pnlMap.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlMap);
            this.Controls.Add(this.picMag);
            this.Controls.Add(this.picWojownik);
            this.Controls.Add(this.btnWojna);
            this.Controls.Add(this.btnWalka);
            this.Controls.Add(this.txtWynik);
            this.Controls.Add(this.btnPojedynek);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picWojownik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMag)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
