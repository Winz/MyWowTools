using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace SD2_ScriptCheck
{
    static partial class Program
    {
        static void CheckClearGossipMenu()
        {
            foreach (KeyValuePair<String, String> Pair in ScriptFiles)
            {
                String Filename = Pair.Key;
                String Code = Pair.Value;

                var GossipSelectMatches = Regex.Matches(Code, @"bool\s+GossipSelect_([\w\d]+)\s*\(",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                foreach (Match GossipSelectMatch in GossipSelectMatches)
                {
                    string FullString = GossipSelectMatch.Groups[0].Value;
                    int index = Code.IndexOf(FullString);
                    string ScriptName = GossipSelectMatch.Groups[1].Value;

                    string BlockCode = CppCode.ExtractBlock(Code, index);

                    int clear_index = BlockCode.IndexOf("ClearMenus");

                    int add_index = BlockCode.IndexOfAny(new string[] {
                        "SEND_GOSSIP_MENU",
                        "ADD_GOSSIP_ITEM",      // Also includes ID and EXTENDED postfixes
                        "SendGossipMenu",
                        "AddMenuItem"
                    });

                    if (add_index != -1 && (clear_index == -1 || clear_index > add_index))
                    {
                        ++Warnings;
                        Console.WriteLine("Warning: GossipSelect_{0} in {1} lacks ClearMenus, potential crash.",
                            ScriptName, Path.GetFileName(Filename));
                    }
                }
            }
        }
    }
}
