// <copyright file="RGTrackBar.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace RG_TrackBar.Controls
{
    public class RGTrackBar : PictureBox /* Modify to User Control after Backup */
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * To Do:
         *
         * [Known Issues]
         * -------------------------------------------------------------------------------------
         *                    
         *          [1]. Using Keyboard to Adjust Value is NOT Working
         *               - Set Keys According to Orientation
         *                      - Horizontal: Left / Right 
         *                      -   Vertical:   Up / Down
         *          
         *          [2]. Limit the Slider to Remain Inside the Rectangle. (It goes half way outside of it)
         *               - Ensure the Value remains between Min and Max.
         *               
         *          [3]. Improve the Customized ToolTip
         *               - The ToolTip Text Location & Design
         *               
         *          [4]. Could I Combine the 'BarEdgePadding' with the 'Padding Value' (Added when Auto-Adjusting the Container Size
         *               to the Slider)?
         *               Perhaps Renaming and Grouping these Properties?
         *               
         *          [5]. Rename ToolTip to Label?
         *          
         *          [6]. Clean Code
         *              
         * 
         * [Info]
         * -------------------------------------------------------------------------------------
         * 
         * A TrackBar that can be used as:
         * 
         *          - SeekBar
         *          - Equalizer Frequency Bar
         *          - Other
         * 

         * 
         * 
         * [Usage Info]
         * -------------------------------------------------------------------------------------
         * 
         * OnValuechanged(value);
         * 
         */
        #endregion

        #region <Fields>
        /// <summary> Determines if Mouse Pointer is Inside the Control. </summary>
        private bool hasMouseInside => ClientRectangle.Contains(PointToClient(MousePosition).X, PointToClient(MousePosition).Y);
        #endregion


        #region <Constructor>
        public RGTrackBar()
        {
            // -> Set Control Style Configuration
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true); // Allow Redrawing when Orientation Changes.

            // -> Redraw Control when it is Resized.
            ResizeRedraw = true;

            // -> Set the Default Client Rectangle Size
            //Size = defaultSize;
        }
        #endregion


        #region [!] <Control Rectangles>
        private Rectangle GetBackgroundRectangle()
        {
            Rectangle backgroundRectangle = new Rectangle();

            switch (orientation)
            {
                case Orientation.Horizontal: backgroundRectangle = BackgroundRectangleH; break;
                case Orientation.Vertical: backgroundRectangle = BackgroundRectangleV; break;
            }

            return backgroundRectangle;
        }

        /// <summary> TrackBar Background Rectangle. </summary>
        private Rectangle BackgroundRectangleH
        {
            get
            {
                return new Rectangle
                {
                    Width = ClientRectangle.Width - (barEdgePadding * 2),
                    Height = backgroundThickness,

                    X = ClientRectangle.X + barEdgePadding,
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
                    Height = ClientRectangle.Height - (barEdgePadding * 2),

                    X = ClientRectangle.Width / 2 - backgroundThickness / 2,
                    Y = ClientRectangle.Y + barEdgePadding,
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
                    Height = sliderSize.Height, // Previous: BackgroundRectangle.Height

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



        #region <Custom Properties> : (Mouse)
        private int numberMouseWheelTicks = 10;
        [Category("1. Custom Properties"), DisplayName("Scroll Ticks")]
        [Description("Set Number of MouseWheel Scrolling Ticks")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public int NumberMouseWheelTicks
        {
            get => numberMouseWheelTicks;
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
            get => barMinimum;
            set { barMinimum = value; }
        }

        /// <summary> TrackBar Maximum Value. </summary>
        private long barMaximum = 100;
        [Category("1. Custom Properties"), DisplayName("Bar Maximum")]
        [Description("Sets Maximum TrackBar Value")]
        [Browsable(true)]
        public long BarMaximum
        {
            get => barMaximum;
            set { barMaximum = value; }
        }

        /// <summary> Gets or Sets TrackBar Value. </summary>
        private long barValue = 50;
        [Category("1. Custom Properties"), DisplayName("Bar Value")]
        [Description("Gets or Sets the TrackBar Value.")]
        [Browsable(true)]
        public long BarValue
        {
            get => barValue;
            set
            {
                barValue = value;
                Invalidate();
            }
        }

        /// <summary> Gets or Sets TrackBar Small Change Value.<br/>
        /// Affects the Behaviour of the Directional Keys when they are Pressed. </summary>
        private decimal barSmallChange = 1;
        [Category("1. Custom Properties"), DisplayName("Bar Small Change")]
        [Description("Gets or Sets the TrackBar 'Small Change' Value.<br/> Affects the behaviour of Directional Keys when they are Pressed.")]
        [Browsable(true)]
        [DefaultValue(1)]
        public decimal BarSmallChange
        {
            get => barSmallChange;
            set { barSmallChange = value; }
        }

        private decimal barLargeChange = 5;
        /// <summary> Gets or Sets TrackBar Large Change Value. <br/>
        /// Affects the Behaviour of PageUp / PageDown keys are Pressed. </summary>
        [Description("Gets or Sets the TrackBar 'Large Change' Value.<br/> Affects the Behaviour of Home / End keys are Pressed.")]
        [Category("ColorSlider")]
        [DefaultValue(5)]
        public decimal BarLargeChange
        {
            get => barLargeChange;
            set { barLargeChange = value; }
        }
        #endregion

        #region <Custom Properties> : (Control Adjustment)
        /// <summary> Bar Padding. Applied on the Sides of the client Control. </summary>
        private int barEdgePadding = 10;
        [Category("1. Custom Properties"), DisplayName("Bar Edge Padding")]
        [Description("Gets or Sets Control Bar Edge Padding Padding")]
        [Browsable(true)]
        public int BarEdgePadding
        {
            get => barEdgePadding;
            set { barEdgePadding = value; Invalidate(); }
        }

        private Orientation orientation = Orientation.Horizontal;
        [Category("1. Custom Properties"), DisplayName("Bar Orientation")]
        [Description("Toggle Bar Orientation")]
        [Browsable(true)]
        public Orientation Orientation
        {
            get => orientation;
            set
            {
                orientation = value;

                SetSizeToSlider();
                Invalidate();

                /*
                 * Note: Even though 'SizeToSlider()' is Already under the 'OnPaintEvent()'
                 *       it is Necessary to Delcare it here as well, to prevent the Selected Control Container Bug Location.
                 */
            }
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
            get => useBackgroundBorder;
            set { useBackgroundBorder = value; Invalidate(); }
        }


        /// <summary> Gets or Sets the Border Color. </summary>
        private Color borderColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Border Color")]
        [Description("Toggles the TrackBar Background Border")]
        [Browsable(true)]
        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }


        /// <summary> TrackBar Background Color. </summary>
        private Color backgroundColor = Color.FromArgb(255, 46, 46, 62);
        [Category("1. Custom Properties"), DisplayName("Background Color")]
        [Description("Gets or Sets Bar Background Color")]
        [Browsable(true)]
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                Invalidate();
            }
        }


        /// <summary> TrackBar Background Rectangle Thickness. </summary>
        private int backgroundThickness = 6;
        [Category("1. Custom Properties"), DisplayName("Background Thickness")]
        [Description("Gets or Sets Background Bar Thickness")]
        [Browsable(true)]
        public int BackgroundThickness
        {
            get => backgroundThickness;
            set
            {
                backgroundThickness = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Bar : Foreground Design)
        /// <summary> TrackBar Foreground Rectangle Color. </summary>
        private Color foregroundColor = Color.Red;
        [Category("1. Custom Properties"), DisplayName("Foreground Color")]
        [Description("Gets or Sets Foreground Bar Color")]
        [Browsable(true)]
        public Color ForegroundColor
        {
            get => foregroundColor;
            set
            {
                foregroundColor = value;
                Invalidate();
            }
        }


        /// <summary> TrackBar Foreground Rectangle Thickness. </summary>
        private int foregroundThickness = 3;
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
            get => sliderVisible;
            set { sliderVisible = value; Invalidate(); }
        }


        /// <summary> Toggle Slider Border Visibility. </summary>
        private bool sliderBorderVisible = true;
        [Category("1. Custom Properties"), DisplayName("Slider Border Visible")]
        [Description("Toggle Slider Border Visibility")]
        [Browsable(true)]
        public bool SliderBorderVisible
        {
            get => sliderBorderVisible;
            set { sliderBorderVisible = value; Invalidate(); }
        }


        /// <summary> Slider Border Width. </summary>
        private int sliderBorderWidth = 1;
        [Category("1. Custom Properties"), DisplayName("Slider Border Width")]
        [Description("Set Slider Border Width")]
        [Browsable(true)]
        public int SliderBorderWidth
        {
            get => sliderBorderWidth;
            set { sliderBorderWidth = value; Invalidate(); }
        }


        /// <summary> Toggle Slider Border Color. </summary>
        private Color sliderBorderColor = Color.White;
        [Category("1. Custom Properties"), DisplayName("Slider Border Color")]
        [Description("Toggle Slider Border Color")]
        [Browsable(true)]
        public Color SliderBorderColor
        {
            get => sliderBorderColor;
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
            get => sliderShape;
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
            get => sliderBackgroundColor;
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
            get => sliderImage;
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
            get => sliderSize;
            set
            {
                sliderSize = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (ToolTip)
        private bool toolTipEnabled = true;
        [Category("1. Custom Properties"), DisplayName("ToolTip Enabled")]
        [Description("Sets ToolTip Text")]
        [Browsable(true)]
        public bool ToolTipEnabled
        {
            get => toolTipEnabled;
            set
            {
                toolTipEnabled = value;

                Invalidate();
            }
        }

        private Font toolTipFont = new Font("Consolas", 8);
        [Category("1. Custom Properties"), DisplayName("ToolTip Font")]
        [Description("Sets ToolTip Font")]
        [Browsable(true)]
        public Font ToolTipFont
        {
            get => toolTipFont;
            set { toolTipFont = value; Invalidate(); }
        }

        private string toolTipText = string.Empty;
        [Category("1. Custom Properties"), DisplayName("ToolTip Text")]
        [Description("Sets ToolTip Text")]
        [Browsable(true)]
        public string ToolTipText
        {
            get => toolTipText;
            set
            {
                toolTipText = value;
                //toolTip.SetToolTip(this, value);
            }
        }

        private Color toolTipForeColor = Color.White;
        [Category("1. Custom Properties"), DisplayName("ToolTip ForeColor")]
        [Description("Set ToolTip Fore Color")]
        [Browsable(true)]
        public Color ToolTipForeColor
        {
            get => toolTipForeColor;
            set { toolTipForeColor = value; Invalidate(); }
        }

        private Color toolTipBackColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("ToolTip BackColor")]
        [Description("Set ToolTip Back Color")]
        [Browsable(true)]
        public Color ToolTipBackColor
        {
            get => toolTipBackColor;
            set { toolTipBackColor = value; Invalidate(); }
        }

        private string symbolBeforeVal = "";
        [Category("1. Custom Properties"), DisplayName("ToolTip Symbol Before Value")]
        [Description("Show a Symbol Before the ProgressBar Value")]
        [Browsable(true)]
        public string SymbolBeforeValue
        {
            get => symbolBeforeVal;
            set
            {
                symbolBeforeVal = value;
                this.Invalidate();
            }
        }

        private string symbolAfterVal = "";
        [Category("1. Custom Properties"), DisplayName("ToolTip Symbol After Value")]
        [Description("Show a Symbol After the ProgressBar Value")]
        [Browsable(true)]
        public string SymbolAfterValue
        {
            get => symbolAfterVal;
            set
            {
                symbolAfterVal = value;
                this.Invalidate();
            }
        }

        private bool showMaximum = false;
        [Category("1. Custom Properties"), DisplayName("ToolTip Show Maximum Value")]
        [Description("Toggle between choosing to display the Maximum Value or not, in Front of Current Value.")]
        [Browsable(true)]
        public bool ShowMaximum
        {
            get => showMaximum;
            set
            {
                showMaximum = value;
                this.Invalidate();
            }
        }
        #endregion


        #region <Custom Events>
        public delegate void ValueChanged(object sender, long value);
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

            // -> Draw Customized ToolTip

            DrawToolTip(e);

            //// -> Adjust the Slider Size (Length)
            SetSizeToSlider();
        }
        #endregion

        #region <Overriden Events> : (Mouse Events)
        /// <summary> Occurs when the Mouse is Hover the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            Invalidate();
        }

        /// <summary> Occurrs when the Mouse Leaves the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

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

        /// <summary> Raises the <see cref="Control.MouseUp"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            Capture = false;
        }

        /// <summary> Raises the <see cref="Control.MouseMove"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

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
                OnValueChanged?.Invoke(this, barValue);

                // Redraw the Control
                Invalidate();
            }
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
            OnValueChanged?.Invoke(this, barValue);
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
            using (var foreBrush = new SolidBrush(foregroundColor))
            {
                switch (orientation)
                {
                    case Orientation.Horizontal: e.Graphics.FillRectangle(foreBrush, ForegroundRectangleH); break;
                    case Orientation.Vertical: e.Graphics.FillRectangle(foreBrush, ForegroundRectangleV); break;
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
            if (sliderVisible)
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

        /// <summary> Show ToolTip: Draws a Formatted TrackBar Value on the Control. </summary>
        /// <param name="e"></param>
        /// <param name="sliderWidth"></param>
        /// <param name="rectSlider"></param>
        private void DrawToolTip(PaintEventArgs e)
        {
            if (!DesignMode)
                if (toolTipEnabled & hasMouseInside)
                {
                    // Default ToolTip Text
                    string toolTipText = $"{symbolBeforeVal}{BarValue}{symbolAfterVal}";

                    // Format ToolTip Text : Show also Maximum TrackBar Value (i.e: 0 / 100)
                    if (showMaximum) { toolTipText = $"{BarValue} / {BarMaximum.ToString("D2")}"; }


                    // [i] Same as Abova bu with Ternary Operator---
                    //toolTipText = showMaximum ? toolTipText + "/" + symbolBeforeVal + BarMaximum.ToString() + symbolAfterVal : $"{symbolBeforeVal}{BarValue}{symbolAfterVal}";


                    var textSize = TextRenderer.MeasureText(toolTipText, this.Font);

                    using (var brushText = new SolidBrush(toolTipForeColor))
                    using (var brushTextBackground = new SolidBrush(toolTipBackColor))
                    using (var textFormat = new StringFormat())
                    {
                        // -> Set the ToolTip Text Rectangle
                        Rectangle rect = new Rectangle();

                        switch (orientation)
                        {
                            case Orientation.Horizontal:
                                rect = new Rectangle(SliderRectangleH.X - (textSize.Width + 1), SliderRectangleH.Y - 3, textSize.Width, textSize.Height);
                                break;

                            case Orientation.Vertical:
                                rect = new Rectangle(SliderRectangleV.X - 2, SliderRectangleV.Y - 15, textSize.Width, textSize.Height);
                                break;
                        }


                        // -> Clean Previous Text Surface
                        using (var _brushClear = new SolidBrush(Parent.BackColor))
                        {
                            rect.Y = rect.Y;
                            rect.Height = rect.Height;
                            e.Graphics.FillRectangle(_brushClear, rect);
                        }

                        // -> Configure the Text Formatting Flags
                        TextFormatFlags textFlags = TextFormatFlags.Default;


                        // -> Draw the ToolTip Text
                        switch (orientation)
                        {
                            case Orientation.Horizontal:
                                TextRenderer.DrawText(e.Graphics, toolTipText, new Font("Consolas", 7), rect, Color.White, textFlags);
                                break;

                            case Orientation.Vertical:
                                TextRenderer.DrawText(e.Graphics, toolTipText, new Font("Consolas", 7), rect, Color.White, textFlags);
                                break;
                        }
                    }
                }
        }

        #endregion

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

        /// <summary> Gets the TrackBar Slider Position Value. </summary>
        /// <param name="barWidth"></param>
        /// <returns> int Containing the Slider Postion Value. </returns>
        private long GetSliderPositionValueH()
        {
            //return Width * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Width / 2);
            return BackgroundRectangleH.Width * (barValue - barMinimum) / (barMaximum - barMinimum) - (sliderSize.Width / 2);
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

        #region <Methods> : (Control Length)
        /// <summary> Calculate the Control Size Length to set the Orientation Size. </summary>
        /// <returns> Returns the Greatest Value from Control Size Width or Height. </returns>
        private int GetControlLength()
        {
            return Math.Max(Width, Height);
        }

        /// <summary> Adjust the Control Container Size to the Slider Size. <br/><br/>
        /// <strong>Note:</strong> Changes Accordingto Control Orientation. </summary>
        private void SetSizeToSlider()
        {
            int length = GetControlLength();

            switch (orientation)
            {
                case Orientation.Horizontal:
                    Size = new Size(length, sliderSize.Height + 4);
                    break;

                case Orientation.Vertical:
                    Size = new Size(sliderSize.Width + 4, length);
                    break;
            }
        }
        #endregion
    }
}
