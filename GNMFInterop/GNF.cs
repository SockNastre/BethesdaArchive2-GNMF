namespace GNMFInterop
{
    /// <summary>
    /// Asset class used by GNMF format for reading/writing.
    /// </summary>
    public class GNF
    {
        public static readonly int Magic = 0x20464E47; // "GNF "
        public static readonly int HeaderSize = 256;

        public string EntryStr { get; set; }
        public string RealPath { get; set; }
        public byte[] Meta { get; set; } = new byte[32];
        public ulong Offset { get; set; }
        public uint Size { get; set; } // Size after packing into archive
        public uint OrgSize { get; set; } // Size before being packed
    }
}