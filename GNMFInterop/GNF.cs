namespace GNMFInterop
{
    /// <summary>
    /// Asset class used by GNMF format for reading/writing.
    /// </summary>
    public class GNF
    {
        public string EntryStr { get; set; }
        public string RealPath { get; set; }
        public byte[] Meta { get; set; } = new byte[32];
        public ulong Offset { get; set; }
        public uint Size { get; set; } // Size when compressed usually
        public uint OrgSize { get; set; } // Size when uncompressed usually
    }
}