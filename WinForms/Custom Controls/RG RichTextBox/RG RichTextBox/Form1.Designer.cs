// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_RichTextBox
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
            this.rgRichTextBox1 = new RG_RichTextBox.Controls.RGRichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rgRichTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 0;
            // 
            // rgRichTextBox1
            // 
            this.rgRichTextBox1.AutoScrollToCaret = true;
            this.rgRichTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(52)))));
            this.rgRichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rgRichTextBox1.ForeColor = System.Drawing.Color.White;
            this.rgRichTextBox1.InputSourceStampColor = System.Drawing.Color.DeepSkyBlue;
            this.rgRichTextBox1.Location = new System.Drawing.Point(12, 12);
            this.rgRichTextBox1.Name = "rgRichTextBox1";
            this.rgRichTextBox1.ReadOnly = true;
            this.rgRichTextBox1.Size = new System.Drawing.Size(176, 176);
            this.rgRichTextBox1.TabIndex = 0;
            this.rgRichTextBox1.Text = "";
            this.rgRichTextBox1.TimeStampColor = System.Drawing.Color.Gainsboro;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.RGRichTextBox rgRichTextBox1;
    }
}

