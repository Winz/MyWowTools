using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("item_class")]
    struct ItemClass
    {
        [PrimaryKey]
        public uint Id;
        private uint unk0;
        private uint unk1; // 1 for weapon, all other 0
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;

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
