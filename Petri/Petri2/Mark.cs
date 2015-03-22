using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Petri2
{
    class Mark
    {
        public int n;
        public byte[] m;

        public int occurs;

        public List<Mark> ToMark;
        public List<Transaction> ToTransaction;


        public Mark(int N, List<Position> PList)
        {
            n = N;
            ToMark = new List<Mark>();
            ToTransaction = new List<Transaction>();
            SetByteArray(PList);
            occurs = 0;
        }

        public Mark(List<Transaction> PList)
        {
            m = new byte[PList.Count];
            int i = 0;
            foreach (Transaction p in PList)
            {
                m[i] = (byte)p.currT;
                i++;
            }
        }

        public void SetByteArray(byte[] arr)
        {
            m = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                m[i] = arr[i];
        }

        public void SetByteArray(List<Position> PList)
        {
            m = new byte[PList.Count];
            int i = 0;
            foreach (Position p in PList)
            {
                m[i] = (byte)p.points;
                i++;
            }
        }

        public string ToString()
        {
            string s = "M" + n + "{";

            foreach (byte b in m)
                s += "," + b.ToString();
            if(s.Contains(','))
                s = s.Remove(s.IndexOf(','), 1);
            s += "}";
            return s;
        }

        public bool Check(Mark mrk)
        {
            for (int i = 0; i < m.Length; i++)
            {
                if (m[i] != mrk.m[i])
                    return false;
            }
            return true;
        }

        public string Name
        {
            get
            {
                return "M" + n + " ";
            }
        }
        public string StringMark
        {
            get
            {
                string s = "";

                foreach (byte b in m)
                    s += "," + b.ToString();
                if (s.Contains(','))
                    s = s.Remove(s.IndexOf(','), 1);
                return s;
            }
        }
    }
}
