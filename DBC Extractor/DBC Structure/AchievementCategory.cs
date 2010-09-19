using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("achievement_category")]
    struct AchievementCategory
    {
        [PrimaryKey]
        public uint Id;
        [Index("category")]
        public int parent;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;

        private uint _nameflags;

        public uint order;
    }
}
