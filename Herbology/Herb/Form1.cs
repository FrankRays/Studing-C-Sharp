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

namespace Herb
{
    public partial class Form1 : Form
    {
        List<El> list;

        public Form1()
        {
            InitializeComponent();
            list = new List<El>();
            getdata();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            sort();
        }


        void getdata()
        {
            StreamReader sr = new StreamReader("list.txt", true);
            StreamReader sr2 = new StreamReader("short.txt", true);

            string description = new StreamReader("description.txt").ReadToEnd();

            string next = sr.ReadLine();
            while (!sr.EndOfStream)
            {
                El el = new El();
                el.name = next;
                el.shortd = sr2.ReadLine();

                next = sr.ReadLine();

                int d = description.IndexOf(next);

                if (d == -1)
                {
                    MessageBox.Show(next);
                    continue;
                }

                el.Set(description.Remove(d));
                description = description.Remove(0, d);

                list.Add(el);
                listView1.Items.Add(el.Get());
            }
            El elast = new El();
            elast.name = next;
            elast.shortd = sr2.ReadLine();
            elast.Set(description);

            list.Add(elast);
            listView1.Items.Add(elast.Get());


        }

        void sort()
        {
            if (comboBox1.SelectedItem == null ||
                comboBox2.SelectedItem == null ||
                comboBox3.SelectedItem == null) return;
            listView1.Items.Clear();
            string s1 = comboBox1.SelectedItem.ToString();
            string s2 = comboBox2.SelectedItem.ToString();
            string s3 = comboBox3.SelectedItem.ToString();

            foreach (El el in list)
            {
                if (el.CheckAll(s1, s2, s3))
                    listView1.Items.Add(el.Get());
            }

            this.Text = listView1.Items.Count.ToString() + " из " + list.Count.ToString();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            foreach(El el in list)
                if(el.name == listView1.SelectedItems[0].Text)
                    MessageBox.Show(el.about);
        }

        private void newsort(object sender, EventArgs e)
        {
            sort();
        }
    }
}
