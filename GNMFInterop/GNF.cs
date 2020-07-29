using System.IO;

namespace GNMFInterop
{
    /// <summary>
    /// Contains metadata that aids with the BA2 packing process.
    /// </summary>
    public class GNF
    {
        public const int Magic = 0x20464E47; // "GNF "
        public const int HeaderSize = 256;

        public string EntryStr { get; set; } = string.Empty;
        public string RealPath { get; set; }

        // These fields get set during packing
        public byte[] Meta { get; set; } = new byte[32];
        public ulong Offset { get; set; }
        public uint Size { get; set; }
        public uint OrgSize { get; set; }

        /// <summary>
        /// Checks if a file is a valid GNF texture file.
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <returns>True or false if file is valid.</returns>
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