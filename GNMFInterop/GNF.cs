using System.IO;

namespace GNMFInterop
{
    /// <summary>
    /// Asset class used by GNMF format for reading/writing.
    /// </summary>
    public class GNF
    {
        public const int Magic = 0x20464E47; // "GNF "
        public const int HeaderSize = 256;

        public string EntryStr { get; set; } = string.Empty;
        public string RealPath { get; set; }

        // These fields get data during the packing phase
        public byte[] Meta { get; set; } = new byte[32];
        public ulong Offset { get; set; }
        public uint Size { get; set; }
        public uint OrgSize { get; set; }

        public static bool IsFileValid(string path)
        {
            var file = new FileInfo(path);
            if (!file.Exists || file.Length < GNF.HeaderSize)
                return false;
            
            using (var reader = new BinaryReader(File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                if (reader.ReadInt32() != GNF.Magic)
                {
                    return false;
                }
            }

            return true;
        }
    }
}