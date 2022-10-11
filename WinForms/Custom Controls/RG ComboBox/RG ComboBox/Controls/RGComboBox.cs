// <copyright file="RGComboBox.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// Color: 38, 224, 127

namespace RG_Custom_Controls.Controls
{
    // Last Edited: 2022, 09.12 (20h11)
    
    #region <Developer Notes>
    /* To Do:
     * 
     * - Change DropDown Box Border Color (See: WndProc)
     * - DropDown Item Selection Back Color (Not Working) (See: WndProc)
     * 
     * 
     * Remarks: This Control is a Combination of My Own ComboBox and RezaAghaei Flat ComboBox
     */
    #endregion

    public class RGComboBox : ComboBox
    {
        #region <Configuration>
        private void SetStyles()
        {
            // SetStyle(ControlStyles.UserPaint, true); // Cannot be used Here
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        #endregion


        #region <Constructor>
        public RGComboBox()
        {
            SetStyles();

            BackColor = Color.FromArgb(255, 25, 25, 25);
            ForeColor = Color.Gainsboro;
        }
        #endregion


        #region <Custom Properties> : (Button & Arrow)
        private Color arrowColor = Color.Black;
        [Category("1. Custom Properties"), DisplayName("Arrow Color")]
        [Description("Set Arrow Color.")]
        [Browsable(true)]
        public Color ArrowColor
        {
            get { return arrowColor; }
            set
            {
                arrowColor = value;
                Invalidate();
            }
        }

        private Color buttonColor = Color.LightGray;
        // [DefaultValue(typeof(Color), "LightGray")]
        [Category("1. Custom Properties"), DisplayName("Button Color")]
        [Description("Set Button Color.")]
        [Browsable(true)]
        public Color ButtonColor
        {
            get { return buttonColor; }
            set
            {
                if (buttonColor != value)
                {
                    buttonColor = value;
                    Invalidate();
                }
            }
        }
        #endregion

        #region [!] <Custom Properties> : (Drop Down)
        private Color dropDownItemSelectionBackColor = Color.FromArgb(255, 38, 224, 127);
        [Category("1. Custom Properties"), DisplayName("DropDown Selection BackColor")]
        [Description("Set DropDown Selection Back Color.")]
        [Browsable(true)]
        public Color DropDownItemSelectionBackColor
        {
            get { return dropDownItemSelectionBackColor; }
            set
            {
                dropDownItemSelectionBackColor = value;
                Invalidate();
            }
        }

        private Color dropDownPanelBackColor = Color.FromArgb(255, 25, 25, 25);
        [Category("1. Custom Properties"), DisplayName("DropDown Panel BackColor")]
        [Description("Set DropDown Panel BackColor.")]
        [Browsable(true)]
        public Color DropDownPanelBackColor
        {
            get { return dropDownPanelBackColor; }
            set
            {
                dropDownPanelBackColor = value;
                Invalidate();
            }
        }

        private Color borderColor = Color.DimGray;  // Affects the Control Border and Arrow in Button
                                                    
        [Category("1. Custom Properties"), DisplayName("Border Color")]
        [Description("Set Border Color.")]
        [Browsable(true)]
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (borderColor != value)
                {
                    borderColor = value;
                    Invalidate();
                }
            }
        }
        #endregion


        #region <Overriden Events>
        /// <summary> Occurs when a ComboBox Item is Drawn. </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            DrawItemSelection(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            //e.Graphics.DrawString(Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), new Point(e.Bounds.X, e.Bounds.Y));

            Invalidate();

            base.OnDrawItem(e);
        }

        /// <summary> Occurs when Item Selection is Drawn. </summary>
        /// <param name="e"></param>
        private void DrawItemSelection(DrawItemEventArgs e)
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality; // TESTING

            using (var _itemSelectionBrush = new SolidBrush(dropDownItemSelectionBackColor))
            using (var _boxBackColorBrush = new SolidBrush(dropDownPanelBackColor))
            {
                // Draw Item Selection
                if ((e.State & DrawItemState.Selected).Equals(DrawItemState.Selected))
                {
                    e.Graphics.FillRectangle(_itemSelectionBrush, e.Bounds);
                }

                // DropDown Back Color (& Remaining Items?)
                else
                {
                    e.Graphics.FillRectangle(_boxBackColorBrush, e.Bounds);
                }
            }
        }
        #endregion


        #region <WndProc>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PAINT && DropDownStyle != ComboBoxStyle.Simple)
            {
                var clientRect = ClientRectangle;
                var dropDownButtonWidth = SystemInformation.HorizontalScrollBarArrowWidth;
                var outerBorder = new Rectangle(clientRect.Location,
                    new Size(clientRect.Width - 1, clientRect.Height - 1));
                var innerBorder = new Rectangle(outerBorder.X + 1, outerBorder.Y + 1,
                    outerBorder.Width - dropDownButtonWidth - 2, outerBorder.Height - 2);
                var innerInnerBorder = new Rectangle(innerBorder.X + 1, innerBorder.Y + 1,
                    innerBorder.Width - 2, innerBorder.Height - 2);
                var dropDownRect = new Rectangle(innerBorder.Right + 1, innerBorder.Y,
                    dropDownButtonWidth, innerBorder.Height + 1);

                if (RightToLeft == RightToLeft.Yes)
                {
                    innerBorder.X = clientRect.Width - innerBorder.Right;
                    innerInnerBorder.X = clientRect.Width - innerInnerBorder.Right;
                    dropDownRect.X = clientRect.Width - dropDownRect.Right;
                    dropDownRect.Width += 1;
                }

                // Se Está Activo -> O QUE? -> BackColor -> QUAL COLOR?
                var innerBorderColor = Enabled ? BackColor : SystemColors.Control;
                var outerBorderColor = Enabled ? BorderColor : SystemColors.ControlDark;
                var buttonColor = Enabled ? ButtonColor : SystemColors.Control;
                var middle = new Point(dropDownRect.Left + dropDownRect.Width / 2, dropDownRect.Top + dropDownRect.Height / 2);
                
                var arrow = new Point[]
                {
                new Point(middle.X - 3, middle.Y - 2),
                new Point(middle.X + 4, middle.Y - 2),
                new Point(middle.X, middle.Y + 2)
                };

                var ps = new PAINTSTRUCT();
                bool shoulEndPaint = false;
                IntPtr dc;

                if (m.WParam == IntPtr.Zero)
                {
                    dc = BeginPaint(Handle, ref ps);
                    m.WParam = dc;
                    shoulEndPaint = true;
                }

                else { dc = m.WParam; }

                var rgn = CreateRectRgn(innerInnerBorder.Left, innerInnerBorder.Top, innerInnerBorder.Right, innerInnerBorder.Bottom);

                SelectClipRgn(dc, rgn);
                DefWndProc(ref m);
                DeleteObject(rgn);

                rgn = CreateRectRgn(clientRect.Left, clientRect.Top, clientRect.Right, clientRect.Bottom);
                SelectClipRgn(dc, rgn);

                /* Colors */
                using (var g = Graphics.FromHdc(dc))
                {
                    // -> Button
                    using (var b = new SolidBrush(buttonColor))
                    {
                        g.FillRectangle(b, dropDownRect);
                    }

                    // -> Arrow
                    using (var b = new SolidBrush(arrowColor))
                    {
                        g.FillPolygon(b, arrow);
                    }

                    // -> Text Rectangle Border (Leave as Default)
                    using (var p = new Pen(innerBorderColor))
                    {
                        g.DrawRectangle(p, innerBorder);
                        g.DrawRectangle(p, innerInnerBorder);
                    }

                    // -> Control Border
                    //using (var p = new Pen(outerBorderColor))
                    using (var p = new Pen(borderColor))
                    {
                        g.DrawRectangle(p, outerBorder);
                    }

                }

                if (shoulEndPaint)
                    EndPaint(Handle, ref ps);
                DeleteObject(rgn);
            }

            else
                base.WndProc(ref m);
        }

        private const int WM_PAINT = 0xF;
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int L, T, R, B;
        }
        [StructLayout(LayoutKind.Sequential)]
        public struct PAINTSTRUCT
        {
            public IntPtr hdc;
            public bool fErase;
            public int rcPaint_left;
            public int rcPaint_top;
            public int rcPaint_right;
            public int rcPaint_bottom;
            public bool fRestore;
            public bool fIncUpdate;
            public int reserved1;
            public int reserved2;
            public int reserved3;
            public int reserved4;
            public int reserved5;
            public int reserved6;
            public int reserved7;
            public int reserved8;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr BeginPaint(IntPtr hWnd,
            [In, Out] ref PAINTSTRUCT lpPaint);

        [DllImport("user32.dll")]
        private static extern bool EndPaint(IntPtr hWnd, ref PAINTSTRUCT lpPaint);

        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hDC, IntPtr hRgn);

        [DllImport("user32.dll")]
        public static extern int GetUpdateRgn(IntPtr hwnd, IntPtr hrgn, bool fErase);
        public enum RegionFlags
        {
            ERROR = 0,
            NULLREGION = 1,
            SIMPLEREGION = 2,
            COMPLEXREGION = 3,
        }

        [DllImport("gdi32.dll")]
        internal static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateRectRgn(int x1, int y1, int x2, int y2);
        #endregion
    }
}
