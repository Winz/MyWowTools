using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("achievement")]
    struct Achievement
    {
        [PrimaryKey]
        public uint Id;
        public int faction;
        public int map;
        [Index("parent")]
        public uint parent;             // Parent Achievement
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint _nameflags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] description;
        private uint _descriptionflags;
        [Index("category")]
        public uint category;
        public uint points;
        public uint order;
        public uint flags;
        public uint icon;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] reward;
        private uint _rewardflags;
        public uint count;
        public uint refAchievement;
    }
}
