using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("item_subclass")]
    struct ItemSubClass
    {
        [PrimaryKey]
        public uint ClassId;
        [PrimaryKey]
        public uint SubClassId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        [Array(8)]
        private uint[] unks;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] desc;
        private uint descflags;

        public bool FixRow()
        {
            foreach (Locale loc in Constants.SupportedLocales)
            {
                if (this.name[(byte)loc] != 0)
                {
                    string name = DBC.GetString(GetType(), this.name[(byte)loc]);
                    if (name.IndexOf("OBSOLETE", StringComparison.InvariantCultureIgnoreCase) != -1)
                        return false;
                }
            }

            return true;
        }
    }
}
