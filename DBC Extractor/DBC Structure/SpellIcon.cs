using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("spell_icon")]
    struct SpellIcon
    {
        [PrimaryKey]
        public uint Id;
        [DBCString(false)]
        public uint Icon;

        public bool FixRow()
        {
            Util.FixIcon(GetType(), Icon);

            return true;
        }
    }
}
