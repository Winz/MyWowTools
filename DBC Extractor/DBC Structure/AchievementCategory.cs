using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("achievementcategory")]
    struct AchievementCategoryEntry
    {
        [PrimaryKey]
        public uint Id;
        [Index("category")]
        public int parentCategory;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;

        private uint _nameflags;
        private uint _order;
    }
}
