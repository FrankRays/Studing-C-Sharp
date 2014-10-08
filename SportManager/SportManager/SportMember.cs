using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportManager
{
    public enum Gender
    {
        male,
        female
    }

    [Serializable()]
    public class SportMember : SportObject
    {
        int age;
        string sportTournament;
        Gender gender;

        public SportMember(string name) : base(name)
        {
        }
        
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }

        public string SportTournament
        {
            get
            {
                return sportTournament;
            }
            set
            {
                sportTournament = value;
            }
        }

        public Gender Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        public override void UpdateIt(SportObject sn, AllData data)
        {
            SportMember sm = sn as SportMember;
            age = sm.Age;
            sportTournament = sm.SportTournament;
            Name = sm.Name;
        }

        public override SportObject RemoveIt(AllData data)
        {
            return null;
        }

        public override ListViewItem ToListViewItem()
        {
            return new ListViewItem(new string[] { Name, age.ToString(), sportTournament });
        }
        
    }
}
