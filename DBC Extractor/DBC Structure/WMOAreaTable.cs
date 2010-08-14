using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("wmoareatable")]
    struct WMOAreaTableEntry
    {
        [PrimaryKey]
        public uint Id;
        public uint zone;
        private uint unk0;
        public int unk1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        private uint[] unk2_9;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint _nameflags;
    }
}
