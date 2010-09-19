using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("skill_line")]
    struct SkillLine
    {
        [PrimaryKey]
        public uint Id;
        public uint category;
        /// <summary>
        /// All 0
        /// </summary>
        private uint cost;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] description;
        private uint descflags;
        
        public uint icon;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] actionVerb;
        private uint actionverbflags;

        private uint canLink;
    }
}
