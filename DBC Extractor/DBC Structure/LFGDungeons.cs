using System;
using System.Runtime.InteropServices;

namespace DbcExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    [TableName("lfg_dungeons")]
    struct LFGDungeons
    {
        [PrimaryKey]
        public uint Id;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(false)]
        public uint name;
        //private uint _nameflags1;

        public uint minlevel;
        public uint maxlevel;
        public uint rec_level;
        public uint rec_minlevel;
        public uint rec_maxlevel;
        public int map;
        public uint difficulty;
        public uint unk_25;
        public uint type;
        public int unk;
        [DBCString(false)]
        public uint systemName;
        public uint expansion;
        [DBCString(false)]
        public uint fullName;
        public uint group;
        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.TotalLocales)]
        [DBCString(false)]
        public uint desc;
        //private uint _descflags;

        static string[] DungeonDifficulties = new string[]
        {
            "Normal", "Heroic", "ERROR 2", "ERROR 3"
        };
        static string[] RaidDifficulties = new string[]
        {
            "Normal", "Normal", "Heroic", "Heroic"
        };

        public bool FixRow()
        {
            string orig;
            switch (group)
            {
                case 12: // Cataclysm Heroic
                case 5: // Wrath of the Lich King Heroic
                case 3: // Burning Crusade Heroic
                    orig = DBC.GetString(typeof(LFGDungeons), name) + " (Heroic)";
                    systemName = DBC.CreateString(typeof(LFGDungeons), orig.Enumize());
                    fullName = DBC.CreateString(typeof(LFGDungeons), orig);
                    break;
                case 13: // Cataclysm Normal
                case 4: // Wrath of the Lich King Normal
                case 2: // Burning Crusade Normal
                    orig = DBC.GetString(typeof(LFGDungeons), name) + " (Normal)";
                    systemName = DBC.CreateString(typeof(LFGDungeons), orig.Enumize());
                    fullName = DBC.CreateString(typeof(LFGDungeons), orig);
                    break;
                case 14: // Cataclysm Raid (25)
                case 9: // Wrath of the Lich King Raid (25)
                    orig = DBC.GetString(typeof(LFGDungeons), name) + " (25)";
                    systemName = DBC.CreateString(typeof(LFGDungeons), orig.Enumize());
                    fullName = DBC.CreateString(typeof(LFGDungeons), orig);
                    break;
                case 15: // Cataclysm Raid (10)
                case 8: // Wrath of the Lich King Raid (10)
                    orig = DBC.GetString(typeof(LFGDungeons), name) + " (10)";
                    systemName = DBC.CreateString(typeof(LFGDungeons), orig.Enumize());
                    fullName = DBC.CreateString(typeof(LFGDungeons), orig);
                    break;
                case 7: // Burning Crusade Raid
                case 6: // Classic Raid
                case 11: // World Events
                case 0: // other (zones)
                case 1: // Classic Dungeons
                    systemName = DBC.CreateString(typeof(LFGDungeons), DBC.GetString(typeof(LFGDungeons), name).Enumize());
                    fullName = name;
                    break;
            }

            return true;
        }
    }
}
