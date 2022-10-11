// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_Knob
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.rgKnob1 = new RG_Knob.Controls.RGKnob();
            this.SuspendLayout();
            // 
            // rgKnob1
            // 
            this.rgKnob1.BarBackgroundColor = System.Drawing.Color.Gray;
            this.rgKnob1.BarBackgroundThickness = 8;
            this.rgKnob1.BarForegroundColor = System.Drawing.Color.DeepSkyBlue;
            this.rgKnob1.BarForegroundThickness = 8;
            this.rgKnob1.BarPadding = 3;
            this.rgKnob1.BarVisible = true;
            this.rgKnob1.InfoDisplayMode = RG_Knob.Controls.RGKnob.InfoMode.LabelValue;
            this.rgKnob1.KnobBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.rgKnob1.KnobCapBorderColor = System.Drawing.Color.Silver;
            this.rgKnob1.KnobCapBorderThickness = 1.5F;
            this.rgKnob1.KnobCapBorderVisible = false;
            this.rgKnob1.KnobCapImage = ((System.Drawing.Image)(resources.GetObject("rgKnob1.KnobCapImage")));
            this.rgKnob1.KnobIconSize = 20;
            this.rgKnob1.KnobImage = null;
            this.rgKnob1.KnobMaximum = 1F;
            this.rgKnob1.KnobMinimum = 0F;
            this.rgKnob1.KnobPointerImage = null;
            this.rgKnob1.KnobSize = new System.Drawing.Size(80, 80);
            this.rgKnob1.KnobValue = 0.5F;
            this.rgKnob1.LabelText = "VOL";
            this.rgKnob1.LabelTitle = "Label Title";
            this.rgKnob1.LabelValueFont = new System.Drawing.Font("Consolas", 7.6F);
            this.rgKnob1.LabelVerticaPadding = 20;
            this.rgKnob1.Location = new System.Drawing.Point(12, 12);
            this.rgKnob1.Name = "rgKnob1";
            this.rgKnob1.PointerBorderColor = System.Drawing.Color.Black;
            this.rgKnob1.PointerBorderVisible = false;
            this.rgKnob1.PointerColor = System.Drawing.Color.Crimson;
            this.rgKnob1.PointerPadding = 10;
            this.rgKnob1.PointerType = RG_Knob.Controls.RGKnob.KnobPointerType.Line;
            this.rgKnob1.Size = new System.Drawing.Size(176, 176);
            this.rgKnob1.TabIndex = 0;
            this.rgKnob1.ValueDesignator = RG_Knob.Controls.RGKnob.KnobValueDesignator.Percentage;
            this.rgKnob1.ValueFormat = RG_Knob.Controls.RGKnob.KnobValueFormat.WholeNumber;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.rgKnob1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RGKnob rgKnob1;
    }
}

