namespace PackerGUI
{
    partial class About
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
            this.buttonOkay = new System.Windows.Forms.Button();
            this.labelAboutInfo = new System.Windows.Forms.Label();
            this.pictureBoxApplicationLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOkay
            // 
            this.buttonOkay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOkay.Location = new System.Drawing.Point(296, 127);
            this.buttonOkay.Name = "buttonOkay";
            this.buttonOkay.Size = new System.Drawing.Size(75, 23);
            this.buttonOkay.TabIndex = 11;
            this.buttonOkay.Text = "OK";
            this.buttonOkay.UseVisualStyleBackColor = true;
            this.buttonOkay.Click += new System.EventHandler(this.buttonOkay_Click);
            // 
            // labelAboutInfo
            // 
            this.labelAboutInfo.AutoSize = true;
            this.labelAboutInfo.Location = new System.Drawing.Point(82, 12);
            this.labelAboutInfo.Name = "labelAboutInfo";
            this.labelAboutInfo.Size = new System.Drawing.Size(165, 39);
            this.labelAboutInfo.TabIndex = 10;
            this.labelAboutInfo.Text = "BethesdaArchive2 GNMF Packer\r\nVersion: 1.0.0.0\r\nCopyright ©  2020, SockNastre";
            // 
            // pictureBoxApplicationLogo
            // 
            this.pictureBoxApplicationLogo.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxApplicationLogo.Name = "pictureBoxApplicationLogo";
            this.pictureBoxApplicationLogo.Size = new System.Drawing.Size(64, 64);
            this.pictureBoxApplicationLogo.TabIndex = 9;
            this.pictureBoxApplicationLogo.TabStop = false;
            // 
            // About
            // 
            this.AcceptButton = this.buttonOkay;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 162);
            this.Controls.Add(this.buttonOkay);
            this.Controls.Add(this.labelAboutInfo);
            this.Controls.Add(this.pictureBoxApplicationLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxApplicationLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOkay;
        private System.Windows.Forms.Label labelAboutInfo;
        private System.Windows.Forms.PictureBox pictureBoxApplicationLogo;
    }
}