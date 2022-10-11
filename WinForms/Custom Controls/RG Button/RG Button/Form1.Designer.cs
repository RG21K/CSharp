// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_Button
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
            this.rgButton1 = new RG_Button.Controls.RGButton();
            this.SuspendLayout();
            // 
            // rgButton1
            // 
            this.rgButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.rgButton1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.rgButton1.BorderColor = System.Drawing.Color.DimGray;
            this.rgButton1.BorderRadius = 0;
            this.rgButton1.BorderSize = 1;
            this.rgButton1.CheckedColor = System.Drawing.Color.Red;
            this.rgButton1.CheckEnabled = true;
            this.rgButton1.FlatAppearance.BorderSize = 0;
            this.rgButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.rgButton1.ForeColor = System.Drawing.Color.White;
            this.rgButton1.HighlightColor = System.Drawing.Color.Red;
            this.rgButton1.HighlightEnabled = false;
            this.rgButton1.HighlightLineThickness = 1;
            this.rgButton1.HighlightLocation = RG_Button.Controls.RGButton.HighlightPosition.Bottom;
            this.rgButton1.IsChecked = false;
            this.rgButton1.Location = new System.Drawing.Point(12, 12);
            this.rgButton1.Name = "rgButton1";
            this.rgButton1.Size = new System.Drawing.Size(160, 30);
            this.rgButton1.TabIndex = 0;
            this.rgButton1.Text = "rgButton1";
            this.rgButton1.TextColor = System.Drawing.Color.White;
            this.rgButton1.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(184, 161);
            this.Controls.Add(this.rgButton1);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RGButton rgButton1;
    }
}

