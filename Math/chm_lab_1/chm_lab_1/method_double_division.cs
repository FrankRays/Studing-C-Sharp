using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using chm_lab_1.Methods;

namespace chm_lab_1
{
    public partial class method_double_division : Form
    {
        string expression;

        public method_double_division(string expression)
        {
            InitializeComponent();
            this.expression = expression;
            label1.Text = expression;
            MyMath.FindRange(expression, dataGridView1);
        }

        


        private void button7_Click(object sender, EventArgs e)
        {
            Log.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new Graphic(expression, dataGridView1.Rows).Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dataGridView2.Rows.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value == null)
                    continue;
                Log.WriteLn("Корень №" + row.Cells[0].Value);
                Double_division method = new Double_division(expression);
                double root = method.Roots(double.Parse(row.Cells[1].Value.ToString().Replace(".", ",")),
                                             double.Parse(row.Cells[2].Value.ToString().Replace(".", ",")));
                dataGridView2.Rows.Add(new object[] { row.Cells[0].Value, root });
            }
        }
    }
}
