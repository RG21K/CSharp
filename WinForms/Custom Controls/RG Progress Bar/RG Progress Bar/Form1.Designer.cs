// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_Progress_Bar
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
            this.btnStartStop = new System.Windows.Forms.Button();
            this.rgProgressBar1 = new RG_Progress_Bar.Controls.RGProgressBar();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rgProgressBar1);
            this.panel1.Controls.Add(this.btnStartStop);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 0;
            // 
            // btnStartStop
            // 
            this.btnStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartStop.Location = new System.Drawing.Point(12, 114);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(176, 23);
            this.btnStartStop.TabIndex = 1;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            // 
            // rgProgressBar1
            // 
            this.rgProgressBar1.BarBackColor = System.Drawing.Color.DimGray;
            this.rgProgressBar1.BarHeight = 6;
            this.rgProgressBar1.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgProgressBar1.LabelBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgProgressBar1.Location = new System.Drawing.Point(12, 49);
            this.rgProgressBar1.Name = "rgProgressBar1";
            this.rgProgressBar1.ShowMaximun = false;
            this.rgProgressBar1.Size = new System.Drawing.Size(176, 23);
            this.rgProgressBar1.SliderColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(224)))), ((int)(((byte)(127)))));
            this.rgProgressBar1.SliderHeight = 6;
            this.rgProgressBar1.SymbolAfter = "";
            this.rgProgressBar1.SymbolBefore = "";
            this.rgProgressBar1.TabIndex = 2;
            this.rgProgressBar1.TextLocation = RG_Progress_Bar.Controls.RGProgressBar.TextPosition.Sliding;
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
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnStartStop;
        private Controls.RGProgressBar rgProgressBar1;
    }
}

