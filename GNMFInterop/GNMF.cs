using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
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
        public static void Write(string path, List<GNF> assets, bool isStrTableSaved)
        {
            using (var writer = new BinaryWriter(File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.Write(0x58445442); // "BTDX"
                writer.Write(1); // Version
                writer.Write(0x464D4E47); // "GNMF"
                writer.Write(assets.Count());
                writer.Write((long)0); // EntryStringTable Offset, remains 0 if saveStringTable is false

                // Pads out Asset Records which we will write to later, each Asset Record takes up 72 bytes
                writer.Write(new byte[assets.Count() * 72]);

                // Writing texture data
                foreach (GNF gnf in assets)
                {
                    gnf.Offset = (uint)writer.BaseStream.Position;

                    var file = new FileInfo(gnf.RealPath);
                    int textureDataLength = (int)file.Length - GNF.HeaderSize;
                    var textureData = new byte[textureDataLength];

                    using (var reader = new BinaryReader(File.Open(gnf.RealPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    {
                        reader.BaseStream.Position = 0x10;
                        gnf.Meta = reader.ReadBytes(32);

                        reader.BaseStream.Position = GNF.HeaderSize;
                        textureData = reader.ReadBytes(textureDataLength);
                    }
                    // Data to be compressed is only the data of the GNF not it's header, header metadata is stored in Asset Records
                    writer.Write(GetCompressedZlibData(textureData, gnf));

                    // Just the size of texture data, not the header
                    gnf.OrgSize = (uint)textureDataLength;
                }

                if (isStrTableSaved)
                {
                    long entryStringTableOffset = writer.BaseStream.Position;

                    // Writing EntryStringTable
                    foreach (GNF gnf in assets)
                    {
                        writer.Write((ushort)gnf.EntryStr.Length);
                        writer.Write(gnf.EntryStr.ToCharArray());
                    }

                    // Where EntryStringTable offset is stored
                    writer.BaseStream.Position = 16;
                    writer.Write(entryStringTableOffset);
                }
                else
                {
                    writer.BaseStream.Position = 24;
                }

                // Writing asset records
                foreach (GNF gnf in assets)
                {
                    writer.Write(CRC32.Compute(Path.GetFileNameWithoutExtension(gnf.EntryStr).ToLower()));
                    writer.Write(0x00736464); // "dds" + 0x00, indicates asset format (in this case dds just means it's a texture)
                    writer.Write(CRC32.Compute(Path.GetDirectoryName(gnf.EntryStr).ToLower()));
                    writer.Write((byte)0); // Unk
                    writer.Write((byte)1); // Chunk count, tool just suppports one chunk atm
                    writer.Write((short)48); // Size of chunk data, does not include "alignment"
                    writer.Write(gnf.Meta);
                    writer.Write(gnf.Offset);
                    writer.Write(gnf.Size);
                    writer.Write(gnf.OrgSize);
                    writer.Write(0); // Unk
                    writer.Write(0xBAADF00D); // "Alignment", not sure what the value indicates but it is constant
                }
            }
        }

        /// <summary>
        /// Deflates byte[] using zlib's DEFAULT_COMPRESSION.
        /// </summary>
        /// <param name="data">byte[] to be deflated.</param>
        /// <param name="gnf">GNF class file representing the byte[] metadata.</param>
        /// <returns>Compressed byte[]</returns>
        private static byte[] GetCompressedZlibData(byte[] data, GNF gnf)
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