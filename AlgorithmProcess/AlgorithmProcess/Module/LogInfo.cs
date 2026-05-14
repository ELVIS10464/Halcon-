using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmProcess
{
    public class LogInfo
    {   

        public void AddLog(RichTextBox rtb, string type, string msg)
        {   
            if (type == "INFO")
            {
                AppendColoredText(rtb, "[" + Get_NowDateTime() + "]　" + type + "-->" + msg + Environment.NewLine, Color.Lime);
            }
            else if (type == "ERROR")
            {
                AppendColoredText(rtb, "[" + Get_NowDateTime() + "]　" + type + "-->" + msg + Environment.NewLine, Color.Red);
            }
            else if (type == "WARNING")
            {
                AppendColoredText(rtb, "[" + Get_NowDateTime() + "]　" + type + "-->" + msg + Environment.NewLine, Color.Yellow);
            }
        }

        private void AppendColoredText(RichTextBox rtb, string text, Color color)
        {
            rtb.SelectionStart = rtb.TextLength; // Set insertion point to the end
            rtb.SelectionLength = 0;             // Clear any existing selection
            rtb.SelectionColor = color;          // Set the color for the new text
            rtb.AppendText(text);                // Append the text
            rtb.SelectionColor = rtb.ForeColor;  // Reset to default fore color for subsequent text (optional)
            rtb.ScrollToCaret();
            rtb.Refresh();
        }

        public string Get_NowDateTime(string format = "")
        {
            string NowDateTime;

            if (format == "yyyyMMdd")
            {
                NowDateTime = DateTime.Now.ToString(format);
            }
            else if (format == "yyyyMMddHHmmss")
            {
                NowDateTime = DateTime.Now.ToString(format);
            }
            else if (format == "1")
            {
                NowDateTime = DateTime.Now.ToString();
            }
            else
            {
                NowDateTime = DateTime.Now.ToString();
            }

            return NowDateTime;
        }
    }
}
