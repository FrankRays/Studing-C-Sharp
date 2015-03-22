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
    public partial class FindWindow : Form
    {
        RichTextBox richTextBox;
        int finds;
        public FindWindow(RichTextBox rtb)
        {
            InitializeComponent();
            richTextBox = rtb;
        }

        private void Close(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string word = textBox1.Text;
            if (word != "")
            {
                int a = richTextBox.Find(word);
                if (a == -1)
                {
                    MessageBox.Show("nothing found");
                    finds = 0;
                    return;
                }
                richTextBox.Select(a, word.Length);
                finds = a + word.Length;
            }
            richTextBox.Parent.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string word = textBox1.Text;
            if (word != "")
            {
                int a = richTextBox.Find(word, finds, new RichTextBoxFinds());
                if (a == -1)
                {
                    MessageBox.Show("nothing found");
                    finds = 0;
                    return;
                }
                richTextBox.Select(a, word.Length);
                finds = a + word.Length;
            }
            richTextBox.Parent.Focus();
        }
    }
}
