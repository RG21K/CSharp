// <copyright file="RGSwitch.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
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

namespace RG_Switch.Controls
{
    public class RGSwitch : CheckBox
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Ideas to Implement]
         * 
         *          [1]. Toggle Orientation (Horizontal & Vertical)
         *          [2]. When Solid Design is Selected: Set also the Background Colors (Checked / Unchecked).
         * 
         */
        #endregion

        #region <Constructor>
        public RGSwitch()
        {
            // Requests the Form to use a Secondary Buffer to Redraw the Content.
            // You can Set Either: DoubleBuffered = true;
            // or
            // SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //
            // Link: https://docs.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-reduce-graphics-flicker-with-double-buffering-for-forms-and-controls?view=netframeworkdesktop-4.8
            //
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            MinimumSize = new Size(40, 20);
            Size = new Size(40, 20);
        }
        #endregion


        #region <Fields>
        private bool defAutoSize = false;
        #endregion


        #region <Properties> : (Overriden)
        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = defAutoSize; }
        }

        public override string Text
        {
            get { return base.Text; }
        }
        #endregion


        #region <Custom Properties>
        private bool solidStyle = false;
        [Category("1. Style"), DisplayName("0. IsSolidStyle")]
        [Description("Set the Control Design Style.")]
        [Bindable(true)]        // Required for Enum Types.
        [Browsable(true)]
        [DefaultValue(true)]    // What the Default Property Value is (It Does NOT Set the Actual Default Value).
        public bool IsSolidStyle
        {
            get { return solidStyle; }
            set { solidStyle = value; Invalidate(); }
        }

        private Color enabledBackColor = Color.Silver;
        [Category("1. Style"), DisplayName("1. Enabled BackColor")]
        [Description("Set Back Color for Enabled Switch Condition.")]
        [Bindable(true)]        // Required for Enum Types.
        [Browsable(true)]
        public Color EnabledBackColor
        {
            get { return enabledBackColor; }
            set { enabledBackColor = value; Invalidate(); }
        }

        private Color disabledBackColor = Color.Silver;
        [Category("1. Style"), DisplayName("2. Disabled BackColor")]
        [Description("Set Back Color for Disabled Switch Condition.")]
        [Bindable(true)]        // Required for Enum Types.
        [Browsable(true)]
        public Color DisabledBackColor
        {
            get { return disabledBackColor; }
            set { disabledBackColor = value; Invalidate(); }
        }

        private Color enabledKnobColor = Color.FromArgb(255, 38, 224, 127);
        [Category("1. Style"), DisplayName("3. Enabled KnobColor")]
        [Description("Set Knob Color for Enabled Switch Condition.")]
        [Bindable(true)]        // Required for Enum Types.
        [Browsable(true)]
        public Color EnabledKnobColor
        {
            get { return enabledKnobColor; }
            set { enabledKnobColor = value; Invalidate(); }
        }

        private Color disabledKnobColor = Color.Crimson;
        [Category("1. Style"), DisplayName("4. Disabled KnobColor")]
        [Description("Set Knob Color for Enabled Switch Condition.")]
        [Bindable(true)]        // Required for Enum Types.
        [Browsable(true)]
        public Color DisabledKnobColor
        {
            get { return disabledKnobColor; }
            set { disabledKnobColor = value; Invalidate(); }
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

            e.Graphics.Clear(Parent.BackColor);

            RedrawControl(e);
        }
        #endregion


        #region <Methods> : (Drawing)
        private GraphicsPath GetFigurePath()
        {
            // Default Block:
            //int arcSize = Height - 1;
            //Rectangle leftArc = new Rectangle(0, 0, arcSize, arcSize);
            //Rectangle rightArc = new Rectangle(Width - arcSize - 2, 0, arcSize, arcSize);

            // Corrected Block (! Miss Margin !)
            int arcSize = Height - 2;
            Rectangle leftArc = new Rectangle(1, 1, arcSize, arcSize);
            Rectangle rightArc = new Rectangle(Width - arcSize - 2, 1, arcSize, arcSize);

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(leftArc, 90, 180);
            path.AddArc(rightArc, 270, 180);
            path.CloseFigure();

            return path;
        }
        private void RedrawControl(PaintEventArgs pe)
        {
            // int toggleSize = Height - 5; // DEFAULT LINE
            int toggleSize = Height - 6;

            if (Checked)
            {
                // Draw the Surface
                if (IsSolidStyle) { pe.Graphics.FillPath(new SolidBrush(enabledBackColor), GetFigurePath()); }
                else { pe.Graphics.DrawPath(new Pen(enabledBackColor, 2), GetFigurePath()); }

                // Draw the Toggle Marker
                //pe.Graphics.FillEllipse(new SolidBrush(enabledKnobColor), new Rectangle(Width - Height + 1, 2, toggleSize, toggleSize)); // DEFAULT LINE
                pe.Graphics.FillEllipse(new SolidBrush(enabledKnobColor), new Rectangle(Width - Height + 1, 3, toggleSize, toggleSize));
                this.Checked = true;
            }

            else
            {
                // Draw the Surface
                if (solidStyle) { pe.Graphics.FillPath(new SolidBrush(disabledBackColor), GetFigurePath()); }
                else { pe.Graphics.DrawPath(new Pen(disabledBackColor, 2), GetFigurePath()); }

                // Draw the Toggle Marker
                // pe.Graphics.FillEllipse(new SolidBrush(disabledKnobColor), new Rectangle(2, 2, toggleSize, toggleSize)); // DEFAULT LINE
                pe.Graphics.FillEllipse(new SolidBrush(disabledKnobColor), new Rectangle(4, 3, toggleSize, toggleSize));
                this.Checked = false;
            }
        }
        #endregion
    }
}
