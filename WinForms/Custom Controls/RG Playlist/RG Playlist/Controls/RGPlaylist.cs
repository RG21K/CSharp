// <copyright file="RGPlaylist.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_Playlist.Controls
{
    public class RGPlaylist : ListBox
    {
        // Last Edited: 2022, 10.10

        #region <Developer Notes>
        /*
         * // Help from Jimi (Stackoverflow)
         * 
         * 
         * 
         */
        #endregion


        #region <Constructor>
        public RGPlaylist()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            DrawMode = DrawMode.OwnerDrawVariable;

            IntegralHeight = false;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.FromArgb(255, 25, 25, 25);
            ForeColor = Color.Gainsboro;
        }
        #endregion


        #region <Fields>
        private const int LB_RESETCONTENT = 0x0184;
        private const int LB_DELETESTRING = 0x0182;

        private TextFormatFlags flags = TextFormatFlags.PreserveGraphicsClipping | TextFormatFlags.LeftAndRightPadding | TextFormatFlags.VerticalCenter;
        #endregion


        #region <Auto Properties>
        public bool IsSelected { get; private set; } = false;
        #endregion


        #region <Custom Properties> : (Behaviour)
        /// <summary> Enables to Auto Select the Control when the Mouse is Over the Control </summary>
        private bool autoSelectEnabled = true;
        [Category("1. Custom Properties"), DisplayName("Auto Select")]
        [Description("Enables to Auto Select the Control when the Mouse is Over the Control")]
        [Bindable(true)] /* Required for Enum Types */
        [Browsable(true)]
        public bool AutoSelectEnabled
        {
            get { return autoSelectEnabled; }
            set { autoSelectEnabled = value; }
        }
        #endregion

        #region <Custom Properties>
        private bool showVScroll = false;
        [Category("1. Custom Properties"), DisplayName("Show V ScrollBar")]
        [Description("Toggles Vertical ScrollBar Visibility")]
        [Browsable(true)]
        public bool ShowVScrollbar
        {
            get { return showVScroll; }
            set
            {
                if (value != showVScroll)
                {
                    showVScroll = value;
                    if (IsHandleCreated)
                        RecreateHandle();
                }
            }
        }

        // Tip: Always Verify that the new Values are Different from the Old Ones (Prevents Waste of Resources and Repeating Code when it is NOT Needed)

        private int markedIndex = -1;
        [Category("1. Custom Properties"), DisplayName("Marked Index")]
        [Description("Gets or Sets the Playlist Marked Item Index")]
        [Browsable(true)]
        public int MarkedIndex
        {
            get { return markedIndex; }
            set
            {
                if (value != markedIndex)
                {
                    markedIndex = value;

                    // Raise the MarkedItemChanged Event
                    OnMarkedItemChanged?.Invoke(markedIndex);

                    Invalidate();
                }
            }
        }

        // Read-Only: Just Return the Marked Item; (To Set: use 'MarkedIndex' Property).
        public object MarkedItem
        {
            get { return Items[markedIndex]; }
        }


        private Color markedItemForeColor = Color.Khaki;
        [Category("1. Custom Properties"), DisplayName("Marked Item ForeColor")]
        [Description("Gets or Sets Marked Item ForeColor")]
        [Browsable(true)]
        public Color MarkedItemForeColor
        {
            get { return markedItemForeColor; }
            set
            {
                if (value != markedItemForeColor)
                {
                    markedItemForeColor = value;
                    Invalidate();
                }
            }
        }


        private Color markedItemBackColor = Color.FromArgb(255, 25, 25, 25);
        [Category("1. Custom Properties"), DisplayName("Marked Item BackColor")]
        [Description("Gets or Sets Marked Item BackColor")]
        [Browsable(true)]
        public Color MarkedItemBackColor
        {
            get { return markedItemBackColor; }
            set
            {
                if (value != markedItemBackColor)
                {
                    markedItemBackColor = value;
                    Invalidate();
                }
            }
        }


        private Color selectionBackColor = Color.DimGray;
        [Category("1. Custom Properties"), DisplayName("Selection BackColor")]
        [Description("Gets or Sets ListBox Selection Rectangle BackColor")]
        [Browsable(true)]
        public Color SelectionBackColor
        {
            get { return selectionBackColor; }
            set
            {
                if (value != selectionBackColor)
                {
                    selectionBackColor = value;
                    Invalidate();
                }
            }
        }


        private Color selectionForeColor = Color.White;
        [Category("1. Custom Properties"), DisplayName("Selection ForeColor")]
        [Description("Gets or Sets ListBox Selection Rectangle ForeColor")]
        [Browsable(true)]
        public Color SelectionForeColor
        {
            get { return selectionForeColor; }
            set
            {
                if (value != selectionForeColor)
                {
                    selectionForeColor = value;
                    Invalidate();
                }
            }
        }
        #endregion


        #region <Custom Events>
        public delegate void MarkedItemChanged(int index);
        public event MarkedItemChanged OnMarkedItemChanged;
        #endregion



        #region <Overriden Events> : (Drawing)
        /// <summary> Draws the Selected Item. </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);

            /* Remarks:
             * 
             * Is Corrected to handle both: 
             * - Custom Selection Colors
             * - Marked Item's Colors
             * 
             * Note:
             * This Method is Called once per Item (so you don't have to loop the entire collection each time).
             * Just Paint the Current Item with Correct Colors.
             * 
             */

            // No Action when ListBox Contains no Items.
            if (Items.Count == 0) return;

            // Draw Selected
            if (e.State.HasFlag(DrawItemState.Focus) || e.State.HasFlag(DrawItemState.Selected))
            {
                // -> Draw Selected Item Background
                using (var brush = new SolidBrush(selectionBackColor))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                // -> Draw Selected Item Text 
                // Set the Color for Selected Item when it is the Same as Marked Index.
                var foreColor = markedIndex == e.Index ? markedItemForeColor : ForeColor;

                // Note: Use TextRenderer to Draw the Text Items (No Anti-Aliasing Required).
                //       Replaces Graphics.DrawString()
                //       This will give a more Natural aspect to the Rendered List of Items. (No Need to use Anti-Aliasing).
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, e.Bounds, foreColor, flags);

                // Default
                // Note: Use TextRenderer to Draw the Items (No Anti-Aliasing Required).
                //       Replaces Graphics.DrawString()
                //       This will give a more Natural aspect to the Rendered List of Items. (No Need to use Anti-Aliasing).
                // TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, e.Bounds, selectionForeColor, flags);
            }

            // Draw Unselected
            else
            {
                var color = markedIndex != -1 && markedIndex == e.Index ? markedItemBackColor : BackColor;
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(brush, e.Bounds);
                }

                var foreColor = markedIndex == e.Index ? markedItemForeColor : ForeColor;

                // Note: Use TextRenderer to Draw the Items (No Anti-Aliasing Required).
                //       Replaces Graphics.DrawString()
                //       This will give a more Natural aspect to the Rendered List of Items. (No Need to use Anti-Aliasing).
                TextRenderer.DrawText(e.Graphics, GetItemText(Items[e.Index]), Font, e.Bounds, foreColor, flags);
            }

            e.DrawFocusRectangle();
        }

        /// <summary> Measure ListBox Selected Item (Index). </summary>
        /// <param name="e"></param>
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            // Remarks:
            // - Set the Height of the Item (The Width: ONLY if Needed).
            // - This is the Standard Value (Modify as Required)
            if (Items.Count > 0) { e.ItemHeight = Font.Height + 4; }

            base.OnMeasureItem(e);
        }

        /// <summary> Selects (Marks) the Selected Item. </summary>
        /// <param name="e"></param>
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { SetMarkedItem(); }

            base.OnMouseDoubleClick(e);
        }
        #endregion

        #region <Overriden Events> : (Key Presses)
        /// <summary> Occurs when a Key is Pressed. </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.KeyChar == (char)Keys.Enter) { SetMarkedItem(); }
        }
        #endregion

        #region <Overriden Events> : (Mouse)

        /* WARNING
         * 
         * - Careful with Vertical Scrolling on: MouseHove, MouseLeave and MouseWheel
         * 
         */

        /// <summary> Occurs when the Mouse is Over the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);

            Capture = true;
        }

        /// <summary> Occurs when the Mouse Leaves the Control. </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            Capture = false;
        }

        /// <summary> Occurs when the Mouse Scroll Wheel Rotates. </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            int v = e.Delta / 120; // Compute a Mouse Wheel Tick

            switch (MouseWheelDetentIsPositive(v))
            {
                case true:
                    if (SelectedIndex >= 0)
                        SelectedIndex--;
                    break;

                case false:
                    if (SelectedIndex < (Items.Count - 1))
                        SelectedIndex++;
                    break;
            }
        }

        /// <summary> Raises the <see cref="Control.MouseMove"></see> event. </summary>
        /// <param name="e">A <see cref="Control.MouseEventArgs"></see> that contains the event data.</param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // -> Select the Control and set its Selected State
            SelectControl();
        }
        #endregion



        #region <Methods> : (Behaviour)
        /// <summary> Selects the Control and Sets it as Selected. </summary>
        private void SelectControl()
        {
            // MouseMove += Panel_MouseMove;
            var loc = MousePosition;

            if (ClientRectangle.Contains(loc) && !IsSelected)
            {
                Select();
                IsSelected = true;
                //knbVolume.Focus();
            }

            else { IsSelected = false; }
        }
        #endregion

        #region <Methods> : (Marked Item)
        /// <summary> Used to Hide Vertical ScrollBar. </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!showVScroll)
                    cp.Style = cp.Style & ~0x200000;
                return cp;
            }
        }

        /// <summary>
        /// WndProc is Overridden in order to Intercept the LB_RESETCONTENT(sent when the ObjectCollection is cleared);<br/> 
        /// and teh LB_DELETESTRING(sent when an Item is removed).
        /// This is done to reset the marked Item when the list is cleared or the marked Item is removed (otherwise an Item will remain marked when it shouldn't).
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                // List cleared
                case LB_RESETCONTENT:
                    markedIndex = -1;
                    break;

                // Item deleted
                case LB_DELETESTRING:
                    if (markedIndex == m.WParam.ToInt32())
                    {
                        markedIndex = -1;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        // Toggle the State of a Marked Item, (in case you Double-Click it Twice).
        private void SetMarkedItem() => MarkedIndex = markedIndex == SelectedIndex ? -1 : SelectedIndex;
        #endregion

        #region <Methods> : (Data Validation)
        /// <summary> Determines if the Mouse Wheel Detend (Rotation Tick) Value is Either Positive or Negative. </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private bool MouseWheelDetentIsPositive(int val)
        {
            return val > 0;
        }
        #endregion
    }
}
