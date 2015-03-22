using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;

namespace Klab_somersby
{
    class MyParabola
    {
        double a, b, c;

        double det(double[,] xk, int size)
        {
            int l;
            double d;
            double sum11 = 1, sum12 = 0, sum21 = 1, sum22 = 0;

            for (int i = 0; i < size; i++)
            {
                sum11 = 1;
                l = 2 * size - 1 - i;
                sum21 = 1;
                for (int j = 0; j < size; j++)
                {
                    sum21 *= xk[j, l % size];
                    l--;
                    sum11 *= xk[j, (j + i) % (size)];
                }
                sum22 += sum21;
                sum12 += sum11;
            }
            d = sum12 - sum22;
            return d;
        }

        public void СalculateСoefficients(PointPairList list)
        {
            double sumX, sumY, sumX2, sumX3, sumX4, sumXY, sumX2Y;
            sumX = sumY = sumX2 = sumX3 = sumX4 = sumXY = sumX2Y = 0;
            for (int i = 0; i < list.Count; i++)
            {
                double x = list[i].X;
                double y = list[i].Y;
                sumX += x;
                sumY += y;
                sumXY += x * y;
                sumX2 += x * x;
                sumX3 += x * x * x;
                sumX4 += x * x * x * x;
                sumX2Y += x * x * y;
            }
            double[,] matr = new double[3, 3];
            matr[0, 0] = list.Count; matr[1, 0] = sumX; matr[2, 0] = sumX2;
            matr[0, 1] = sumX; matr[1, 1] = sumX2; matr[2, 1] = sumX3;
            matr[0, 2] = sumX2; matr[1, 2] = sumX3; matr[2, 2] = sumX4;

            double det0 = det(matr, 3);

            double[] p = new double[3];

            for (int i = 0; i < 3; i++)
            {
                double[,] matrx = new double[3, 3];
                matrx = (double[,])matr.Clone();

                matrx[i, 0] = sumY;
                matrx[i, 1] = sumXY;
                matrx[i, 2] = sumX2Y;

                p[i] = det(matrx, 3) / det0;
            }

            a = p[0];
            b = p[1];
            c = p[2];
        }

        public double СalculateY(double x)
        {
            return a + b * x + c * x * x;
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

        public double koef_C
        {
            get
            {
                return c;
            }
        }
    }
}
