using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace SD2_ScriptCheck
{
    static partial class Program
    {
        static int Errors = 0;

        static void Main(string[] args)
        {
            ConsoleWriter.Instance.BaseWriter = Console.Out;
            Console.SetOut(ConsoleWriter.Instance);

            var ver = Assembly.GetExecutingAssembly().GetName().Version;
            string title = String.Format("ScriptDev2 Script Checker v{0}.{1}.{2}", ver.Major, ver.Minor, ver.Build);
            Console.Title = title;
            Console.WriteLine(title);
            Console.WriteLine();

            if (!File.Exists("system/ScriptLoader.cpp"))
            {
                Console.WriteLine("Error: 'system/ScriptLoader.cpp' file not found");
                Console.ReadLine();
                return;
            }

            if (!Directory.Exists("scripts"))
            {
                Console.WriteLine("Error: 'scripts' directory not found");
                Console.ReadLine();
                return;
            }

            try
            {
                // Load all script files into memory
                LoadScriptFiles();

                // Check ScriptLoader.cpp and loaders consistency
                CheckScriptLoader();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: An exception occured while running checks: " + ex.ToString());
            }

            /*********** SUMMARY ***********/

            Console.WriteLine();
            Console.WriteLine("Total Errors: {0}", Errors);

            ConsoleWriter.Instance.Close();
            ScriptFiles.Clear();
            ScriptLoaders.Clear();

            Console.ReadLine();
        }
    }
}
