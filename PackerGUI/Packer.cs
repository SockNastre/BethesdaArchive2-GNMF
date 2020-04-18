using GNMFInterop;
using PackerGUI.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PackerGUI
{
    public partial class Packer : Form
    {
        private readonly string Version = "1.0.0.0";
        public string ArchivePath = String.Empty;

        public Packer()
        {
            InitializeComponent();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.ResetForm(this);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ArchivePath != String.Empty)
                {
                    List<GNF> gnf = GUI.ConvertListViewItems(listViewAssets.Items);
                    GNMF.Write(ArchivePath, gnf);

                    MessageBox.Show("Done!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (saveFileDialogGNMF.ShowDialog() == DialogResult.OK)
                {
                    string savePath = saveFileDialogGNMF.FileName;
                    List<GNF> gnf = GUI.ConvertListViewItems(listViewAssets.Items);
                    GNMF.Write(savePath, gnf);

                    MessageBox.Show("Done!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    // Sets public path to archive for "Save" function
                    ArchivePath = savePath;
                    saveToolStripMenuItem.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogGNF.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialogGNF.FileNames)
                {
                    try
                    {
                        GUI.AddAsset(file, Path.GetFileName(file), listViewAssets);
                        saveAsToolStripMenuItem.Enabled = true;
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void addFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Creates new instance of OpenFolderDialog, similar to OpenFileDialog except folders are selected
            var folderDialog = new OpenFolderDialog();

            if (folderDialog.ShowDialog(this) == DialogResult.OK)
            {
                string dir = folderDialog.Folder;
                int subLength = dir.Length + 1; // +1 accounts for an extra backslash

                // If made true indicates that an asset was not imported because it wasn't GNF or was a duplicate entry string
                bool errorAsset = false;

                foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        GUI.AddAsset(file, file.Substring(subLength), listViewAssets);
                    }
                    catch
                    {
                        errorAsset = true;
                    }
                }

                // Checks to make sure there are items to be saved before enabling save button
                if (listViewAssets.Items.Count > 0)
                {
                    saveAsToolStripMenuItem.Enabled = true;
                }

                if (errorAsset == true)
                {
                    MessageBox.Show("One or more assets has been omitted due to those assets not being GNF or being duplicate names.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void removeFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listViewAssets.SelectedItems)
            {
                listViewAssets.Items.RemoveAt(item.Index);
            }

            if (listViewAssets.Items.Count < 1)
            {
                // Just resets everything if all items are deleted
                newToolStripMenuItem.PerformClick();
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Function makes selecting large amounts of items more optimized
            ListViewUtils.SelectAll(listViewAssets);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForm = new About(Version);
            aboutForm.ShowDialog();
        }

        private void listViewAssets_DoubleClick(object sender, EventArgs e)
        {
            var editorForm = new EntryStringEditor(listViewAssets.SelectedItems[0], this);
            editorForm.ShowDialog();
        }

        private void listViewAssets_DragDrop(object sender, DragEventArgs e)
        {
            // If made true indicates that an asset was not imported because it wasn't GNF or was a duplicate entry string
            bool errorAsset = false;

            string dir = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (Directory.Exists(dir))
            {
                int subLength = dir.Length + 1; // +1 accounts for an extra backslash
                foreach (string file in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    try
                    {
                        GUI.AddAsset(file, file.Substring(subLength), listViewAssets);
                    }
                    catch
                    {
                        errorAsset = true;
                    }
                }
            }
            else
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    try
                    {
                        GUI.AddAsset(file, Path.GetFileName(file), listViewAssets);
                        saveAsToolStripMenuItem.Enabled = true;
                    }
                    catch
                    {
                        errorAsset = true;
                    }
                }
            }

            // Checks to make sure there are items to be saved before enabling save button
            if (listViewAssets.Items.Count > 0)
            {
                saveAsToolStripMenuItem.Enabled = true;
            }

            if (errorAsset == true)
            {
                MessageBox.Show("One or more assets has been omitted due to those assets not being GNF or being duplicate names.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listViewAssets_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void listViewAssets_ItemActivate(object sender, EventArgs e)
        {
            var editorForm = new EntryStringEditor(listViewAssets.SelectedItems[0], this);
            editorForm.ShowDialog();
        }

        private void listViewAssets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAssets.SelectedItems.Count > 0)
            {
                removeFilesToolStripMenuItem.Enabled = true;
            }
            else
            {
                removeFilesToolStripMenuItem.Enabled = false;
            }
        }
    }
}