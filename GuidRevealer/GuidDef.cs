using System;

namespace GuidRevealer
{
    public enum HighGuid
    {
        HIGHGUID_ITEM           = 0x4700,                       // blizz 4700
        HIGHGUID_CONTAINER      = 0x4700,                       // blizz 4700
        HIGHGUID_PLAYER         = 0x0000,                       // blizz 0700 (temporary reverted back to 0 high guid
                                                                // in result unknown source visibility player with
                                                                // player problems. please reapply only after its resolve)
        HIGHGUID_GAMEOBJECT     = 0xF110,                       // blizz F110/F510
        HIGHGUID_TRANSPORT      = 0xF120,                       // blizz F120/F520 (for GAMEOBJECT_TYPE_TRANSPORT)
        HIGHGUID_UNIT           = 0xF130,                       // blizz F130/F530
        HIGHGUID_PET            = 0xF140,                       // blizz F140/F540
        HIGHGUID_VEHICLE        = 0xF150,                       // blizz F150/F550
        HIGHGUID_DYNAMICOBJECT  = 0xF100,                       // blizz F100/F500
        HIGHGUID_CORPSE         = 0xF500,                       // blizz F100/F500 used second variant to resolve conflict with HIGHGUID_DYNAMICOBJECT
        HIGHGUID_MO_TRANSPORT   = 0x1FC0,                       // blizz 1FC0 (for GAMEOBJECT_TYPE_MO_TRANSPORT)
    }

    public class Guid
    {
        private ulong m_raw;

        public Guid(ulong decimal_representaion)
        {
            m_raw = decimal_representaion;
        }

        public HighGuid GetHigh() { return (HighGuid)((m_raw >> 48) & 0x0000FFFF); }
        public uint GetEntry() { return HasEntry() ? (uint)((m_raw >> 24) & 0x00FFFFFF) : 0; }
        public uint GetCounter()
        {
            return HasEntry() ? (uint)(m_raw & 0x00FFFFFF) : (uint)(m_raw & 0xFFFFFFFF);
        }

        public string GetHighURLType()
        {
            switch (GetHigh())
            {
                case HighGuid.HIGHGUID_ITEM:
                    return "item";
                case HighGuid.HIGHGUID_UNIT:
                case HighGuid.HIGHGUID_VEHICLE:
                    return "npc";
                case HighGuid.HIGHGUID_GAMEOBJECT:
                case HighGuid.HIGHGUID_TRANSPORT:
                case HighGuid.HIGHGUID_MO_TRANSPORT:
                    return "object";
                default:
                    return "";
            }
        }

        public bool HasEntry()
        {
            switch (GetHigh())
            {
                case HighGuid.HIGHGUID_ITEM:
                case HighGuid.HIGHGUID_PLAYER:
                case HighGuid.HIGHGUID_DYNAMICOBJECT:
                case HighGuid.HIGHGUID_CORPSE:
                case HighGuid.HIGHGUID_MO_TRANSPORT:
                    return false;
                case HighGuid.HIGHGUID_GAMEOBJECT:
                case HighGuid.HIGHGUID_TRANSPORT:
                case HighGuid.HIGHGUID_UNIT:
                case HighGuid.HIGHGUID_PET:
                case HighGuid.HIGHGUID_VEHICLE:
                default:
                    return true;
            }
        }
    }
}
