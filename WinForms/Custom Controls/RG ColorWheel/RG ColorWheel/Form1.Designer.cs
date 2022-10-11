namespace RG_ColorWheel
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlColorPreview = new System.Windows.Forms.Panel();
            this.rgColorWheel1 = new RG_ColorWheel.Controls.RGColorWheel();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rgColorWheel1);
            this.panel2.Controls.Add(this.pnlColorPreview);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 200);
            this.panel2.TabIndex = 2;
            // 
            // pnlColorPreview
            // 
            this.pnlColorPreview.Location = new System.Drawing.Point(12, 168);
            this.pnlColorPreview.Name = "pnlColorPreview";
            this.pnlColorPreview.Size = new System.Drawing.Size(176, 20);
            this.pnlColorPreview.TabIndex = 3;
            // 
            // rgColorWheel1
            // 
            this.rgColorWheel1.ColorWheelBorderColor = System.Drawing.Color.Black;
            this.rgColorWheel1.ColorWheelBorderEnabled = true;
            this.rgColorWheel1.Location = new System.Drawing.Point(24, 12);
            this.rgColorWheel1.MaximumSize = new System.Drawing.Size(500, 500);
            this.rgColorWheel1.MinimumSize = new System.Drawing.Size(50, 50);
            this.rgColorWheel1.Name = "rgColorWheel1";
            this.rgColorWheel1.SelectionEllipseBorderColor = System.Drawing.Color.Black;
            this.rgColorWheel1.SelectionEllipseDiameter = 15;
            this.rgColorWheel1.SelectionEllipseEnabled = true;
            this.rgColorWheel1.SelectionEllipseThickness = 3;
            this.rgColorWheel1.Size = new System.Drawing.Size(150, 150);
            this.rgColorWheel1.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.panel2);
            this.ForeColor = System.Drawing.Color.Gainsboro;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlColorPreview;
        private Controls.RGColorWheel rgColorWheel1;
    }
}

