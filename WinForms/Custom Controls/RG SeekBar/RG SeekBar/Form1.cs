// <copyright file="Form1.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_SeekBar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SeekBar_Init();
        }

        private void SeekBar_Init()
        {
            SeekBar_SubscribeEvents();
        }

        private void SeekBar_SubscribeEvents()
        { 
            rgSeekBar1.OnValueChanged += SeekBar_OnValueChanged; 
        }

        private void SeekBar_OnValueChanged(long value)
        {
            lblVolume.Text = $"{value}";
        }
    }
}
