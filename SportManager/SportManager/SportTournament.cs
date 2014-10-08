using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportManager
{
    public enum TournamentType
    {
        Cup,
        Championship
    }

    [Serializable()]
    public class SportTournament : SportObject
    {
        DateTime date;
        TournamentType type;
        string sportType;

        public SportTournament(string name):base(name)
        {
        }

        public TournamentType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
            set
            {
                date = value;
            }
        }

        public string SportType
        {
            get
            {
                return sportType;
            }
            set
            {
                sportType = value;
            }
        }

        public override void UpdateIt(SportObject sn, AllData data)
        {
            SportTournament st = sn as SportTournament;
            foreach (SportObject sm in data.GetSportList)
            {
                if (sm is SportMember)
                    if ((sm as SportMember).SportTournament == Name)
                    {
                        (sm as SportMember).SportTournament = st.Name;
                    }
            }

            Name = st.Name;
            type = st.Type;
            sportType = st.SportType;
            date = st.Date;
        }

        public override SportObject RemoveIt(AllData data)
        {
            foreach (SportObject sm in data.GetSportList)
            {
                if (sm is SportMember)
                    if ((sm as SportMember).SportTournament == Name)
                    {
                        return sm;
                    }
            }
            return null;
        }

        public override ListViewItem ToListViewItem()
        {
            return new ListViewItem(new string[] { Name, Date.ToLongDateString(), sportType });
        }
    }
}
