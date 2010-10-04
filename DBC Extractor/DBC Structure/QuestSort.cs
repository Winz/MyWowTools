using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("quest_sorts")]
    struct QuestSort
    {
        [PrimaryKey]
        public uint Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;

        public bool FixRow()
        {
            string name_str = DBC.GetString(GetType(), name[0]);
            if (name_str.IndexOf("reuse", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("unused", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("test", StringComparison.InvariantCultureIgnoreCase) != -1
                || name_str.IndexOf("do not use", StringComparison.InvariantCultureIgnoreCase) != -1)
                return false;

            return true;
        }
    }
}
