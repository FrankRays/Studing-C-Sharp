using chm_lab_1.Methods;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace chm_lab_1
{
    public partial class method_lobachevskogo : Form
    {
        string expression;
        Dictionary<int, double> KoefDictionary;

        public method_lobachevskogo(string expression)
        {
            InitializeComponent();
            this.expression = expression;
            label2.Text = expression;

            GetKoef();
        }

        private void GetKoef()
        {
            KoefDictionary = new Dictionary<int, double>();

            string buff = "";
            double tmp = double.NaN;
            string buff2 = "";
            int tmp2;

            bool koef = true;

            expression = expression.Replace(" ", "");

            foreach (char c in expression)
            {
                if (c == '*' || c == '^')
                    continue;
                if (c == '=')
                {
                    if (buff != "")
                    {
                        if (KoefDictionary.ContainsKey(0))
                            KoefDictionary[0] += Double.Parse(buff);
                        else
                            KoefDictionary.Add(0, Double.Parse(buff));
                    }

                    if (buff2 != "")
                    {
                        tmp2 = int.Parse(buff2);

                        if (KoefDictionary.ContainsKey(tmp2))
                            KoefDictionary[tmp2] += tmp;
                        else
                            KoefDictionary.Add(tmp2, tmp);
                    }
                    break;
                }

                if ((c == '+' || c == '-') && buff != "")
                {
                    if (KoefDictionary.ContainsKey(0))
                        KoefDictionary[0] += Double.Parse(buff);
                    else
                        KoefDictionary.Add(0, Double.Parse(buff));
                    buff = "";
                }

                if (c == 'x')
                {
                    koef = false;
                    if (buff == String.Empty)
                        tmp = 1;
                    else
                    {
                        if (buff == "+" || buff == "-")
                            buff += "1";
                        tmp = Double.Parse(buff);
                    }
                    buff = String.Empty;
                    continue;
                }

                if (koef)
                    buff += c;
                else if (c == '+' || c == '-')
                {
                    if (buff2 == "")
                        tmp2 = 1;
                    else
                        tmp2 = int.Parse(buff2);

                    if (KoefDictionary.ContainsKey(tmp2))
                        KoefDictionary[tmp2] += tmp;
                    else
                        KoefDictionary.Add(tmp2, tmp);
                    
                    buff2 = "";
                    koef = true;
                    buff += c;
                }
                else
                    buff2 += c;

                
            }

            foreach (KeyValuePair<int, double> p in KoefDictionary)
            {
                Debug.WriteLine(p.ToString());
            }

            var bl = KoefDictionary.ToBindingList();
            dataGridView1.DataSource = bl;

        }

        private void button8_Click(object sender, EventArgs e)
        {
            Lobachevskogo lob = new Lobachevskogo(KoefDictionary, expression);
            dataGridView2.DataSource = lob.Roots().ToBindingList();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Log.Output.Show();
        }

        //double division
        private void button2_Click(object sender, EventArgs e)
        {
            Run(new method_double_division(expression));
        }
        //hord
        private void button1_Click(object sender, EventArgs e)
        {
            Run(new method_comb(expression));
        }
        //simple
        private void button3_Click(object sender, EventArgs e)
        {
            Run(new method_simple_iter(expression));
        }

        void Run(Form f)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                double root = double.Parse(row.Cells[1].Value.ToString());
            }
            f.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (expression != null)
            new Graphic(expression, null).Show();
        }
    }
}
