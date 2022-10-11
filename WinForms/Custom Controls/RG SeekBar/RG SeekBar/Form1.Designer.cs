// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_SeekBar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rgSeekBar1 = new RG_SeekBar.Controls.RGSeekBar();
            this.lbltVolume = new System.Windows.Forms.Label();
            this.lblVolume = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgSeekBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblVolume);
            this.panel1.Controls.Add(this.lbltVolume);
            this.panel1.Controls.Add(this.rgSeekBar1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 0;
            // 
            // rgSeekBar1
            // 
            this.rgSeekBar1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(52)))));
            this.rgSeekBar1.BackgroundThickness = 8;
            this.rgSeekBar1.BarMaximum = ((long)(100));
            this.rgSeekBar1.BarMinimum = ((long)(0));
            this.rgSeekBar1.BarSliderShape = RG_SeekBar.Controls.RGSeekBar.SliderShape.Rectangle;
            this.rgSeekBar1.BarValue = ((long)(50));
            this.rgSeekBar1.BorderColor = System.Drawing.Color.Black;
            this.rgSeekBar1.ForegroundColor = System.Drawing.Color.Red;
            this.rgSeekBar1.ForegroundThickness = 4;
            this.rgSeekBar1.HorizontalPadding = 10;
            this.rgSeekBar1.Location = new System.Drawing.Point(12, 66);
            this.rgSeekBar1.Name = "rgSeekBar1";
            this.rgSeekBar1.NumberMouseWheelTicks = 10;
            this.rgSeekBar1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.rgSeekBar1.Size = new System.Drawing.Size(176, 25);
            this.rgSeekBar1.SliderBackgroundColor = System.Drawing.Color.Silver;
            this.rgSeekBar1.SliderBorderColor = System.Drawing.Color.Silver;
            this.rgSeekBar1.SliderBorderVisible = true;
            this.rgSeekBar1.SliderBorderWidth = 1;
            this.rgSeekBar1.SliderImage = null;
            this.rgSeekBar1.SliderVisible = true;
            this.rgSeekBar1.SliderWSize = new System.Drawing.Size(10, 6);
            this.rgSeekBar1.TabIndex = 0;
            this.rgSeekBar1.TabStop = false;
            this.rgSeekBar1.UseBackgroundBorder = true;
            // 
            // lbltVolume
            // 
            this.lbltVolume.AutoSize = true;
            this.lbltVolume.Location = new System.Drawing.Point(12, 94);
            this.lbltVolume.Name = "lbltVolume";
            this.lbltVolume.Size = new System.Drawing.Size(25, 13);
            this.lbltVolume.TabIndex = 1;
            this.lbltVolume.Text = "Vol:";
            // 
            // lblVolume
            // 
            this.lblVolume.AutoSize = true;
            this.lblVolume.Location = new System.Drawing.Point(43, 94);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(13, 13);
            this.lblVolume.TabIndex = 2;
            this.lblVolume.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rgSeekBar1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.RGSeekBar rgSeekBar1;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lbltVolume;
    }
}

