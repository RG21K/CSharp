// <copyright file="RGSeekBar.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_SeekBar.Controls
{
    public class RGSeekBar : PictureBox
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Info]
         * --------------------------------------------------------------------------
         * 
         * A SeekBar Suitable for Media Projects, to Seek Audio or Video Tracks.
         * 
         * 
         * [Known Issues]
         * --------------------------------------------------------------------------
         *          [1]. Adjusting the Value using KeyBoard is NOT Working.
         *          [2]. Bar Size Could also Fit the Control Size.
         * 
         * 
         * [Usage Info]
         * --------------------------------------------------------------------------
         * 
         * Event: OnValueChanged(value);
         * 
         */
        #endregion

        #region <Constructor>
        public RGSeekBar()
        {
            // Requests the Form to use a Secondary Buffer to Redraw the Content.
            // You can Set Either: DoubleBuffered = true;
            // or
            // SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //
            // Link: https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-reduce-graphics-flicker-with-double-buffering-for-forms-and-controls?view=netframeworkdesktop-4.8
            //
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true); // Allow Redrawing when Orientation Changes.

            Size = new Size(200, 25);   // Default Client Rectangle Size
        }
        #endregion


        #region <Fields> : (Control Rectangles)
        /// <summary> TrackBar Background Rectangle. </summary>
        private Rectangle BackgroundRectangleH
        {
            get
            {
                return new Rectangle
                {
                    Width = ClientRectangle.Width - (horizontalPadding * 2),
                    Height = backgroundThickness,

                    X = ClientRectangle.X + horizontalPadding,
                    Y = ClientRectangle.Height / 2 - backgroundThickness / 2,
                };
            }
        }

        /// <summary> TrackBar Background Rectangle. </summary>
        private Rectangle BackgroundRectangleV
        {
            get
            {
                return new Rectangle
                {
                    Width = backgroundThickness,
                    Height = ClientRectangle.Height - (horizontalPadding * 2),

                    X = ClientRectangle.Width / 2 - backgroundThickness / 2,
                    Y = ClientRectangle.Y + horizontalPadding,
                };
            }
        }



        /// <summary> TrackBar Foreground Rectangle. </summary>
        private Rectangle ForegroundRectangleH
        {
            get
            {
                return new Rectangle
                {
                    Width = (int)GetBarForegroundValueH() + sliderSize.Width / 2,
                    Height = foregroundThickness,

                    //X = ClientRectangle.X + horizontalPadding,
                    X = BackgroundRectangleH.X,
                    Y = ClientRectangle.Height / 2 - foregroundThickness / 2,
                };
            }
        }

        /// <summary> TrackBar Foreground Rectangle. </summary>
        private Rectangle ForegroundRectangleV
        {
            get
            {
                return new Rectangle
                {
                    Width = foregroundThickness,
                    Height = BackgroundRectangleV.Height - SliderRectangleV.Y,

                    X = BackgroundRectangleV.X + 2,
                    Y = BackgroundRectangleV.Bottom - (BackgroundRectangleV.Height - SliderRectangleV.Y),
                };
            }
        }


        /// <summary> The Slider Rectangle. 
        /// Note: Slider Rectangle MUST be the same for Both Orientation Values (Horizontal or Vertical). </summary>
        private Rectangle SliderRectangleH
        {
            get
            {
                // Previous
                return new Rectangle
                {
                    Width = sliderSize.Width,
                    Height = sliderSize.Height,

                    //X = GetSliderPositionValue(),
                    X = BackgroundRectangleH.X + (int)GetSliderPositionValueH(),
                    Y = (ClientRectangle.Height / 2) - (sliderSize.Height / 2),
                };
            }
        }

        /// <summary> The Slider Rectangle. 
        /// Note: Slider Rectangle MUST be the same for Both Orientation Values (Horizontal or Vertical). </summary>
        private Rectangle SliderRectangleV
        {
            get
            {
                return new Rectangle
                {
                    Width = sliderSize.Width,
                    Height = sliderSize.Height,

                    X = BackgroundRectangleV.X + (BackgroundRectangleV.Width / 2) - (sliderSize.Width / 2),
                    Y = (BackgroundRectangleV.Y) + (int)GetSliderPositionValueV(),
                };
            }
        }
        #endregion


        #region [!] <Auto Properties>
        /// <summary>
        /// Gets the TrackBar Value Changed Interaction.
        /// Used in Circumstances where the TrackBar is used as a SeekBar that Automatically Updated its Value;
        /// and there is a need to determine if the Value was Changed by the user or some sort of Automation.
        /// </summary>
        // public bool HasUserInteraction { get; private set; } 
        #endregion


        // - > ToolBar
        #region <Custom Properties> : (Mouse)
        private int numberMouseWheelTicks = 10;
        [Category("1. Custom Properties"), DisplayName("Scroll Ticks")]
        [Description("Set Number of MouseWheel Scrolling Ticks")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public int NumberMouseWheelTicks
        {
            get { return numberMouseWheelTicks; }
            set
            {
                if (value > 0) { numberMouseWheelTicks = value; }
                else throw new ArgumentOutOfRangeException("The Number of Mouse Wheel Ticks has to be Greather than 0");
            }
        }
        #endregion

        #region <Custom Properties> : (Bar Values)
        /// <summary> TrackBar Minimum Value. </summary>
        private long barMinimum = 0;
        [Category("1. Custom Properties"), DisplayName("Bar Minimum")]
        [Description("Sets Minimum TrackBar Value")]
        [Browsable(true)]
        public long BarMinimum
        {
            get { return barMinimum; }
            set { barMinimum = value; }
        }

        /// <summary> TrackBar Maximum Value. </summary>
        private long barMaximum = 100;
        [Category("1. Custom Properties"), DisplayName("Bar Maximum")]
        [Description("Sets Maximum TrackBar Value")]
        [Browsable(true)]
        public long BarMaximum
        {
            get { return barMaximum; }
            set { barMaximum = value; }
        }

        /// <summary> Gets or Sets TrackBar Value. </summary>
        private long barValue = 50;
        [Category("1. Custom Properties"), DisplayName("Bar Value")]
        [Description("Gets or Sets the TrackBar Value.")]
        [Browsable(true)]
        public long BarValue
        {
            get { return barValue; }
            set
            {
                barValue = value;
                Invalidate();
            }
        }

        /// <summary> Gets or Sets TrackBar Small Change Value.<br/>
        /// Affects the Behaviour of the Directional Keys when they are Pressed. </summary>
        private int barSmallChange = 1; // Before: Double <----
        [Category("1. Custom Properties"), DisplayName("Bar Small Change")]
        [Description("Gets or Sets the TrackBar 'Small Change' Value.<br/> Affects the behaviour of Directional Keys when they are Pressed.")]
        [Browsable(true)]
        [DefaultValue(1)]
        public int BarSmallChange
        {
            get { return barSmallChange; }
            set { barSmallChange = value; }
        }

        private int barLargeChange = 5; // Before: Double <----
        /// <summary> Gets or Sets TrackBar Large Change Value. <br/>
        /// Affects the Behaviour of PageUp / PageDown keys are Pressed. </summary>
        [Description("Gets or Sets the TrackBar 'Large Change' Value.<br/> Affects the Behaviour of Home / End keys are Pressed.")]
        [Category("ColorSlider")]
        [DefaultValue(5)]
        public int BarLargeChange
        {
            get { return barLargeChange; }
            set { barLargeChange = value; }
        }
        #endregion

        #region <Custom Properties> : (Control Adjustment)
        /// <summary> Bar Padding. Applied on the Sides of the client Control. </summary>
        private int horizontalPadding = 10;
        [Category("1. Custom Properties"), DisplayName("Horizontal Padding")]       // NEEDS A BETTER NAME TO SUIT HORIZONTAL AND VERTICAL
        [Description("Gets or Sets Control Horizontal Padding")]
        [Browsable(true)]
        public int HorizontalPadding
        {
            get { return horizontalPadding; }
            set { horizontalPadding = value; Invalidate(); }
        }

        private Orientation orientation = Orientation.Horizontal;
        [Category("1. Custom Properties"), DisplayName("Bar Orientation")]
        [Description("Toggle Bar Orientation")]
        [Browsable(true)]
        public Orientation Orientation
        {
            get { return orientation; }
            set { orientation = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Bar : Background Rectangle)
        /// <summary> Gets or Sets the TrackBar Background Rectangle Border. </summary>
        private bool useBackgroundBorder = true;
        [Category("1. Custom Properties"), DisplayName("Border Enabled")]
        [Description("Toggles the TrackBar Background Border Visibility")]
        [Browsable(true)]
        public bool UseBackgroundBorder
        {
            get { return useBackgroundBorder; }
            set { useBackgroundBorder = value; Invalidate(); }
        }

        /// <summary> Gets or Sets the Border Color. </summary>
        private Color borderColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Border Color")]
        [Description("Toggles the TrackBar Background Border")]
        [Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set { borderColor = value; Invalidate(); }
        }

        /// <summary> TrackBar Background Color. </summary>
        private Color backgroundColor = Color.FromArgb(255, 46, 46, 52);
        [Category("1. Custom Properties"), DisplayName("Background Color")]
        [Description("Gets or Sets Bar Background Color")]
        [Browsable(true)]
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                Invalidate();
            }
        }

        /// <summary> TrackBar Background Rectangle Thickness. </summary>
        private int backgroundThickness = 8;
        [Category("1. Custom Properties"), DisplayName("Background Thickness")]
        [Description("Gets or Sets Background Bar Thickness")]
        [Browsable(true)]
        public int BackgroundThickness
        {
            get { return backgroundThickness; }
            set
            {
                backgroundThickness = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Bar : Foreground Design)
        /// <summary> TrackBar Foreground Rectangle Color. </summary>
        private Color foregroundColor = Color.Maroon;
        [Category("1. Custom Properties"), DisplayName("Foreground Color")]
        [Description("Gets or Sets Foreground Bar Color")]
        [Browsable(true)]
        public Color ForegroundColor
        {
            get { return foregroundColor; }
            set
            {
                foregroundColor = value;
                Invalidate();
            }
        }

        /// <summary> TrackBar Foreground Rectangle Thickness. </summary>
        private int foregroundThickness = 4;
        [Category("1. Custom Properties"), DisplayName("Foreground Thickness")]
        [Description("Gets or Sets Foreground Bar Thickness")]
        [Browsable(true)]
        public int ForegroundThickness
        {
            get { return foregroundThickness; }
            set
            {
                foregroundThickness = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Slider Design)
        /// <summary> Toggle Slider Visibility. </summary>
        private bool sliderVisible = true;
        [Category("1. Custom Properties"), DisplayName("Slider Visible")]
        [Description("Toggle Slider Visibility")]
        [Browsable(true)]
        public bool SliderVisible
        {
            get { return sliderVisible; }
            set { sliderVisible = value; Invalidate(); }
        }

        /// <summary> Toggle Slider Border Visibility. </summary>
        private bool sliderBorderVisible = true;
        [Category("1. Custom Properties"), DisplayName("Slider Border Visible")]
        [Description("Toggle Slider Border Visibility")]
        [Browsable(true)]
        public bool SliderBorderVisible
        {
            get { return sliderBorderVisible; }
            set { sliderBorderVisible = value; Invalidate(); }
        }


        /// <summary> Slider Border Width. </summary>
        private int sliderBorderWidth = 1;
        [Category("1. Custom Properties"), DisplayName("Slider Border Width")]
        [Description("Set Slider Border Width")]
        [Browsable(true)]
        public int SliderBorderWidth
        {
            get { return sliderBorderWidth; }
            set { sliderBorderWidth = value; Invalidate(); }
        }

        /// <summary> Toggle Slider Border Color. </summary>
        private Color sliderBorderColor = Color.White;
        [Category("1. Custom Properties"), DisplayName("Slider Border Color")]
        [Description("Toggle Slider Border Color")]
        [Browsable(true)]
        public Color SliderBorderColor
        {
            get { return sliderBorderColor; }
            set { sliderBorderColor = value; Invalidate(); }
        }

        /// <summary> Sets the Slider Shape. </summary>
        public enum SliderShape { Rectangle, Ellipse }
        private SliderShape sliderShape = SliderShape.Rectangle;
        [Category("1. Custom Properties"), DisplayName("Slider Shape")]
        [Description("Set the Slider Shape")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public SliderShape BarSliderShape
        {
            get { return sliderShape; }
            set
            {
                sliderShape = value;
                Invalidate();
            }
        }


        /// <summary> Gets or Sets the Slider Image. </summary>
        private Color sliderBackgroundColor = Color.Silver;
        [Category("1. Custom Properties"), DisplayName("Slider Background Color")]
        [Description("Set the Slider Background Color")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public Color SliderBackgroundColor
        {
            get { return sliderBackgroundColor; }
            set
            {
                sliderBackgroundColor = value;
                Invalidate();
            }
        }




        /// <summary> Gets or Sets the Slider Image. </summary>
        private Image sliderImage = null;
        [Category("1. Custom Properties"), DisplayName("Slider Image")]
        [Description("Set the Slider Background Image")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public Image SliderImage
        {
            get { return sliderImage; }
            set
            {
                sliderImage = value;
                Invalidate();
            }
        }


        /// <summary> Gets or Sets the Slider Size. </summary>
        private Size sliderSize = new Size(10, 6);
        [Category("1. Custom Properties"), DisplayName("Slider Size")]
        [Description("Sets the Slider Size")]
        [Browsable(true)]
        public Size SliderWSize
        {
            get { return sliderSize; }
            set
            {
                sliderSize = value;
                Invalidate();
            }
        }
        #endregion


        #region <Custom Events>
        public delegate void ValueChanged(long value);
        public event ValueChanged OnValueChanged;
        #endregion


        #region <Overriden Events> : (Design)
        /// <summary> Raises the <see cref="Control.Paint"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // -> Configuration for Best Smooth Graphics
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // May Cause Glitches
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            DrawBarBackground(e);
            DrawBarForeground(e);
            DrawSlider(e);
        }
        #endregion

        #region <Overriden Events> : (Mouse Events)
        /// <summary> Raises the <see cref="Control.MouseHover"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            Invalidate();
        }

        /// <summary> Raises the <see cref="Control.MouseDown"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                // Similar to "hasMouseDown".
                // It also Determines if the Mouse has Captured the Location (Point)
                Capture = true;
            }

            OnMouseMove(e); // Do This on the ColorWheel Control. This Should Fix 1 of the Selection Bugs and Improve Accuracy!
        }

        /// <summary> Raises the <see cref="Control.MouseMove"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if (hasMouseDown)
            if (Capture & e.Button == MouseButtons.Left)
            {
                Point mouseLocation = e.Location;

                // -> Set Bar Value
                switch (orientation)
                {
                    case Orientation.Horizontal:
                        barValue = barMinimum + mouseLocation.X * (barMaximum - barMinimum) / (ClientRectangle.Width);
                        break;

                    case Orientation.Vertical:
                        barValue = barMinimum + mouseLocation.Y * (barMaximum - barMinimum) / (ClientRectangle.Height);
                        break;
                }

                // -> Limit TrackBar Values
                if (barValue <= barMinimum) { barValue = barMinimum; }
                else if (barValue >= barMaximum) { barValue = barMaximum; }

                // Raise the ValueChanged Event.
                OnValueChanged?.Invoke(barValue);

                // Redraw the Control
                Invalidate();
            }
        }

        /// <summary> Raises the <see cref="Control.MouseUp"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            Capture = false;
        }

        /// <summary> Raises the <see cref="Control.MouseWheel"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // Calculate and Set the Slider Value
            // Previous Line (Backup | Default):
            // int v = e.Delta / 120 * (barMaximum - barMinimum) / numberMouseWheelTicks;

            long v = e.Delta / 120 * (barMaximum - barMinimum) / numberMouseWheelTicks / 5;

            Bar_SetValue(barValue + v);

            // Raise the ValueChanged Event
            OnValueChanged?.Invoke(barValue);
        }
        #endregion

        #region [!] <Overriden Events> : (Keyboard)

        //protected override void OnKeyUp(KeyEventArgs e)
        //{
        //    base.OnKeyUp(e);

        //    switch (e.KeyCode)
        //    {
        //        case Keys.Down:
        //        case Keys.Left:
        //        case Keys.Subtract:
        //            SetValue(barValue - (int)barSmallChange);

        //            break;

        //        case Keys.Up:
        //        case Keys.Right:
        //        case Keys.Add:
        //            SetValue(barValue + (int)barSmallChange);
        //            break;


        //        case Keys.Home:
        //            SetValue(barMinimum);
        //            break;

        //        case Keys.End:
        //            SetValue(barMaximum);
        //            break;
        //    }
        //}

        //        Point pt = PointToClient(Cursor.Position);
        //OnMouseMove(new MouseEventArgs(MouseButtons.None, 0, pt.X, pt.Y, 0));
        #endregion


        #region <Methods> : (Drawing)
        /// <summary> Draws the Bar Foreground. </summary>
        /// <param name="e"></param>
        private void DrawBarForeground(PaintEventArgs e)
        {
            using (var backBrush = new SolidBrush(foregroundColor))
            {
                switch (orientation)
                {
                    case Orientation.Horizontal: e.Graphics.FillRectangle(backBrush, ForegroundRectangleH); break;
                    case Orientation.Vertical: e.Graphics.FillRectangle(backBrush, ForegroundRectangleV); break;
                }
            }
        }

        /// <summary> Draws the Bar Background. </summary>
        /// <param name="e"></param>
        private void DrawBarBackground(PaintEventArgs e)
        {
            using (var backBrush = new SolidBrush(backgroundColor))
            {
                switch (orientation)
                {
                    case Orientation.Horizontal:
                        // -> Background Rectangle
                        e.Graphics.FillRectangle(backBrush, BackgroundRectangleH);

                        // -> Border Rectangle.
                        if (useBackgroundBorder)
                        {
                            using (var borderPen = new Pen(borderColor))
                            {
                                e.Graphics.DrawRectangle(borderPen, BackgroundRectangleH);
                            }
                        }
                        break;

                    case Orientation.Vertical:
                        // -> Background Rectangle
                        e.Graphics.FillRectangle(backBrush, BackgroundRectangleV);

                        // -> Border Rectangle.
                        if (useBackgroundBorder)
                        {
                            using (var borderPen = new Pen(borderColor))
                            {
                                e.Graphics.DrawRectangle(borderPen, BackgroundRectangleV);
                            }
                        }
                        break;
                }
            }
        }

        /// <summary> Draws the Slider. </summary>
        /// <param name="e"></param>
        private void DrawSlider(PaintEventArgs e)
        {
            if (sliderVisible)
            {
                switch (orientation)
                {
                    case Orientation.Horizontal:
                        // -> Draw Slider Without Background Image
                        if (sliderImage == null)
                        {
                            using (var sliderBrush = new SolidBrush(sliderBackgroundColor))
                            {
                                switch (sliderShape)
                                {
                                    case SliderShape.Rectangle: e.Graphics.FillRectangle(sliderBrush, SliderRectangleH); break;
                                    case SliderShape.Ellipse: e.Graphics.FillEllipse(sliderBrush, SliderRectangleH); break;
                                }
                            }
                        }

                        // -> Draw Slider Background Image
                        else
                        {
                            e.Graphics.DrawImage(sliderImage, SliderRectangleH);
                        }
                        break;

                    case Orientation.Vertical:
                        // -> Draw Slider Without Background Image
                        if (sliderImage == null)
                        {
                            using (var sliderBrush = new SolidBrush(sliderBackgroundColor))
                            {
                                switch (sliderShape)
                                {
                                    case SliderShape.Rectangle: e.Graphics.FillRectangle(sliderBrush, SliderRectangleV); break;
                                    case SliderShape.Ellipse: e.Graphics.FillEllipse(sliderBrush, SliderRectangleV); break;
                                }
                            }
                        }

                        // -> Draw Slider Background Image
                        else { e.Graphics.DrawImage(sliderImage, SliderRectangleV); }
                        break;
                }

                DrawSliderBorder(e);
            }
        }

        /// <summary> Draws the Slider Boder. </summary>
        /// <param name="e"></param>
        private void DrawSliderBorder(PaintEventArgs e)
        {
            // -> Draw a Border Around the Slider
            if (SliderVisible)
            {
                if (sliderBorderVisible)
                {
                    using (var sliderPen = new Pen(sliderBorderColor, sliderBorderWidth))
                    {
                        switch (orientation)
                        {
                            case Orientation.Horizontal:
                                switch (sliderShape)
                                {
                                    case SliderShape.Rectangle: e.Graphics.DrawRectangle(sliderPen, SliderRectangleH); break;
                                    case SliderShape.Ellipse: e.Graphics.DrawEllipse(sliderPen, SliderRectangleH); break;
                                }
                                break;

                            case Orientation.Vertical:
                                switch (sliderShape)
                                {
                                    case SliderShape.Rectangle: e.Graphics.DrawRectangle(sliderPen, SliderRectangleV); break;
                                    case SliderShape.Ellipse: e.Graphics.DrawEllipse(sliderPen, SliderRectangleV); break;
                                }
                                break;
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary> Gets the TrackBar Slider Position Value. </summary>
        /// <param name="barWidth"></param>
        /// <returns> int Containing the Slider Postion Value. </returns>
        private long GetSliderPositionValueH()
        {
            //return Width * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Width / 2);
            return BackgroundRectangleH.Width * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Width / 2);
        }

        #region <Methods> : (TrackBar Values)
        /// <summary> Sets the TrackBar Value on Key Press or OnMouseWheel Scroll.
        /// Also Prevents the Ranges to be Exceeded. </summary>
        /// <param name="val">The value.</param>
        private void Bar_SetValue(long val)
        {
            if (val < barMinimum) { barValue = barMinimum; }
            else if (val > barMaximum) { barValue = barMaximum; }
            else { barValue = val; }
        }

        /// <summary> Gets TrackBar Foreground Size Value. </summary>
        /// <param name="barWidth"></param>
        /// <returns> int Containing the Bar Foreground Size Value. </returns>
        private long GetBarForegroundValueH()
        {
            //return Width * (barValue - barMinimum) / (barMaximum - barMinimum);
            return BackgroundRectangleH.Width * (barValue - barMinimum) / (barMaximum - barMinimum) - sliderSize.Width;
        }

        // VERTICAL -------------------
        /// <summary> Gets TrackBar Foreground Size Value. </summary>
        /// <param name="barWidth"></param>
        /// <returns> int Containing the Bar Foreground Size Value. </returns>
        private long GetBarForegroundValueV()
        {
            //return Width * (barValue - barMinimum) / (barMaximum - barMinimum);
            // return BackgroundVerticalRectangle.Height * (barValue - barMinimum) / (barMaximum - barMinimum) - sliderSize.Height;
            long val = BackgroundRectangleV.Height * (barValue - barMinimum) / (barMaximum - barMinimum) - sliderSize.Height; // <- Continue HERE <---

            return val;
        }

        /// <summary> Gets the TrackBar Slider Position Value. </summary>
        /// <param name="barWidth"></param>
        /// <returns> int Containing the Slider Postion Value. </returns>
        private long GetSliderPositionValueV()
        {
            //return Width * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Width / 2);
            return BackgroundRectangleV.Height * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Height / 2);
        }
        #endregion

    }
}
