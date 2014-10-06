using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri2
{
    public partial class Form2 : Form
    {
        Mark mrk;
        int max;

        List<int> prlist;

        public Form2()
        {
            InitializeComponent();
            mrk = new Mark(0, data.PList);
            prlist = new List<int>();
            Random r = new Random();

            dataGridView1.ColumnCount = dataGridView4.ColumnCount = dataGridView2.ColumnCount = data.TList.Count;
            dataGridView1.RowCount = dataGridView4.RowCount = dataGridView2.RowCount = 1;

            for (int i = 0; i < data.TList.Count; i++)
            {
                dataGridView1.Columns[i].CellTemplate.ValueType = typeof(int);
                dataGridView1.Columns[i].HeaderText = data.TList[i].name;
                dataGridView1.Rows[0].Cells[i].Value = i;

                dataGridView2.Columns[i].CellTemplate.ValueType = typeof(int);
                dataGridView2.Columns[i].HeaderText = data.TList[i].name;
                dataGridView2.Rows[0].Cells[i].Value = 3;

                dataGridView4.Columns[i].CellTemplate.ValueType = typeof(int);
                dataGridView4.Columns[i].HeaderText = data.TList[i].name;
                int ri = r.Next(7) + 1;
                dataGridView4.Rows[0].Cells[i].Value = ri;
                data.TList[i].baseT = ri;
                data.TList[i].currT = ri;
            }
            dataGridView1.ClearSelection();
            dataGridView2.ClearSelection();


            dataGridView3.ColumnCount = 5;
            dataGridView3.Columns[0].Name = "Tact";
            dataGridView3.Columns[1].Name = "№";
            dataGridView3.Columns[2].Name = "Begin";
            dataGridView3.Columns[3].Name = "End";
            dataGridView3.Columns[4].Name = "Time";

            dataGridView3.ClearSelection();
            max = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < data.TList.Count; i++)
            {
                int num = 0;
                string strNum = dataGridView1.Rows[0].Cells[i].Value.ToString();
                if (!int.TryParse(strNum, out num))
                {
                    MessageBox.Show("Error: incorrect data format.");
                }
                else
                    prlist.Add(num);
            }

            if (!Check())
            {
                MessageBox.Show("Priority shoud not be equal.");
                return;
            }

            dataGridView3.RowCount = 100;
            int k = 0;
            for (int j = 0; j < numericUpDown1.Value; j++)
            {
                bool b = false;
                for (int i = 0; i <= max; i++)
                {
                    if (prlist.Contains(i))
                    {
                        dataGridView3.Rows[j].Cells[0].Value = j;
                        dataGridView3.Rows[j].Cells[1].Value = k;
                        Mark m = new Mark(j, data.PList);
                        dataGridView3.Rows[j].Cells[2].Value = m.ToString();


                        dataGridView3.Rows[j].Cells[4].Value = new Mark(data.TList).StringMark;

                        foreach (Transaction tr in data.TList)
                        {
                            int n = int.Parse(tr.name.Remove(0, 1)) - 1;
                            if ((int)dataGridView1.Rows[0].Cells[n].Value == i)
                            {
                                int a = 0;
                                string strNum = dataGridView2.Rows[0].Cells[n].Value.ToString();
                                if (!int.TryParse(strNum, out a))
                                {
                                    MessageBox.Show("Error: incorrect data format.");
                                    continue;
                                }

                                if (a == 0)
                                    continue;

                                if (tr.currT > 0)
                                    tr.currT--;

                                if (tr.currT == 0)
                                if (Run(tr))
                                {
                                    a--;
                                    k++;
                                    tr.currT = tr.baseT;
                                    dataGridView2.Rows[0].Cells[n].Value = a;
                                }

                                
                            }
                        }

                        Mark q = new Mark(j + 1, data.PList);
                        if(q.StringMark == m.StringMark)
                            dataGridView3.Rows[j].Cells[3].Value = new Mark(j, data.PList).ToString();
                        else
                            dataGridView3.Rows[j].Cells[3].Value = new Mark(j+1, data.PList).ToString();
                    }
                    
                }
                //if (!b) j--;
            }
            MessageBox.Show("Completed succeeded.");

            for (int i = 0; i < data.PList.Count; i++)
            {
                data.PList[i].points = mrk.m[i];
            }
        }

        bool Check()
        {
            foreach (DataGridViewCell dgc in dataGridView1.Rows[0].Cells)
            {
                if ((int)dgc.Value > max)
                    max = (int)dgc.Value;
            }

            foreach(DataGridViewCell dgc in dataGridView1.Rows[0].Cells)
            {
                int i = 0;
                foreach (DataGridViewCell dgc2 in dataGridView1.Rows[0].Cells)
                {
                    if (dgc.Value == dgc2.Value)
                        i++;
                }
                if (i > 1)
                    return false;
            }

            return true;
        }

        bool Run(Transaction tr)
        {
            List<Position> Oflist = new List<Position>();
            foreach (Arrow a in data.AList)
                if (a.In == tr) Oflist.Add((Position)a.Of);


            bool can = true;

            Position p;
            int k;
            while (Oflist.Count != 0)
            {
                p = Oflist[0];
                k = 1;
                Oflist.Remove(p);
                if (Oflist.Contains(p)) k++;
                else
                {
                    if (p.points < k)
                    {
                        can = false;
                        break;
                    }
                    if (Oflist.Count != 0)
                        p = Oflist[0];
                    k = 1;
                }
            }
            if (can)
            {
                foreach (Arrow a in data.AList)
                    if (a.In == tr) Oflist.Add((Position)a.Of);
                while (Oflist.Count != 0)
                {
                    p = Oflist[0];
                    p.points--;
                    Oflist.Remove(p);
                    //if (!Oflist.Contains(p)) p.AddPoints(field);
                }
                List<Position> Inlist = new List<Position>();
                foreach (Arrow a in data.AList)
                    if (a.Of == tr) Inlist.Add((Position)a.In);

                while (Inlist.Count != 0)
                {
                    p = Inlist[0];
                    p.points++;
                    Inlist.Remove(p);
                    //if (!Inlist.Contains(p)) p.AddPoints(field);
                }
            }
            return can;
        }
    }
}
