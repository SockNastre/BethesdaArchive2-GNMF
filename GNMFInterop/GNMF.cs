using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GNMFInterop
{
    public static class GNMF
    {
        /// <summary>
        /// Current archive version
        /// </summary>
        private static readonly uint Version = 1;

        /// <summary>
        /// Writes GNMF BA2 using a list of assets.
        /// </summary>
        /// <param name="path">Path to save GNMF BA2.</param>
        /// <param name="assets">List of GNF assets in correct format.</param>
        public static void Write(string path, List<GNF> assets)
        {
            using (var writer = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.Write((uint)KeyInts.GNMFMagic);
                writer.Write(Version);
                writer.Write((uint)KeyInts.Format);
                writer.Write(assets.Count);
                writer.Write((long)-1); // Temporary, EntryStringTable Offset

                // Pads out Asset Records which we will write to later
                writer.Write(new byte[assets.Count * (uint)KeyInts.AssetRecordSize]);

                // Writing texture data
                foreach (GNF gnf in assets)
                {
                    gnf.Offset = (uint)writer.BaseStream.Position;

                    // We can skip reading any of the header, since we already got the gnf meta beforehand
                    byte[] texture = File.ReadAllBytes(gnf.RealPath).Skip((int)KeyInts.GNFHeaderSize).ToArray();

                    using (var compress = new ZlibStream(new MemoryStream(texture), CompressionMode.Compress, CompressionLevel.Default))
                    {
                        // Creates new stream and convertes to byte array
                        var m = new MemoryStream();
                        compress.CopyTo(m);
                        writer.Write(m.ToArray());

                        gnf.Size = (uint)m.Length;
                    }

                    // Just the size of texture data, not the header
                    gnf.OrgSize = (uint)texture.Length;
                }

                long entryStringTableOffset = writer.BaseStream.Position;

                // Writing EntryStringTable
                foreach (GNF gnf in assets)
                {
                    ushort length = Convert.ToUInt16(gnf.EntryStr.Length);

                    writer.Write(length);
                    writer.Write(gnf.EntryStr.ToCharArray());
                }

                // Where EntryStringTable offset is stored
                writer.BaseStream.Position = 16;
                writer.Write(entryStringTableOffset);

                // Writing asset records
                foreach (GNF gnf in assets)
                {
                    writer.Write(CRC32.Compute(Path.GetFileNameWithoutExtension(gnf.EntryStr).ToLower()));
                    writer.Write((uint)KeyInts.TextureIndicator);
                    writer.Write(CRC32.Compute(Path.GetDirectoryName(gnf.EntryStr).ToLower()));
                    writer.Write((byte)0); // Unk
                    writer.Write((byte)1); // Chunk count, tool just suppports one chunk atm
                    writer.Write((short)48); // Unk
                    writer.Write(gnf.Meta);
                    writer.Write(gnf.Offset);
                    writer.Write(gnf.Size);
                    writer.Write(gnf.OrgSize);
                    writer.Write(0); // Unk
                    writer.Write((uint)KeyInts.Alignment);
                }
            }
        }
    }
}