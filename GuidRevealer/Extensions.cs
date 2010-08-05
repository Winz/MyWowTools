using System;
using System.Windows.Forms;
using System.Drawing;

namespace GuidRevealer
{
    static class Extensions
    {
        public static void AppendBold(this RichTextBox tb, string text)
        {
            Font orig = tb.SelectionFont;
            tb.SelectionFont = new Font(orig.FontFamily, orig.Size, FontStyle.Bold);
            tb.AppendText(text);
            tb.SelectionFont = orig;
        }
    }
}
