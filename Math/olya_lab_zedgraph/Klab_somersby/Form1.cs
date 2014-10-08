using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using ZedGraph;
namespace Klab_somersby
{
    public partial class Form1 : Form
    {
        PointPairList pointPairList;
        MyLine myLine = new MyLine();
        MyParabola myParabola = new MyParabola();

        public Form1()
        {
            InitializeComponent();
        }

        //удалить точку
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                data.Rows.RemoveAt(data.CurrentRow.Index);
                general_call();
            }
            catch
            {
                MessageBox.Show("База пуста");
            }
        }

        //очистка
        private void button3_Click(object sender, EventArgs e)
        {
            data.Rows.Clear();
            general_call();
        }

        //добавить значение из текстбокса
        private void button5_Click(object sender, EventArgs e)
        {
              try
              {
                  data.Rows.Add(Double.Parse(textBox1.Text), Double.Parse(textBox2.Text));                  
                  general_call();
              }
              catch
              {
                  MessageBox.Show("Значение Х или У не верно");
              }
        }

        double d(object s)
        {
            double value;
            if (!Double.TryParse(s.ToString(), out value))
                value = Double.NaN;
            return value;
        }

        void MakePointPairList()
        {
            pointPairList = new PointPairList();
            foreach (DataGridViewRow row in data.Rows)
            {
                double x = d(row.Cells[0].Value);
                double y = d(row.Cells[1].Value);
                if (x == Double.NaN || y == Double.NaN)
                    continue;
                pointPairList.Add(x, y);
            }
        }

        //построениe точек
        private void Draw_Points()
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            pane.CurveList.Clear();

            LineItem myCurve = pane.AddCurve("Точка", pointPairList, Color.Blue, SymbolType.Diamond);
            myCurve.Line.IsVisible = false;
            myCurve.Symbol.Fill.Color = Color.Blue;
            myCurve.Symbol.Fill.Type = FillType.Solid;
            myCurve.Symbol.Size = 7;

            pane.YAxis.Scale.Min = MinMax.min_Y - 5;
            pane.YAxis.Scale.Max = MinMax.max_Y + 5;
            pane.XAxis.Scale.Min = MinMax.min_X - 5;
            pane.XAxis.Scale.Max = MinMax.max_X + 5;
        
            zedGraphControl1.AxisChange();

            zedGraphControl1.Invalidate();

            zedGraphControl1.ZoomOutAll(pane);
        }
                
        private void DrawGraph_line()
        {

            myLine.СalculateСoefficients(pointPairList);
            k2.Text = myLine.koef_A.ToString();
            k1.Text = myLine.koef_B.ToString();

            PointPairList list = new PointPairList();
            list.Add(MinMax.min_X, myLine.СalculateY(MinMax.min_X));
            list.Add(MinMax.max_X, myLine.СalculateY(MinMax.max_X));
         
            LineItem myCurve = zedGraphControl1.GraphPane.AddCurve("Прямая", list, Color.Red, SymbolType.None);

            zedGraphControl1.AxisChange();

            zedGraphControl1.Invalidate();
        }
        
        private void DrawGraph_parabola()
        {
            PointPairList list = new PointPairList();
            
            myParabola.СalculateСoefficients(pointPairList);
            p1.Text = myParabola.koef_A.ToString();
            p2.Text = myParabola.koef_B.ToString();
            p3.Text = myParabola.koef_C.ToString();

            double x;
            if (MinMax.min_X > 0)
                x = 0.5 * MinMax.min_X;
            else
                x = 1.5 * MinMax.min_X;

            double k = (MinMax.max_X - MinMax.min_X) / 100;

            while (x <= MinMax.max_X * 1.2)
            {
                list.Add(x, myParabola.СalculateY(x));
                x += k;
            }

            LineItem myCurve = zedGraphControl1.GraphPane.AddCurve("парабола", list, Color.Green, SymbolType.None);

            zedGraphControl1.AxisChange();

            zedGraphControl1.Invalidate();
        }

        //добавление точки с помощью мыши
        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {
            double x;
            double y;
            zedGraphControl1.GraphPane.ReverseTransform(e.Location, out x, out y);
            data.Rows.Add(Math.Round(x, 3), Math.Round(y, 3));
            general_call();
        }

        private void general_call()
        {
            MakePointPairList();
            MinMax.Refresh(pointPairList);

            Draw_Points();
            if (checkBox1.Checked && data.Rows.Count > 1)
            {
                DrawGraph_line();
            }
            if (checkBox2.Checked && data.Rows.Count > 2)
            {
                DrawGraph_parabola();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            general_call();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            general_call();
        }

        private void Rebuild(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            general_call();
        }

        private void Rebuild(object sender, DataGridViewCellEventArgs e)
        {
            general_call();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                //прямая
                double x = d(textBox3.Text.Replace(".", ","));
                line_y.Text = myLine.СalculateY(x).ToString();
                //парабола
                par_y.Text = myParabola.СalculateY(x).ToString();

            }
            catch
            {
            }
        }

    }
}

