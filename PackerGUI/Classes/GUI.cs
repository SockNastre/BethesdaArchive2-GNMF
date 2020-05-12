using GNMFInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PackerGUI.Classes
{
    public static class GUI
    {
        /// <summary>
        /// Resets form back controls back to original state.
        /// </summary>
        /// <param name="packerForm">Main form being used for packing.</param>
        public static void ResetForm(Packer packerForm)
        {
            packerForm.ArchivePath = String.Empty;
            packerForm.saveToolStripMenuItem.Enabled = false;
            packerForm.saveAsToolStripMenuItem.Enabled = false;
            packerForm.removeFilesToolStripMenuItem.Enabled = false;
            packerForm.listViewAssets.Items.Clear();
        }

        /// <summary>
        /// Adds asset into ListView with correct properties.
        /// </summary>
        /// <param name="path">Path of asset to-add in file system.</param>
        /// <param name="entryString">Entry string of asset.</param>
        /// <param name="listViewAssets">ListView being used to house asset items.</param>
        public static void AddAsset(string path, string entryString, ListView listViewAssets)
        {
            if (listViewAssets.FindItemWithText(entryString) != null) throw new Exception("Entry string already exists.");

            using (var reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                if (reader.ReadInt32() != GNF.Magic) throw new Exception("Invalid magic number, double check file is a GNF texture.");

                // Where gnf meta used in GNMF BA2 is stored
                reader.BaseStream.Position = 0x10;

                var item = new ListViewItem
                {
                    Text = entryString,
                    Tag = reader.ReadBytes(32) // Tag is important gnf meta used in GNMF BA2
                };
                item.SubItems.Add(path);

                listViewAssets.Items.Add(item);
            }
        }

        /// <summary>
        /// Turns Asset items from ListView into GNF classes so that GNMFInterop can read them later.
        /// </summary>
        /// <param name="items">Asset ItemCollection of ListView.</param>
        /// <returns>List of GNF with correct properties.</returns>
        public static List<GNF> ConvertListViewItems(ListView.ListViewItemCollection items)
        {
            if (items.Count == 0) throw new Exception("No assets to save.");

            var gnf = new List<GNF>();
            bool errorAsset = false;

            foreach (ListViewItem item in items)
            {
                var g = new GNF
                {
                    EntryStr = item.Text,
                    RealPath = item.SubItems[1].Text,
                    Meta = (byte[])item.Tag // Item tag contains gnf meta
                };

                if (File.Exists(g.RealPath))
                {
                    gnf.Add(g);
                }
                else
                {
                    errorAsset = true;
                }
            }

            if (gnf.Count == 0)
            {
                throw new Exception("No assets to save, cannot locate real file path on OS.");
            }
            else if (errorAsset)
            {
                DialogResult dr = MessageBox.Show("One or more assets has been omitted due to those assets not being found in real file path on OS." +
                       "\n\nWould you like to continue?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

                if (dr == DialogResult.No)
                {
                    throw new Exception("Cancel");
                }
            }

            return gnf;
        }
    }
}