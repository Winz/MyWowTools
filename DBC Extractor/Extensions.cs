using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace DbcExtractor
{
    static class Extensions
    {
        public static string ReadCString(this BinaryReader reader)
        {
            byte num;
            List<byte> temp = new List<byte>();

            while ((num = reader.ReadByte()) != 0)
                temp.Add(num);

            return Encoding.UTF8.GetString(temp.ToArray());
        }

        public static unsafe object ReadStruct(this BinaryReader reader, Type T)
        {
            byte[] rawData = reader.ReadBytes(Marshal.SizeOf(T));

            GCHandle handle = GCHandle.Alloc(rawData, GCHandleType.Pinned);
            object returnObject = Marshal.PtrToStructure(handle.AddrOfPinnedObject(), T);

            handle.Free();

            return returnObject;
        }
        public static StringBuilder AppendFormatLine(this StringBuilder builder, string format, params object[] arg0)
        {
            return builder.AppendFormat(format, arg0).AppendLine();
        }
        public static string Escape(this String str)
        {
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace(Environment.NewLine, "\\n");
        }
    }
}
