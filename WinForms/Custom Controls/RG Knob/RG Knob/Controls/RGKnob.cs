// <copyright file="RGKnob.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace RG_Knob.Controls
{
    public class RGKnob : UserControl
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * [Knob Control Information]
         * 
         *      1. [External Code References]
         *         A few Lines of Code were Extracted from 'NAudio POT' (Mark Heath).
         *         
         *         Specifically: 
         *              - The Calculations for the Line Pointer 
         *              - Partial Code for the the Arc Ellipse.
         *              - Mouse Dragging
         *
         *      2. [Tricky Things to Take into Consideration]
         *      
         *              2.1. [Client Rectangle X & Y Repositioning]
         *              The Client Rectangle 'Zero' Position Points (X & Y) Become as the Center of the Ellipse.
         *              In order to Restore to Default Reset the Previous Transform:
         *              
         *                          See: OnPaintEvent:
         *                          
         *                          -> Redefine Client Rectangle Positions (X & Y)
         *                          e.Graphics.TranslateTransform(this.Width / 2, this.Height / 2);
         *                          
         *                          -> Reset Previous Transform on Client Rectangle (Reset X & Y)
         *                          e.Graphics.ResetTransform();
         *                     
         *              2.2. [Bar Value Formula]
         *              
         *                 To Get the Bar Value: Multiply the Knob Value x Bar Sweep angle (270º)
         *                 The Sweep Angle Refers to the Angle of the Drawn Bar.
         *      
         *              2.3. Control Size (Within Client Rectanlge)
         *              
         *                  Zooming / Resizing the Control within the Client Rectangle can be found inside the
         *                  PaintEvent
         *                  
         *                  Line:
         *                  // Arc Diameter (Adjust Size Within Control Here)
         *                  int arcDiameter = Math.Min(this.Width - 15, this.Height - 15);
         *                  
         *                  
         *               // Note: Bar Value = Knob Value * Sweep Angle
         *      
         */
        #endregion

        #region <To Do>
        /*
         * 
         * -> Knob Bar:
         *          - Missing: Padding Between Control Arc and Client Rectangle : SEE: PaintEvent
         *
         * -> Knob Pointers:
         *          - Refactor the Idea: Set an Image Instead of Drawing a Polygon.
         *            Enum Pointers (Line, Picture).
         *          - Add Custom Property to Set Image (Pointer) Size.
         *          - When Ellipse is Selected the Ellipse Location Angles are NOT Precise
         *          - Check the Need for: Custom Properties for Pointer Border (Color and Visible State)
         * 
         */
        #endregion



        #region <Configuration>
        /// <summary> Set Control Style Configuration. </summary>
        private void SetStyles()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
        }
        #endregion


        #region <Constructor>
        /// <summary>
        /// Creates a new pot control
        /// </summary>
        public RGKnob()
        {
            SetStyles();

            Size = new Size(100, 100);                          // Set the Control Default Size.

            KnobCapImage = Properties.Resources.VolumeKnob;     // Set the Knob Cap Image (from Image on Embeded Resources)
        }
        #endregion


        #region <Fields>
        private int beginDragY;
        private float beginDragValue;

        /// <summary> The Initial Angle Where the Bar Will Start to be Drawn. </summary>
        private int barStartAngle = 135;

        /// <summary> The Bar (Arc) Angle to be Drawn. </summary>
        private int barSweepAngle = 270;

        /// <summary> Formatted Knob Value. </summary>
        public string KnobValueFormatted => GetFormattedKnobValue();
        #endregion


        #region <Custom Properties> : (Knob Size)
        private Size knobSize = new Size(80, 80);
        [Category("1. Custom Properties"), DisplayName("Knob Size")]
        [Description("Gets or Set Knob Size. (The Size of the Drawn Knob Inside the Client Rectangle)")]
        [Browsable(true)]
        public Size KnobSize
        {
            get => knobSize;
            set
            {
                knobSize = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Knob Values)
        public enum KnobValueFormat { Decimal, Centesimal, Milesimal, WholeNumber }
        private KnobValueFormat knobValueFormat = KnobValueFormat.WholeNumber;
        [Category("1. Custom Properties"), DisplayName("Value Format")]
        [Description("Set Knob Value Format.")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public KnobValueFormat ValueFormat
        {
            get => knobValueFormat;
            set
            {
                knobValueFormat = value;
                Invalidate();
            }
        }

        /// <summary> Knob Control, Minimum Value. </summary>
        private float knobMinimum = 0F;
        [Category("1. Custom Properties"), DisplayName("Value Minimum")]
        [Description("Sets Minimum Knob Value")]
        [Browsable(true)]
        public float KnobMinimum
        {
            get { return knobMinimum; }
            set { knobMinimum = value; Invalidate(); }
        }

        /// <summary> Knob control, Maximum Value. </summary>
        private float knobMaximum = 1.0F;
        [Category("1. Custom Properties"), DisplayName("Value Maximum")]
        [Description("Sets Maximum Knob Value")]
        [Browsable(true)]
        public float KnobMaximum
        {
            get { return knobMaximum; }
            set { knobMaximum = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Knob Control Value. </summary>
        private float knobValue = 0.5F;
        [Category("1. Custom Properties"), DisplayName("Value")]
        [Description("Gets or Sets the Knob Value.")]
        [Browsable(true)]
        public float KnobValue
        {
            get { return knobValue; }
            set
            {
                if (value >= knobMinimum & value <= knobMaximum)
                {
                    knobValue = value;
                    // SetValue(value);    // Also Calls Invalidate(); but Does NOT Redraw the Control when Value is Set.
                    Invalidate();
                }
            }
        }
        #endregion

        #region <Custom Properties> : (Knob Label)
        /// <summary> Gets or Sets the Knob Label Type. </summary>
        public enum InfoMode { None, LabelValue, LabelCustom }
        private InfoMode infoDisplayMode = InfoMode.LabelValue;
        [Category("1. Custom Properties"), DisplayName("Label Type")]
        [Description("Set Knob Label Type (None, Label or ToolTip).")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public InfoMode InfoDisplayMode
        {
            get { return infoDisplayMode; }
            set { infoDisplayMode = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Knob Label Font. </summary>
        private Font labelFont = new Font("Consolas", 7.6F);
        [Category("1. Custom Properties"), DisplayName("Label Font")]
        [Description("Gets or Sets Knob Label Font.")]
        [Browsable(true)]
        public Font LabelValueFont
        {
            get { return labelFont; }
            set { labelFont = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Knob Value Scale Designator. </summary>
        public enum KnobValueDesignator
        {
            None,

            Centimeters,
            Centimeters2,
            Centimeters3,

            Degrees,

            Millimeters,
            Millimeters2,
            Millimeters3,

            Meters,
            Meters2,
            Meters3,

            ParticlesPerMolecule,

            Percentage,
        }
        private KnobValueDesignator knobValueDesignator = KnobValueDesignator.Percentage;
        [Category("1. Custom Properties"), DisplayName("Value Designator")]
        [Description("Set Knob Value Scale Designator")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public KnobValueDesignator ValueDesignator
        {
            get { return knobValueDesignator; }
            set { knobValueDesignator = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Knob Label Title. </summary>
        private string labelTitle = "Label Title";
        [Category("1. Custom Properties"), DisplayName("Label Title")]
        [Description("Gets or Set Knob Label Title.")]
        [Browsable(true)]
        public string LabelTitle
        {
            get { return labelTitle; }
            set { labelTitle = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Knob Label Text. </summary>
        private string labelText = "VOL";
        [Category("1. Custom Properties"), DisplayName("Label Text")]
        [Description("Gets or Set Knob Label Text.")]
        [Browsable(true)]
        public string LabelText
        {
            get { return labelText; }
            set { labelText = value; Invalidate(); }
        }

        private int labelVerticalPadding = 20;
        [Category("1. Custom Properties"), DisplayName("Label Vertical Padding")]
        [Description("Gets or Set Label Vertical Padding.")]
        [Browsable(true)]
        public int LabelVerticaPadding
        {
            get => labelVerticalPadding;
            set
            {
                labelVerticalPadding = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Knob Cap)
        /// <summary> Gets or Sets the Knob Background Rectangle Border. </summary>
        private bool knobCapBorderVisible = false;
        [Category("1. Custom Properties"), DisplayName("Cap Border Visible")]
        [Description("Toggles the Knob Cap Border Visibility")]
        [Browsable(true)]
        public bool KnobCapBorderVisible
        {
            get { return knobCapBorderVisible; }
            set { knobCapBorderVisible = value; Invalidate(); }
        }

        /// <summary> Knob Cap Background Color. </summary>
        private Color knobCapBackgroundColor = Color.FromArgb(255, 36, 36, 52);
        [Category("1. Custom Properties"), DisplayName("Cap Background Color")]
        [Description("Gets or Sets Knob Cap Background Color")]
        [Browsable(true)]
        public Color KnobBackColor
        {
            get { return knobCapBackgroundColor; }
            set
            {
                knobCapBackgroundColor = value;
                Invalidate();
            }
        }

        /// <summary> Knob Cap Border Thickness. </summary>
        private float knobCapBorderThickness = 1.5F;
        [Category("1. Custom Properties"), DisplayName("Cap Border Thickness")]
        [Description("Gets or Sets Knob Cap Border Thickness")]
        [Browsable(true)]
        public float KnobCapBorderThickness
        {
            get { return knobCapBorderThickness; }
            set
            {
                knobCapBorderThickness = value;
                Invalidate();
            }
        }

        /// <summary> Gets or Sets the Knob Border Color. </summary>
        private Color knobCapBorderColor = Color.Silver;
        [Category("1. Custom Properties"), DisplayName("Cap Border Color")]
        [Description("Gets or Sets the Knob Cap Border Color")]
        [Browsable(true)]
        public Color KnobCapBorderColor
        {
            get { return knobCapBorderColor; }
            set { knobCapBorderColor = value; Invalidate(); }
        }

        /// <summary> Gets or Sets the Knob Background Image. </summary>
        private Image knobCapImage = null;
        [Category("1. Custom Properties"), DisplayName("Cap Image")]
        [Description("Set the Knob Cap Image")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public Image KnobCapImage
        {
            get { return knobCapImage; }
            set { knobCapImage = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Knob Pointer)
        /// <summary> Gets or Sets the Knob Pointer Shape. </summary>
        public enum KnobPointerType { Line, Ellipse, Picture }
        private KnobPointerType pointerType = KnobPointerType.Line;
        [Category("1. Custom Properties"), DisplayName("Pointer Type")]
        [Description("Set the Pointer Type (Line or Picture)")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public KnobPointerType PointerType
        {
            get { return pointerType; }
            set { pointerType = value; Invalidate(); }
        }

        /// <summary> Knob Pointer Color. </summary>
        private Color pointerColor = Color.Crimson;
        [Category("1. Custom Properties"), DisplayName("Pointer Color")]
        [Description("Gets or Sets Pointer Color")]
        [Browsable(true)]
        public Color PointerColor
        {
            get { return pointerColor; }
            set
            {
                pointerColor = value;
                Invalidate();
            }
        }

        private Color pointerBorderColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Pointer Border Color")]
        [Description("Gets or Sets Pointer Border Color")]
        [Browsable(true)]
        public Color PointerBorderColor
        {
            get => pointerBorderColor;
            set
            {
                pointerBorderColor = value;
                Invalidate();
            }
        }

        private int pointerPadding = 10;
        [Category("1. Custom Properties"), DisplayName("Pointer Padding")]
        [Description("Gets or Sets Pointer Padding")]
        [Browsable(true)]
        public int PointerPadding
        {
            get => pointerPadding;
            set
            {
                pointerPadding = value;
                Invalidate();
            }
        }

        private bool pointerBorderVisible = false;
        [Category("1. Custom Properties"), DisplayName("Pointer Border Visible")]
        [Description("Toggles the Pointer Border")]
        [Browsable(true)]
        public bool PointerBorderVisible
        {
            get => pointerBorderVisible;
            set
            {
                pointerBorderVisible = value;
                Invalidate();
            }
        }

        /// <summary> Gets or Sets the Knob Pointer Image. </summary>
        private Image knobPointerImage = null;
        [Category("1. Custom Properties"), DisplayName("Pointer Image")]
        [Description("Set the Knob Pointer Image")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public Image KnobPointerImage
        {
            get { return knobPointerImage; }
            set { knobPointerImage = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Knob Icon)
        /// <summary> Gets or Sets the Knob Image. </summary>
        private Image knobIcon = null;
        [Category("1. Custom Properties"), DisplayName("Knob Icon Image")]
        [Description("Set the Knob (Slider) Background Image")]
        [Browsable(true)]
        public Image KnobImage
        {
            get { return knobIcon; }
            set
            {
                knobIcon = value;
                Invalidate();
            }
        }

        private int knobIconSize = 20;
        [Category("1. Custom Properties"), DisplayName("Knob Icon Size")]
        [Description("Gets or Sets the Knob Icon Size")]
        [Browsable(true)]
        public int KnobIconSize
        {
            get { return knobIconSize; }
            set { knobIconSize = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Knob Bar Visibility)
        private bool barVisible = true;
        [Category("1. Custom Properties"), DisplayName("Bar Visible")]
        [Description("Toggles Bar Visibility.")]
        [Browsable(true)]
        public bool BarVisible
        {
            get => barVisible;
            set
            {
                barVisible = value;
                Invalidate();
            }
        }
        #endregion

        #region <Custom Properties> : (Knob Bar Foreground)
        /// <summary> Gets or Sets Bar Foreground Thickness. </summary>
        private int barForegroundThickness = 8;
        [Category("1. Custom Properties"), DisplayName("Bar Foreground Thickness")]
        [Description("Sets the Tick Bar Thickness.")]
        [Browsable(true)]
        public int BarForegroundThickness
        {
            get { return barForegroundThickness; }
            set { barForegroundThickness = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Bar Foreground Color. </summary>
        private Color barForegroundColor = Color.DeepSkyBlue;
        [Category("1. Custom Properties"), DisplayName("Bar Foreground Color")]
        [Description("Sets the Tick Bar Thickness.")]
        [Browsable(true)]
        public Color BarForegroundColor
        {
            get { return barForegroundColor; }
            set { barForegroundColor = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Knob Bar Background)
        /// <summary> Gets or Sets Bar Background Thickness. </summary>
        private int barBackgroundThickness = 8;
        [Category("1. Custom Properties"), DisplayName("Bar Background Thickness")]
        [Description("Sets the Bar Background Thickness.")]
        [Browsable(true)]
        public int BarBackgroundThickness
        {
            get { return barBackgroundThickness; }
            set { barBackgroundThickness = value; Invalidate(); }
        }

        /// <summary> Gets or Sets Bar Background Color. </summary>
        private Color barBackgroundColor = Color.Gray;
        [Category("1. Custom Properties"), DisplayName("Bar Background Color")]
        [Description("Sets the Bar Background Color.")]
        [Browsable(true)]
        public Color BarBackgroundColor
        {
            get { return barBackgroundColor; }
            set { barBackgroundColor = value; Invalidate(); }
        }
        #endregion

        #region <Custom Properties> : (Knob Bar Padding from Control)
        /// <summary> The Distance Between the Bar and Knob Border. </summary>
        private int barPadding = 3;
        [Category("1. Custom Properties"), DisplayName("Bar Padding")]
        [Description("Sets the Distance Between the Bar and Knob Border.")]
        [Browsable(true)]
        public int BarPadding
        {
            get { return barPadding; }
            set { barPadding = value; Invalidate(); }
        }
        #endregion


        #region <Custom Events>
        public delegate void ValueChanged(object sender);
        public event ValueChanged OnValueChanged;
        #endregion


        #region <Overriden Events> : (Paint)
        /// <summary>
        /// Draws the control
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Default Code
            base.OnPaint(e);

            // -> Fields
            //int arcDiameter = Math.Min(this.Width - 20, this.Height - 20);            // Default Line : Arc Diameter (Adjust Size Within Control Here)
            int arcDiameter = Math.Min(knobSize.Width, knobSize.Height);                // Used to Adjust the Knob Size (NOT Client Control!)
            double percent = (knobValue - knobMinimum) / (knobMaximum - knobMinimum);
            double degrees = barStartAngle + (percent * barSweepAngle);
            double arcX = (arcDiameter / 2.0) * Math.Cos(Math.PI * degrees / 180);      // Used for Pointer Location X : (2.0 Ajusts Position)
            double arcY = (arcDiameter / 2.0) * Math.Sin(Math.PI * degrees / 180);      // Used for Pointer Location Y : (2.0 Ajusts Position)

            // -> Graphics Rendering
            // -> Configuration for Best Smooth Graphics
            // Ref: https://stackoverflow.com/questions/33878184/c-sharp-how-to-make-smooth-arc-region-using-graphics-path
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighSpeed;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // -> Bar Rectangle
            var barRect = new Rectangle
            {
                X = arcDiameter / -2,
                Y = arcDiameter / -2,
                Width = arcDiameter,
                Height = arcDiameter
            };


            /* -------------------------------------------------------------------- 
             *  IMPORTANT WARNING 
             * --------------------------------------------------------------------
             * - After Graphics Translation the Client Rectangle X,Y Zero Positions 
             *   become the Center of the Control (Circumference)!
             * - Bellow you can place (ONLY) Code Related to:
             * 
             *      * Knob Circumference, 
             *      * Knob Diameter 
             *      * Knob Pointers
             *      
             * --------------------------------------------------------------------
             */

            // -> Redefine Client Rectangle Positions (X & Y)
            e.Graphics.TranslateTransform(Width / 2, Height / 2);

            // -> Draw Components
            if (barVisible) { DrawBar(e, barRect, knobValue); }
            DrawKnobCap(e, barRect /*arcDiameter*/);
            DrawPointer(e, 0, 0, arcX, arcY);

            // -> Reset Previous Transform on Client Rectangle (Reset X & Y)
            e.Graphics.ResetTransform();

            // -> Draw Remaining Components
            DrawKnobIcon(e);
            DrawLabel(e);
        }
        #endregion


        #region <Overriden Events> : (Keyboard)
        /// <summary> Occurs when a Key is Pressed (Previewed) on the Form Container. </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            // Increase
            if (e.KeyCode.Equals(Keys.Up) ^ e.KeyCode.Equals(Keys.Add))
            { KnobValue += 0.01F; }

            else if (e.KeyCode.Equals(Keys.PageUp)) { KnobValue = knobMaximum; }

            // Decrease
            else if (e.KeyCode.Equals(Keys.Down) ^ e.KeyCode.Equals(Keys.Subtract))
            {
                KnobValue -= 0.01F;
            }
            else if (e.KeyCode.Equals(Keys.PageDown)) { KnobValue = knobMinimum; }
        }
        #endregion

        #region <Overriden Events> : (Mouse)
        /// <summary>
        /// Handles the mouse down event to allow changing value by dragging
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Capture = true;
            beginDragY = e.Y;
            beginDragValue = knobValue;
        }

        /// <summary>
        /// Handles the mouse up event to allow changing value by dragging
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            Capture = false;
        }

        /// <summary>
        /// Handles the mouse down event to allow changing value by dragging
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (Capture)
            {
                int yDifference = beginDragY - e.Y;
                // 100 is the number of pixels of vertical movement that represents the whole scale
                float delta = (knobMaximum - knobMinimum) * (yDifference / 150.0F);
                float newValue = beginDragValue + delta;

                // -> Limit Knob Value to Min. / Max. Values
                if (newValue < knobMinimum)
                    newValue = knobMinimum;

                if (newValue > knobMaximum)
                    newValue = knobMaximum;

                MouseScroll_SetValue(newValue);
            }
        }

        /// <summary> Raises the <see cref="Control.MouseWheel"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            // -> Fine Tune the Mouse Wheel Ticks
            int mouseWheelTicks = 50;

            // -> Calculate the Knob Value
            float val = e.Delta / 120 * (knobMaximum - knobMinimum) / mouseWheelTicks;

            // -> Set the Knob Value
            MouseScroll_SetValue(knobValue + val);
        }
        #endregion


        #region <Methods> : (Drawing : Bar)
        /// <summary> Draw the Knob Bar Value (Surrounding the Contol) </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        /// <param name="value"></param>
        private void DrawBar(PaintEventArgs e, RectangleF rect, float value)
        {
            using (var foreBrush = new SolidBrush(barForegroundColor))          // Foreground
            using (var forePen = new Pen(foreBrush, barForegroundThickness))
            using (var backBrush = new SolidBrush(barBackgroundColor))          // Background
            using (var backPen = new Pen(backBrush, barBackgroundThickness))
            {
                /* Note(s):
                 * 
                 * Parameter Representation:
                 * 
                 *      1. Bar Pen (Foreground or Background)
                 *      2. Bar Rectangle
                 *      3. Bar Start Angle (Default: 135º)
                 *      4. Bar Sweep Angle (Default 270º)
                 * 
                 */

                // -> Draw Background
                e.Graphics.DrawArc(backPen, rect, barStartAngle, barSweepAngle);

                // -> Draw Foreground

                /* Note: 
                 * 
                 * To Get the Bar Value Multiply the Knob Value x Bar Sweep angle (270º)
                 * The Sweep Angle Refers to the Angle of the Drawn Bar.
                 * 
                 */

                e.Graphics.DrawArc(forePen, rect, barStartAngle, knobValue * barSweepAngle);
            }
        }
        #endregion

        #region <Methods> : (Drawing : Cap)
        /// <summary> Draw the Knob Cap (with Solid Color or Specified Image). </summary>
        /// <param name="e"></param>
        /// <param name="rect"></param>
        /// <param name="diameter"></param>
        private void DrawKnobCap(PaintEventArgs e, RectangleF rect)
        {
            // -> Draw the Knob Background
            using (var backBrush = new SolidBrush(knobCapBackgroundColor))
            using (var borderPen = new Pen(knobCapBorderColor, KnobCapBorderThickness))
            {
                // Draw Knob Background (Solid Color or Image)
                if (knobCapImage == null) { e.Graphics.FillEllipse(backBrush, rect); }
                else { e.Graphics.DrawImage(knobCapImage, rect); }

                // Draw Knob Background Border
                if (knobCapBorderVisible) { e.Graphics.DrawEllipse(borderPen, rect); }
            }
        }
        #endregion


        #region [!] <Methods> : (Drawing : Pointer) [!] Incomplete!
        /// <summary> Draws the Knob Pointer. </summary>
        /// <param name="e"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private void DrawPointer(PaintEventArgs e, double x1, double y1, double x2, double y2)
        {
            /*
             * [Used by Line Pointer]
             * x1: 
             * y1: 
             * x2: 
             * y2: 
             * 
             * [To be Used by Shape Pointers (Picures or Polygons)]
             * Object X:
             * Object Y:
             */

            // -> Set Pointer
            switch (pointerType)
            {
                case KnobPointerType.Ellipse:
                    // Note: Bar Value = Knob Value * Sweep Angle

                    // IS THE ELLIPSE SIZE THE DIFFERENCE IN THE POSITION?
                    var shapeRectangle = new RectangleF
                    {
                        X = (float)x2,
                        Y = (float)y2,
                        Width = 7,
                        Height = 7
                    };

                    using (var backBrush = new SolidBrush(pointerColor))
                    using (var borderPen = new Pen(pointerBorderColor, 1))        // Default Border Pointer is Always 1.
                    {
                        // -> Draw the Pointer Background:
                        e.Graphics.FillEllipse(backBrush, shapeRectangle);

                        // -> Draw Pointer Border
                        if (pointerBorderVisible) { e.Graphics.DrawEllipse(borderPen, shapeRectangle); }
                    }
                    break;


                case KnobPointerType.Line:
                    using (var _pen = new Pen(pointerColor, 3.0f))
                    {
                        e.Graphics.DrawLine(_pen, (float)x1, (float)y1, (float)x2, (float)y2);
                    }
                    break;

                case KnobPointerType.Picture:
                    var rect = new RectangleF
                    {
                        X = (float)x2,
                        Y = (float)y2,
                        Width = 10,
                        Height = 10
                    };

                    // Draw Knob Background (Solid Color or Image)
                    if (knobPointerImage != null)
                    {
                        e.Graphics.DrawImage(knobCapImage, rect);
                    }
                    break;
            }
        }
        #endregion

        #region <Methods> : (Drawing : Knob Icon)
        /// <summary> Draws the Knob Icon. </summary>
        /// <param name="e"></param>
        private void DrawKnobIcon(PaintEventArgs e)
        {
            // Draw Knob Background Image
            if (knobIcon != null)
            {
                // Set the Rectangle for the Knob Icon (Centered in Knob)
                var rect = new Rectangle
                {
                    X = (Width / 2) - (knobIconSize / 2),
                    Y = (Height / 2) - (knobIconSize / 2),
                    Width = knobIconSize,
                    Height = knobIconSize
                };

                e.Graphics.DrawImage(knobIcon, rect);
            }
        }
        #endregion

        #region <Methods> : (Drawing : Label)
        /// <summary> Draws a Label to Show Knob Information (Title and/or Value). </summary>
        /// <param name="e"></param>
        private void DrawLabel(PaintEventArgs e)
        {
            string text;

            if (infoDisplayMode.Equals(InfoMode.LabelValue))
            {
                // -> Configure the Text Formatting Flags
                TextFormatFlags textFlags = TextFormatFlags.Default;

                // -> Set the Label Text (Knob Value or Custom Text Value)
                text = GetFormattedKnobValue();

                // -> Configure the Label Rectagle
                Size textSize = TextRenderer.MeasureText(text, labelFont);

                // -> Set the Label Rectangle
                var textRect = new Rectangle
                {
                    X = ClientRectangle.X + (ClientRectangle.Width / 2) - (textSize.Width / 2),
                    Y = ClientRectangle.Y + ClientRectangle.Size.Height - textSize.Height - labelVerticalPadding,

                    Width = ClientRectangle.Width,
                    Height = textSize.Height + 3,
                };


                // -> Draw Text Value
                TextRenderer.DrawText(e.Graphics, text, labelFont, textRect, Color.White, textFlags);
            }

            if (infoDisplayMode.Equals(InfoMode.LabelCustom))
            {
                // -> Configure the Text Formatting Flags
                TextFormatFlags textFlags = TextFormatFlags.Default;

                // -> Set the Label Text (Knob Value or Custom Text Value)
                text = LabelText;

                // -> Configure the Label Rectagle
                Size textSize = TextRenderer.MeasureText(text, labelFont);

                // -> Set the Label Rectangle
                var textRect = new Rectangle
                {
                    X = ClientRectangle.X + (ClientRectangle.Width / 2) - (textSize.Width / 2),
                    Y = ClientRectangle.Y + ClientRectangle.Size.Height - textSize.Height - labelVerticalPadding,

                    Width = ClientRectangle.Width,
                    Height = textSize.Height + 3,
                };

                // -> Draw Text Value
                TextRenderer.DrawText(e.Graphics, text, labelFont, textRect, Color.White, textFlags);
            }
        }
        #endregion


        #region <Methods> : (Fn)
        /// <summary> Set the Knob Value using the Mouse Wheel. </summary>
        /// <param name="newValue"></param>
        /// <param name="raiseEvents"></param>
        private void MouseScroll_SetValue(float newValue)
        {
            if (knobValue != newValue)
            {
                // Fine Tune the Value (Prevents Limiting to 1 and to 99)
                if (newValue < knobMinimum) { newValue = knobMinimum; }
                if (newValue > knobMaximum) { newValue = knobMaximum; }

                // Set the Value Using the Mouse Scroll Wheel
                if (newValue >= knobMinimum & newValue <= knobMaximum)
                {
                    knobValue = newValue;

                    // -> Raise the Event
                    OnValueChanged?.Invoke(this);

                    Invalidate();
                }
            }
        }

        /// <summary> Formats the Knob Value. </summary>
        /// <returns> Knob Value Formatted According to Specified Format. </returns>
        private string GetFormattedKnobValue()
        {
            string val = string.Empty;

            /* Reference:
             * https://stackoverflow.com/questions/6356351/formatting-a-float-to-2-decimal-places
             * 
             * myFloatVariable.ToString("0.00");    // 2dp Number
             * myFloatVariable.ToString("n2");      // 2dp Number
             * myFloatVariable.ToString("c2");      // 2dp currency
             * 
             * For Precision: use 'decimal' type instead of float.
             * i.e: for the prices. 
             * Using float is absolutely unacceptable for precision because 
             * it cannot accurately represent most decimal fractions.
             * Also: Decimal.Round() can be used to round to 2 places.
             * 
             * Interpolated strings are slow. In my experience this is the order (fast to slow):
             * value.ToString(format)+" blah blah"
             * string.Format("{0:format} blah blah", value)
             * $"{value:format} blah blah"
             * 
             */

            switch (knobValueFormat)
            {
                case KnobValueFormat.Decimal: val = knobValue.ToString("0.0"); break;
                case KnobValueFormat.Centesimal: val = knobValue.ToString("0.00"); break;
                case KnobValueFormat.Milesimal: val = knobValue.ToString("0.000"); break;
                case KnobValueFormat.WholeNumber:
                    float valF = (knobValue * 100);
                    int v = (int)valF;
                    val = v.ToString();
                    break;
            }


            // Set the Value Scale Designator
            switch (knobValueDesignator)
            {
                case KnobValueDesignator.None: break;
                case KnobValueDesignator.Centimeters: val += " cm"; break;
                case KnobValueDesignator.Centimeters2: val += " cm²"; break;
                case KnobValueDesignator.Centimeters3: val += " cm³"; break;
                case KnobValueDesignator.Degrees: val += " º"; break;
                case KnobValueDesignator.Millimeters: val += " mm"; break;
                case KnobValueDesignator.Millimeters2: val += " mm²"; break;
                case KnobValueDesignator.Millimeters3: val += " mm³"; break;
                case KnobValueDesignator.Meters: val += " m²"; break;
                case KnobValueDesignator.Meters2: val += " m"; break;
                case KnobValueDesignator.Meters3: val += " m³"; break;
                case KnobValueDesignator.ParticlesPerMolecule: val += " ppm"; break;
                case KnobValueDesignator.Percentage: val += " %"; break;
            }

            return val;
        }
        #endregion
    }
}
