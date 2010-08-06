using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SD2_ScriptCheck
{
    static partial class Program
    {
        internal class ScriptLoader
        {
            public string ScriptName = "";
            public bool Externed = false;
            public bool Called = false;
            public bool Defined = false;
        }

        static List<ScriptLoader> ScriptLoaders = new List<ScriptLoader>();

        static void CheckScriptLoader()
        {
            // Load Script Loader Definitions
            foreach (KeyValuePair<string, string> Pair in ScriptFiles)
            {
                string file = Pair.Key;
                string Code = Pair.Value;

                var loaderCallMatches = Regex.Matches(Code, @"\s+void\s+AddSC_([\w\d]+)\s*\(",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                foreach (var callmatch in loaderCallMatches)
                {
                    string loaderName = ((Match)callmatch).Groups[1].Value;

                    ScriptLoader loader = ScriptLoaders.FirstOrDefault((x) => { return x.ScriptName == loaderName; });
                    if (loader != null)
                    {
                        ++Errors;
                        Console.WriteLine("Error: Loader '{0}' redefined in file '{1}'",
                            loaderName, Path.GetFileName(file));
                    }
                    else
                    {
                        ScriptLoaders.Add(new ScriptLoader
                        {
                            ScriptName = loaderName,
                            Defined = true
                        });
                    }
                }
            }

            // Load Externed Loaders

            String FullLoaderCode = PrepareScriptFile(File.ReadAllText("system/ScriptLoader.cpp"));

            var externs = Regex.Matches(FullLoaderCode, @"^\s*?extern\s+void\s+AddSC_([\w\d]+)\s*\(",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline);

            foreach (var externMatch in externs)
            {
                string loaderName = ((Match)externMatch).Groups[1].Value;

                ScriptLoader loader = ScriptLoaders.FirstOrDefault((x) => { return x.ScriptName == loaderName; });
                if (loader != null)
                {
                    if (!loader.Externed)
                        loader.Externed = true;
                    else
                    {
                        ++Errors;
                        Console.WriteLine("Error: Loader '{0}' externed multiple times", loaderName);
                    }
                }
                else
                {
                    ScriptLoaders.Add(new ScriptLoader
                    {
                        ScriptName = loaderName,
                        Externed = true
                    });
                }
            }
            
            // Load Called Loaders

            Match LoaderCodeMatch = Regex.Match(FullLoaderCode, @"void AddScripts\(\)\s+{(.+)}",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

            if (!LoaderCodeMatch.Success)
            {
                Console.WriteLine("Error: Failed to find 'void AddScripts' in ScriptLoader.cpp");
                Console.ReadLine();
                return;
            }
            string LoaderCode = LoaderCodeMatch.Groups[1].Value;

            var calls = Regex.Matches(LoaderCode, @"^\s*?AddSC_([\w\d]+)\s*\(",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline);

            foreach (var callmatch in calls)
            {
                string loaderName = ((Match)callmatch).Groups[1].Value;

                ScriptLoader loader = ScriptLoaders.FirstOrDefault((x) => { return x.ScriptName == loaderName; });
                if (loader != null)
                {
                    if (!loader.Called)
                        loader.Called = true;
                    else
                    {
                        ++Errors;
                        Console.WriteLine("Error: Loader '{0}' called multiple times", loaderName);
                    }
                }
                else
                {
                    ScriptLoaders.Add(new ScriptLoader
                    {
                        ScriptName = loaderName,
                        Called = true
                    });
                }
            }

            // Output Summary

            Errors += ScriptLoaders.Count((x) =>
            {
                bool result = !x.Defined || !x.Externed || !x.Called;

                if (result)
                {
                    String output = "Error: Loader '" + x.ScriptName + "'";

                    if (!x.Defined)
                        output += ", not defined";

                    if (!x.Externed)
                        output += ", not externed";
                    else if (!x.Called)
                        output += ", not called";

                    Console.WriteLine(output);
                }

                return result;
            });
        }
    }
}
