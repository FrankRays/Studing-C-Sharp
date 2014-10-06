using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba2
{
    [Serializable()]
    public class SportType : SportObject
    {
        public SportType(string name) : base(name)
        {
        }

        public override void UpdateIt(SportObject sn, AllData data)
        {
            SportType stype = sn as SportType;

            foreach (SportObject st in data.GetSportList)
            {
                if (st is SportTournament)
                    if ((st as SportTournament).SportType == Name)
                        (st as SportTournament).SportType = stype.Name;
            }

            Name = stype.Name;
        }

        public override SportObject RemoveIt(AllData data)
        {
            foreach (SportObject st in data.GetSportList)
            {
                if(st is SportTournament)
                if ((st as SportTournament).SportType == Name)
                {
                    return st;
                }
            }
            return null;
        }
    }
}
