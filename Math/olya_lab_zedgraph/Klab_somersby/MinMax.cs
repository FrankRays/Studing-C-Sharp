using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Klab_somersby
{
    class MinMax
    {
        public static double min_X;
        public static double max_X;
        public static double min_Y;
        public static double max_Y;

        public static void Refresh(PointPairList list)
        {
            min_Y = min_X = Double.MaxValue;
            max_Y = max_X = Double.MinValue;

            foreach (PointPair pp in list)
            {
                if (min_X > pp.X)
                    min_X = pp.X;
                if (max_X < pp.X)
                    max_X = pp.X;

                if (min_Y > pp.Y)
                    min_Y = pp.Y;
                if (max_Y < pp.Y)
                    max_Y = pp.Y;
            }
            min_Y = (min_Y == Double.MaxValue) ? 0 : min_Y;
            min_X = (min_X == Double.MaxValue) ? 0 : min_X;

            max_Y = (max_Y == Double.MinValue) ? 0 : max_Y;
            max_X = (max_X == Double.MinValue) ? 0 : max_X;
        }
    }
}
