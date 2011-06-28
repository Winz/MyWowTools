using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace opcodemerge
{
    class Program
    {
        static Tuple<string, uint> GetPair(Dictionary<string, uint> dict, uint value)
        {
            foreach (var pair in dict)
            {
                if (pair.Value == value)
                    return Tuple.Create(pair.Key, pair.Value);
            }

            return null;
        }

        static Tuple<string, uint> GetPair(Dictionary<string, uint> dict, string key)
        {
            foreach (var pair in dict)
            {
                if (pair.Key == key)
                    return Tuple.Create(pair.Key, pair.Value);
            }

            return null;
        }

        static void usage()
        {
            Console.WriteLine("Usage: opcodemerge 'our file' 'their file' [flags]");
            Console.WriteLine("    flags:");
            Console.WriteLine("       -D             ignore our opcodes with wrong build");
            Console.WriteLine("       -d             ignore their opcodes with wrong build");
            Console.WriteLine("       -b [build]     use this build (required for d,D)");
            Console.WriteLine("       -o [file]      log file");
            Console.WriteLine("Example: opcodemerge opcodes.cpp opcodes_their.cpp -b 12340 -d");
        }

        static void Main(string[] args)
        {
            bool ignoreTheirBuild = false;
            bool ignoreOurBuild = false;
            ushort build = 0;
            string their;
            string our;
            StreamWriter writer = null;

            try
            {
                our = args[0];
                their = args[1];

                for (int i = 2; i < args.Length; ++i)
                {
                    switch (args[i])
                    {
                        case "-d":
                            ignoreTheirBuild = true;
                            break;
                        case "-D":
                            ignoreOurBuild = true;
                            break;
                        case "-b":
                            build = ushort.Parse(args[++i]);
                            break;
                        case "-o":
                            Console.SetOut(writer = new StreamWriter(args[++i]));
                            break;
                    }
                }

                if ((ignoreTheirBuild || ignoreOurBuild) && build == 0)
                    throw new Exception();
            }
            catch
            {
                usage();
                return;
            }

            var ourOpcodes = new Dictionary<string, uint>();
            var theirOpcodes = new Dictionary<string, uint>();

            Action<string, bool, Dictionary<string, uint>> LoadFile = (filename, ignoreBuild, dict) =>
            {
                var regex = new Regex(@"([\w\d]+)\s*= ?([\da-fA-Fxhu]+),");
                var lines = File.ReadAllLines(filename);
                foreach (var line in lines)
                {
                    if (ignoreBuild && line.IndexOf(build.ToString()) == -1)
                        continue;

                    var match = regex.Match(line);
                    if (match.Success)
                    {
                        ulong val = match.Groups[2].Value.ToNumeric<ulong>();
                        if (val < 0xFFFF)
                        {
                            if (dict.ContainsValue((uint)val))
                                Console.WriteLine("Error: Duplicate opcode {0} in file '{1}'", val, filename);
                            else if (!dict.ContainsKey(match.Groups[1].Value))
                                dict.Add(match.Groups[1].Value, (uint)val);
                            else
                                Console.WriteLine("Error: Multiple defined opcode in file '{0}': " + match.Groups[1].Value, filename);
                        }
                    }
                }
            };

            LoadFile(our, ignoreOurBuild, ourOpcodes);
            LoadFile(their, ignoreTheirBuild, theirOpcodes);

            var ourContent = File.ReadAllText(our);
            bool modified = false;

            for (uint i = 0; i < ushort.MaxValue; ++i)
            {
                var ourPair = GetPair(ourOpcodes, i);
                var theirPair = GetPair(theirOpcodes, i);

                if (ourPair == null && theirPair == null)
                    continue;

                if (ourPair == null)
                {
                    ourPair = GetPair(ourOpcodes, theirPair.Item1);
                    if (ourPair != null)
                        Console.WriteLine("Opcode {0} value conflict: our {1}, their {2}", theirPair.Item1, ourPair.Item2, theirPair.Item2);
                    else
                    {
                        Console.Write("Their unique opcode: {0} = {1}", theirPair.Item1, theirPair.Item2);

                        string newCont = Regex.Replace(ourContent, "(" + theirPair.Item1 + @"\s*= ?)([\d\w]+),",
                            "${1}" + theirPair.Item2 + ",");
                        if (newCont != ourContent)
                        {
                            modified = true;
                            ourContent = newCont;
                            Console.WriteLine(" (added)");
                        }
                        else
                        {
                            var color = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(" (NOT ADDED)");
                            Console.ForegroundColor = color;
                        }
                    }
                }
                //else
                //    ourOpcodes.Remove(ourPair.Item1);

                if (theirPair == null)
                {
                    theirPair = GetPair(theirOpcodes, ourPair.Item1);
                    if (theirPair != null)
                        Console.WriteLine("Opcode {0} value conflict: our {1}, their {2}", theirPair.Item1, ourPair.Item2, theirPair.Item2);
                    else
                        Console.WriteLine("Our unique opcode: {0} = {1},", ourPair.Item1, ourPair.Item2);
                }
                //else
                //    theirOpcodes.Remove(theirPair.Item1);

                if (theirPair != null && ourPair != null)
                {
                    if (theirPair.Item1 != ourPair.Item1)
                        Console.WriteLine("Opcode {0} name conflict: our {1}, their {2}", theirPair.Item2, ourPair.Item1, theirPair.Item1);
                }

                //Console.WriteLine("");
            }

            if (modified)
                File.WriteAllText(our + ".new", ourContent);

            if (writer != null)
                writer.Close();
        }
    }
}
