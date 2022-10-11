// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_TrackBar
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rgTrackBar1 = new RG_TrackBar.Controls.RGTrackBar();
            this.rgTrackBar2 = new RG_TrackBar.Controls.RGTrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.rgTrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTrackBar2)).BeginInit();
            this.SuspendLayout();
            // 
            // rgTrackBar1
            // 
            this.rgTrackBar1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(62)))));
            this.rgTrackBar1.BackgroundThickness = 6;
            this.rgTrackBar1.BarEdgePadding = 10;
            this.rgTrackBar1.BarLargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.rgTrackBar1.BarMaximum = ((long)(100));
            this.rgTrackBar1.BarMinimum = ((long)(0));
            this.rgTrackBar1.BarSliderShape = RG_TrackBar.Controls.RGTrackBar.SliderShape.Rectangle;
            this.rgTrackBar1.BarSmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rgTrackBar1.BarValue = ((long)(50));
            this.rgTrackBar1.BorderColor = System.Drawing.Color.Black;
            this.rgTrackBar1.ForegroundColor = System.Drawing.Color.Red;
            this.rgTrackBar1.ForegroundThickness = 3;
            this.rgTrackBar1.Location = new System.Drawing.Point(0, 0);
            this.rgTrackBar1.Name = "rgTrackBar1";
            this.rgTrackBar1.NumberMouseWheelTicks = 10;
            this.rgTrackBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.rgTrackBar1.ShowMaximum = false;
            this.rgTrackBar1.Size = new System.Drawing.Size(100, 50);
            this.rgTrackBar1.SliderBackgroundColor = System.Drawing.Color.Silver;
            this.rgTrackBar1.SliderBorderColor = System.Drawing.Color.White;
            this.rgTrackBar1.SliderBorderVisible = true;
            this.rgTrackBar1.SliderBorderWidth = 1;
            this.rgTrackBar1.SliderImage = null;
            this.rgTrackBar1.SliderVisible = true;
            this.rgTrackBar1.SliderWSize = new System.Drawing.Size(10, 6);
            this.rgTrackBar1.SymbolAfterValue = "";
            this.rgTrackBar1.SymbolBeforeValue = "";
            this.rgTrackBar1.TabIndex = 0;
            this.rgTrackBar1.TabStop = false;
            this.rgTrackBar1.ToolTipBackColor = System.Drawing.Color.DimGray;
            this.rgTrackBar1.ToolTipEnabled = true;
            this.rgTrackBar1.ToolTipFont = new System.Drawing.Font("Consolas", 8F);
            this.rgTrackBar1.ToolTipForeColor = System.Drawing.Color.White;
            this.rgTrackBar1.ToolTipText = "";
            this.rgTrackBar1.UseBackgroundBorder = true;
            // 
            // rgTrackBar2
            // 
            this.rgTrackBar2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(62)))));
            this.rgTrackBar2.BackgroundThickness = 6;
            this.rgTrackBar2.BarEdgePadding = 10;
            this.rgTrackBar2.BarLargeChange = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.rgTrackBar2.BarMaximum = ((long)(100));
            this.rgTrackBar2.BarMinimum = ((long)(0));
            this.rgTrackBar2.BarSliderShape = RG_TrackBar.Controls.RGTrackBar.SliderShape.Rectangle;
            this.rgTrackBar2.BarSmallChange = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.rgTrackBar2.BarValue = ((long)(50));
            this.rgTrackBar2.BorderColor = System.Drawing.Color.Black;
            this.rgTrackBar2.ForegroundColor = System.Drawing.Color.Red;
            this.rgTrackBar2.ForegroundThickness = 3;
            this.rgTrackBar2.Location = new System.Drawing.Point(34, 68);
            this.rgTrackBar2.Name = "rgTrackBar2";
            this.rgTrackBar2.NumberMouseWheelTicks = 10;
            this.rgTrackBar2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.rgTrackBar2.ShowMaximum = false;
            this.rgTrackBar2.Size = new System.Drawing.Size(100, 10);
            this.rgTrackBar2.SliderBackgroundColor = System.Drawing.Color.Silver;
            this.rgTrackBar2.SliderBorderColor = System.Drawing.Color.White;
            this.rgTrackBar2.SliderBorderVisible = true;
            this.rgTrackBar2.SliderBorderWidth = 1;
            this.rgTrackBar2.SliderImage = null;
            this.rgTrackBar2.SliderVisible = true;
            this.rgTrackBar2.SliderWSize = new System.Drawing.Size(10, 6);
            this.rgTrackBar2.SymbolAfterValue = "";
            this.rgTrackBar2.SymbolBeforeValue = "";
            this.rgTrackBar2.TabIndex = 0;
            this.rgTrackBar2.TabStop = false;
            this.rgTrackBar2.ToolTipBackColor = System.Drawing.Color.DimGray;
            this.rgTrackBar2.ToolTipEnabled = true;
            this.rgTrackBar2.ToolTipFont = new System.Drawing.Font("Consolas", 8F);
            this.rgTrackBar2.ToolTipForeColor = System.Drawing.Color.White;
            this.rgTrackBar2.ToolTipText = "";
            this.rgTrackBar2.UseBackgroundBorder = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.rgTrackBar2);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.rgTrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTrackBar2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RGTrackBar rgTrackBar1;
        private Controls.RGTrackBar rgTrackBar2;
    }
}

