using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri2
{
    public partial class Form1 : Form
    {
        List<Point[]> map = new List<Point[]>();
        bool[,] Map;

        Graphics field;
        int Art = 0;
        bool ArrowStarted = false;

        public Form1(string FileName)
        {
            filename = FileName;
            InitializeComponent();
            global.outtxt = toolStripStatusLabel6;
            global.lasttxt = toolStripStatusLabel3;

            global.panel = panel1;
            global.field = field;
            field = panel1.CreateGraphics();
            global.Out(4);
            global.Out(11);

            ReMap(null, null);
            if(filename != null)
                ReadFile();
            panel1.Invalidate();
        }

        void ReDraw(object sender, PaintEventArgs e)
        {
            Bitmap g = new Bitmap(panel1.Width, panel1.Height, e.Graphics);
            Graphics gr = Graphics.FromImage(g);

            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            
            DrawPoints(gr);
            foreach (Position p in data.PList)
                p.Draw(gr);
            foreach (Transaction t in data.TList)
                t.Draw(gr);
            foreach (Arrow a in data.AList)
                a.Draw(gr);

            e.Graphics.DrawImageUnscaled(g, 0, 0);
            gr.Dispose();
        }

        void DrawPoints(Graphics gr)
        {
            foreach (Point[] point in map)
                foreach (Point p in point)
                    gr.DrawEllipse(new Pen(Color.Black, 1), new Rectangle(p, new Size(1, 1)));
        }

        private void ReMap(object sender, EventArgs e)
        {
            map.Clear();

            for (int i = 0; i < panel1.Size.Height / global.cell; i++)
            {
                Point[] mapi = new Point[(int)(panel1.Size.Width / global.cell)];
                for (int j = 0; j < mapi.Length; j++)
                {
                    mapi[j] = new Point(global.cell * (j + 1), global.cell * (i + 1));
                }
                map.Add(mapi);
            }
            
            Map = new bool[map.Count, map[0].Length];
            ClearMap();

            field = panel1.CreateGraphics();
            panel1.Invalidate();
        }
        
        void ClearMap()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[0].Length; j++)
                {
                    Map[i, j] = true;
                }
            }
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            switch (Art)
            {
                case 0:
                    global.Out(4);
                    break;
                case 1:
                    AddP(e);
                    break;
                case 2:
                    AddT(e);
                    break;
                case 3:
                    AddA(e);
                    break;
                case 4:
                    AddF(e);
                    break;
                case 5:
                    delete(e);
                    break;
                case 6:
                    Run(e);
                    break;
            }
        }

        void Run(MouseEventArgs e)
        {
            Transaction tr = null;
            foreach (Transaction t in data.TList)
                if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                    if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                    {
                        tr = t;
                        break;
                    }
            if (tr != null)
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
                        if (!Oflist.Contains(p)) p.AddPoints(field);
                    }
                    List<Position> Inlist = new List<Position>();
                    foreach (Arrow a in data.AList)
                        if (a.Of == tr) Inlist.Add((Position)a.In);

                    while (Inlist.Count != 0)
                    {
                        p = Inlist[0];
                        p.points++;
                        Inlist.Remove(p);
                        if (!Inlist.Contains(p)) p.AddPoints(field);
                    }
                    global.Out(2);
                }
                else global.Out(1);
            }
            else global.Out(0);
        }

        void delete(MouseEventArgs e)
        {
            Transaction tel = null; 
            Position pel = null;

            foreach (Transaction t in data.TList)
                if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                    if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                        tel = t;

            foreach (Position t in data.PList)
                if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                    if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                        pel = t;

            string name;
            if (pel != null)
                name = pel.name;
            else
                if (tel != null)
                    name = tel.name;
                else
                {
                    global.Out(16);
                    return;
                }

            if (MessageBox.Show("Are you sure you want to delete " + name + " and all its links?",
                    "", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (pel != null)
                {
                    data.PList.Remove(pel);
                    CleanSpace(new Point(pel.location.X / global.cell, pel.location.Y / global.cell));
                    for (int i = 0; i < data.AList.Count; i++)
                    {
                        Arrow a = data.AList[i];
                        if (a.In == pel || a.Of == pel)
                        {
                            listBox1.Items.Remove(a.Of.name + "-->" + a.Of.name);
                            data.AList.Remove(a);
                            i--;
                        }
                    }
                }
                else
                {
                    data.TList.Remove(tel);
                    CleanSpace(new Point(tel.location.X / global.cell, tel.location.Y / global.cell));
                    for (int i = 0; i < data.AList.Count; i++)
                    {
                        Arrow a = data.AList[i];
                        if (a.In == tel || a.Of == tel)
                        {
                            listBox1.Items.Remove(a.Of.name + "-->" + a.Of.name);
                            data.AList.Remove(a);
                            i--;
                        }
                    }
                }
                panel1.Invalidate();
                global.Out(23);
            }
        }

        void AddT(MouseEventArgs e)
        {
            var El = new Transaction();

            if (CanCreate(El.Create(e.X, e.Y)))
            {
                El.Draw(field);
                data.TList.Add(El);
                global.Out(24);
            }
            else
            {
                global.Out(17);
            }
        }

        void AddP(MouseEventArgs e)
        {
            var El = new Position();
            
            if (CanCreate(El.Create(e.X, e.Y)))
            {
                El.Draw(field);
                data.PList.Add(El);
                global.Out(24);
            }
            else
            {
                global.Out(17);
            }
        }

        void AddA(MouseEventArgs e)
        {
            if (ArrowStarted)
            {
                if (e.Button.Equals(MouseButtons.Right))
                {
                    data.AList.RemoveAt(data.AList.Count - 1);
                    ArrowStarted = false;
                    panel1.Invalidate();
                    global.Out(18);
                    return;
                }
                if (data.AList[data.AList.Count - 1].Of.GetType() == (new Position()).GetType())
                    foreach (Transaction t in data.TList)
                    {
                        if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                            if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                            {
                                ArrowStarted = false;
                                data.AList[data.AList.Count - 1].In = t;
                            }
                    }
                else
                    foreach (Position t in data.PList)
                    {
                        if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                            if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                            {
                                ArrowStarted = false;
                                data.AList[data.AList.Count - 1].In = t;
                            }
                    }

                if (!ArrowStarted)
                {
                    data.AList[data.AList.Count - 1].Draw(field);
                    listBox1.Items.Add(data.AList[data.AList.Count - 1].Of.name + "-->"
                        + data.AList[data.AList.Count - 1].In.name);
                    global.Out(19);
                }
            }
            else
            {
                foreach (Transaction t in data.TList)
                    if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                        if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                        {
                            Arrow a = new Arrow();
                            a.Of = t;
                            a.SetFromPoint(e.Location, Color.Red);
                            ArrowStarted = true;
                            data.AList.Add(a);
                            global.Out(20);
                        }

                foreach (Position t in data.PList)
                    if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                        if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                        {
                            Arrow a = new Arrow();
                            a.Of = t;
                            a.SetFromPoint(e.Location, Color.Blue);
                            ArrowStarted = true;
                            data.AList.Add(a);
                            global.Out(21);
                        }

            }
        }

        void AddF(MouseEventArgs e)
        {
            int d = (e.Button.Equals(MouseButtons.Left))? 1 : -1;
            foreach (Position t in data.PList)
            {
                if ((t.location.X < e.X) && (t.location.X + t.size.Width > e.X))
                    if ((t.location.Y < e.Y) && (t.location.Y + t.size.Height > e.Y))
                    {
                        if (t.points + d >= 0 && t.points + d <= 5)
                        {
                            t.points += d;
                            t.AddPoints(field);
                            global.Out(22);
                        }
                        else
                        {
                            global.Out(25);
                        }
                    }
            }
        }

        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (ArrowStarted)
            {
                data.AList[data.AList.Count - 1].SetToPoint(e.Location);
                panel1.Invalidate();
            }
        }

        bool CanCreate(Point pos)
        {
            for (int i = pos.Y - 1; i < pos.Y + global.h; i++)
            {
                for (int j = pos.X - 1; j < pos.X + global.w; j++)
                {
                    if (CheckMap(i, j))
                    {
                        if (!Map[i, j])
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            for (int i = pos.Y - 1; i < pos.Y + global.h; i++)
            {
                for (int j = pos.X - 1; j < pos.X + global.w; j++)
                {
                    if (CheckMap(i, j))
                        Map[i, j] = false;
                }
            }
            return true;
        }

        void CleanSpace(Point pos)
        {
            for (int i = pos.Y - 1; i < pos.Y + global.h; i++)
            {
                for (int j = pos.X - 1; j < pos.X + global.w; j++)
                {
                    if (CheckMap(i, j))
                        Map[i, j] = true;
                }
            }
        }

        bool CheckMap(int x, int y)
        {
            if (x < 0) return false;
            if (y < 0) return false;
            if (x >= map.Count) return false;
            if (y >= map[0].Length) return false;
            return true;
        }

        private void ArtChange(object sender, EventArgs e)
        {
            if (ArrowStarted)
            {
                global.outtxt.Text = "Set arrow ending at first";
                return;
            }


            foreach (ToolStripItem b in toolStrip1.Items)
            {
                try
                {
                    ((ToolStripButton)b).Checked = false;
                }
                catch { }
            }

            var s = sender as ToolStripButton;
            s.Checked = true;

            Art = int.Parse(s.Tag.ToString());
            switch (Art)
            {
                case 0: global.Out(3); break;
                case 1: global.Out(5); break;
                case 2: global.Out(6); break;
                case 3: global.Out(7); break;
                case 4: global.Out(8); break;
                case 5: global.Out(9); break;
                case 6: global.Out(10); break;
            }
        }

        private void CreateTable(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
           
            listBox2.Items.Clear();
            listBox3.Items.Clear();
            listBox3.Items.AddRange(listBox1.Items);
            
            #region first
            for (int i = 0; i < data.PList.Count; i++)
            {
                for (int j = 0; j < data.TList.Count; j++)
                {
                    if (!dataGridView1.Columns.Contains(data.TList[j].name))
                        dataGridView1.Columns.Add(data.TList[j].name, data.TList[j].name);
                    if (dataGridView1.Rows.Count <= i) dataGridView1.Rows.Add();
                    try
                    {
                        dataGridView1.Rows[i].Cells[j].Value = "0";
                    }catch{}
                }
                if (dataGridView1.Rows.Count != 0) dataGridView1.Rows[i].HeaderCell.Value = data.PList[i].name;
            }
            foreach (Arrow a in data.AList)
            {
                if (a.Of.GetType() == (new Position()).GetType())
                {
                    int i = 0, j = 0;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (r.HeaderCell.Value.ToString() != a.Of.name)
                            i++;
                        else break;
                    }
                    foreach (DataGridViewColumn c in dataGridView1.Columns)
                    {
                        if (c.Name != a.In.name)
                            j++;
                        else break;
                    }
                    dataGridView1.Rows[i].Cells[j].Value = 
                        int.Parse(dataGridView1.Rows[i].Cells[j].Value.ToString()) + 1;
                }
            }
            #endregion

            #region second
            for (int i = 0; i < data.TList.Count; i++)
            {
                for (int j = 0; j < data.PList.Count; j++)
                {
                    if (!dataGridView2.Columns.Contains(data.PList[j].name))
                        dataGridView2.Columns.Add(data.PList[j].name, data.PList[j].name);
                    if (dataGridView2.Rows.Count <= i) dataGridView2.Rows.Add();

                    dataGridView2.Rows[i].Cells[j].Value = "0";
                }
                if (dataGridView2.Rows.Count != 0)
                    dataGridView2.Rows[i].HeaderCell.Value = data.TList[i].name;
            }

            foreach (Arrow a in data.AList)
            {
                if (a.Of.GetType() == (new Transaction()).GetType())
                {
                    int i = 0, j = 0;
                    foreach (DataGridViewRow r in dataGridView1.Rows)
                    {
                        if (r.HeaderCell.Value.ToString() != a.In.name)
                            i++;
                        else break;
                    }
                    foreach (DataGridViewColumn c in dataGridView1.Columns)
                    {
                        if (c.Name != a.Of.name)
                            j++;
                        else break;
                    }
                    try
                    {
                        dataGridView2.Rows[i].Cells[j].Value =
                            int.Parse(dataGridView2.Rows[i].Cells[j].Value.ToString()) + 1;
                    }
                    catch { }
                }
            }
            #endregion

            #region third

            MarkList = new List<Mark>();
            Mark m = new Mark(0, data.PList);

            MarkList.Add(m);
            ////
            rec(m);
            foreach(Mark mrk in MarkList)
            {
                listBox2.Items.Add(mrk.ToString());
            }

            dataGridView3.RowCount = MarkList.Count;
            dataGridView3.ColumnCount = 4;// 5;
            dataGridView3.Columns[0].Name = "Вершина";
            dataGridView3.Columns[3].Name = "Вершины";
            dataGridView3.Columns[2].Name = "Дуги";
            dataGridView3.Columns[1].Name = "Маркировка";
            //dataGridView3.Columns[4].Name = "Тип";
            for (int i = 0; i < MarkList.Count; i++)
            {
                dataGridView3.Rows[i].Cells[0].Value += MarkList[i].Name;
                foreach (Mark b in MarkList[i].ToMark)
                    dataGridView3.Rows[i].Cells[3].Value += b.Name;
                foreach (Transaction b in MarkList[i].ToTransaction)
                    dataGridView3.Rows[i].Cells[2].Value += b.name + " ";

                dataGridView3.Rows[i].Cells[1].Value += MarkList[i].StringMark;

                /*
                if (MarkList[i].occurs > 1)
                    dataGridView3.Rows[i].Cells[4].Value = "Дублирующая";
                else 
                    dataGridView3.Rows[i].Cells[4].Value = "Достижима";
                 */
            }
            dataGridView3.ClearSelection();


            ////
            setMark(m);
            pictureBox1.Invalidate();
            #endregion
        }

        List<Mark> MarkList;

        bool run(Transaction tr)
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
                    if (!Oflist.Contains(p)) p.AddPoints(field);
                }
                List<Position> Inlist = new List<Position>();
                foreach (Arrow a in data.AList)
                    if (a.Of == tr) Inlist.Add((Position)a.In);

                while (Inlist.Count != 0)
                {
                    p = Inlist[0];
                    if (p.points >= 3)
                    {
                        int ina, ofa;
                        ina = ofa = 0;
                        foreach (Arrow a in data.AList)
                        {
                            if (a.Of == p)
                            {
                                ofa++;
                            }
                            else if (a.In == p)
                            {
                                ina++;
                            }
                        }
                        if (ina > ofa || ((ina >= ofa) && (data.PList.Count < data.TList.Count)))
                        {
                            return false;
                        }
                    }
                    p.points++;
                    Inlist.Remove(p);
                    if (!Inlist.Contains(p)) p.AddPoints(field);
                }
            }

            return can;
        }

        void rec(Mark mrk)
        {
            foreach (Transaction tr in data.TList)
            {
                setMark(mrk);
                if (run(tr))
                {
                    Mark m = new Mark(MarkList.Count, data.PList);
                    int n = CheckMarkEquals(m);
                    if (n == -1)
                    {
                        MarkList.Add(m);
                        mrk.ToMark.Add(m);
                        mrk.ToTransaction.Add(tr);
                        rec(m);
                    }
                    else
                    {
                        MarkList[n].occurs++;
                        mrk.ToMark.Add(MarkList[n]);
                        mrk.ToTransaction.Add(tr);
                    }
                }
            }
        }

        void setMark(Mark mrk)
        {
            for (int i = 0; i < data.PList.Count; i++)
            {
                data.PList[i].points = mrk.m[i];
            }
        }

        int CheckMarkEquals(Mark mrk)
        {
            foreach(Mark m in MarkList)
            {
                if (m.Check(mrk))
                {
                    return MarkList.IndexOf(m);
                }
            }

            return -1;
        }

        private void GraphDraw(object sender, PaintEventArgs e)
        {
            Bitmap g = new Bitmap(pictureBox1.Width, pictureBox1.Height, e.Graphics);
            Graphics G = Graphics.FromImage(g);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            /////////////////////////////////////////////
            //foreach (MClass mc in marksObj)
            //    mc.Draw(gr);
            float AngleDelta = 360 / MarkList.Count;
            System.Drawing.Pen cPen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            System.Drawing.Pen aPen = new System.Drawing.Pen(System.Drawing.Color.Red, 2);
            System.Drawing.Brush fBr = System.Drawing.Brushes.Black;
            string ArcW;
            float cSize = 20;
            float Xs, Ys, Xf, Yf, Xc, Yc;
            
            for (int i = 0; i < MarkList.Count; i++)
            {
                //  Draw Arcs
                Xs = 300 + (int)(Math.Cos(Math.PI * (i * AngleDelta) / 180) * (200));
                Ys = 300 + (int)(Math.Sin(Math.PI * (i * AngleDelta) / 180) * (200));
                int ThroughT = MarkList[i].ToMark.Count;
                ArcW = "1";
                if (MarkList[i].ToMark.Count > 1) ArcW += "/" + String.Format("{0}", MarkList[i].ToMark.Count);
                
                for (int j = 0; j < MarkList[i].ToMark.Count; j++)
                {
                    Xf = 250 + (int)(Math.Cos(Math.PI * (MarkList[i].ToMark[j].n * AngleDelta) / 180) * (200));
                    Yf = 250 + (int)(Math.Sin(Math.PI * (MarkList[i].ToMark[j].n * AngleDelta) / 180) * (200));
                    Xs = 250 + (int)(Math.Cos(Math.PI * (i * AngleDelta) / 180) * (200));
                    Ys = 250 + (int)(Math.Sin(Math.PI * (i * AngleDelta) / 180) * (200));

                    double L = Math.Sqrt((Xf - Xs) * (Xf - Xs) + (Yf - Ys) * (Yf - Ys));
                    double Angle = Math.Asin((Xf - Xs) / L);
                    double dY = Math.Cos(Math.PI - Angle) * cSize;
                    double dX = Math.Sin(Math.PI - Angle) * cSize;
                    double dYf = Math.Cos(Math.PI - Angle) * cSize;
                    double dXf = Math.Sin(Math.PI - Angle) * cSize;
                    if ((Xs > Xf) && (Ys > Yf)) { Xs += (float)dX; Ys += (float)dY; Xf -= (float)dXf; Yf -= (float)dYf; }
                    else if ((Xs > Xf) && (Ys < Yf)) { Xs += (float)dX; Ys -= (float)dY; Xf -= (float)dXf; Yf += (float)dYf; }
                    else if ((Xs < Xf) && (Ys > Yf)) { Xs += (float)dX; Ys += (float)dY; Xf -= (float)dXf; Yf -= (float)dYf; }
                    else if ((Xs < Xf) && (Ys < Yf)) { Xs += (float)dX; Ys -= (float)dY; Xf -= (float)dXf; Yf += (float)dYf; }
                    Xc = (Xs + Xf) / 2;
                    Yc = (Ys + Yf) / 2;
                    G.DrawLine(aPen, Xs, Ys, Xf, Yf);
                    G.DrawEllipse(aPen, Xf - 5, Yf - 5, 10, 10);
                    G.DrawString(ArcW, new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 9), fBr, Xc + 6, Yc + 3);

                }
             
                int X = 250 + (int)(Math.Cos(Math.PI * (i * AngleDelta) / 180) * 200);
                int Y = 250 + (int)(Math.Sin(Math.PI * (i * AngleDelta) / 180) * 200);
                G.DrawEllipse(cPen, X - cSize, Y - cSize, 2 * cSize, 2 * cSize);
                G.DrawString(String.Format("M{0}", MarkList[i].n), new System.Drawing.Font(System.Drawing.FontFamily.GenericSerif, 9), fBr, X - 10, Y - 8);
            }

            /////////////////////////////////////////////
            e.Graphics.DrawImageUnscaled(g, 0, 0);
            G.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            { 
                string arrow = (string)listBox1.SelectedItem;
                string ofname = arrow.Remove(arrow.IndexOf("-->"));
                string inname = arrow.Remove(0, arrow.IndexOf("-->") + 3);
                foreach (Arrow a in data.AList)
                {
                    if (a.In.name == inname && a.Of.name == ofname)
                    {
                        data.AList.Remove(a);
                        break;
                    }
                }
                listBox1.Items.Remove(listBox1.SelectedItem);
                panel1.Invalidate();
                global.Out(15);
            }
        }

        public string filename;
        //new
        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            filename = null;
            data.Clear();
            listBox1.Items.Clear();
            ClearMap();
            ArrowStarted = false;
            panel1.Invalidate();
            global.Out(11);
        }

        //open
        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            OpenFileDialog sfd = new OpenFileDialog();
            sfd.Filter = "File Petri (*.petri)|*.petri"; //"Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                toolStripButton8_Click(sender, e);//new
                filename = sfd.FileName;
                ReadFile();
                global.Out(12);
            }
        }

        void ReadFile()
        {
            global.outtxt.Text = filename;
            DataToSave dts = new DataToSave();
            dts.Deserialize(filename);
            panel1.Invalidate();
            foreach (Transaction t in data.TList)
                CanCreate(t.Create(t.location.X, t.location.Y));
            foreach (Position t in data.PList)
                CanCreate(t.Create(t.location.X, t.location.Y));
            foreach (Arrow a in data.AList)
                listBox1.Items.Add(a.Of.name + "-->" + a.In.name);
        }

        //save
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            if (filename == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "File Petri (*.petri)|*.petri";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                    DataToSave dts = new DataToSave();
                    dts.AList = data.AList;
                    dts.PList = data.PList;
                    dts.TList = data.TList;
                    dts.Serialize(filename, dts);
                    global.Out(13);
                }
                else
                    global.Out(14);
            }
            else
            {
                DataToSave dts = new DataToSave();
                dts.AList = data.AList;
                dts.PList = data.PList;
                dts.TList = data.TList;
                dts.Serialize(filename, dts);
                global.Out(13);
            }
        }

        //save as
        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            filename = null;
            toolStripButton10_Click(sender, e);
        }

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            (new Form2()).Show();
        }
    }
}
