using System.Media;
using WMPLib;

namespace SymulacjaGryTaktycznejWinFormTEB
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel pnlBattlefield;
        private System.Windows.Forms.PictureBox picWojownik;
        private System.Windows.Forms.PictureBox picMag;
        private System.Windows.Forms.Button btnPojedynek;
        private System.Windows.Forms.Button btnWalka;
        private System.Windows.Forms.Button btnWojna;
        private System.Windows.Forms.TextBox txtWynik;
        private System.Windows.Forms.Label lblElapsedTime;

        private WindowsMediaPlayer berserkSound;
        private WindowsMediaPlayer shieldBlockSound;
        private WindowsMediaPlayer doubleStrikeSound;
        private WindowsMediaPlayer fireballSound;
        private WindowsMediaPlayer healSound;
        private WindowsMediaPlayer manaShieldSound;

        // BATTLE SOUNDS
        private WindowsMediaPlayer battleCrySound;
        private WindowsMediaPlayer arrowRainSound;
        private WindowsMediaPlayer warDrumSound;

        private System.Windows.Forms.ProgressBar pbWojownikHP;
        private System.Windows.Forms.ProgressBar pbMagHP;
        private System.Windows.Forms.Label lblWojownikUnits;
        private System.Windows.Forms.Label lblMagUnits;

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
            lblWojownikHP = new Label();
            lblMagHP = new Label();
            pnlBattlefield = new Panel();
            picWojownik = new PictureBox();
            picMag = new PictureBox();
            pbWojownikHP = new ProgressBar();
            pbMagHP = new ProgressBar();
            lblWojownikUnits = new Label();
            lblMagUnits = new Label();
            btnPojedynek = new Button();
            btnWalka = new Button();
            btnWojna = new Button();
            txtWynik = new TextBox();
            lblElapsedTime = new Label();
            pnlBattlefield.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picWojownik).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMag).BeginInit();
            SuspendLayout();
            // 
            // lblWojownikHP
            // 
            lblWojownikHP.AutoSize = true;
            lblWojownikHP.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWojownikHP.Location = new Point(50, 210);
            lblWojownikHP.Name = "lblWojownikHP";
            lblWojownikHP.Size = new Size(73, 20);
            lblWojownikHP.TabIndex = 0;
            lblWojownikHP.Text = "HP: 100";
            // 
            // lblMagHP
            // 
            lblMagHP.AutoSize = true;
            lblMagHP.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMagHP.Location = new Point(626, 210);
            lblMagHP.Name = "lblMagHP";
            lblMagHP.Size = new Size(73, 20);
            lblMagHP.TabIndex = 1;
            lblMagHP.Text = "HP: 100";
            // 
            // pnlBattlefield
            // 
            pnlBattlefield.Controls.Add(picWojownik);
            pnlBattlefield.Controls.Add(picMag);
            pnlBattlefield.Controls.Add(lblWojownikHP);
            pnlBattlefield.Controls.Add(lblMagHP);
            pnlBattlefield.Controls.Add(pbWojownikHP);
            pnlBattlefield.Controls.Add(pbMagHP);
            pnlBattlefield.Controls.Add(lblWojownikUnits);
            pnlBattlefield.Controls.Add(lblMagUnits);
            pnlBattlefield.Location = new Point(12, 12);
            pnlBattlefield.Name = "pnlBattlefield";
            pnlBattlefield.Size = new Size(776, 300);
            pnlBattlefield.TabIndex = 0;
            // 
            // picWojownik
            // 
            picWojownik.Location = new Point(50, 100);
            picWojownik.Name = "picWojownik";
            picWojownik.Size = new Size(100, 100);
            picWojownik.SizeMode = PictureBoxSizeMode.StretchImage;
            picWojownik.TabIndex = 0;
            picWojownik.TabStop = false;
            // 
            // picMag
            // 
            picMag.Location = new Point(626, 100);
            picMag.Name = "picMag";
            picMag.Size = new Size(100, 100);
            picMag.SizeMode = PictureBoxSizeMode.StretchImage;
            picMag.TabIndex = 1;
            picMag.TabStop = false;
            // 
            // pbWojownikHP
            // 
            pbWojownikHP.Location = new Point(50, 206);
            pbWojownikHP.Name = "pbWojownikHP";
            pbWojownikHP.Size = new Size(100, 23);
            pbWojownikHP.TabIndex = 2;
            pbWojownikHP.Value = 100;
            pbWojownikHP.Visible = false;
            // 
            // pbMagHP
            // 
            pbMagHP.Location = new Point(626, 206);
            pbMagHP.Name = "pbMagHP";
            pbMagHP.Size = new Size(100, 23);
            pbMagHP.TabIndex = 2;
            pbMagHP.Value = 100;
            pbMagHP.Visible = false;
            // 
            // lblWojownikUnits
            // 
            lblWojownikUnits.AutoSize = true;
            lblWojownikUnits.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWojownikUnits.Location = new Point(50, 232);
            lblWojownikUnits.Name = "lblWojownikUnits";
            lblWojownikUnits.Size = new Size(116, 20);
            lblWojownikUnits.TabIndex = 4;
            lblWojownikUnits.Text = "Jednostki: 30";
            lblWojownikUnits.Visible = false;
            // 
            // lblMagUnits
            // 
            lblMagUnits.AutoSize = true;
            lblMagUnits.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMagUnits.Location = new Point(626, 232);
            lblMagUnits.Name = "lblMagUnits";
            lblMagUnits.Size = new Size(116, 20);
            lblMagUnits.TabIndex = 5;
            lblMagUnits.Text = "Jednostki: 15";
            lblMagUnits.Visible = false;
            // 
            // btnPojedynek
            // 
            btnPojedynek.Location = new Point(12, 318);
            btnPojedynek.Name = "btnPojedynek";
            btnPojedynek.Size = new Size(75, 23);
            btnPojedynek.TabIndex = 1;
            btnPojedynek.Text = "Pojedynek";
            btnPojedynek.UseVisualStyleBackColor = true;
            btnPojedynek.Click += btnPojedynek_Click;
            // 
            // btnWalka
            // 
            btnWalka.Location = new Point(93, 318);
            btnWalka.Name = "btnWalka";
            btnWalka.Size = new Size(75, 23);
            btnWalka.TabIndex = 2;
            btnWalka.Text = "Walka";
            btnWalka.UseVisualStyleBackColor = true;
            btnWalka.Click += btnWalka_Click;
            // 
            // btnWojna
            // 
            btnWojna.Location = new Point(174, 318);
            btnWojna.Name = "btnWojna";
            btnWojna.Size = new Size(75, 23);
            btnWojna.TabIndex = 3;
            btnWojna.Text = "Wojna";
            btnWojna.UseVisualStyleBackColor = true;
            btnWojna.Click += btnWojna_Click;
            // 
            // txtWynik
            // 
            txtWynik.Location = new Point(12, 347);
            txtWynik.Multiline = true;
            txtWynik.Name = "txtWynik";
            txtWynik.ScrollBars = ScrollBars.Vertical;
            txtWynik.Size = new Size(776, 91);
            txtWynik.TabIndex = 4;
            // 
            // lblElapsedTime
            // 
            lblElapsedTime.AutoSize = true;
            lblElapsedTime.Font = new Font("Segoe UI", 12F);
            lblElapsedTime.Location = new Point(12, 9);
            lblElapsedTime.Name = "lblElapsedTime";
            lblElapsedTime.Size = new Size(0, 21);
            lblElapsedTime.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 500);
            Controls.Add(lblElapsedTime);
            Controls.Add(txtWynik);
            Controls.Add(btnWojna);
            Controls.Add(btnWalka);
            Controls.Add(btnPojedynek);
            Controls.Add(pnlBattlefield);
            Name = "Form1";
            Text = "Tactical Battle Simulator by Nicola Kaleta";
            pnlBattlefield.ResumeLayout(false);
            pnlBattlefield.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picWojownik).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMag).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
