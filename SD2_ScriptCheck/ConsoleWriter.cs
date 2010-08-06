using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace SD2_ScriptCheck
{
    internal class ConsoleWriter : StreamWriter
    {
        public static readonly ConsoleWriter Instance = new ConsoleWriter();

        public TextWriter BaseWriter;

        public ConsoleWriter()
            : base(new FileStream(Assembly.GetExecutingAssembly().GetName().Name + ".log", FileMode.Create))
        {
            this.AutoFlush = true;

            if (base.BaseStream.Position != 0)
            {
                base.WriteLine();
                base.WriteLine();
            }
        }

        // Overloads

        public override Encoding Encoding
        {
            get { return Encoding.UTF8; }
        }

        public override void WriteLine(string value)
        {
            _Write(value + Environment.NewLine);
        }

        public override void WriteLine(string format, params object[] arg)
        {
            _Write(String.Format(format, arg) + Environment.NewLine);
        }

        public override void WriteLine()
        {
            _Write(Environment.NewLine);
        }

        public override void Write(string value)
        {
            _Write(value);
        }

        public override void Write(string format, params object[] arg)
        {
            _Write(String.Format(format, arg));
        }

        // Generic Write Method

        private void _Write(string text)
        {
            if (text.StartsWith("error", StringComparison.InvariantCultureIgnoreCase))
                Console.ForegroundColor = ConsoleColor.Red;
            else if (text.StartsWith("warning", StringComparison.InvariantCultureIgnoreCase))
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.Gray;

            // Write to console
            BaseWriter.Write(text);

            // Write to log file
            base.Write(String.Format("[{0:yyyy.MM.dd HH:mm:ss.ffff}] {1}", DateTime.Now, text));
        }
    }
}
