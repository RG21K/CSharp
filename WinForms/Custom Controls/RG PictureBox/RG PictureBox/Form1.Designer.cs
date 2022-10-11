// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>
namespace RG_PictureBox
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
            this.rgPictureBox1 = new RG_PictureBox.Controls.RGPictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.rgPictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // rgPictureBox1
            // 
            this.rgPictureBox1.BorderCapStyle = System.Drawing.Drawing2D.DashCap.Flat;
            this.rgPictureBox1.BorderColor = System.Drawing.Color.White;
            this.rgPictureBox1.BorderColor2 = System.Drawing.Color.DeepSkyBlue;
            this.rgPictureBox1.BorderLineStyle = System.Drawing.Drawing2D.DashStyle.Solid;
            this.rgPictureBox1.BorderSize = 2;
            this.rgPictureBox1.GradientAngle = 50F;
            this.rgPictureBox1.Location = new System.Drawing.Point(25, 24);
            this.rgPictureBox1.Name = "rgPictureBox1";
            this.rgPictureBox1.Size = new System.Drawing.Size(150, 150);
            this.rgPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.rgPictureBox1.TabIndex = 0;
            this.rgPictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.rgPictureBox1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.rgPictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RGPictureBox rgPictureBox1;
    }
}

