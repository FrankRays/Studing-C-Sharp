using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace lvl_editor
{
    public partial class Form1 : Form
    {
        List<EnemyObj> obj_list;
        EnemyObj it_obj;

        PictureBox selected;
        PictureBox editable;
        Point xy;

        int count;

        public Form1()
        {
            InitializeComponent();

            obj_list = new List<EnemyObj>();
            count = 0;
        }

        private void add_enemy(object sender, EventArgs e)
        {
            if (selected == null) return;
            PictureBox mini_pic = new PictureBox();
            count++;
            mini_pic.Image = selected.Image;
            mini_pic.SizeMode = PictureBoxSizeMode.StretchImage;
            mini_pic.Width = 40;
            mini_pic.Height = 40;
            mini_pic.Location = xy;
            mini_pic.Click += select_enemy;
            mini_pic.Tag = count;

            EnemyObj en_obj = new EnemyObj(mini_pic, count);

            en_obj.properties.x = xy.X;
            en_obj.properties.y = xy.Y;
            en_obj.properties.type = int.Parse(selected.Tag.ToString());

            obj_list.Add(en_obj);

            panel1.Controls.Add(mini_pic);            
        }

        private void get_mouse_xy(object sender, MouseEventArgs e)
        {
            xy = e.Location;
            toolStripStatusLabel1.Text = "x=" + xy.X.ToString();
            toolStripStatusLabel2.Text = "y=" + xy.Y.ToString();
        }

        private void select_enemy_type(object sender, EventArgs e)
        {
            if (selected != null) selected.BorderStyle = BorderStyle.None;
            if (selected == (PictureBox)sender)
            {
                selected.BorderStyle = BorderStyle.None;
                selected = null;
                return;
            }
            selected = (PictureBox)sender;
            selected.BorderStyle = BorderStyle.FixedSingle;
            if (editable != null) editable.BorderStyle = BorderStyle.None;
        }

        private void select_enemy(object sender, EventArgs e)
        {
            if (editable != null) editable.BorderStyle = BorderStyle.None;
            editable = (PictureBox)sender;
            editable.BorderStyle = BorderStyle.FixedSingle;

            if (selected != null)
            {
                selected.BorderStyle = BorderStyle.None;
                selected = null;
            }

            foreach(EnemyObj it in obj_list)
                if(it.namber == (int)editable.Tag) it_obj = it;

            numericUpDown1.Text = it_obj.properties.hp.ToString();
            numericUpDown2.Text = it_obj.properties.bonus.ToString();

            numericUpDown3.Text = it_obj.properties.fire_delay.ToString();
            numericUpDown4.Text = it_obj.properties.delta.ToString();

            numericUpDown5.Text = it_obj.properties.speedx.ToString();
            numericUpDown6.Text = it_obj.properties.speedy.ToString();

            numericUpDown7.Text = it_obj.properties.x.ToString();
            numericUpDown8.Text = it_obj.properties.y.ToString();
        }

        private void edit_value(object sender, EventArgs e)
        {
            int val = int.Parse(((NumericUpDown)sender).Tag.ToString());
            switch ( val )
            {
                case 1: it_obj.properties.hp = (int)((NumericUpDown)sender).Value; break;
                case 2: it_obj.properties.bonus = (int)((NumericUpDown)sender).Value; break;
                case 3: it_obj.properties.fire_delay = (int)((NumericUpDown)sender).Value; break;
                case 4: it_obj.properties.delta = (int)((NumericUpDown)sender).Value; break;
                case 5: it_obj.properties.speedx = (int)((NumericUpDown)sender).Value; break;
                case 6: it_obj.properties.speedy = (int)((NumericUpDown)sender).Value; break;
                case 7: it_obj.properties.x = (int)((NumericUpDown)sender).Value; break;
                case 8: it_obj.properties.y = (int)((NumericUpDown)sender).Value; break;
            }
        }

        private void delete(object sender, EventArgs e)
        {
            if (it_obj != null)
            {
                obj_list.Remove(it_obj);
                panel1.Controls.Remove(editable);
            }

        }

        private void save(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void save_true(object sender, CancelEventArgs e)
        {
            StreamWriter wrt = new StreamWriter(saveFileDialog1.FileName);

            foreach (EnemyObj it in obj_list)
            {
                wrt.WriteLine(it.properties.type);

                wrt.WriteLine(it.properties.x);
                wrt.WriteLine(it.properties.y);

                wrt.WriteLine(it.properties.speedx);
                wrt.WriteLine(it.properties.speedy);

                wrt.WriteLine(it.properties.hp);
                wrt.WriteLine(it.properties.bonus);

                wrt.WriteLine(it.properties.fire_delay);
                wrt.WriteLine(it.properties.delta); 
            }
            wrt.Close();
        }

        private void open(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void open_true(object sender, CancelEventArgs e)
        {
            StreamReader rdr = new StreamReader(openFileDialog1.FileName);

            while (!rdr.EndOfStream)
                                                //foreach (EnemyObj it in obj_list)
            {
                EnemyObj it;
                PictureBox mini_pic = new PictureBox();
                count++;
                int type = int.Parse(rdr.ReadLine());
                switch(type)
                {
                    case 1: mini_pic.Image = pictureBox1.Image; break;
                    case 2: mini_pic.Image = pictureBox2.Image; break;
                    case 3: mini_pic.Image = pictureBox3.Image; break;
                }
                mini_pic.SizeMode = PictureBoxSizeMode.StretchImage;
                mini_pic.Width = 40;
                mini_pic.Height = 40;
                mini_pic.Tag = count;
                mini_pic.Click += select_enemy;
                int x = int.Parse(rdr.ReadLine());
                int y = int.Parse(rdr.ReadLine());

                mini_pic.Location = new Point(x, y);

                it = new EnemyObj(mini_pic, count);
                panel1.Controls.Add(mini_pic);      

                it.properties.type = type;
                it.properties.x = x;
                it.properties.y = y;

                it.properties.speedx = int.Parse(rdr.ReadLine());
                it.properties.speedy = int.Parse(rdr.ReadLine());
                
                it.properties.hp = int.Parse(rdr.ReadLine());
                it.properties.bonus = int.Parse(rdr.ReadLine());
                
                it.properties.fire_delay = int.Parse(rdr.ReadLine());
                it.properties.delta = int.Parse(rdr.ReadLine());
                obj_list.Add(it);
            }
            rdr.Close();
        }

    }
}
