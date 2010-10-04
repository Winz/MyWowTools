using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("pet_foods")]
    struct ItemPetFood
    {
        [PrimaryKey]
        public uint Id;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(true)]
        public uint[] name;
        private uint nameflags;
    }
}
