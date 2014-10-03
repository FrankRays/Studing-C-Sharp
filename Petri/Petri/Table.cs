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
    public partial class Table : Form
    {
       static string name;

        private static bool FoundItem(TPClass tpc)
        {
            if (name == tpc.name.Text)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Table()
        {
            InitializeComponent();

            for(int i = 0; i < Info.PCList.Count; i++)
            {
                for(int j = 0; j < Info.JCList.Count; j++)
                {
                    if (!dataGridView1.Columns.Contains(Info.JCList[j].name.Text)) 
                        dataGridView1.Columns.Add(Info.JCList[j].name.Text, Info.JCList[j].name.Text);
                    if (dataGridView1.Rows.Count <= i) dataGridView1.Rows.Add();
                    if (Info.JCList[j].OutList.Contains(Info.PCList[i]))
                    {
                        name = Info.PCList[i].name.Text;
                        dataGridView1.Rows[i].Cells[j].Value =
                            Info.JCList[j].OutList.FindAll(FoundItem).Count.ToString();
                    }
                    else
                        dataGridView1.Rows[i].Cells[j].Value = "0";
                }
                dataGridView1.Rows[i].HeaderCell.Value = Info.PCList[i].name.Text;
            }

            for (int i = 0; i < Info.JCList.Count; i++)
            {
                for (int j = 0; j < Info.PCList.Count; j++)
                {
                    if (!dataGridView2.Columns.Contains(Info.PCList[j].name.Text))
                        dataGridView2.Columns.Add(Info.PCList[j].name.Text, Info.PCList[j].name.Text);
                    if (dataGridView2.Rows.Count <= i) 
                        dataGridView2.Rows.Add();
                    if (Info.PCList[j].OutList.Contains(Info.JCList[i]))
                    {
                        name = Info.JCList[i].name.Text;
                        dataGridView2.Rows[i].Cells[j].Value =
                            Info.PCList[j].OutList.FindAll(FoundItem).Count.ToString();
                    }
                    else
                        dataGridView2.Rows[i].Cells[j].Value = "0";
                }
                dataGridView2.Rows[i].HeaderCell.Value = Info.JCList[i].name.Text;
            }
        }
    }
}
