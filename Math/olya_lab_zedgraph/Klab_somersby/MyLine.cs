using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Klab_somersby
{
    class MyLine
    {
        double a, b;

        public void СalculateСoefficients(PointPairList list)
        {
            double sumX, sumY, sumXY, sumXX;
            sumX = sumY = sumXY = sumXX = 0;
            foreach(PointPair pp in list)
            {
                sumX += pp.X;
                sumY += pp.Y;
                sumXY += pp.X * pp.Y;
                sumXX += pp.X * pp.X;
            }
            a = (sumXY - (sumXX * sumY) / sumX) / (sumX - (sumXX * list.Count) / sumX);
            b = (sumY - a * list.Count) / sumX;
        }

        public double СalculateY(double x)
        {
            return b * x + a;
        }

        public double koef_A
        {
            get 
            {
                return a;
            }
        }

        public double koef_B
        {
            get
            {
                return b;
            }
        }
    }
}
