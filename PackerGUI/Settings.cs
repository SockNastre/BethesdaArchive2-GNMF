using PackerGUI.Classes;
using System;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class Settings : Form
    {
        private Ini SettingsIni;

        public Settings(Ini settingsIni)
        {
            InitializeComponent();

            this.SettingsIni = settingsIni;
            checkBoxIsStringTableSaved.Checked = bool.Parse(settingsIni.Data["Archive"]["IsStringTableSaved"]);
        }

        private void Settings_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("\"Save String Table\" - When building the archive chooses to save list of entry string " +
                "names associated with files. Disabling setting this may prevent BSA/BA2 opening software from opening " +
                "the archive.", "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonResetSettings_Click(object sender, EventArgs e)
        {
            checkBoxIsStringTableSaved.Checked = true;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.SettingsIni.Data["Archive"]["IsStringTableSaved"] = checkBoxIsStringTableSaved.Checked.ToString();

            this.Close();
        }
    }
}