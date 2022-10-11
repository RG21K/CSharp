// <copyright file="RGButton.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
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

namespace RG_Button.Controls
{
    public class RGButton : Button
    {
        // Last Edited: 2022, 09.12 (18h40)

        #region :: Snippets ::
        /*  
         *  -> Snippet to Allow Deselecting the Buttons.

        private void Button_Click(object sender, EventArgs e)
        {
            var _btn = (RGButton)sender;

            _btn.IsSelected = true;

            for (int i = 0; i < Controls.Count; i++)
            {
                var ctrl = Controls[i];

                if (ctrl is RGButton button)
                {
                    if (button != _btn)
                    {
                        button.IsSelected = false;
                    }
                }
            }
        }
        */

        /* 
         * Toggle Button Highlight Mark
         * 
         * private void MenuPanel_SetMarkedItem()
         * {
         *      // Clear any Marked Menu Buttons
         *      for (int i = 0; i < pnlMenu.Controls.Count; i++)
         *      {
         *          var ctrl = pnlMenu.Controls[i];
         *          
         *          if (ctrl is RG_Custom_Controls.Controls.RGButton _btn)
         *          {
         *              if (_btn.IsSelected) { _btn.IsSelected = false; }
         *          }
         *      }
         *      
         *      // Set the Currently Selected Button
         *      switch (FormManager.FormIndex)
         *      {
         *          case 0: btnMenuScreen.IsSelected = true; break;
         *          case 1: btnMenuList.IsSelected = true; break;
         *          case 2: btnMenuEQ.IsSelected = true; break;
         *          case 3: btnMenuSettings.IsSelected = true; break;
         *          case 4: btnMenuInfo.IsSelected = true; break;
         *          
         *          default: btnMenuScreen.IsSelected = false; break;
         *      }
         *  }
         *  
         */
        #endregion


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
        public RGButton()
        {
            SetStyles();

            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(90, 30);
            this.BackColor = Color.FromArgb(255, 36, 36, 52);
            this.ForeColor = Color.White;
            this.Resize += new EventHandler(Button_Resize);
        }

        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height) { borderRadius = this.Height; }
        }
        #endregion


        #region <Custom Properties> : (Checked Fn)
        private bool checkingEnabled = false;
        [Category("1. Custom Properties"), DisplayName("Check Enabled")]
        [Description("Set the Button Check Function")]
        [Browsable(true)]
        public bool CheckEnabled
        {
            get { return checkingEnabled; }
            set { checkingEnabled = value; Invalidate(); }
        }

        private Color checkedColor = Color.Khaki;
        [Category("1. Custom Properties"), DisplayName("Check Color")]
        [Description("Set the Button Checked Color")]
        [Browsable(true)]
        public Color CheckedColor
        {
            get { return checkedColor; }
            set
            {
                checkedColor = value;
                this.Invalidate();
            }
        }

        private bool isChecked = false;
        [Category("1. Custom Properties"), DisplayName("Checked State")]
        [Description("Set the Button Checked State")]
        [Browsable(true)]
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Highlight)
        private bool highlightEnabled = false;
        [Category("1. Custom Properties"), DisplayName("Highlight Enabled")]
        [Description("Set the Control Highlight Enabled State")]
        [Browsable(true)]
        public bool HighlightEnabled
        {
            get { return highlightEnabled; }
            set
            {
                highlightEnabled = value;
                Invalidate();
            }
        }

        public enum HighlightPosition { None, Left, Right, Bottom };
        private HighlightPosition highlightLocation = HighlightPosition.Bottom;
        [Category("1. Custom Properties"), DisplayName("Highlight Location")]
        [Description("Set the Control Highlight Mode on Mouse Over")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public HighlightPosition HighlightLocation
        {
            get { return highlightLocation; }
            set
            {
                if (value != HighlightLocation)
                {
                    highlightLocation = value;
                    Invalidate();
                }
            }
        }

        private int highlightLineThickness = 1;
        [Category("1. Custom Properties"), DisplayName("Highlight Thickness")]
        [Description("Set the Control Highlight Line Thickness")]
        [Browsable(true)]
        public int HighlightLineThickness
        {
            get { return highlightLineThickness; }
            set { highlightLineThickness = value; }
        }

        private Color highlightLineColor = Color.Red;
        [Category("1. Custom Properties"), DisplayName("Highlight Color")]
        [Description("Set the Control Highlight Color")]
        [Browsable(true)]
        public Color HighlightColor
        {
            get { return highlightLineColor; }
            set { highlightLineColor = value; }
        }
        #endregion

        #region <Custom Properties> : (Border)
        // Default:
        private int borderSize = 1;
        [Category("1. Custom Properties"), DisplayName("BordeSize")]
        [Description("Set the Control Border Size")]
        [Browsable(true)]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        private int borderRadius = 0;
        [Category("1. Custom Properties"), DisplayName("BorderRadius")]
        [Description("Set the Control Border Radius")]
        [Browsable(true)]
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        private Color borderColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("BorderColor")]
        [Description("Set the Control Border Color")]
        [Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Other)

        [Category("1. Custom Properties"), DisplayName("BackgroundColor")]
        [Description("Set the Control Background Color")]
        [Browsable(true)]
        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }

        [Category("1. Custom Properties"), DisplayName("TextColor")]
        [Description("Set the Control Text Color")]
        [Browsable(true)]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        #endregion


        #region <Overriden Events>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // Under Testing

            DrawButtonShape(e);

            DrawHighlight(e);
            DrawCheckedState(e);
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (checkingEnabled) { if (!isChecked) { isChecked = true; } }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            // hasMouseOver = true;
            Capture = true;
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);

            if (!ClientRectangle.Contains(mevent.Location)) { Capture = false; }
            else { Capture = true; }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            // hasMouseOver = false;
            Capture = false;
        }
        #endregion


        #region <Methods> : (Drawing)
        /// <summary> Round Corner GraphicsPath. </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary> Draw Button Shape; Corners & Borders. </summary>
        /// <param name="pe"></param>
        private void DrawButtonShape(PaintEventArgs pe)
        {
            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;

            if (borderSize > 0)
                smoothSize = borderSize;

            // -> Rounded Button Corners
            if (borderRadius > 2)
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                    // Draw Button Surface
                    this.Region = new Region(pathSurface);

                    // Draw Button Surface Border (for HD Result)
                    pe.Graphics.DrawPath(penSurface, pathSurface);

                    // Draw Button Border                    
                    if (borderSize >= 1)
                        pe.Graphics.DrawPath(penBorder, pathBorder);
                }
            }

            // -> Default Button Corners
            else
            {
                pe.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pe.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }

        /// <summary> Draw Mouse Over Highlight Effect. </summary>
        /// <param name="e"></param>
        private void DrawHighlight(PaintEventArgs e)
        {
            if (highlightEnabled)
            {
                using (Pen _underlinePen = new Pen(highlightLineColor, highlightLineThickness))
                {
                    // -> Revert the Highlight Color to the Default Button Border Color
                    if (!Capture)
                    {
                        _underlinePen.Color = borderColor;
                        e.Graphics.DrawLine(_underlinePen, GetHighlightPoints()[0], GetHighlightPoints()[1], GetHighlightPoints()[2], GetHighlightPoints()[3]);
                    }

                    // -> Set the Highlight Color as the Highlighed Color
                    else
                    {
                        e.Graphics.DrawLine(_underlinePen, GetHighlightPoints()[0], GetHighlightPoints()[1], GetHighlightPoints()[2], GetHighlightPoints()[3]);
                    }
                }
            }
        }

        /// <summary> Draw Button Cheked Mark. </summary>
        /// <param name="e"></param>
        private void DrawCheckedState(PaintEventArgs e)
        {
            if (CheckEnabled)
            {
                using (Pen _underlinePen = new Pen(highlightLineColor, highlightLineThickness))
                {
                    // -> Revert the Checked Color to the Default Button Border Color
                    if (!Capture)
                    {
                        if (isChecked)
                        {
                            _underlinePen.Color = checkedColor;
                            e.Graphics.DrawLine(_underlinePen, GetHighlightPoints()[0], GetHighlightPoints()[1], GetHighlightPoints()[2], GetHighlightPoints()[3]);
                        }
                    }

                    // -> Set the Checked Color to the Checked State Color
                    else
                    {
                        e.Graphics.DrawLine(_underlinePen, GetHighlightPoints()[0], GetHighlightPoints()[1], GetHighlightPoints()[2], GetHighlightPoints()[3]);
                    }
                }
            }
        }
        #endregion

        #region <Methods> : (Highlight & Checked Points)
        private float[] GetHighlightPoints()
        {
            float[] points = new float[4];
            
            switch (highlightLocation)
            {
                case HighlightPosition.None: break;

                case HighlightPosition.Left:
                    points[0] = 0;                                                      /* LocX1 */
                    points[1] = 0;                                                      /* LocY1 */
                    points[2] = 0;                                                      /* LocX2 */
                    points[3] = ClientRectangle.Height;                                 /* LocY2 */
                    break;

                case HighlightPosition.Right:
                    points[0] = ClientRectangle.Width - (highlightLineThickness);       /* LocX1 */
                    points[1] = 0;                                                      /* LocY1 */
                    points[2] = ClientRectangle.Width - (highlightLineThickness);       /* LocX2 */
                    points[3] = ClientRectangle.Height;                                 /* LocY2 */
                    break;

                case HighlightPosition.Bottom:
                    points[0] = ClientRectangle.Location.X;                             /* LocX1 */
                    points[1] = ClientRectangle.Height - (highlightLineThickness);      /* LocY1 */
                    points[2] = ClientRectangle.Width;                                  /* LocX2 */
                    points[3] = ClientRectangle.Height - (highlightLineThickness);      /* LocY2 */
                    break;
            }

            return points;
        }
        #endregion
    }
}
