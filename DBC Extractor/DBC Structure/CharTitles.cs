using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("char_titles")]
    struct CharTitles
    {
        [PrimaryKey]
        public uint Id;
        private uint unk0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint _nameflags1;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] nameFemale;
        private uint _nameflags2;
        public uint index;
    }
}
