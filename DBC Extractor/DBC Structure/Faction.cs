using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("factions")]
    struct Faction
    {
        [PrimaryKey]
        public uint factionID;
        public int reputationListID;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private uint[] BaseStandingDatas;

        public uint team;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private uint[] SpilloverDatas;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] description;

        /// <summary>
        /// WORKAROUND: reused descflags to fix queries.
        /// </summary>
        public uint side;

        public bool FixRow()
        {
            if (reputationListID < 0)
                return false;

            string name_str = DBC.GetString(GetType(), name[0]);
            if (name_str.IndexOf("reuse", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("unused", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("test", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("do not use", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.StartsWith(" "))
                return false;

            side = 0;

            return true;
        }
    }
}
