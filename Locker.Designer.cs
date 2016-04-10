namespace LiteLock
{
    partial class Locker
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Locker));
            this.EncryptLabel = new System.Windows.Forms.TextBox();
            this.DecryptLabel = new System.Windows.Forms.TextBox();
            this.encryptionProgress = new System.Windows.Forms.ProgressBar();
            this.currentFileLabel = new System.Windows.Forms.Label();
            this.fileProgressBar = new System.Windows.Forms.ProgressBar();
            this.overwriteOriginal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // EncryptLabel
            // 
            this.EncryptLabel.AllowDrop = true;
            this.EncryptLabel.BackColor = System.Drawing.Color.Black;
            this.EncryptLabel.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EncryptLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.EncryptLabel.Location = new System.Drawing.Point(12, 12);
            this.EncryptLabel.Multiline = true;
            this.EncryptLabel.Name = "EncryptLabel";
            this.EncryptLabel.ReadOnly = true;
            this.EncryptLabel.Size = new System.Drawing.Size(412, 72);
            this.EncryptLabel.TabIndex = 0;
            this.EncryptLabel.TabStop = false;
            this.EncryptLabel.Text = "\r\nDrag Files Here to Encrypt";
            this.EncryptLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EncryptLabel.Click += new System.EventHandler(this.EncryptLabel_Click);
            this.EncryptLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.EncryptLabel_DragDrop);
            this.EncryptLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.EncryptLabel_DragEnter);
            this.EncryptLabel.MouseEnter += new System.EventHandler(this.EncryptLabel_MouseEnter);
            this.EncryptLabel.MouseLeave += new System.EventHandler(this.EncryptLabel_MouseLeave);
            // 
            // DecryptLabel
            // 
            this.DecryptLabel.AllowDrop = true;
            this.DecryptLabel.BackColor = System.Drawing.Color.Black;
            this.DecryptLabel.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DecryptLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.DecryptLabel.Location = new System.Drawing.Point(12, 97);
            this.DecryptLabel.Multiline = true;
            this.DecryptLabel.Name = "DecryptLabel";
            this.DecryptLabel.ReadOnly = true;
            this.DecryptLabel.Size = new System.Drawing.Size(412, 72);
            this.DecryptLabel.TabIndex = 1;
            this.DecryptLabel.TabStop = false;
            this.DecryptLabel.Text = "\r\nDrag Files Here to Decrypt";
            this.DecryptLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DecryptLabel.Click += new System.EventHandler(this.DecryptLabel_Click);
            this.DecryptLabel.DragDrop += new System.Windows.Forms.DragEventHandler(this.DecryptLabel_DragDrop);
            this.DecryptLabel.DragEnter += new System.Windows.Forms.DragEventHandler(this.DecryptLabel_DragEnter);
            this.DecryptLabel.MouseEnter += new System.EventHandler(this.DecryptLabel_MouseEnter);
            this.DecryptLabel.MouseLeave += new System.EventHandler(this.DecryptLabel_MouseLeave);
            // 
            // encryptionProgress
            // 
            this.encryptionProgress.Location = new System.Drawing.Point(12, 208);
            this.encryptionProgress.MarqueeAnimationSpeed = 0;
            this.encryptionProgress.Name = "encryptionProgress";
            this.encryptionProgress.Size = new System.Drawing.Size(412, 23);
            this.encryptionProgress.TabIndex = 2;
            // 
            // currentFileLabel
            // 
            this.currentFileLabel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentFileLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.currentFileLabel.Location = new System.Drawing.Point(8, 266);
            this.currentFileLabel.Name = "currentFileLabel";
            this.currentFileLabel.Size = new System.Drawing.Size(415, 32);
            this.currentFileLabel.TabIndex = 3;
            // 
            // fileProgressBar
            // 
            this.fileProgressBar.Location = new System.Drawing.Point(11, 237);
            this.fileProgressBar.MarqueeAnimationSpeed = 0;
            this.fileProgressBar.Name = "fileProgressBar";
            this.fileProgressBar.Size = new System.Drawing.Size(412, 23);
            this.fileProgressBar.TabIndex = 4;
            // 
            // overwriteOriginal
            // 
            this.overwriteOriginal.AutoSize = true;
            this.overwriteOriginal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.overwriteOriginal.Location = new System.Drawing.Point(12, 185);
            this.overwriteOriginal.Name = "overwriteOriginal";
            this.overwriteOriginal.Size = new System.Drawing.Size(255, 17);
            this.overwriteOriginal.TabIndex = 5;
            this.overwriteOriginal.Text = "Securily Overwrite Original File During Encryption";
            this.overwriteOriginal.UseVisualStyleBackColor = true;
            // 
            // Locker
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(435, 310);
            this.Controls.Add(this.overwriteOriginal);
            this.Controls.Add(this.fileProgressBar);
            this.Controls.Add(this.currentFileLabel);
            this.Controls.Add(this.encryptionProgress);
            this.Controls.Add(this.DecryptLabel);
            this.Controls.Add(this.EncryptLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Locker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiteLock v2.1 - Created by DarkTussin";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Locker_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EncryptLabel;
        private System.Windows.Forms.TextBox DecryptLabel;
        private System.Windows.Forms.ProgressBar encryptionProgress;
        public System.Windows.Forms.ProgressBar fileProgressBar;
        public System.Windows.Forms.Label currentFileLabel;
        private System.Windows.Forms.CheckBox overwriteOriginal;

    }
}

