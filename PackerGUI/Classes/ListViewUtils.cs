using GNMFInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PackerGUI.Classes
{
    // https://stackoverflow.com/a/1118396/10216412
    public static class ListViewSelectionUtils
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct LVITEM
        {
            public int mask;
            public int iItem;
            public int iSubItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public string pszText;
            public int cchTextMax;
            public int iImage;
            public IntPtr lParam;
            public int iIndent;
            public int iGroupId;
            public int cColumns;
            public IntPtr puColumns;
        };

        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageLVItem(IntPtr hWnd, int msg, int wParam, ref LVITEM lvi);

        /// <summary>
        /// Select all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be selected</param>
        public static void SelectAll(this ListView list)
        {
            ListViewSelectionUtils.SetItemState(list, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows on the given listview
        /// </summary>
        /// <param name="list">The listview whose items are to be deselected</param>
        public static void DeselectAll(this ListView list)
        {
            ListViewSelectionUtils.SetItemState(list, -1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item
        /// </summary>
        /// <param name="list">The listview whose item's state is to be changed</param>
        /// <param name="itemIndex">The index of the item to be changed</param>
        /// <param name="mask">Which bits of the value are to be set?</param>
        /// <param name="value">The value to be set</param>
        public static void SetItemState(ListView list, int itemIndex, int mask, int value)
        {
            LVITEM lvItem = new LVITEM
            {
                stateMask = mask,
                state = value
            };

            SendMessageLVItem(list.Handle, LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }
    }

    public static class ListViewExtensions
    {
        public static List<GNF> ConvertItemsToGNFList(this ListView listView)
        {
            var items = listView.Items;
            var gnfList = new List<GNF>();

            if (items.Count == 0)
                throw new ArgumentOutOfRangeException("listView.Items.Count", listView.Items.Count, "ListView item count must be greater than 0.");

            foreach (ListViewItem item in items)
            {
                var gnfFile = new GNF
                {
                    EntryStr = item.Text,
                    RealPath = item.SubItems[1].Text
                };

                if (File.Exists(gnfFile.RealPath))
                {
                    gnfList.Add(gnfFile);
                }
            }

            return gnfList;
        }
    }
}