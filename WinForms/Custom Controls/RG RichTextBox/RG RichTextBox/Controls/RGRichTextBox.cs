// <copyright file="RGRichTextBox.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_RichTextBox.Controls
{
    public class RGRichTextBox : RichTextBox
    {
        // Last Edited: 2022, 10.10
0
        #region <Developer Notes>
        /*
         * [Info]
         * -----------------------------------------------------------------------------------------
         * 
         * An Inproved RichTextBox to Suit:
         * 
         *      - Chat Windows
         *      - Logging
         *      - Etc
         * 
         * 
         * [Features]
         * -----------------------------------------------------------------------------------------
         * 
         *      - Time Stamps
         *      - Auto Scroll to Caret
         *      - Customized Message Colors ?
         * 
         * [Usage]
         * -----------------------------------------------------------------------------------------
         * 
         * Println(source, message);
         * 
         */
        #endregion

        #region <Constructor>
        public RGRichTextBox()
        {
            BackColor = Color.FromArgb(255, 36, 36, 52);
            BorderStyle = BorderStyle.None;
            ReadOnly = true;
            ForeColor = Color.White;
        }
        #endregion


        #region <Fields>
        // -> Stamps
        private string currentTime = DateTime.Now.ToString("HH:mm");
        private string timeStamp => $"[{currentTime}]";

        // -> Input Source
        private string inputSource { get; set; } = string.Empty;
        private string inputSourceStamp => $"<{inputSource}>";
        #endregion


        #region <Custom Properties>
        private bool autoScrollToCaret = true;
        [Category("1. Custom Text"), DisplayName("Auto Scroll to Caret")]
        [Description("Set AutoScrollToCaret.\n AutoScrolls to Caret when Custom Text is Printed")]
        [Browsable(true)]
        public bool AutoScrollToCaret
        {
            get { return autoScrollToCaret; }
            set { autoScrollToCaret = value; }
        }

        private Color timeStampColor = Color.Gainsboro;
        [Category("1. Custom Text"), DisplayName("Time Color")]
        [Description("Set Time Stamp Color")]
        [Browsable(true)]
        public Color TimeStampColor
        {
            get { return timeStampColor; }
            set { timeStampColor = value; }
        }

        private Color inputSourceStampColor = Color.DeepSkyBlue;
        [Category("1. Custom Text"), DisplayName("InputSource Color")]
        [Description("Set Input Source Stamp Color")]
        [Browsable(true)]
        public Color InputSourceStampColor
        {
            get { return inputSourceStampColor; }
            set { inputSourceStampColor = value; }
        }
        #endregion


        #region <Methods> : (Public)
        public void Println(string source, string text) => PrintLine(source, text);
        #endregion


        #region <Methods> : (Private)
        private void AppendCustomText(string text, Color color)
        {
            SelectionStart = TextLength;
            SelectionLength = 0;

            SelectionColor = color; // ? is this Correct?
            AppendText(text);
        }

        private void PrintLine(string textInputSource, string text)
        {
            // -> Update Control Data
            inputSource = textInputSource;

            // -> Append Stamps
            AppendCustomText($"{timeStamp} ", timeStampColor);
            AppendCustomText($"{inputSourceStamp} ", inputSourceStampColor);

            // -> Append Text
            AppendCustomText($"{text}\n", ForeColor);

            // -> Auto Scroll to Caret
            if (AutoScrollToCaret) { ScrollToCaret(); }
        }
        #endregion
    }
}
