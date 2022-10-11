// <copyright file="RGRadioButton.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_RadioButton.Controls
{
    public class RGRadioButton : RadioButton
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Known Issues]
         * ------------------------------------------------------------------------------------------------
         * 
         *          [1]. Auto Size Issue (Even if Code Sets AutoSize to 'false'; it NEVER Initiates as Such.
         *          [2]. Control Size NEVER Resizes to Fit Text Content.
         *          [3]. Minimum Size MUST Contain the Radio Ellipse Size
         *          [4]. Expose the Size of Radio Button Ellipses (Set the to Custom Properties)
         *          [5]. If Border Size is Set Over the Control Height (It Looks Great)
         * 
         */
        #endregion

        #region <Configuration>
        /// <summary> Set Control Style Configuration. </summary>
        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion


        #region <Constructor>
        public RGRadioButton()
        {
            // -> Set Control Style Configuration
            SetStyles();

            // -> Set Initial Configuration
            ForeColor = Color.White;

            // -> Set Inital Control Size
            MinimumSize = new Size(30, 21);
        }
        #endregion


        #region <Properties>
        private Color checkedColor = Color.Red;
        [Category("1. Custom Properties"), DisplayName("1. Checked Color")]
        [Description("Set Checked Color")]
        [Browsable(true)]
        public Color CheckedColor
        {
            get { return checkedColor; }
            set { checkedColor = value; }
        }

        private Color unCheckedColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("2. Unchecked Color")]
        [Description("Set Unchecked Color")]
        [Browsable(true)]
        public Color UnCheckedColor
        {
            get { return unCheckedColor; }
            set { unCheckedColor = value; Invalidate(); }
        }

        [Category("1. Custom Properties"), DisplayName("3. Text Color")]
        [Description("Set Text Color.\n (A Shortcut to ForeColor)")]
        [Browsable(true)]
        public Color TextColor
        {
            get { return ForeColor; }
            set { ForeColor = value; }
        }
        #endregion


        #region <Overriden Events>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // -> Configuration for Best Smooth Graphics
            // Ref: https://stackoverflow.com/questions/33878184/c-sharp-how-to-make-smooth-arc-region-using-graphics-path
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // May Cause Glitches
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            DrawRadioButton(e);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // The Tutorial Uses the Line Bellow.
            // However; the Line Bellow Causes Problems when Setting Anchoring.
            // Width = TextRenderer.MeasureText(Text, Font).Width + 30;
        }
        #endregion

        #region <Methods>
        private void DrawRadioButton(PaintEventArgs e)
        {
            float rbBorderSize = 18F; // Just Try 29F! =)
            float rbCheckSize = 12F;

            RectangleF rbRectBorder = new RectangleF()
            {
                X = 0.5F,
                Y = (Height - rbBorderSize) / 2,                                // Center
                Width = rbBorderSize,
                Height = rbBorderSize
            };

            RectangleF rbRectCheck = new RectangleF()
            {
                X = rbRectBorder.X + ((rbRectBorder.Width - rbCheckSize) / 2),  // Center
                Y = (Height - rbCheckSize) / 2,                                 // Center
                Width = rbCheckSize,
                Height = rbCheckSize
            };


            // Drawing
            using (Pen penBorder = new Pen(checkedColor, 1.6F))
            using (SolidBrush rbBrushCheck = new SolidBrush(checkedColor))
            using (SolidBrush rbBrushText = new SolidBrush(ForeColor))
            {
                // Draw Surface
                e.Graphics.Clear(BackColor);

                // Draw Radio Button
                if (Checked)
                {
                    e.Graphics.DrawEllipse(penBorder, rbRectBorder);                   // Circle Border
                    e.Graphics.FillEllipse(rbBrushCheck, rbRectCheck);                 // Circle Radio Check
                }

                else
                {
                    penBorder.Color = unCheckedColor;
                    e.Graphics.DrawEllipse(penBorder, rbRectBorder);                   // Circle Border
                }

                // Draw Text
                e.Graphics.DrawString(Text, Font, rbBrushText, rbBorderSize + 8, (Height - TextRenderer.MeasureText(Text, Font).Height) / 2); // Y = Center
            }
        }
        #endregion
    }
}
