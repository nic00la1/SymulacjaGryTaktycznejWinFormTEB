namespace SymulacjaGryTaktycznejWinFormTEB
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPojedynek = new System.Windows.Forms.Button();
            this.txtWynik = new System.Windows.Forms.TextBox();
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
            this.txtWynik.Size = new System.Drawing.Size(776, 397);
            this.txtWynik.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtWynik);
            this.Controls.Add(this.btnPojedynek);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnPojedynek;
        private System.Windows.Forms.TextBox txtWynik;

        #endregion
    }
}
