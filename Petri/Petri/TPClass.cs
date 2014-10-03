using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.PowerPacks;

namespace Petri
{
    enum Type
    {
        pos, jmp
    }

    class TPClass
    {
        public SimpleShape icon;
        public Label name;
        public ShapeContainer obj;
        public Type type;
        public List<TPClass> OutList, InList;
        public List<Arrow> OutArrow, InArrow;

        public int points = 0;

        bool IsDragMode;

        protected void GiveName(Point location, int n, Type t)
        {
            type = t;
            OutList = new List<TPClass>();
            InList = new List<TPClass>();
            OutArrow = new List<Arrow>();
            InArrow = new List<Arrow>();

            obj = new ShapeContainer();
            obj.Parent = Master.freespace;
            obj.Size = new Size(25, 40);
            obj.BorderStyle = BorderStyle.FixedSingle;
            

            name = new Label();
            name.Location = new Point(location.X, location.Y + 26);
            name.Size = new Size(25, 15);
            name.TextAlign = ContentAlignment.MiddleCenter;
            name.Font = new System.Drawing.Font
                ("Microsoft Sans Serif", 6.0F, System.Drawing.FontStyle.Regular, 
                System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            if(type == Type.pos)
            {
                name.Text = "P" + n.ToString() + "(0)";
                icon = new OvalShape();
            }
            else
            {
                name.Text = "T" + n.ToString();
                icon = new RectangleShape();
            }

            obj.Controls.Add(name);
                
            icon.Location = location;
            icon.Size = new Size(25, 25);
            icon.Parent = obj;

            obj.MouseClick += ShawProperties;
            obj.KeyDown += OnMouseDown;
            obj.KeyUp += OnMouseUp;
            obj.MouseMove += OnMouseMove;
            obj.Parent.MouseMove += OnMouseMove;
        }
        
        private void ShawProperties(object sender, EventArgs e)
        {
            Master.selected = this;
            Master.properties.Controls.Find("name", false)[0].Text = name.Text;
            Master.properties.Controls.Find("label3", false)[0].Text = "In: " + InList.Count;
            Master.properties.Controls.Find("label4", false)[0].Text = "Out: " + OutList.Count;

#region intab
            TabPage tpIn = ((TabControl)Master.properties.Controls.Find("tabControl1", false)[0]).TabPages[0];
            tpIn.Controls.Clear();
            string[] list;
            if (type == Type.pos)
            {
                list = new string[Info.JCList.Count+1];
                foreach (JumpClass sub in Info.JCList)
                    list[Info.JCList.IndexOf(sub)] = sub.name.Text;
                list[Info.JCList.Count] = "DELETE";
            }
            else
            {
                list = new string[Info.PCList.Count+1];
                foreach (PositionClass sub in Info.PCList)
                    list[Info.PCList.IndexOf(sub)] = sub.name.Text;
                list[Info.PCList.Count] = "DELETE";
            }

            int i = 0;
            foreach (TPClass item in InList)
            {
                ComboBox cb = new ComboBox();
                cb.Location = new Point(0, cb.Height * i);
                cb.Items.AddRange(list);
                cb.SelectedIndex = cb.FindStringExact(item.name.Text);
                cb.Tag = item.name.Text;
                cb.SelectedIndexChanged += addIn;
                tpIn.Controls.Add(cb);
                i++;
            }
            ComboBox newcb = new ComboBox();
            newcb.Location = new Point(0, newcb.Height * (InList.Count));
            newcb.Items.AddRange(list);
            newcb.SelectedIndexChanged += addIn;
            tpIn.Controls.Add(newcb);
            #endregion

#region outtab
            TabPage tpOut = ((TabControl)Master.properties.Controls.Find("tabControl1", false)[0]).TabPages[1];
            tpOut.Controls.Clear();
            
            i = 0;
            foreach (TPClass item in OutList)
            {
                ComboBox cb = new ComboBox();
                cb.Location = new Point(0, cb.Height * i);
                cb.Items.AddRange(list);
                cb.SelectedIndex = cb.FindStringExact(item.name.Text);
                cb.Tag = item.name.Text;
                cb.SelectedIndexChanged += addIn;
                tpOut.Controls.Add(cb);
                i++;
            }
            newcb = new ComboBox();
            newcb.Location = new Point(0, newcb.Height * OutList.Count);
            newcb.Items.AddRange(list);
            newcb.SelectedIndexChanged += addIn;
            tpOut.Controls.Add(newcb);
            #endregion

            if (type == Type.pos)
            {
                Master.properties.Controls.Find("numericUpDown1", false)[0].Enabled = true;
                ((NumericUpDown)Master.properties.Controls.Find("numericUpDown1", false)[0]).ValueChanged
                    += valueChanged;
            }
            else
                Master.properties.Controls.Find("numericUpDown1", false)[0].Enabled = false;


            Master.properties.Enabled = false;
            Master.properties.Enabled = true;
        }

        private void addIn(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            List<TPClass> all = new List<TPClass>();
            all.AddRange(Info.JCList.GetRange(0, Info.JCList.Count));
            all.AddRange(Info.PCList.GetRange(0, Info.PCList.Count));
            //PanelMaster.ss.Text += ";";
            #region dell
            if(cb.SelectedItem != null)
            if (cb.SelectedItem.ToString() == "DELETE")
            {
                if (cb.Parent.Name == "tabPage1") InList.Clear();
                else OutList.Clear();

                foreach (ComboBox c in cb.Parent.Controls)
                {
                    if (c.Text != "DELETE")
                    {
                        foreach (TPClass item in all)
                        {
                            if (c.SelectedItem == null) continue;
                            if ((c.SelectedItem.ToString() == item.name.Text) && (type != item.type))
                                if (c.Parent.Name == "tabPage1")
                                {
                                    InList.Add(item);
                                }
                                else
                                {
                                    OutList.Add(item);
                                }
                        }
                    }
                    else 
                    {
                        cb.Parent.Controls[cb.Parent.Controls.Count - 1].Dispose();
                        Point loc = cb.Parent.Controls[cb.Parent.Controls.Count - 1].Location;
                        cb.Parent.Controls[cb.Parent.Controls.Count - 1].Location = 
                            cb.Parent.Controls[cb.Parent.Controls.GetChildIndex(c)].Location;
                        cb.Parent.Controls[cb.Parent.Controls.GetChildIndex(c)].Location = loc;
                        cb.SelectedItem = null;
                        for(int i = 0; i < all.Count; i++)
                            if (all[i].name.Text == cb.Tag.ToString())
                            {
                                if (all[i].InList.Contains(this))
                                    all[i].InList.Remove(this);
                                else
                                    all[i].OutList.Remove(this);
                                break;
                            }
                    }
                }

            }
            else
            #endregion
            #region add
            {
                cb.Tag = cb.SelectedItem.ToString();
                foreach (TPClass item in all)
                {
                    if ((cb.SelectedItem.ToString() == item.name.Text) && (type != item.type))
                        if (cb.Parent.Name == "tabPage1")
                        {
                            InList.Add(item);
                            item.OutList.Add(this);
                        }
                        else
                        {
                            OutList.Add(item);
                            item.InList.Add(this);
                        }
                }
                #region new combo box
                string[] list;
                if (type == Type.pos)
                {
                    list = new string[Info.JCList.Count+1];
                    foreach (JumpClass sub in Info.JCList)
                        list[Info.JCList.IndexOf(sub)] = sub.name.Text;
                    list[Info.JCList.Count] = "DELETE";
                }
                else
                {
                    list = new string[Info.PCList.Count+1];
                    foreach (PositionClass sub in Info.PCList)
                        list[Info.PCList.IndexOf(sub)] = sub.name.Text;
                    list[Info.PCList.Count] = "DELETE";
                }
                ComboBox newcb = new ComboBox();
                newcb.Location = new Point(0, newcb.Height * (type==Type.pos?InList.Count: OutList.Count));
                newcb.Items.AddRange(list);
                newcb.SelectedIndexChanged += addIn;
                cb.Parent.Controls.Add(newcb);
                #endregion
            }
            #endregion

            Master.properties.Controls.Find("label3", false)[0].Text = "In: " + InList.Count;
            Master.properties.Controls.Find("label4", false)[0].Text = "Out: " + OutList.Count;
        }

        private void valueChanged(object sender, EventArgs e)
        {
            points = (int)(sender as NumericUpDown).Value;
            name.Text = name.Text.Remove(name.Text.IndexOf('(') + 1, 
                name.Text.Length - name.Text.IndexOf('(') - 1);
            
            name.Text += points.ToString() + ")"; 
            
        }

        #region moving
        private void OnMouseDown(object sender, KeyEventArgs mevent)
        {
            if(mevent.Control)
            IsDragMode = true;

            //PanelMaster.ss.Text = "down" + IsDragMode.ToString();
        }

        private void OnMouseUp(object sender, KeyEventArgs mevent)
        {
            if(!mevent.Control)
            IsDragMode = false;
            //PanelMaster.ss.Text = "up" + IsDragMode.ToString();
        }

        private void OnMouseMove(object sender, MouseEventArgs mevent)
        {
            if (IsDragMode)
            {
                obj.Location = new Point(mevent.X - 12, mevent.Y - 12);
                icon.Location = new Point(mevent.X - 12, mevent.Y - 12);
                name.Location = new Point(mevent.X - 12, mevent.Y - 12 + 26);
            }
        }
        #endregion
    }
}
