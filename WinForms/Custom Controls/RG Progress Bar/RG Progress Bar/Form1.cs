// <copyright file="Form1.cs" company="None"> Copyright (c) Ricardo Garcia. </copyright>
// <author> Ricardo Garcia </author>
// <date> 2022, 10.10 </date>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RG_Progress_Bar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Timer_Init();
            Button_Init();
        }

        // Avoid Timer!
        // Timer in here Serves ONLY Example Purposes.
        // Use BackgroundWorker or an Alternative timer.

        private Timer tmr;

        private void Timer_Init()
        {
            if (tmr == null)
            {
                tmr = new Timer() { Interval = 50 };

                Timer_SubscribeEvents();
            }
        }

        private void Timer_SubscribeEvents()
        { 
            tmr.Tick += Timer_Tick;
            tmr.Disposed += Timer_Disposed;
        }

        private void Timer_Disposed(object sender, EventArgs e)
        {
            if (tmr != null) { tmr = null; }
        }

        private void Timer_Start()
        {
            if (tmr != null)
            {
                tmr.Enabled = true;
                tmr.Start();
                btnStartStop.Text = "Stop";
                rgProgressBar1.Value = 0;
            }
        }

        private void Timer_Stop()
        {
            if (tmr != null)
            {
                tmr.Stop();
                tmr.Dispose();
                btnStartStop.Text = "Start";
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (rgProgressBar1.Value < 100) { rgProgressBar1.Value++; }
            else { Timer_Stop(); }
        }


        private void Button_Init()
        {
            Button_SubscribeEvents();
        }

        private void Button_SubscribeEvents()
        {
            btnStartStop.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;

            switch (btn.Text)
            {
                case "Start": Timer_Start(); break;
                case "Stop": Timer_Stop(); break;
            }
        }
    }
}
