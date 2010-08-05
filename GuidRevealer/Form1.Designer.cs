namespace GuidRevealer
{
    partial class MainForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.revealHex = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.revealDecimal = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.whUrl = new System.Windows.Forms.LinkLabel();
            this.output = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.revealHex);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.revealDecimal);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(425, 70);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hex:";
            // 
            // revealHex
            // 
            this.revealHex.Location = new System.Drawing.Point(356, 41);
            this.revealHex.Name = "revealHex";
            this.revealHex.Size = new System.Drawing.Size(63, 21);
            this.revealHex.TabIndex = 3;
            this.revealHex.Text = "Reveal";
            this.revealHex.UseVisualStyleBackColor = true;
            this.revealHex.Click += new System.EventHandler(this.revealHex_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(56, 41);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(294, 21);
            this.textBox2.TabIndex = 1;
            // 
            // revealDecimal
            // 
            this.revealDecimal.Location = new System.Drawing.Point(356, 14);
            this.revealDecimal.Name = "revealDecimal";
            this.revealDecimal.Size = new System.Drawing.Size(63, 21);
            this.revealDecimal.TabIndex = 2;
            this.revealDecimal.Text = "Reveal";
            this.revealDecimal.UseVisualStyleBackColor = true;
            this.revealDecimal.Click += new System.EventHandler(this.revealDecimal_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Decimal:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(56, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(294, 21);
            this.textBox1.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.whUrl);
            this.groupBox2.Controls.Add(this.output);
            this.groupBox2.Location = new System.Drawing.Point(11, 88);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 147);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // whUrl
            // 
            this.whUrl.AutoSize = true;
            this.whUrl.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.whUrl.Location = new System.Drawing.Point(6, 131);
            this.whUrl.Name = "whUrl";
            this.whUrl.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.whUrl.Size = new System.Drawing.Size(209, 13);
            this.whUrl.TabIndex = 1;
            this.whUrl.TabStop = true;
            this.whUrl.Text = "http://www.wowhead.com/object=123456";
            this.whUrl.Visible = false;
            this.whUrl.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.whUrl_LinkClicked);
            // 
            // output
            // 
            this.output.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.output.Location = new System.Drawing.Point(6, 20);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(414, 108);
            this.output.TabIndex = 0;
            this.output.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 247);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GUID Revealer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button revealHex;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button revealDecimal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox output;
        private System.Windows.Forms.LinkLabel whUrl;
    }
}

