namespace GNMFInterop
{
    /// <summary>
    /// Constant integers or integers used throughout the process of read/write.
    /// </summary>
    public enum KeyInts : uint
    {
        GNFMagic = 0x20464E47, // "GNF "
        GNFHeaderSize = 256,
        GNMFMagic = 0x58445442, // "BTDX"
        Format = 0x464D4E47, // "GNMF"
        AssetRecordSize = 72,
        TextureIndicator = 0x00736464, // "dds" + 0x00
        Alignment = 0xBAADF00D
    }
}