using Laba2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LAB2
{
    public partial class Form1 : Form
    {
        string Selected;

        public Form1()
        {
            InitializeComponent();
            Selected = null;
            filltables();
        }

        void filltables()
        {
            listView4.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            listView1.Items.Clear();

            ListView target = null;
            foreach (SportObject so in AllData.Instance.GetSportList)
            {
                if (so is SportMember)
                    target = listView1;
                else if (so is SportType)
                    target = listView2;
                else if (so is SportTournament)
                {
                    SportTournament st = so as SportTournament;
                    if (st.Type == TournamentType.Cup)
                        target = listView3;
                    else if (st.Type == TournamentType.Championship)
                        target = listView4;
                }
                target.Items.Add(so.ToListViewItem());
            }                
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddSport AS = new AddSport(tabControl1.SelectedIndex);
            if (AS.ShowDialog() == DialogResult.OK)
            {
                AllData.Instance.Add(AS.Result());
                filltables();
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                if (MessageBox.Show("Delete \"" + Selected + "\" Are you sure?", "Warning", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                {
                    AllData.Instance.Remove(AllData.Instance.GetByName(Selected));
                    Selected = null;
                    filltables();
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                AddSport AS = new AddSport(AllData.Instance.GetByName(Selected));
                if (AS.ShowDialog() == DialogResult.OK)
                {
                    AllData.Instance.Edit(AllData.Instance.GetByName(Selected), AS.Result());
                    Selected = null;
                    filltables();
                }
            }
        }

        private void check(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Selected = e.Item.Text;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AllData.Instance.Serialize();
        }
    }
}
