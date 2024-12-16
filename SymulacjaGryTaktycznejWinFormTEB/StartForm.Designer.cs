using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymulacjaGryTaktycznejWinFormTEB;

partial class StartForm
{
    private System.ComponentModel.IContainer components = null;
    private System.Windows.Forms.ComboBox cbCharacter;
    private System.Windows.Forms.ComboBox cbOpponent;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Label lblInstruction;

    private void InitializeComponent()
    {
        this.cbCharacter = new System.Windows.Forms.ComboBox();
        this.cbOpponent = new System.Windows.Forms.ComboBox();
        this.btnStart = new System.Windows.Forms.Button();
        this.lblInstruction = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // cbCharacter
        // 
        this.cbCharacter.FormattingEnabled = true;
        this.cbCharacter.Location = new System.Drawing.Point(12, 50);
        this.cbCharacter.Name = "cbCharacter";
        this.cbCharacter.Size = new System.Drawing.Size(121, 21);
        this.cbCharacter.TabIndex = 0;
        // 
        // cbOpponent
        // 
        this.cbOpponent.FormattingEnabled = true;
        this.cbOpponent.Location = new System.Drawing.Point(12, 77);
        this.cbOpponent.Name = "cbOpponent";
        this.cbOpponent.Size = new System.Drawing.Size(121, 21);
        this.cbOpponent.TabIndex = 1;
        // 
        // btnStart
        // 
        this.btnStart.Location = new System.Drawing.Point(12, 104);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(75, 23);
        this.btnStart.TabIndex = 2;
        this.btnStart.Text = "Start";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        // 
        // lblInstruction
        // 
        this.lblInstruction.AutoSize = true;
        this.lblInstruction.Location = new System.Drawing.Point(12, 20);
        this.lblInstruction.Name = "lblInstruction";
        this.lblInstruction.Size = new System.Drawing.Size(150, 13);
        this.lblInstruction.TabIndex = 3;
        this.lblInstruction.Text = "Wybierz na kogo chcesz się bić";
        // 
        // StartForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(284, 261);
        this.Controls.Add(this.lblInstruction);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.cbOpponent);
        this.Controls.Add(this.cbCharacter);
        this.Name = "StartForm";
        this.Text = "StartForm";
        this.ResumeLayout(false);
        this.PerformLayout();
    }
}
