using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("talent_tab")]
    struct TalentTab
    {
        [PrimaryKey]
        public uint id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
        public uint icon;
        private uint unk;
        public uint classId;
        public uint petType;
        public uint order;
        [DBCString(false)]
        private uint internalName;

        public bool FixRow()
        {
            if (classId != 0)
                classId = (uint)Math.Log(classId, 2) + 1;

            if (petType != 0)
                petType = (uint)Math.Log(petType, 2) + 1;

            return true;
        }
    }
}
