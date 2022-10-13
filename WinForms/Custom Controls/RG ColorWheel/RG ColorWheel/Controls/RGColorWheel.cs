// <copyright file="RGColorWheel.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
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

namespace RG_ColorWheel.Controls
{
    public class RGColorWheel : Panel
    {
        // LastEdited: 2022, 10.10

        #region <Configuration>
        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion


        #region <Constructor>
        public RGColorWheel()
        {
            SetStyles();

            MinimumSize = new Size(50, 50);
            MaximumSize = new Size(500, 500);
            Size = new Size(150, 150);
        }
        #endregion


        #region <Fields>
        /// <summary> Color Wheel Rectangle </summary>
        private Rectangle colorWheelEllipse;
        #endregion


        #region <Auto Properties>
        private bool hasMouseLeftButtonDown { get; set; } = false;
        private Point mouseLocation { get; set; }
        public Color SelectedColor { get; private set; } = Color.Black;

        #endregion


        #region <Custom Properties>
        // -> Control Border
        private bool colorWheelBorderEnabled = true;
        [Category("1. Custom Properties"), DisplayName("Show Border")]
        [Description("Toggle Color Wheel Border")]
        [Browsable(true)]
        public bool ColorWheelBorderEnabled
        {
            get { return colorWheelBorderEnabled; }
            set { if (colorWheelBorderEnabled != value) { colorWheelBorderEnabled = value; } Invalidate(); } // Previous: Refresh();
        }

        private Color colorWheelBorderColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Border Color")]
        [Description("Set Color Wheel Border Color")]
        [Browsable(true)]
        public Color ColorWheelBorderColor
        {
            get { return colorWheelBorderColor; }
            set { if (colorWheelBorderColor != value) { colorWheelBorderColor = value; } Invalidate(); }
        }

        // -> Selection Ellipse
        private bool selectionEllipseEnabled = true;
        [Category("1. Custom Properties"), DisplayName("Selection Visible")]
        [Description("Set Visual Selection Enabled State. \nDefault: true")]
        [Browsable(true)]
        public bool SelectionEllipseEnabled
        {
            get { return selectionEllipseEnabled; }
            set { if (selectionEllipseEnabled != value) { selectionEllipseEnabled = value; } }
        }

        private Color selectionEllipseBorderColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Selection Border Color")]
        [Description("Set Visual Selection Border Color. \nDefault: Black")]
        [Browsable(true)]
        public Color SelectionEllipseBorderColor
        {
            get { return selectionEllipseBorderColor; }
            set { if (selectionEllipseBorderColor != value) { selectionEllipseBorderColor = value; } }
        }

        private int selectionEllipseThickness = 3;
        [Category("1. Custom Properties"), DisplayName("Selection Border Thickness")]
        [Description("Set Visual Selection Border Thickness. \nDefault: 3")]
        [Browsable(true)]
        public int SelectionEllipseThickness
        {
            get { return selectionEllipseThickness; }
            set { if (selectionEllipseThickness != value) { selectionEllipseThickness = value; } }
        }

        private int selectionEllipseDiameter = 15;
        [Category("1. Custom Properties"), DisplayName("Selection Diameter")]
        [Description("Set Selector Size. \nDefault: 15")]
        [Browsable(true)]
        public int SelectionEllipseDiameter
        {
            get { return selectionEllipseDiameter; }
            set
            {
                if (selectionEllipseDiameter != value)
                {
                    selectionEllipseDiameter = value;
                }
            }
        }
        #endregion


        #region <Custom Events>
        public delegate void ColorChanged(object sender, Color args);
        public event ColorChanged OnSelectedColorChanged;
        #endregion


        #region <Overriden Events>
        /// <summary> Occurs when the Control is Repainted. </summary>
        /// <param name="pe"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // May Cause Glitches
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill the Ellipse with Colors
            FillColors(e);

            // Draw Color Wheel Border (if Border Visisbility is Enabled)
            DrawBorder(e);

            DrawColorSelectionEllipse(e);
        }

        /// <summary> Occurs when the Control is Resized. </summary>
        /// <param name="eventargs"></param>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
        }

        /// <summary> Occurs when the Mouse Enters the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(System.EventArgs e)
        {
            base.OnMouseEnter(e);

            Cursor = Cursors.Hand;
        }

        /// <summary> Occurs when the Mouse Leaves the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(System.EventArgs e)
        {
            base.OnMouseLeave(e);

            Cursor = Cursors.Default;
        }

        /// <summary> Occurs when a Mouse Button is Released. </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                if (hasMouseLeftButtonDown) { hasMouseLeftButtonDown = false; }
            }
        }

        /// <summary> Occurs when a Mouse Button is Pressed and Held Down. </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            // Note: Do Not Set Color on Mouse Down.
            //       1. Will NOT Set
            //       2. Color Selection Accuracy Fail Rate Increases.

            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                if (!hasMouseLeftButtonDown) { hasMouseLeftButtonDown = true; }

                mouseLocation = e.Location;

                // Improve Selection Accuracy by Calling the Local MouseMove Event.
                OnMouseMove(e);
            }
        }

        /// <summary> Occurs when Mouse Pointer Moves while Inside the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (hasMouseLeftButtonDown)
            {
                mouseLocation = e.Location;
                UpdateSelectedColor(mouseLocation);
                Invalidate();
            }

            // Using "Refresh()" (here) Improves the Color Selection Ellipse Movement.
            // The Movement Becomes Fluid.
            // Test if the Color Selection Becomes Accurate.
            Refresh();
        }
        #endregion


        #region <Methods> : (ColorWheel Fn)
        /// <summary> Update the Color Wheel Selected Color. </summary>
        /// <param name="mousePos"></param>
        private void UpdateSelectedColor(Point mousePos)
        {
            if (HasMouseInsideControl(mousePos))
            {
                using (var _bitmap = new Bitmap(1, 1))
                {
                    using (var _graphics = Graphics.FromImage(_bitmap))
                    {
                        // Note: Using Cursor.Position (here) to Get Color on Color Wheel is Far More Accurate;
                        //       than using e.Location or MousePosition
                        _graphics.CopyFromScreen(Cursor.Position, new Point(0, 0), new Size(1, 1));
                    }

                    SelectedColor = _bitmap.GetPixel(0, 0);
                    
                    // -> Raise the Color changed Event
                    OnSelectedColorChanged?.Invoke(this, SelectedColor);
                }
            }
        }

        /// <summary> Determines if Mouse Location is Inside Color Wheel. </summary>
        /// <param name="location"></param>
        /// <returns> True if Mouse is Inside the Control. </returns>
        private bool HasMouseInsideControl(Point location)
        {
            var contains = false;

            using (var _gfx = new GraphicsPath())
            {
                _gfx.AddEllipse(colorWheelEllipse);
                contains = _gfx.IsVisible(location);
            }

            return contains;
        }
        #endregion

        #region <Methods> : (Drawing : ColorWheel)
        private GraphicsPath GetColorWheelPath()
        {
            int wheelLocX, wheelLocY, wheelWidth, wheelHeight;

            wheelLocX = 0;
            wheelLocY = 0;
            wheelWidth = ClientRectangle.Width - 1;   // Default: wheelWidth = this.Width;
            wheelHeight = ClientRectangle.Height - 1;  // Default: wheelHeight = this.Height;

            colorWheelEllipse = new Rectangle(wheelLocX, wheelLocY, wheelWidth, wheelHeight);

            var _path = new GraphicsPath();
            _path.AddEllipse(colorWheelEllipse);
            _path.Flatten();

            return _path;
        }

        /// <summary> Fill the Wheel with Colors. </summary>
        private void FillColors(PaintEventArgs e)
        {
            float nrPoints = (GetColorWheelPath().PointCount - 1) / 6;
            Color[] _surroundingColors = new Color[GetColorWheelPath().PointCount];

            int index = 0;
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, 1 * nrPoints, 255, 255, 0, 0, 255, 255, 0, 255);
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, 2 * nrPoints, 255, 255, 0, 255, 255, 0, 0, 255);
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, 3 * nrPoints, 255, 0, 0, 255, 255, 0, 255, 255);
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, 4 * nrPoints, 255, 0, 255, 255, 255, 0, 255, 0);
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, 5 * nrPoints, 255, 0, 255, 0, 255, 255, 255, 0);
            this.ColorWheel_InterpolateColors(_surroundingColors, ref index, GetColorWheelPath().PointCount, 255, 255, 255, 0, 255, 255, 0, 0);

            using (PathGradientBrush _brushPath = new PathGradientBrush(GetColorWheelPath()))
            {
                _brushPath.CenterColor = Color.White;
                _brushPath.SurroundColors = _surroundingColors;

                e.Graphics.FillPath(_brushPath, GetColorWheelPath());
            }
        }


        /// <summary> Draw a Boarder Around the Color Wheel. </summary>
        /// <param name="_gfx"></param>
        private void DrawBorder(PaintEventArgs e)
        {
            if (colorWheelBorderEnabled)
            {
                using (Pen _pen = new Pen(colorWheelBorderColor, 1))
                {
                    _pen.Color = colorWheelBorderColor;

                    e.Graphics.DrawPath(_pen, GetColorWheelPath());
                }
            }
        }



        /// <summary> Fill the Circle Area with Interpolating Colors Between: 'From' and 'To' Values. </summary>
        /// <param name="surround_colors"></param>
        /// <param name="index"></param>
        /// <param name="stop_pt"></param>
        /// <param name="from_a"></param>
        /// <param name="from_r"></param>
        /// <param name="from_g"></param>
        /// <param name="from_b"></param>
        /// <param name="to_a"></param>
        /// <param name="to_r"></param>
        /// <param name="to_g"></param>
        /// <param name="to_b"></param>
        private void ColorWheel_InterpolateColors(Color[] surround_colors, ref int index, float stop_pt, int from_a, int from_r, int from_g, int from_b, int to_a, int to_r, int to_g, int to_b)
        {
            int num_pts = (int)stop_pt - index;
            float a = from_a, r = from_r, g = from_g, b = from_b;
            float da = (to_a - from_a) / (num_pts - 1);
            float dr = (to_r - from_r) / (num_pts - 1);
            float dg = (to_g - from_g) / (num_pts - 1);
            float db = (to_b - from_b) / (num_pts - 1);

            for (int i = 0; i < num_pts; i++)
            {
                surround_colors[index++] = Color.FromArgb((int)a, (int)r, (int)g, (int)b);

                a += da;
                r += dr;
                g += dg;
                b += db;
            }
        }
        #endregion

        #region <Methods> : (Drawing : Color Selection Ellipse)
        private GraphicsPath GetColorSelectionEllipsePath()
        {
            var _path = new GraphicsPath();

            int locX, locY;

            locX = (mouseLocation.X - selectionEllipseDiameter / 2);
            locY = (mouseLocation.Y - selectionEllipseDiameter / 2);

            _path.AddEllipse(ColorSelectionEllipse(locX, locY, selectionEllipseDiameter, selectionEllipseDiameter));
            _path.Flatten();

            return _path;
        }

        /// <summary> Color Wheel Visual Selection Ellipse. </summary>
        private Rectangle ColorSelectionEllipse(int locX, int locY, int sizeW, int sizeH)
        {
            return new Rectangle { X = locX, Y = locY, Width = sizeW, Height = sizeH };
        }

        /// <summary> Draw an Ellipse with Border (Only). </summary>
        /// <param name="msLoc"></param>
        private void DrawColorSelectionEllipse(PaintEventArgs e)
        {
            if (SelectionEllipseEnabled)
            {
                if (hasMouseLeftButtonDown)
                {
                    // Current
                    using (Pen _pen = new Pen(selectionEllipseBorderColor, selectionEllipseThickness))
                    {
                        _pen.Color = selectionEllipseBorderColor;

                        e.Graphics.DrawPath(_pen, GetColorSelectionEllipsePath());
                    }
                }
            }
        }
        #endregion
    }
}
