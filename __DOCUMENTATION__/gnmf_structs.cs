struct Header{
    int32 magic = 1480873026;
    uint32 version = 1;
    int32 format = 1179471431;
    uint32 assetCount;
    uint64 entryStringTableOffset;
}

struct AssetRecord{
    int32 nameHash;
    int32 format = 7562340;
    int32 directoryHash;
    int8 unk = 0;
    uint8 chunkCount = 1;
    int16 chunkDataSize = 48; // Size of chunk data, does not include "alignment"
    byte[] meta = new byte[32];
    uint64 offset;
    uint32 size;
    uint32 originalSize;
    int32 unk2 = 0; // Probably padding
    uint32 alignment = 3131961357;
}

struct EntryString{
    uint16 strLength;
    char[] str;
}