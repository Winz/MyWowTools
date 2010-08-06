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
        // filename (scripts\somedir\somefile.cpp) => cleaned code
        static Dictionary<string, string> ScriptFiles = new Dictionary<string, string>();

        static void LoadScriptFiles()
        {
            string[] files = Directory.GetFiles("scripts", "*.cpp", SearchOption.AllDirectories);
            foreach (string file in files)
            {
                String Code = CppCode.Clean(File.ReadAllText(file));
                ScriptFiles.Add(file, Code);
            }
        }
    }
}
