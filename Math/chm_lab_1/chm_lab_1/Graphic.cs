using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace chm_lab_1
{
    public partial class Graphic : Form
    {
        DataGridViewRowCollection out_rows;
        string expression;

        public Graphic(string expression, DataGridViewRowCollection rows)
        {
            this.expression = expression;

            InitializeComponent();

            zed.Dock = DockStyle.Fill;
            splitContainer1.Panel2.Controls.Add(zed);
            
            if (rows == null)
                splitContainer1.Panel1Collapsed = true;
            else
            {
                dataGridView1.CellValueChanged += reDraw;
                out_rows = rows;
                foreach (DataGridViewRow row in rows)
                    dataGridView1.Rows.Add(new object[] { row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value});
            }

            DrawMain();
        }


        void DrawMain()
        {
            PointPairList list = new PointPairList();

            double x = -10;

            while (x <= 10)
            {
                double y = MyMath.F(x, expression);
                list.Add(x, MyMath.F(x, expression));
                x += 0.1;
            }

            zed.GraphPane.CurveList.Clear();
            zed.GraphPane.AddCurve( "График", list, Color.Green, SymbolType.None);

            //zed.AxisChange();

            zed.Invalidate();
        }

        private void reDraw(object sender, DataGridViewCellEventArgs e)
        {
            if (out_rows != null)
                out_rows = dataGridView1.Rows;
        }
    }
}
