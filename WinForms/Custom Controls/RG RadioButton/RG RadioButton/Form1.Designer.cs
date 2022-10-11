// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_RadioButton
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
            this.rgRadioButton1 = new RG_RadioButton.Controls.RGRadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rgRadioButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 200);
            this.panel1.TabIndex = 0;
            // 
            // rgRadioButton1
            // 
            this.rgRadioButton1.AutoSize = true;
            this.rgRadioButton1.CheckedColor = System.Drawing.Color.Red;
            this.rgRadioButton1.ForeColor = System.Drawing.Color.White;
            this.rgRadioButton1.Location = new System.Drawing.Point(38, 78);
            this.rgRadioButton1.MinimumSize = new System.Drawing.Size(30, 21);
            this.rgRadioButton1.Name = "rgRadioButton1";
            this.rgRadioButton1.Size = new System.Drawing.Size(99, 21);
            this.rgRadioButton1.TabIndex = 0;
            this.rgRadioButton1.TabStop = true;
            this.rgRadioButton1.Text = "rgRadioButton1";
            this.rgRadioButton1.TextColor = System.Drawing.Color.White;
            this.rgRadioButton1.UnCheckedColor = System.Drawing.Color.DimGray;
            this.rgRadioButton1.UseVisualStyleBackColor = true;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Controls.RGRadioButton rgRadioButton1;
    }
}

