// Default WinForms Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace EHR_Monitor_Test
{
    public partial class MainWindow : Form
    {
        private int ticks = 5;  // How many seconds do we want to allow a user to find the EHR Window before attempting to poll it

        public MainWindow()
        {
            InitializeComponent();

            timer1.Stop();
        }
    }
}
