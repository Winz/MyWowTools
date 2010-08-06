using System;
using System.Text.RegularExpressions;

namespace SD2_ScriptCheck
{
    static class CppCode
    {
        public static string Clean(string input)
        {
            /* Block comments */
            input = Regex.Replace(input, @"\/\*.*?\*\/", "", RegexOptions.Singleline);
            // Single line comments
            input = Regex.Replace(input, @"\/\/.+?$", "", RegexOptions.Multiline);

            return input;
        }

        public static string ExtractBlock(string input, int start_pos)
        {
            int depth = 0;
            bool in_string = false;
            int block_start_pos = -1;
            int block_end_pos = -1;

            int pos = start_pos;
            while ((pos = input.IndexOfAny(new char[] { '"', '{', '}' }, pos)) != -1)
            {
                switch (input[pos])
                {
                    case '"':
                        in_string = !in_string;
                        break;
                    case '{':
                        if (in_string)
                            break;

                        if (block_start_pos == -1)
                            block_start_pos = pos + 1;
                        else
                            ++depth;

                        break;
                    case '}':
                        if (in_string)
                            break;

                        if (depth == 0)
                            block_end_pos = pos;
                        else
                            --depth;

                        break;
                }

                pos += 1;

                if (block_start_pos != -1 && block_end_pos != -1)
                    return input.Substring(block_start_pos, block_end_pos - block_start_pos);
            }

            return String.Empty;
        }
    }
}
