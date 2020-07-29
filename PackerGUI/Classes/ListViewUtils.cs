using GNMFInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PackerGUI.Classes
{
    /// <summary>
    /// From <see href="https://stackoverflow.com/a/1118396/10216412">Stack Overflow thread</see>.
    /// Provides static methods, structs, and fields for enhancing entire list selection capability in a ListView.
    /// </summary>
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
        /// Select all rows in the given ListView.
        /// </summary>
        /// <param name="list">The ListView whose items are to be selected.</param>
        public static void SelectAll(this ListView list)
        {
            ListViewSelectionUtils.SetItemState(list, -1, 2, 2);
        }

        /// <summary>
        /// Deselect all rows in the given ListView.
        /// </summary>
        /// <param name="list">The ListView whose items are to be deselected.</param>
        public static void DeselectAll(this ListView list)
        {
            ListViewSelectionUtils.SetItemState(list, -1, 2, 0);
        }

        /// <summary>
        /// Set the item state on the given item.
        /// </summary>
        /// <param name="list">The ListView whose item's state is to be changed.</param>
        /// <param name="itemIndex">The index of the item to be changed.</param>
        /// <param name="mask">Which bits of the value are to be set.</param>
        /// <param name="value">The value to be set.</param>
        public static void SetItemState(ListView list, int itemIndex, int mask, int value)
        {
            var lvItem = new LVITEM
            {
                stateMask = mask,
                state = value
            };

            ListViewSelectionUtils.SendMessageLVItem(list.Handle, LVM_SETITEMSTATE, itemIndex, ref lvItem);
        }
    }

    /// <summary>
    /// Provided static method for utilizing a ListView with the BSA packing process.
    /// </summary>
    public static class ListViewExtensions
    {
        /// <summary>
        /// Converts items from <see cref="ListView"/> to a list of <see cref="GNF"/>.
        /// </summary>
        /// <param name="listView">ListView to be converted.</param>
        /// <returns>List generated from ListView.</returns>
        public static List<GNF> ConvertItemsToGNFList(this ListView listView)
        {
            ListView.ListViewItemCollection itemCollection = listView.Items;
            var gnfList = new List<GNF>();

            foreach (ListViewItem item in itemCollection)
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