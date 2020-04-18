using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class EntryStringEditor : Form
    {
        // Needed so we can modify the ListViewItem on this form
        readonly Packer PackerForm;
        readonly int ItemIndex;

        public EntryStringEditor(ListViewItem item, Packer packerForm)
        {
            InitializeComponent();
            richTextBoxEntryString.Text = item.Text;
            PackerForm = packerForm;
            ItemIndex = item.Index;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var r = new Regex(@"[^\ a-zA-Z0-9.]$");
            if (r.IsMatch(richTextBoxEntryString.Text))
            {
                MessageBox.Show("Invalid characters being used, please fix and try again." , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                PackerForm.listViewAssets.Items[ItemIndex].Text = richTextBoxEntryString.Text.Replace('/', '\\');
                this.Close();
            }
        }
    }
}