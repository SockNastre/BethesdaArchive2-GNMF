using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GNMFInterop
{
    public static class GNMF
    {
        /// <summary>
        /// Writes GNMF BA2 using a list of assets.
        /// </summary>
        /// <param name="path">Path to save GNMF BA2.</param>
        /// <param name="assets">List of GNF assets in correct format.</param>
        public static void Write(string path, List<GNF> assets)
        {
            using (var writer = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.Write(0x58445442); // "BTDX"
                writer.Write(1); // Version
                writer.Write(0x464D4E47); // "GNMF"
                writer.Write(assets.Count);
                writer.Write((long)-1); // Temporary, EntryStringTable Offset

                // Pads out Asset Records which we will write to later, each Asset Record takes up 72 bytes
                writer.Write(new byte[assets.Count * 72]);

                // Writing texture data
                foreach (GNF gnf in assets)
                {
                    gnf.Offset = (uint)writer.BaseStream.Position;

                    // We can skip reading any of the header, since we already got the gnf meta beforehand
                    byte[] texture = File.ReadAllBytes(gnf.RealPath).Skip(GNF.HeaderSize).ToArray();

                    // Data to be compressed is only the data of the GNF not it's header, header metadata is stored in Asset Records
                    writer.Write(GetCompressedZlib(texture, gnf));

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
                    writer.Write(0x00736464); // "dds" + 0x00, indicates asset format (in this case dds just means it's a texture)
                    writer.Write(CRC32.Compute(Path.GetDirectoryName(gnf.EntryStr).ToLower()));
                    writer.Write((byte)0); // Unk
                    writer.Write((byte)1); // Chunk count, tool just suppports one chunk atm
                    writer.Write((short)48); // Size of data here and until "Alignment"
                    writer.Write(gnf.Meta);
                    writer.Write(gnf.Offset);
                    writer.Write(gnf.Size);
                    writer.Write(gnf.OrgSize);
                    writer.Write(0); // Unk
                    writer.Write(0xBAADF00D); // "Alignment", not sure what this is but it is constant
                }
            }
        }

        private static byte[] GetCompressedZlib(byte[] data, GNF gnf)
        {
            if (data == null || data.Length == 0) return data;

            using (var inStream = new MemoryStream(data))
            {
                var outStream = new MemoryStream();
                var compressStream = new DeflaterOutputStream(outStream, new Deflater(Deflater.DEFAULT_COMPRESSION));
                int bufferSize;
                var buffer = new byte[4096];

                while ((bufferSize = inStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    compressStream.Write(buffer, 0, bufferSize);
                }

                compressStream.Finish();
                gnf.Size = (uint)outStream.Length;
                return outStream.ToArray();
            }
        }
    }
}