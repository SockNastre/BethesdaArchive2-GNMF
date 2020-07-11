namespace PackerGUI
{
    partial class Settings
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
            this.groupBoxArchiveSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxIsStringTableSaved = new System.Windows.Forms.CheckBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonResetSettings = new System.Windows.Forms.Button();
            this.groupBoxArchiveSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxArchiveSettings
            // 
            this.groupBoxArchiveSettings.Controls.Add(this.checkBoxIsStringTableSaved);
            this.groupBoxArchiveSettings.Location = new System.Drawing.Point(14, 12);
            this.groupBoxArchiveSettings.Name = "groupBoxArchiveSettings";
            this.groupBoxArchiveSettings.Size = new System.Drawing.Size(378, 50);
            this.groupBoxArchiveSettings.TabIndex = 1;
            this.groupBoxArchiveSettings.TabStop = false;
            this.groupBoxArchiveSettings.Text = "Archive Settings";
            // 
            // checkBoxIsStringTableSaved
            // 
            this.checkBoxIsStringTableSaved.AutoSize = true;
            this.checkBoxIsStringTableSaved.Checked = true;
            this.checkBoxIsStringTableSaved.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIsStringTableSaved.Location = new System.Drawing.Point(10, 19);
            this.checkBoxIsStringTableSaved.Name = "checkBoxIsStringTableSaved";
            this.checkBoxIsStringTableSaved.Size = new System.Drawing.Size(111, 17);
            this.checkBoxIsStringTableSaved.TabIndex = 1;
            this.checkBoxIsStringTableSaved.Text = "Save String Table";
            this.checkBoxIsStringTableSaved.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(317, 75);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonResetSettings
            // 
            this.buttonResetSettings.Location = new System.Drawing.Point(12, 75);
            this.buttonResetSettings.Name = "buttonResetSettings";
            this.buttonResetSettings.Size = new System.Drawing.Size(105, 23);
            this.buttonResetSettings.TabIndex = 3;
            this.buttonResetSettings.Text = "Reset to Default";
            this.buttonResetSettings.UseVisualStyleBackColor = true;
            this.buttonResetSettings.Click += new System.EventHandler(this.buttonResetSettings_Click);
            // 
            // Settings
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 108);
            this.Controls.Add(this.buttonResetSettings);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.groupBoxArchiveSettings);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.Settings_HelpButtonClicked);
            this.groupBoxArchiveSettings.ResumeLayout(false);
            this.groupBoxArchiveSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxArchiveSettings;
        private System.Windows.Forms.CheckBox checkBoxIsStringTableSaved;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonResetSettings;
    }
}