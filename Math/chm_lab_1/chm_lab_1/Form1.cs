using System;
using System.Windows.Forms;

namespace chm_lab_1
{
    public partial class Form1 : Form
    {
        double[] koef = new double[] { 2, 48, -67, -722, -141, 988, -288, -14 };
        
        public Form1()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;

            Random r = new Random();
            Log.WriteLn(r.Next(1, 28).ToString());  
            Log.WriteLn(r.Next(1, 28).ToString());
            Log.WriteLn(r.Next(1, 28).ToString());
            Log.WriteLn(r.Next(1, 28).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            new method_lobachevskogo(listBox1.Items[listBox1.SelectedIndex].ToString()).ShowDialog();
        }

        private void CheckIfAddNew(object sender, EventArgs e)
        {
            if (listBox1.Items[listBox1.SelectedIndex].Equals("<.. Добавить уровнение ..>"))
            {
                AddEquation add = new AddEquation();
                string str = add.Expression();
                if (str != null)
                {
                    listBox1.Items.Insert(listBox1.Items.Count - 1, str);
                    listBox1.SelectedIndex = 0;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            new method_double_division(listBox1.Items[listBox1.SelectedIndex].ToString()).ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            new method_simple_iter(listBox1.Items[listBox1.SelectedIndex].ToString()).ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1)
                return;
            new method_comb(listBox1.Items[listBox1.SelectedIndex].ToString()).ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s1 = "1 + x ^ 7 - ln(1 + pi * cos(x ^ 3)) + x ^ 10 - (tg(x)) ^ 5 + x = 0";
            //s1 = "1 + x ^ 7 + x ^ 10 - (tg(x)) ^ 5 + x = 0";
            MessageBox.Show(MyMath.F(-0.9, s1).ToString());

        } 
    }
}
