using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chm_lab_1
{
    public partial class AddEquation : Form
    {
        static string eq = null;

        public AddEquation()
        {
            InitializeComponent();
            DialogResult = System.Windows.Forms.DialogResult.None;
            eq = null;
        }

        public string Expression()
        {
            this.ShowDialog();
            return eq;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            eq = textBox1.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void cancel(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult != System.Windows.Forms.DialogResult.OK)
            {
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
        }
    }
}
