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
                if (reader.ReadInt32() != (int)KeyInts.GNFMagic) throw new Exception("Invalid magic number");

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
            var gnf = new List<GNF>();

            foreach (ListViewItem item in items)
            {
                var g = new GNF
                {
                    EntryStr = item.Text,
                    RealPath = item.SubItems[1].Text,
                    Meta = (byte[])item.Tag // Item tag contains gnf meta
                };

                gnf.Add(g);
            }

            return gnf;
        }
    }
}