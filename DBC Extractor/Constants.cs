using System.Collections.Generic;

namespace DbcExtractor
{
    static class Constants
    {
        public const int SpellEffects = 3;
        public const int TotalLocales = 16;
        public static readonly List<Locale> SupportedLocales = new List<Locale> { Locale.Default, Locale.ruRU };
        public const string TablePrefix = "aowow";
    }

    enum Locale
    {
        Default = 0,
        ruRU = 8,
    }

    enum Teams : uint
    {
        Horde       = 67,
        Alliance    = 469,
    }
}
