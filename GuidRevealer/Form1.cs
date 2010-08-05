using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GuidRevealer
{
    public partial class MainForm : Form
    {
        private string getDefaultBrowser()
        {
            string browser = string.Empty;
            RegistryKey key = null;
            try
            {
                key = Registry.ClassesRoot.OpenSubKey(@"HTTP\shell\open\command", false);

                //trim off quotes
                browser = key.GetValue(null).ToString().ToLower().Replace("\"", "");
                if (!browser.EndsWith("exe"))
                {
                    //get rid of everything after the ".exe"
                    browser = browser.Substring(0, browser.LastIndexOf(".exe") + 4);
                }
            }
            finally
            {
                if (key != null) key.Close();
            }
            return browser;
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void OutputGuid(Guid guid)
        {
            output.Clear();

            output.AppendText("HighGuid: ");
            output.AppendBold(String.Format("{0} (0x{1:X4})", guid.GetHigh(), (int)guid.GetHigh()));
            output.AppendText(Environment.NewLine);

            output.AppendText("Entry: ");
            output.AppendBold(String.Format("{0}", guid.GetEntry()));
            output.AppendText(Environment.NewLine);

            output.AppendText("Counter: ");
            output.AppendBold(String.Format("{0}", guid.GetCounter()));

            string type = guid.GetHighURLType();
            if (!string.IsNullOrEmpty(type))
            {
                whUrl.Visible = true;
                whUrl.Text = "http://www.wowhead.com/" + type + "=" + guid.GetEntry();
            }
            else
                whUrl.Visible = false;
        }

        private void revealDecimal_Click(object sender, EventArgs e)
        {
            ulong decRepr = 0;
            try
            {
                decRepr = ulong.Parse(textBox1.Text);
            }
            finally
            {
                OutputGuid(new Guid(decRepr));
            }
        }

        private void revealHex_Click(object sender, EventArgs e)
        {
            ulong decRepr = 0;
            try
            {
                string text = textBox2.Text;

                if (text.StartsWith("0x"))
                    text = text.Substring(2);
                if (text.EndsWith("h"))
                    text = text.Substring(0, text.Length - 1);

                decRepr = ulong.Parse(text, System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            finally
            {
                OutputGuid(new Guid(decRepr));
            }
        }

        private void whUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(getDefaultBrowser(), ((LinkLabel)sender).Text);
        }
    }
}
