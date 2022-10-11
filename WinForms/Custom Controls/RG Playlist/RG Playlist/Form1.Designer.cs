// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_Playlist
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
            this.rgPlaylist1 = new RG_Playlist.Controls.RGPlaylist();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rgPlaylist1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 0;
            // 
            // rgPlaylist1
            // 
            this.rgPlaylist1.AutoSelectEnabled = true;
            this.rgPlaylist1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgPlaylist1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgPlaylist1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.rgPlaylist1.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgPlaylist1.FormattingEnabled = true;
            this.rgPlaylist1.IntegralHeight = false;
            this.rgPlaylist1.Location = new System.Drawing.Point(12, 12);
            this.rgPlaylist1.MarkedIndex = -1;
            this.rgPlaylist1.MarkedItemBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgPlaylist1.MarkedItemForeColor = System.Drawing.Color.Khaki;
            this.rgPlaylist1.Name = "rgPlaylist1";
            this.rgPlaylist1.SelectionBackColor = System.Drawing.Color.DimGray;
            this.rgPlaylist1.SelectionForeColor = System.Drawing.Color.White;
            this.rgPlaylist1.ShowVScrollbar = false;
            this.rgPlaylist1.Size = new System.Drawing.Size(176, 176);
            this.rgPlaylist1.TabIndex = 0;
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
        private Controls.RGPlaylist rgPlaylist1;
    }
}

