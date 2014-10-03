using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Herb
{
    class El
    {
        public string name;
        public string season;
        public string zone;
        public string place;
        public string chance;
        public string shortd;
        public string about;

        public ListViewItem Get()
        {
            return new ListViewItem(new string[] { name, season, zone, place, chance, shortd});
        }

        public void Set(string data)
        {
            about = data;
            chance = string.Concat(about[about.IndexOf("%") - 2],
                    about[about.IndexOf("%") - 1],
                    about[about.IndexOf("%")]);

            zone = about.Substring(about.IndexOf("Климатическая зона:") + "Климатическая зона:".Length,
                about.IndexOf("Местность:") - about.IndexOf("Климатическая зона:") - "Климатическая зона:".Length);

            place = about.Substring(about.IndexOf("Местность:") + "Местность:".Length,
                about.IndexOf("Приготовление:") - about.IndexOf("Местность:") - "Местность:".Length);

            season = about.Substring(about.IndexOf("Доступность:") + "Доступность:".Length,
                about.IndexOf("%") - 2 - about.IndexOf("Доступность:") - "Доступность:".Length);
        }

        public bool CheckSeason(string value)
        {
            return value == "Все" || season.Contains(value) || season.Contains("Постоянно");
        }

        public bool CheckZone(string value)
        {
            return value == "Все" || zone.Contains(value);
        }

        public bool CheckPlace(string value)
        {
            return value == "Все" || place.Contains(value) || season.Contains("Везде");
        }

        public bool CheckAll(string v1, string v2, string v3)
        {
            return CheckSeason(v1) && CheckZone(v2) && CheckPlace(v3);
        }
    }
}
