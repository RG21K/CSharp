// <copyright file="RGPanel.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace RG_Panel.Controls
{
    public class RGPanel : Panel
    {
        // Last Edited: 2022, 10.10

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
        public RGPanel()
        {
            SetStyles();

            BackColor = Color.FromArgb(255, 36, 36, 52);

            leftBorderColor = Color.DimGray;
            topBorderColor = Color.DimGray;
            rightBorderColor = Color.DimGray;
            bottomBorderColor = Color.DimGray;
        }
        #endregion


        #region <Custom Properties> : (Highlight)
        public bool highlightEnabled = false;
        [Category("1. Borders"), DisplayName("Highlight Enabled")]
        [Description("Toggle Highlight Effect")]
        [Browsable(true)]
        public bool HighlightEnabled
        {
            get { return highlightEnabled; }
            set { highlightEnabled = value; Invalidate(); }
        }


        private Color highlightColor = Color.DeepSkyBlue;
        [Category("1. Borders"), DisplayName("Highlight Color")]
        [Description("Set Highlight Color")]
        [Browsable(true)]
        public Color HighlightColor
        {
            get { return highlightColor; }
            set { highlightColor = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Borders)
        // -> Left Border (Visibility)
        private bool leftBorderVisible = true;
        [Category("1. Borders"), DisplayName("1. Left Visible")]
        [Description("Toggle Left Border Visibility")]
        [Browsable(true)]
        public bool LeftBorderVisible
        {
            get { return leftBorderVisible; }
            set { if (leftBorderVisible != value) { leftBorderVisible = value; Invalidate(); } }
        }

        // -> Left Border (Color)
        private Color leftBorderColor = Color.DimGray;
        [Category("1. Borders"), DisplayName("1. Left Color")]
        [Description("Set Left Border Color")]
        [Browsable(true)]
        public Color LeftBorderColor
        {
            get { return leftBorderColor; }
            set { if (leftBorderColor != value) { leftBorderColor = value; Invalidate(); } }
        }

        // -> Top Border (Visibility)
        private bool topBorderVisible = true;
        [Category("1. Borders"), DisplayName("2. Top Visible")]
        [Description("Set Top Border Visibility")]
        [Browsable(true)]
        public bool TopBorderEnabled
        {
            get { return topBorderVisible; }
            set { if (topBorderVisible != value) { topBorderVisible = value; Invalidate(); } }
        }

        // -> Top Border (Color)
        private Color topBorderColor = Color.DimGray;
        [Category("1. Borders"), DisplayName("2. Top Color")]
        [Description("Set Top Border Color")]
        [Browsable(true)]
        public Color TopBorderColor
        {
            get { return topBorderColor; }
            set { if (topBorderColor != value) { topBorderColor = value; Invalidate(); } }
        }

        // -> Right Border (Visibility)
        private bool rightBorderVisible = true;
        [Category("1. Borders"), DisplayName("3. Right Visible")]
        [Description("Set Right Border Visibility")]
        [Browsable(true)]
        public bool RightBorderVisible
        {
            get { return rightBorderVisible; }
            set { if (rightBorderVisible != value) { rightBorderVisible = value; Invalidate(); } }
        }

        // -> Right Border (Color)
        private Color rightBorderColor = Color.DimGray;
        [Category("1. Borders"), DisplayName("3. Right Color")]
        [Description("Set Right Border Color")]
        [Browsable(true)]
        public Color RightBorderColor
        {
            get { return rightBorderColor; }
            set { if (rightBorderColor != value) { rightBorderColor = value; Invalidate(); } }
        }

        // -> Bottom Border (Visibility)
        private bool bottomBorderVisible = true;
        [Category("1. Borders"), DisplayName("4. Bottom Visible")]
        [Description("Toggle Bottom Border Visibility")]
        [Browsable(true)]
        public bool BottomBorderVisible
        {
            get { return bottomBorderVisible; }
            set { if (bottomBorderVisible != value) { bottomBorderVisible = value; Invalidate(); } }
        }

        // -> Bottom Border (Color)
        private Color bottomBorderColor = Color.DimGray;
        [Category("1. Borders"), DisplayName("4. Bottom Color")]
        [Description("Set Bottom Border Color")]
        [Browsable(true)]
        public Color BottomBorderColor
        {
            get { return bottomBorderColor; }
            set { if (bottomBorderColor != value) { bottomBorderColor = value; Invalidate(); } }
        }
        #endregion

        #region <Custom Properties> : (Title Bar)
        private bool titleBarEnabled = false;
        [Category("2. TitleBar"), DisplayName("TitleBar Enabled")]
        [Description("Specify if Panel will be used as a TitleBar.")]
        [Browsable(true)]
        public bool TitleBarEnabled
        {
            get { return titleBarEnabled; }
            set { titleBarEnabled = value; Invalidate(); }
        }

        private Font titleBarFont = new Font("Arial", 10, FontStyle.Regular);
        [Category("2. TitleBar"), DisplayName("TitleBar Font")]
        [Description("Set Title Bar Text Font (Requires TitleBar Property to be Enabled and Text value).")]
        [Browsable(true), Bindable(true)]
        public Font TitleBarFont
        {
            get { return titleBarFont; }
            set { if (titleBarFont != value) { titleBarFont = value; Invalidate(); } } // Refresh(); 
        }

        [Category("2. TitleBar"), DisplayName("TitleBar Font Style")]
        [Description("Set Title Bar Title Text Style (Requires TitleBar Property to be Enabled and Text value).")]
        [Browsable(true), Bindable(true)]
        public FontStyle FontStyle
        {
            get { return TitleBarFont.Style; }
            set { if (TitleBarFont.Style != value) { TitleBarFont = new Font(titleBarFont.FontFamily, titleBarFont.Size, value); Invalidate(); } }
        }

        private Color titleBarForeColor = Color.White;
        [Category("2. TitleBar"), DisplayName("TitleBar ForeColor")]
        [Description("Set Title Bar Text ForeColor.")]
        [Browsable(true)]
        public Color TitleBarForeColor
        {
            get { return titleBarForeColor; }
            set { if (titleBarForeColor != value) { titleBarForeColor = value; Invalidate(); } }
        }

        private string titleBarText = "Window Title";
        [Category("2. TitleBar"), DisplayName("TitleBar Text")]
        [Description("Set TitleBar Title Text. (Requires TitleBar Property to be Enabled).")]
        [Browsable(true)]
        public string TitleBarText
        {
            get { return titleBarText; }
            set { if (titleBarText != value) { titleBarText = value; Invalidate(); } }
        }

        public enum TextAlignment { LeftTop, LeftMiddle, LeftBottom, CenterTop, CenterMiddle, CenterBottom, RightTop, RightMiddle, RightBottom }
        private TextAlignment textAlignment = TextAlignment.LeftMiddle;
        [Category("2. TitleBar"), DisplayName("Text Alignment")]
        [Description("Set Horizontal Text Alignment")]
        [Browsable(true)]
        public TextAlignment TitleBarHorizontalTextAlignment
        {
            get { return textAlignment; }
            set
            {
                textAlignment = value;
                Invalidate();
            }
        }
        #endregion


        private bool mouseIsOver = false;

        #region <Overriden Events>
        /// <summary> Occurs when the Control is Requested to be Redrawn. </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            DrawBorders(g);

            DrawTitleBarText(g);
        }

        /// <summary> Occurs when the Mouse Enters the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            mouseIsOver = true;
            //if (highlightEnabled) { Invalidate(); }
        }

        /// <summary> Occurs when the Control Moves within the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mouseIsOver = ClientRectangle.Contains(PointToClient(MousePosition));
            //if (highlightEnabled) { Capture = ClientRectangle.Contains(PointToClient(MousePosition)); }
        }

        /// <summary> Occurs when the Mouse Leaves the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseEnter(e);

            mouseIsOver = false;
            //if (highlightEnabled) { Invalidate(); }
        }
        #endregion


        #region <Methods> : (TitleBar)
        /// <summary> Set the Title Bar Text Alignment. </summary>
        /// <returns></returns>
        private TextFormatFlags TitleBarTextAlignment()
        {
            TextFormatFlags textFlags = TextFormatFlags.Default;

            switch (textAlignment)
            {
                // -> Left
                case TextAlignment.LeftTop: textFlags = TextFormatFlags.Left | TextFormatFlags.Top; break;
                case TextAlignment.LeftMiddle: textFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter; break;
                case TextAlignment.LeftBottom: textFlags = TextFormatFlags.Left | TextFormatFlags.Bottom; break;

                // -> Center
                case TextAlignment.CenterTop: textFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.Top; break;
                case TextAlignment.CenterMiddle: textFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter; break;
                case TextAlignment.CenterBottom: textFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom; break;

                // -> Right
                case TextAlignment.RightTop: textFlags = TextFormatFlags.Right | TextFormatFlags.Top; break;
                case TextAlignment.RightMiddle: textFlags = TextFormatFlags.Right | TextFormatFlags.VerticalCenter; break;
                case TextAlignment.RightBottom: textFlags = TextFormatFlags.Right | TextFormatFlags.Bottom; break;
            }

            return textFlags;
        }

        /// <summary> Draw TitleBar Text. </summary>
        /// <param name="pe"></param>
        private void DrawTitleBarText(Graphics g)
        {
            if (TitleBarEnabled)
            {
                // -> Improved Text Rendering
                // Note: Use TextRenderer to Draw the Text Object (No Anti-Aliasing Required).
                //       Replaces Graphics.DrawString()
                //       This will give a more Natural aspect to the Rendered List of Items. (No Need to use Anti-Aliasing).

                TextRenderer.DrawText(g, titleBarText, titleBarFont, ClientRectangle, titleBarForeColor, TitleBarTextAlignment());
            }
        }
        #endregion

        #region <Methods> : (Borders)
        /// <summary> Draw Default Border Colors. </summary>
        /// <param name="e"></param>
        private void DrawDefaultBorders(Graphics g)
        {
            if (leftBorderVisible) { g.DrawLine(new Pen(leftBorderColor), 0, 0, 0, Height); }
            if (topBorderVisible) { g.DrawLine(new Pen(topBorderColor), 0, 0, Width, 0); }
            if (rightBorderVisible) { g.DrawLine(new Pen(rightBorderColor), (Width - 1), 0, (Width - 1), Height); }
            if (bottomBorderVisible) { g.DrawLine(new Pen(bottomBorderColor), 0, (Height - 1), Width, (Height - 1)); }
        }

        /// <summary> Draw Border Highlight (Highlight Control Borders). </summary>
        /// <param name="e"></param>
        private void DrawBorderHighlight(Graphics g)
        {
            if (leftBorderVisible)      { g.DrawLine(new Pen(HighlightColor), 0, 0, 0, Height); }
            if (topBorderVisible)       { g.DrawLine(new Pen(HighlightColor), 0, 0, Width, 0); }
            if (rightBorderVisible)     { g.DrawLine(new Pen(HighlightColor), (Width - 1), 0, (Width - 1), Height); }
            if (bottomBorderVisible)    { g.DrawLine(new Pen(HighlightColor), 0, (Height - 1), Width, (Height - 1)); }
        }

        /// <summary> Draw the Control Borders. </summary>
        /// <param name="g"></param>
        private void DrawBorders(Graphics g)
        {
            if (highlightEnabled)
            {
                if (mouseIsOver) { DrawBorderHighlight(g); }
                else { DrawDefaultBorders(g); }
            }

            else { DrawDefaultBorders(g); }
        }
        #endregion
    }
}


/* Kept for Reference (ONLY)
 * 
 * The Method Bellow Shows 2 Ways of Drawing a Text Object into the Panel, using:
 *      - g.DrawString
 *      - TextRenderer.DrawText
 *      
 *      Notes:
 *      'g.DrawString': Requires some Rendering Configuration, on the other hand:
 *      'TextRenderer': already Renders the Text, and (Slightly) Simplifies Drawing a Text Object.
 * 
 *      Example:
 *      
 * /// <summary> Draw TitleBar Text. </summary>
 * /// <param name="pe"></param>
 * private void DrawTitleBarText(Graphics g)
 * {
 *      if (TitleBarEnabled)
 *      {
 *          // -> Drawing Text Object with Graphics.DrawString:
 *          // g.DrawString(titleBarText, titleBarFont, new SolidBrush(titleBarForeColor), ClientRectangle, str);
 *          
 *          // -> Graphics.DrawString Text Rendering (Uncomment 1 of these 3)
 *          // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit; // Highest
 *          // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit; // High
 *          // g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;    // System Default
 *          
 *          
 *          
 *          // -> Drawing Text Object using Text Renderer (No Anti-Aliasing Required. Text Renderer already Renders the Text Object).
 *          //    This will give a more Natural aspect to the Rendered List of Items. (No Need to use Anti-Aliasing).
 *          
 *          TextRenderer.DrawText(g, titleBarText, titleBarFont, ClientRectangle, titleBarForeColor, TitleBarTextAlignment());
 *          
 *          // or:
 *          // TextRenderer.DrawText(g, titleBarText, titleBarFont, ClientRectangle.Location, titleBarForeColor, Color.Transparent, TextFormatFlags.Left);
 *      }
 *  }
 */