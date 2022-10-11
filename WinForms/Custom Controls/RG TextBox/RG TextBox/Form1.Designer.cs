// <copyright file="Form1.Designer.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

namespace RG_TextBox
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
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.rgTextBox1 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox2 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox3 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox4 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox5 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox6 = new RG_TextBox.Controls.RGTextBox();
            this.rgTextBox7 = new RG_TextBox.Controls.RGTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rgTextBox7);
            this.panel1.Controls.Add(this.rgTextBox6);
            this.panel1.Controls.Add(this.rgTextBox5);
            this.panel1.Controls.Add(this.rgTextBox4);
            this.panel1.Controls.Add(this.rgTextBox3);
            this.panel1.Controls.Add(this.rgTextBox2);
            this.panel1.Controls.Add(this.rgTextBox1);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 200);
            this.panel1.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(59, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Default (Placeholder)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 66);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Char Limiter (i.e: Max. 5 Chars)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(141, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Decimal Numbers (Currency)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 170);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "IPv4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(75, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Decimal Numbers";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Whole Numbers";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(124, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Default";
            // 
            // rgTextBox1
            // 
            this.rgTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox1.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox1.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox1.CurrencyDesignator = "€";
            this.rgTextBox1.DecimalPlaces = 2;
            this.rgTextBox1.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox1.IsCurrency = false;
            this.rgTextBox1.LimitedChars = false;
            this.rgTextBox1.Location = new System.Drawing.Point(171, 12);
            this.rgTextBox1.MaximumChars = 5;
            this.rgTextBox1.Name = "rgTextBox1";
            this.rgTextBox1.PlaceholderEnabled = false;
            this.rgTextBox1.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox1.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox1.PlaceholderText = "Enter Text";
            this.rgTextBox1.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox1.TabIndex = 13;
            this.rgTextBox1.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.Default_ASCII;
            // 
            // rgTextBox2
            // 
            this.rgTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox2.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox2.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox2.CurrencyDesignator = "€";
            this.rgTextBox2.DecimalPlaces = 2;
            this.rgTextBox2.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox2.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox2.ForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox2.IsCurrency = false;
            this.rgTextBox2.LimitedChars = false;
            this.rgTextBox2.Location = new System.Drawing.Point(171, 38);
            this.rgTextBox2.MaximumChars = 5;
            this.rgTextBox2.Name = "rgTextBox2";
            this.rgTextBox2.PlaceholderEnabled = true;
            this.rgTextBox2.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox2.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox2.PlaceholderText = "Enter Text";
            this.rgTextBox2.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox2.TabIndex = 14;
            this.rgTextBox2.Text = "Enter Text";
            this.rgTextBox2.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.Default_ASCII;
            // 
            // rgTextBox3
            // 
            this.rgTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox3.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox3.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox3.CurrencyDesignator = "€";
            this.rgTextBox3.DecimalPlaces = 2;
            this.rgTextBox3.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox3.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox3.IsCurrency = false;
            this.rgTextBox3.LimitedChars = true;
            this.rgTextBox3.Location = new System.Drawing.Point(171, 64);
            this.rgTextBox3.MaximumChars = 5;
            this.rgTextBox3.Name = "rgTextBox3";
            this.rgTextBox3.PlaceholderEnabled = false;
            this.rgTextBox3.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox3.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox3.PlaceholderText = "Enter Text";
            this.rgTextBox3.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox3.TabIndex = 15;
            this.rgTextBox3.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.Default_ASCII;
            // 
            // rgTextBox4
            // 
            this.rgTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox4.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox4.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox4.CurrencyDesignator = "€";
            this.rgTextBox4.DecimalPlaces = 2;
            this.rgTextBox4.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox4.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox4.IsCurrency = false;
            this.rgTextBox4.LimitedChars = false;
            this.rgTextBox4.Location = new System.Drawing.Point(171, 90);
            this.rgTextBox4.MaximumChars = 5;
            this.rgTextBox4.Name = "rgTextBox4";
            this.rgTextBox4.PlaceholderEnabled = false;
            this.rgTextBox4.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox4.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox4.PlaceholderText = "Enter Text";
            this.rgTextBox4.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox4.TabIndex = 16;
            this.rgTextBox4.Text = "0";
            this.rgTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rgTextBox4.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.WholeNumbers;
            // 
            // rgTextBox5
            // 
            this.rgTextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox5.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox5.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox5.CurrencyDesignator = "€";
            this.rgTextBox5.DecimalPlaces = 2;
            this.rgTextBox5.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox5.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox5.IsCurrency = false;
            this.rgTextBox5.LimitedChars = false;
            this.rgTextBox5.Location = new System.Drawing.Point(171, 116);
            this.rgTextBox5.MaximumChars = 5;
            this.rgTextBox5.Name = "rgTextBox5";
            this.rgTextBox5.PlaceholderEnabled = false;
            this.rgTextBox5.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox5.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox5.PlaceholderText = "Enter Text";
            this.rgTextBox5.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox5.TabIndex = 17;
            this.rgTextBox5.Text = "0.00";
            this.rgTextBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rgTextBox5.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.DecimalNumbers;
            // 
            // rgTextBox6
            // 
            this.rgTextBox6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox6.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox6.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox6.CurrencyDesignator = "€";
            this.rgTextBox6.DecimalPlaces = 2;
            this.rgTextBox6.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox6.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox6.IsCurrency = true;
            this.rgTextBox6.LimitedChars = false;
            this.rgTextBox6.Location = new System.Drawing.Point(171, 142);
            this.rgTextBox6.MaximumChars = 5;
            this.rgTextBox6.Name = "rgTextBox6";
            this.rgTextBox6.PlaceholderEnabled = false;
            this.rgTextBox6.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox6.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox6.PlaceholderText = "Enter Text";
            this.rgTextBox6.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox6.TabIndex = 18;
            this.rgTextBox6.Text = "0 €";
            this.rgTextBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.rgTextBox6.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.DecimalNumbers;
            // 
            // rgTextBox7
            // 
            this.rgTextBox7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.rgTextBox7.BaseFont = new System.Drawing.Font("Arial", 8F);
            this.rgTextBox7.BaseForeColor = System.Drawing.Color.White;
            this.rgTextBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rgTextBox7.CurrencyDesignator = "€";
            this.rgTextBox7.DecimalPlaces = 2;
            this.rgTextBox7.DesignatorLocation = RG_TextBox.Controls.RGTextBox.DesignatorAlignment.Right;
            this.rgTextBox7.ForeColor = System.Drawing.Color.Gainsboro;
            this.rgTextBox7.IsCurrency = false;
            this.rgTextBox7.LimitedChars = false;
            this.rgTextBox7.Location = new System.Drawing.Point(171, 168);
            this.rgTextBox7.MaximumChars = 5;
            this.rgTextBox7.Name = "rgTextBox7";
            this.rgTextBox7.PlaceholderEnabled = false;
            this.rgTextBox7.PlaceholderFont = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Italic);
            this.rgTextBox7.PlaceholderForeColor = System.Drawing.Color.DimGray;
            this.rgTextBox7.PlaceholderText = "Enter Text";
            this.rgTextBox7.Size = new System.Drawing.Size(91, 20);
            this.rgTextBox7.TabIndex = 19;
            this.rgTextBox7.Text = "0.0.0.0";
            this.rgTextBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rgTextBox7.TextBoxType = RG_TextBox.Controls.RGTextBox.TextInputType.IPV4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(274, 200);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private Controls.RGTextBox rgTextBox7;
        private Controls.RGTextBox rgTextBox6;
        private Controls.RGTextBox rgTextBox5;
        private Controls.RGTextBox rgTextBox4;
        private Controls.RGTextBox rgTextBox3;
        private Controls.RGTextBox rgTextBox2;
        private Controls.RGTextBox rgTextBox1;
    }
}

