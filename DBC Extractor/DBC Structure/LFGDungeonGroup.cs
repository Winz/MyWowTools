using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("lfg_dungeon_group")]
    struct LFGDungeonGroup
    {
        [PrimaryKey]
        public uint Id;
        [DBCString(false)]
        public uint name;
        public uint unk0;
        public uint unk1;
        public uint unk2;
    }
}
