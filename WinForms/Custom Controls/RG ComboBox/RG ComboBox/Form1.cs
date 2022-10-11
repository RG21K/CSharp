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

namespace RG_ComboBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ComboBox_Init();
        }

        private void ComboBox_Init()
        {
            ComboBox_GetData();
        }

        private void ComboBox_GetData()
        {
            for (int i = 0; i < 10; i++)
            {
                var item = $"{i}. Item";

                //rgComboBox1.Items.Add(item);
            }
        }
    }
}
