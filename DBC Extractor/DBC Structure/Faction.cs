using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("faction")]
    struct Faction
    {
        [PrimaryKey]
        public uint Id;
        public int listId;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private uint[] BaseStandingDatas;

        public uint parent;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private uint[] SpilloverDatas;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] description;
        private uint descflags;

        public bool FixRow()
        {
            if (listId < 0)
                return false;

            string name_str = DBC.GetString(GetType(), name[0]);
            if (name_str.IndexOf("reuse", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("unused", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("test", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("do not use", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.StartsWith(" "))
                return false;

            return true;
        }
    }
}
