using System;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }

        private void buttonOkay_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}