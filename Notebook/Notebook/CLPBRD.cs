using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Notebook
{
    public partial class CLPBRD : Form
    {
        public CLPBRD()
        {
            InitializeComponent();
        }

        private void Change(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
                Clipboard.SetDataObject(listBox1.SelectedItem.ToString());
        }

        private void Closing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != 0)
            {
                Clipboard.SetDataObject("");
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                
            }
        }
    }
}
