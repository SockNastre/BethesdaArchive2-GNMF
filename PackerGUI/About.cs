using System;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class About : Form
    {
        public About(string version)
        {
            InitializeComponent();
            labelAboutInfo.Text = "BethesdaArchive2 GNMF Packer\nVersion: " + version + "\nCopyright ©  2020, SockNastre";
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}