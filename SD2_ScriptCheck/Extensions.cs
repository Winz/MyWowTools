using System;

namespace SD2_ScriptCheck
{
    internal static class Extensions
    {
        public static int IndexOfAny(this String input, string[] strings)
        {
            return input.IndexOfAny(strings, 0);
        }

        public static int IndexOfAny(this String input, string[] strings, int startIndex)
        {
            int index = -1;

            foreach (string str in strings)
            {
                if ((index = input.IndexOf(str)) != -1)
                    break;
            }

            return index;
        }
    }
}
