struct Header{
    int32 magic;
    uint32 version;
    int32 format;
    uint32 assetCount;
    uint64 entryStringTableOffset;
}

struct AssetRecord{
    int32 nameHash;
    int32 format;
    int32 directoryHash;
    int8 unk;
    uint8 chunkCount;
    int16 unk2;
    byte[] meta = new byte[32];
    uint64 offset;
    uint32 size;
    uint32 originalSize;
    int32 unk3;
    uint32 alignment;
}

struct EntryString{
    uint16 length;
    char[] str;
}