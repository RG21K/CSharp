using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
// <copyright file="RGPictureBox.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_PictureBox.Controls
{
    public class RGPictureBox : PictureBox
    {
        // Last Edited: 2022, 10.10
        #region <Configuration>
        /// <summary> Set Control Style Configuration. </summary>
        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion


        #region <Constructor>
        public RGPictureBox()
        {
            SetStyles();

            this.Size = new Size(150, 150);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        #endregion


        #region <Custom Properties>
        private int borderSize = 2;
        [Category("1. Custom Properties")]
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        private Color borderColor = Color.Red;
        [Category("1. Custom Properties")]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        private Color borderColor2 = Color.Red;
        [Category("1. Custom Properties")]
        public Color BorderColor2
        {
            get { return borderColor2; }
            set
            {
                borderColor2 = value;
                this.Invalidate();
            }
        }

        private DashStyle borderLineStyle = DashStyle.Solid;
        [Category("1. Custom Properties")]
        public DashStyle BorderLineStyle
        {
            get { return borderLineStyle; }
            set
            {
                borderLineStyle = value;
                this.Invalidate();
            }
        }

        private DashCap borderCapStyle = DashCap.Flat;
        [Category("1. Custom Properties")]
        public DashCap BorderCapStyle
        {
            get { return borderCapStyle; }
            set
            {
                borderCapStyle = value;
                this.Invalidate();
            }
        }

        private float gradientAngle = 50F;
        [Category("1. Custom Properties")]
        public float GradientAngle
        {
            get { return gradientAngle; }
            set
            {
                gradientAngle = value;
                this.Invalidate();
            }
        }
        #endregion


        #region <Overriden Events>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Size = new Size(this.Width, this.Width);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // -> Rendering Configuration (Set Here and Pass to Further Drawing Methods via PaintEventArgs Parameter)
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            RedrawControl(e);
        }
        #endregion


        #region <Methods> : (Drawing)
        private void RedrawControl(PaintEventArgs e)
        {
            var graph = e.Graphics;
            var rectContourSmooth = Rectangle.Inflate(this.ClientRectangle, -1, -1);
            var rectBorder = Rectangle.Inflate(rectContourSmooth, -borderSize, -borderSize);
            var smoothSize = borderSize > 0 ? borderSize * 3 : 1;
            
            using (var borderGColor = new LinearGradientBrush(rectBorder, borderColor, borderColor2, gradientAngle))
            using (var pathRegion = new GraphicsPath())
            using (var penSmooth = new Pen(this.Parent.BackColor, smoothSize))
            using (var penBorder = new Pen(borderGColor, borderSize))
            {
                penBorder.DashStyle = borderLineStyle;
                penBorder.DashCap = borderCapStyle;
                pathRegion.AddEllipse(rectContourSmooth);

                // -> Set Rounded Region 
                this.Region = new Region(pathRegion);

                // -> Draw Contour Smoothing
                graph.DrawEllipse(penSmooth, rectContourSmooth);

                // -> Draw Border
                if (borderSize > 0) { graph.DrawEllipse(penBorder, rectBorder); }
            }
        }
        #endregion
    }
}
