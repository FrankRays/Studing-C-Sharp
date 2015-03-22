using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SportManager
{
    [Serializable()]
    public class AllData
    {
        static string CurrentFile = "data.extension";
        static AllData allData;

        List<SportObject> allSportObjects;

        private AllData() 
        {
            allSportObjects = new List<SportObject>();
        }

        public static AllData Instance
        {
            get
            {
                if (allData == null)
                    allData = Deserialize();
                return allData;
            }
        }

        public void Serialize()
        {
            FileStream FS = new FileStream(CurrentFile, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            new BinaryFormatter().Serialize(FS, this);
            FS.Close();
            allData = this;
        }

        static AllData Deserialize()
        {
            if (!File.Exists(CurrentFile))
            {
                MessageBox.Show("file with data is missing");
                return new AllData();
            }
            FileStream FS = new FileStream(CurrentFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            var data = new BinaryFormatter().Deserialize(FS);
            FS.Close();
            return data as AllData;
        }


        /////////////////////////////////////////////////////////
        public void Add(SportObject so)
        {
            foreach (SportObject sto in allSportObjects)
            {
                if (sto.Equals(so))
                {
                    MessageBox.Show("This name already exists");
                    return;
                }
            }

            allSportObjects.Add(so);
        }

        public void Remove(SportObject so)
        {
            if (so == null)
                return;
            foreach (SportObject sto in allSportObjects)
            {
                if (sto.Equals(so))
                {
                    for (SportObject r = sto.RemoveIt(this); r != null; r = sto.RemoveIt(this))
                    {
                        Remove(r);
                    }
                    allSportObjects.Remove(sto);
                    break;
                }
            }
        }

        public void Edit(SportObject so, SportObject newso)
        {
            foreach (SportObject sto in allSportObjects)
            {
                if (sto.Equals(so))
                {
                    sto.UpdateIt(newso, this);
                    break;
                }
            }
        }

        public SportObject GetByName(string name)
        {
            foreach (SportObject so in allSportObjects)
                if (so.Name.Equals(name))
                    return so;
            return null;
        }

        public List<SportObject> GetSportList
        {
            get { return allSportObjects; }
        }
    }
}
