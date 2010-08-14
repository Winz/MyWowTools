using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DbcExtractor
{
    class Program
    {
        public static string prefix = "./dbc/";

        static void Main(string[] args)
        {
            Console.WriteLine("Extract full DBC structure? (y/n)");
            Boolean AsLocalized = Console.ReadLine() == "y" ? false : true;

            Extract(AsLocalized, Locale.Default);

            if (!AsLocalized)
            {
                string orig_prefix = prefix;

                foreach (Locale loc in Constants.SupportedLocales)
                {
                    if (loc == Locale.Default)
                        continue;

                    string dir = Path.Combine(orig_prefix, loc.ToString());
                    if (!Directory.Exists(dir))
                        continue;

                    Console.WriteLine("Directory {0} exists and may contain localized DBCs. Extract? (y/n)", dir);
                    if (Console.ReadLine().Trim() == "y")
                    {
                        prefix = dir;
                        Extract(true, loc);
                    }
                }
            }

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        static void Extract(bool AsLocalized, Locale ExtractAs)
        {
            string filename = AsLocalized ? "Localization" : "Data";
            if (ExtractAs != Locale.Default)
                filename += "_" + ExtractAs.ToString();

            FileStream FS = new FileStream("DBC_" + filename + ".sql", FileMode.Create);

            List<DBC> DBCs = new List<DBC>
            {
                //DBC.Open<SpellEntry>                ("Spell.dbc"),
                //DBC.Open<AchievementCategoryEntry>  ("Achievement_Category.dbc"),
                //DBC.Open<AchievementEntry>          ("Achievement.dbc"),
                //DBC.Open<AchievementCriteriaEntry>  ("Achievement_Criteria.dbc"),
                //DBC.Open<ChrClasses>                ("ChrClasses.dbc"),
                //DBC.Open<ChrRaces>                  ("ChrRaces.dbc"),
                DBC.Open<CreatureFamily>            ("CreatureFamily.dbc"),
                //DBC.Open<GlyphPropertiesEntry>      ("GlyphProperties.dbc"),
                //DBC.Open<ItemClass>                 ("ItemClass.dbc"),
                //DBC.Open<ItemSubClass>              ("ItemSubClass.dbc"),
                //DBC.Open<ItemPetFood>               ("ItemPetFood.dbc"),
                //DBC.Open<WMOAreaTableEntry>         ("WMOAreaTable.dbc"),
                //DBC.Open<Talent>                    ("Talent.dbc"),
                //DBC.Open<TalentTab>                 ("TalentTab.dbc"),
            };

            Console.WriteLine("Extracting...");
            foreach (DBC dbc in DBCs)
            {
                byte[] Bytes = Encoding.UTF8.GetBytes(dbc.ToSQL(AsLocalized));
                FS.Write(Bytes, 0, Bytes.Length);
            }
            FS.Close();
        }
    }
}
