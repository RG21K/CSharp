// <copyright file="RGProgressBar.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
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

namespace RG_Progress_Bar.Controls
{
    public class RGProgressBar : ProgressBar
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Known Issues]
         * ---------------------------------------------------------------------------------------------
         *          [1]. ProgressBar Value Label NOT Propely Aligned.
         *               When the Label Value is Set to "Sliding" and Value is Bellow 5%; the Label is Shown
         *               either Hidden or PArtially Hidden.
         *  
         *          [2]. While ProgressBar is Running the Value Label Text is Overlapped
         *                  -> [Solution]: Value Label Background Color must NOT be Transparent.
         *          
         *          [3]. While ProgressBar is Running it Draws Garbage (Mimics Other Controls) into the Control
         *                  -> Why?
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
        public RGProgressBar()
        {
            // -> Set Control Style Configuration
            SetStyles();

            // -> Value Label ForeColor
            ForeColor = Color.Gainsboro;
        }
        #endregion


        #region <Fields>
        // -> Others
        private bool drawn = false;
        private bool painting = true;
        #endregion


        #region <Custom Properties>
        // -> Bar
        private Color barBackColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("01. Bar BackColor")]
        [Description("Set Bar Color")]
        [Browsable(true)]
        public Color BarBackColor
        {
            get { return barBackColor; }
            set { barBackColor = value; Invalidate(); }
        }

        private int barHeight = 6;
        [Category("1. Custom Properties"), DisplayName("02. Bar Heigth")]
        [Description("Set ProgressBar Height")]
        [Browsable(true)]
        public int BarHeight
        {
            get { return barHeight; }
            set { barHeight = value; Invalidate(); }
        }

        // -> Slider
        private Color barSliderColor = Color.FromArgb(255, 38, 224, 127);
        [Category("1. Custom Properties"), DisplayName("03. Slider BackColor")]
        [Description("Set Slider Color")]
        [Browsable(true)]
        public Color SliderColor
        {
            get { return barSliderColor; }
            set { barSliderColor = value; Invalidate(); }
        }

        private int barSliderHeight = 6;
        [Category("1. Custom Properties"), DisplayName("04. Slider Heigth")]
        [Description("Set ProgressBar Slider Height")]
        [Browsable(true)]
        public int SliderHeight
        {
            get { return barSliderHeight; }
            set
            {
                barSliderHeight = value;
                this.Invalidate();
            }
        }

        // -> Label
        [Category("1. Custom Properties"), DisplayName("05. Label Font")]
        [Description("Set Label Font")]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
            }
        }

        [Category("1. Custom Properties"), DisplayName("06. Label ForeColor")]
        [Description("Set Label ForeColor")]
        [Browsable(true)]
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        private Color labelBackColor = Color.FromArgb(255, 25, 25, 25);
        [Category("1. Custom Properties"), DisplayName("07. Label BackColor")]
        [Description("Set Label Back Color")]
        [Browsable(true)]
        public Color LabelBackColor
        {
            get { return labelBackColor; }
            set { labelBackColor = value; Invalidate(); }
        }

        public enum TextPosition { Left, Right, Center, Sliding, None }
        private TextPosition barTextLocation = TextPosition.Right;
        [Category("1. Custom Properties"), DisplayName("08. Label Text Alignment")]
        [Description("Set Text Position")]
        [Browsable(true)]
        public TextPosition TextLocation
        {
            get { return barTextLocation; }
            set
            {
                barTextLocation = value;
                this.Invalidate();
            }
        }

        private string symbolBefore = "";
        [Category("1. Custom Properties"), DisplayName("09. Symbol Before")]
        [Description("Show Symbol Before ProgressBar Value")]
        [Browsable(true)]
        public string SymbolBefore
        {
            get { return symbolBefore; }
            set
            {
                symbolBefore = value;
                this.Invalidate();
            }
        }

        private string symbolAfter = "";
        [Category("1. Custom Properties"), DisplayName("10. Symbol After")]
        [Description("Show Symbol After ProgressBar Value")]
        [Browsable(true)]
        public string SymbolAfter
        {
            get { return symbolAfter; }
            set
            {
                symbolAfter = value;
                this.Invalidate();
            }
        }

        private bool showMaximun = false;
        [Category("1. Custom Properties"), DisplayName("11. Show Maximum")]
        [Description("Shows ProgressBar Maximum Value in Front of Current Value.")]
        [Browsable(true)]
        public bool ShowMaximun
        {
            get { return showMaximun; }
            set
            {
                showMaximun = value;
                this.Invalidate();
            }
        }
        #endregion


        #region <Overriden Events>
        /// <summary> Paint the Background and the Bar. </summary>
        /// <param name="_pe"></param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            if (painting)
            {
                if (!drawn)
                {
                    // -> Graphic Rendering (Configuration for Best & Smooth Graphics)
                    // Ref: https://stackoverflow.com/questions/33878184/c-sharp-how-to-make-smooth-arc-region-using-graphics-path
                    e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                    e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // May Cause Glitches
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    Rectangle barRectangle = new Rectangle(0, 0, this.Width, BarHeight);

                    using (var _brushChannel = new SolidBrush(barBackColor))
                    {
                        if (barHeight >= barSliderHeight) { barRectangle.Y = this.Height - barHeight; }
                        else { barRectangle.Y = this.Height - ((barHeight + barSliderHeight) / 2); }

                        // Painting
                        e.Graphics.Clear(this.Parent.BackColor);               // Surface
                        e.Graphics.FillRectangle(_brushChannel, barRectangle); // Bar

                        // Stop Painting
                        if (!DesignMode) { drawn = true; }
                    }
                }

                // Reset Painting (Clear the Background and the Bar)
                if (Value.Equals(Maximum) || Value.Equals(Minimum)) { drawn = false; }
            }
        }

        // -> Paint Slider
        protected override void OnPaint(PaintEventArgs e)
        {
            if (painting)
            {
                // -> Configuration for Best Smooth Graphics
                // Ref: https://stackoverflow.com/questions/33878184/c-sharp-how-to-make-smooth-arc-region-using-graphics-path
                e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
                e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
                e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // May Cause Glitches
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                double scaleFactor = ((double)Value - Minimum) / ((double)Maximum - Minimum);
                int sliderWidth = (int)(Width * scaleFactor);
                Rectangle sliderRectangle = new Rectangle(0, 0, sliderWidth, barSliderHeight);

                using (var _brushSlider = new SolidBrush(barSliderColor))
                {
                    if (barSliderHeight >= barHeight) { sliderRectangle.Y = this.Height - barSliderHeight; }
                    else { sliderRectangle.Y = this.Height - ((barSliderHeight + barHeight) / 2); }

                    // Paint Slider
                    if (sliderWidth > 1)
                        e.Graphics.FillRectangle(_brushSlider, sliderRectangle);

                    // Paint Text
                    //if (barTextLocation != TextPosition.None)
                    //{
                        DrawTextValue(e, sliderWidth, sliderRectangle);
                    //}
                }
            }

            if (Value.Equals(Maximum)) { painting = false; }     // Stop Painting
            else { painting = true; }                            // Keep Painting
        }
        #endregion


    

        #region <Methods> : (Drawing)
        //-> Paint value text
        private void DrawTextValue(PaintEventArgs e, int sliderWidth, Rectangle rectSlider)
        {
            // Default Label Text
            string text = $"{symbolBefore}{Value}{symbolAfter}";

            // Label Text Showing Maximum Value
            if (showMaximun) text = text + "/" + symbolBefore + this.Maximum.ToString() + symbolAfter;

            var textSize = TextRenderer.MeasureText(text, this.Font);
            var rectText = new Rectangle(0, 0, textSize.Width, textSize.Height + 2);

            using (var brushText = new SolidBrush(this.ForeColor))
            using (var brushTextBack = new SolidBrush(labelBackColor))
            using (var textFormat = new StringFormat())
            {
                switch (barTextLocation)
                {
                    case TextPosition.Left:
                        rectText.X = 0;
                        textFormat.Alignment = StringAlignment.Near;
                        break;

                    case TextPosition.Right:
                        rectText.X = Width - textSize.Width;
                        textFormat.Alignment = StringAlignment.Far;
                        break;

                    case TextPosition.Center:
                        rectText.X = (Width - textSize.Width) / 2;
                        textFormat.Alignment = StringAlignment.Center;
                        break;

                    case TextPosition.Sliding:
                        var textRight = rectText.Right;

                        if (textRight >= ClientRectangle.Right)
                        {
                            rectText.X = sliderWidth - text.Length;
                        }
                        else
                        {
                            rectText.X = sliderWidth + text.Length;
                        }
                        //rectText.X = sliderWidth - textSize.Width; // Previous
                        
                        textFormat.Alignment = StringAlignment.Center;

                        // Clean Previous Text Surface
                        using (var _brushClear = new SolidBrush(labelBackColor))
                        // using (var _brushClear = new SolidBrush(Parent.BackColor))
                        {
                            var rect = rectSlider;
                            rect.Y = rectText.Y;
                            rect.Height = rectText.Height;
                            e.Graphics.FillRectangle(_brushClear, rect);
                        }
                        break;
                }

                // Painting
                e.Graphics.FillRectangle(brushTextBack, rectText);
                e.Graphics.DrawString(text, this.Font, brushText, rectText, textFormat);
            }
        }
        #endregion
    }
}
