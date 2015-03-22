using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightChain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Button[,] map;

        int max_size = 11;

        private void Form1_Load(object sender, EventArgs e)
        {
            map = new Button[max_size, max_size];
            int s = 41;

            Point start = button1.Location;

            foreach (Control ctl in Controls)
            {
                if (ctl is Button)
                {
                    Point c = ctl.Location;
                    c.X -= start.X;
                    c.X /= s;

                    c.
                        -= start.X;
                    c.X /= s;
                }
            }
        }
    }
}
