using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SD2_ScriptCheck
{
    class Program
    {
        class ScriptLoader
        {
            public string ScriptName = "";
            public bool Externed = false;
            public bool Called = false;
            public bool Defined = false;
        }

        static List<ScriptLoader> ScriptLoaders = new List<ScriptLoader>();

        static void Main(string[] args)
        {
            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            Console.WriteLine("ScriptDev2 Script Checker v{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
            Console.WriteLine();

            if (!File.Exists("system/ScriptLoader.cpp"))
            {
                Console.WriteLine("<system/ScriptLoader.cpp> not found");
                Console.ReadLine();
                return;
            }

            if (!Directory.Exists("scripts"))
            {
                Console.WriteLine("<scripts> directory not found");
                Console.ReadLine();
                return;
            }

            int errors = 0;

            /*********** DEFINED LOADERS ***********/

            string[] files = Directory.GetFiles("./scripts/", "*.cpp", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                String Code = File.ReadAllText(file);
                Code = Regex.Replace(Code, @"\/\*.*?\*\/", "", RegexOptions.Singleline);

                var loaderCallMatches = Regex.Matches(Code, @"\s+void\s+AddSC_([\w\d]+)\s*\(",
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                foreach (var callmatch in loaderCallMatches)
                {
                    string loaderName = ((Match)callmatch).Groups[1].Value;

                    ScriptLoader loader = ScriptLoaders.FirstOrDefault((x) => { return x.ScriptName == loaderName; });
                    if (loader != null)
                    {
                        ++errors;
                        Console.WriteLine("Loader '{0}' redefined in file '{1}'",
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

            /*********** EXTERNED LOADERS ***********/

            String FullLoaderCode = File.ReadAllText("system/ScriptLoader.cpp");
            FullLoaderCode = Regex.Replace(FullLoaderCode, @"\/\*.*?\*\/", "", RegexOptions.Singleline);

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
                        ++errors;
                        Console.WriteLine("Loader '{0}' externed multiple times", loaderName);
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

            /*********** CALLED LOADERS ***********/

            Match LoaderCodeMatch = Regex.Match(FullLoaderCode, @"void AddScripts\(\)\s+{(.+)}",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Singleline);

            if (!LoaderCodeMatch.Success)
            {
                Console.WriteLine("Failed to find 'void AddScripts' in ScriptLoader.cpp");
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
                        ++errors;
                        Console.WriteLine("Loader '{0}' called multiple times", loaderName);
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

            /*********** SUMMARY ***********/

            errors += ScriptLoaders.Count((x) =>
            {
                bool result = !x.Defined || !x.Externed || !x.Called;

                if (result)
                {
                    Console.Write("Loader '{0}'", x.ScriptName);

                    if (!x.Defined)
                        Console.Write(", not defined");

                    if (!x.Externed)
                        Console.Write(", not externed");
                    else if (!x.Called)
                        Console.Write(", not called");

                    Console.WriteLine();
                }

                return result;
            });

            Console.WriteLine();
            Console.WriteLine("Total Errors: {0}", errors);

            Console.ReadLine();
        }
    }
}
