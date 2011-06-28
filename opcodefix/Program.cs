using System;
using System.IO;
using System.Text;

namespace opcodefix
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cppLines = File.ReadAllLines("Opcodes.cpp");
            string[] hdrLines = File.ReadAllLines("Opcodes.h");
            StringBuilder content = new StringBuilder(250000);

            // Skip text before opcode definitions
            int cppIndex;
            int hdrIndex;
            {
                for (cppIndex = 0; cppIndex < cppLines.Length; )
                {
                    content.AppendLine(cppLines[cppIndex]);

                    if (cppLines[cppIndex++].StartsWith("{"))
                        break;
                }

                for (hdrIndex = 0; hdrIndex < hdrLines.Length; )
                {
                    if (hdrLines[hdrIndex++].StartsWith("{"))
                        break;
                }
            }

            int pad;
            {
                pad = cppLines[cppIndex].IndexOf(" STATUS_") + 1;

                if (pad <= 0)
                {
                    Console.WriteLine("ERROR: Cannot resolve pad in Opcodes.cpp:{0}", cppIndex + 1);
                    Console.WriteLine("ERROR: Cannot continue.");
                    return;
                }
            }

            int count = 0;
            do
            {
                string hdrLine = hdrLines[hdrIndex];
                string cppLine = cppLines[cppIndex];

                if (cppLines[cppIndex].StartsWith("}") || hdrLines[hdrIndex].StartsWith("}"))
                {
                    content.AppendLine(cppLine);
                    continue;
                }

                string hdrOpcodeName;
                {
                    int idx = hdrLine.IndexOf('=');
                    if (idx < 0)
                    {
                        Console.WriteLine("ERROR: Not found '=' in Opcodes.h:{0}", hdrIndex + 1);
                        break;
                    }

                    hdrOpcodeName = hdrLine.Substring(0, idx).Trim();
                }

                string cppOpcodeName;
                {
                    int idx1 = cppLine.IndexOf('"');
                    int idx2 = cppLine.LastIndexOf('"');

                    if (idx1 < 0 || idx2 < 0 || idx1 == idx2)
                    {
                        Console.WriteLine("ERROR: Failed to find two quotes in Opcodes.cpp:{0}", cppIndex + 1);
                        break;
                    }

                    cppOpcodeName = cppLine.Substring(idx1 + 1, idx2 - idx1 - 1);
                }

                if (cppOpcodeName == hdrOpcodeName)
                {
                    content.AppendLine(cppLine);
                }
                else
                {
                    int statusIdx;
                    {
                        // space is here because opcode names can contain "STATUS_"
                        statusIdx = cppLine.IndexOf(" STATUS_") + 1;
                        if (statusIdx <= 0)
                        {
                            Console.WriteLine("ERROR: Failed to find ' STATUS_' in Opcodes.cpp:{0}", cppIndex + 1);
                            break;
                        }
                    }

                    string line;
                    {
                        string beforeOpcodeName = cppLine.Substring(0, cppLine.IndexOf('"') + 1);
                        string opcodeName = hdrOpcodeName;
                        string opcodeAfterDelim = "\",";
                        string after = cppLine.Substring(statusIdx);

                        line = (beforeOpcodeName + opcodeName + opcodeAfterDelim);

                        if (line.Length > pad)
                        {
                            // Opcode name is too long to fit space between other initializer arguments.
                            // We will remove spaces between the other arguments to fit everything in.
                            // Note: this can be done faster.

                            int delta = line.Length - pad;
                            StringBuilder builder = new StringBuilder(after.Length - delta);

                            for (int i = 0; i < after.Length; ++i)
                            {
                                if (after[i] != ' ' || delta == 0)
                                    builder.Append(after[i]);
                                else
                                    --delta;
                            }

                            after = builder.ToString();
                        }
                        else
                            line = line.PadRight(pad);

                        line += after;
                    }

                    content.AppendLine(line);

                    ++count;
                }
            }
            while (++cppIndex < cppLines.Length && ++hdrIndex < hdrLines.Length);

            File.WriteAllText("Opcodes.cpp", content.ToString());

            Console.WriteLine("Done. Fixed opcodes: {0}", count);
        }
    }
}
