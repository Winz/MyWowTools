using System;
using System.IO;
using System.Text.RegularExpressions;

namespace DbcExtractor
{
    static class Util
    {
        internal static void FixIcon(Type T, uint id)
        {
            string icon = DBC.GetString(T, id);
            if (!String.IsNullOrEmpty(icon))
                DBC.SetString(T, id, Path.GetFileNameWithoutExtension(icon).Urlize());
        }

        internal static string Urlize(this string input)
        {
            input = input
                .Replace(" / ", "-")
                .Replace("'", "")
                .Trim();

            input = Regex.Replace(input, @"[^a-z0-9_]", "-", RegexOptions.IgnoreCase);

            input = input
                .Trim('-')
                .Replace("--", "-")
                .Replace("--", "-");

            return input.ToLower();
        }
    }
}
