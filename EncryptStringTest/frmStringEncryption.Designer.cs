namespace EncryptStringTest
{
    partial class frmStringEncryption
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
            this.txtStartNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxString = new System.Windows.Forms.TextBox();
            this.textBoxEncrypted = new System.Windows.Forms.TextBox();
            this.butEncrypt = new System.Windows.Forms.Button();
            this.butDecrypt = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRefreshStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtStartNo
            // 
            this.txtStartNo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStartNo.Enabled = false;
            this.txtStartNo.Location = new System.Drawing.Point(91, 195);
            this.txtStartNo.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartNo.Name = "txtStartNo";
            this.txtStartNo.Size = new System.Drawing.Size(242, 22);
            this.txtStartNo.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 195);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "Start No";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(603, 201);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Count";
            // 
            // txtCount
            // 
            this.txtCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCount.Location = new System.Drawing.Point(677, 201);
            this.txtCount.Margin = new System.Windows.Forms.Padding(4);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(242, 22);
            this.txtCount.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "String";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 140);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Encrypted";
            // 
            // textBoxString
            // 
            this.textBoxString.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxString.Location = new System.Drawing.Point(91, 16);
            this.textBoxString.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxString.Name = "textBoxString";
            this.textBoxString.Size = new System.Drawing.Size(1323, 22);
            this.textBoxString.TabIndex = 1;
            // 
            // textBoxEncrypted
            // 
            this.textBoxEncrypted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEncrypted.Location = new System.Drawing.Point(91, 137);
            this.textBoxEncrypted.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEncrypted.Name = "textBoxEncrypted";
            this.textBoxEncrypted.Size = new System.Drawing.Size(1323, 22);
            this.textBoxEncrypted.TabIndex = 7;
            // 
            // butEncrypt
            // 
            this.butEncrypt.Location = new System.Drawing.Point(19, 72);
            this.butEncrypt.Margin = new System.Windows.Forms.Padding(4);
            this.butEncrypt.Name = "butEncrypt";
            this.butEncrypt.Size = new System.Drawing.Size(80, 28);
            this.butEncrypt.TabIndex = 4;
            this.butEncrypt.Text = "Encrypt";
            this.butEncrypt.UseVisualStyleBackColor = true;
            this.butEncrypt.Click += new System.EventHandler(this.butEncrypt_Click);
            // 
            // butDecrypt
            // 
            this.butDecrypt.Location = new System.Drawing.Point(107, 72);
            this.butDecrypt.Margin = new System.Windows.Forms.Padding(4);
            this.butDecrypt.Name = "butDecrypt";
            this.butDecrypt.Size = new System.Drawing.Size(80, 28);
            this.butDecrypt.TabIndex = 5;
            this.butDecrypt.Text = "Decrypt";
            this.butDecrypt.UseVisualStyleBackColor = true;
            this.butDecrypt.Click += new System.EventHandler(this.butDecrypt_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(985, 195);
            this.btnExport.Margin = new System.Windows.Forms.Padding(4);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 28);
            this.btnExport.TabIndex = 12;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtPath
            // 
            this.txtPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPath.Location = new System.Drawing.Point(91, 259);
            this.txtPath.Margin = new System.Windows.Forms.Padding(4);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(1159, 22);
            this.txtPath.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 262);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Path";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1278, 256);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 15;
            this.button1.Text = "Open File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRefreshStart
            // 
            this.btnRefreshStart.Location = new System.Drawing.Point(378, 192);
            this.btnRefreshStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefreshStart.Name = "btnRefreshStart";
            this.btnRefreshStart.Size = new System.Drawing.Size(80, 28);
            this.btnRefreshStart.TabIndex = 16;
            this.btnRefreshStart.Text = "Refresh";
            this.btnRefreshStart.UseVisualStyleBackColor = true;
            this.btnRefreshStart.Click += new System.EventHandler(this.btnRefreshStart_Click);
            // 
            // frmStringEncryption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 642);
            this.Controls.Add(this.btnRefreshStart);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtStartNo);
            this.Controls.Add(this.butDecrypt);
            this.Controls.Add(this.butEncrypt);
            this.Controls.Add(this.textBoxEncrypted);
            this.Controls.Add(this.textBoxString);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmStringEncryption";
            this.Text = "String Encryption";
            this.Load += new System.EventHandler(this.frmStringEncryption_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtStartNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxString;
        private System.Windows.Forms.TextBox textBoxEncrypted;
        private System.Windows.Forms.Button butEncrypt;
        private System.Windows.Forms.Button butDecrypt;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnRefreshStart;
    }
}

