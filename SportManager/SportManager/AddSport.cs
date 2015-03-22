using SportManager;
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
    public partial class AddSport : Form
    {
        int tab;

        public AddSport(int i)
        {
            InitializeComponent();
            tab = i;
            tabControl1.SelectedIndex = i;

            foreach (SportObject so in AllData.Instance.GetSportList)
            {
                if(so is SportType)
                    comboBox1.Items.Add(so.Name);
                else if(so is SportTournament)
                    comboBox2.Items.Add(so.Name);
            }
        }
        public AddSport(SportObject so)
        {
            InitializeComponent();
            button1.Text = "Done)";
            if (so is SportType)
            {
                tabControl1.SelectedIndex = 0;
                tab = 0;
                textBox1.Text = so.Name;
            }
            else if (so is SportTournament)
            {
                tabControl1.SelectedIndex = 1;
                tab = 1;
                textBox2.Text = so.Name;

                foreach (SportObject st in AllData.Instance.GetSportList)
                    if (st is SportType)
                        comboBox1.Items.Add(st.Name);
                comboBox1.SelectedItem = ((SportTournament)so).SportType;

                dateTimePicker1.Value = ((SportTournament)so).Date;
                if (((SportTournament)so).Type == TournamentType.Cup)
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;
            }
            else if (so is SportMember)
            {
                tabControl1.SelectedIndex = 2;
                tab = 2;
                textBox3.Text = so.Name;

                foreach (SportObject st in AllData.Instance.GetSportList)
                    if (st is SportTournament)
                        comboBox2.Items.Add(st.Name);
                comboBox2.SelectedItem = ((SportMember)so).SportTournament;

                numericUpDown1.Value = ((SportMember)so).Age;
            }
        }

        private void Block(object sender, EventArgs e)
        {
            if(tab != -1)
                tabControl1.SelectedIndex = tab;
        }

        public SportObject Result()
        {
            SportObject so = null;
            
            switch (tab)
            {
                case 0:
                    so = new SportType(textBox1.Text);
                    break;
                case 1:
                    so = new SportTournament(textBox2.Text);
                    ((SportTournament)so).SportType = comboBox1.SelectedItem.ToString();
                    ((SportTournament)so).Type = (radioButton1.Checked) ? 
                        TournamentType.Cup : TournamentType.Championship;
                    ((SportTournament)so).Date = dateTimePicker1.Value;
                    break;
                case 2:
                    so = new SportMember(textBox3.Text);
                    ((SportMember)so).Age = (int)numericUpDown1.Value;
                    ((SportMember)so).SportTournament = comboBox2.SelectedItem.ToString();
                    ((SportMember)so).Gender = (true) ?
                        Gender.male : Gender.female;
                    break;
 
            }

            return so;
        }

        private void check(object sender, EventArgs e)
        {
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            (sender as RadioButton).Checked = true;
        }


    }
}
