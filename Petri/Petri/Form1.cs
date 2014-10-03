using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Petri
{
    public partial class Form1 : Form
    {
        int art = 0;
        
        public Form1()
        {
            InitializeComponent();
            Master.MouseLocation = new Point();
            Master.properties = panel2;
            Master.freespace = panel3;
            //PanelMaster.ss = toolStripStatusLabel1;            
        }

        private void AddElement(object sender, MouseEventArgs e)
        {
            switch (art)
            {
                case 0: break;
                case 1:
                    Master.MouseLocation.X -= 12;
                    Master.MouseLocation.Y -= 12;
                    PositionClass PC = new PositionClass(Master.MouseLocation, Info.PCList.Count + 1);
                    Info.PCList.Add(PC);
                    break;
                case 2:
                    Master.MouseLocation.X -= 12;
                    Master.MouseLocation.Y -= 12;
                    JumpClass JC = new JumpClass(Master.MouseLocation, Info.JCList.Count + 1);
                    Info.JCList.Add(JC);
                    break;
                case 3:
                    if (Info.AList.Count == 0)
                    {
                        Arrow arrow = new Arrow(panel3);
                        arrow.SetPoint(Master.MouseLocation.X, Master.MouseLocation.Y);
                        panel3.MouseMove += arrow.MouseMoveing;
                        Info.AList.Add(arrow);
                    }
                    else
                    if (Info.AList[Info.AList.Count - 1].Ended)
                    {
                        Arrow arrow = new Arrow(panel3);
                        arrow.SetPoint(Master.MouseLocation.X, Master.MouseLocation.Y);
                        panel3.MouseMove += arrow.MouseMoveing;
                        Info.AList.Add(arrow);
                    }
                    else
                    {
                        if (e.Button.Equals(MouseButtons.Right))
                        {
                            Info.AList[Info.AList.Count - 1].SetToPoint(Master.MouseLocation.X, Master.MouseLocation.Y);
                            panel3.MouseMove -= Info.AList[Info.AList.Count - 1].MouseMoveing;
                            Info.AList[Info.AList.Count - 1].Ended = true;
                        }
                        else
                        {
                            Info.AList[Info.AList.Count - 1].SetPoint(Master.MouseLocation.X, Master.MouseLocation.Y);
                            Info.AList[Info.AList.Count - 1].SetPoint(Master.MouseLocation.X, Master.MouseLocation.Y);
                        }
                    }

                    break;
            }
        }

        private void GetMouseLocation(object sender, MouseEventArgs e)
        {
            Master.MouseLocation = e.Location;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ToolStripButton tsb in toolStrip1.Items)
                {
                    if (tsb != sender)
                        tsb.Checked = false;
                }
            }
            catch { }
            (sender as ToolStripButton).Checked = true;
            panel2.Enabled = false;
            art = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ToolStripButton tsb in toolStrip1.Items)
                {
                    if (tsb != sender)
                        tsb.Checked = false;
                }
            }
            catch { }
            (sender as ToolStripButton).Checked = true;
            panel2.Enabled = false;
            art = 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ToolStripButton tsb in toolStrip1.Items)
                {
                    if (tsb != sender)
                        tsb.Checked = false;
                }
            }
            catch { }
            (sender as ToolStripButton).Checked = true;
            panel2.Enabled = false;
            art = 3;
        }

        private void rename(object sender, EventArgs e)
        {
            Master.selected.name.Text = ((TextBox)sender).Text;
        }

        private void enabledChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void Clear(object sender, EventArgs e)
        {
            Info.JCList.Clear();
            Info.PCList.Clear();
            panel2.Enabled = false;
            panel3.Controls.Clear();
        }

        private void ShowTable(object sender, EventArgs e)
        {
            (new Table()).ShowDialog();
        }

        private void GetMouse(object sender, EventArgs e)
        {
            try
            {
                foreach (ToolStripButton tsb in toolStrip1.Items)
                {
                    if (tsb != sender)
                        tsb.Checked = false;
                }
            }
            catch { }
            (sender as ToolStripButton).Checked = true;
            panel2.Enabled = false;
            art = 0;
        }

        private void redraw(object sender, EventArgs e)
        {
            panel3.Refresh();
        }

        private void ArrowCheck(object sender, EventArgs e)
        {
            if ((sender as ToolStripButton).Checked && !Info.AList.Count.Equals(0))
            {
                Info.AList[Info.AList.Count - 1].Ended = true;
                panel3.MouseMove -= Info.AList[Info.AList.Count - 1].MouseMoveing;
            }

        }    


    }
}
