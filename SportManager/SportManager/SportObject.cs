using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportManager
{
    [Serializable()]
    public class SportObject : ISport
    {
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public SportObject(string name)
        {
            this.name = name;
        }

        public virtual void UpdateIt(SportObject sn, AllData data) { }
        public virtual SportObject RemoveIt(AllData data) { return null; }

        public virtual ListViewItem ToListViewItem()
        {
            return new ListViewItem(name);
        }

        public override bool Equals(object obj)
        {
            return GetType().Equals(obj.GetType()) && name.Equals((obj as SportObject).Name);
        }
    }
}
