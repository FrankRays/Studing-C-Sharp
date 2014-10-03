using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chm_rgr
{
    public static class Extension
    {
        public static string MatrixToString(this double[,] m)
        {
            string s = "";

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                    s += m[i, j].ToString("00.00") + "\t";
                s += Environment.NewLine;
            }
            return s;
        }

        public static string ArrayToString(this double[] m)
        {
            string s = "";

            for (int j = 0; j < m.Length; j++)
                s += m[j].ToString() + "\t";

            return s;
        }

        public static double[] GetRow(this double[,] m, int r)
        {
            int l = m.GetLength(1);
            return m.Cast<double>().Skip(l * r).Take(l).ToArray();
        }

        public static double[][] ToJuggedArray(this double[,] m)
        {
            int row_n = m.GetLength(0);
            int col_n = m.GetLength(1);

            double[][] a = new double[row_n][];

            for (int i = 0; i < row_n; i++)
            {
                a[i] = new double[col_n];
                for (int j = 0; j < col_n; j++)
                {
                    a[i][j] = m[i, j];
                }
            }

            return a;
        }

        public static double[] GetColumn(this double[,] m, int r)
        {
            return m.ToJuggedArray().Select(row => row[r]).ToArray();
        }


        public static double[] GetRowFrom(this double[,] m, int r, int start_index)
        {
            int l = m.GetLength(1);
            return m.Cast<double>().Skip(l * r + start_index).Take(l - start_index).ToArray();
        }

        public static void InsertRow(this double[,] m, double[] d, int r)
        {
            for (int i = 0; i < d.Length; i++)
                m[r, i] = d[i];
        }

        public static void InsertRowAt(this double[,] m, double[] d, int r, int start_index)
        {
            for (int i = 0; i < d.Length; i++)
                m[r, start_index + i] = d[i];
        }

        public static double[,] AddColumn(this double[,] m, double[] col)
        {
            int row_n = m.GetLength(0);
            int col_n = m.GetLength(1);

            double[,] n = new double[row_n, col_n + 1];

            for (int i = 0; i < row_n; i++)
            {
                for (int j = 0; j < col_n; j++)
                    n[i, j] = m[i, j];
                n[i, col_n] = col[i];
            }

            m = n;

            return n;
        }

        public static double[] Plus(this double[] a, double[] b)
        {
            for (int i = 0; i < a.Length; i++)
                a[i] += b[i];

            return a;
        }

        public static double[,] Menor(this double[,] m, int a, int b)
        {
            int i, j, p, q;
            int row = m.GetLength(0) - 1;

            double[,] MEN = new double[row, row];

            for (j = 0, q = 0; q < row; j++, q++)
                for (i = 0, p = 0; p < row; i++, p++)
                {
                    if (i == a) i++;
                    if (j == b) j++;
                    MEN[p, q] = m[i, j];
                }
            return MEN;
        }

        public static double Det(this double[,] m)
        {
            int n;
            int sign;
            double det = 0;
            int row = m.GetLength(0);

            if (row == 1)
                return m[0, 0];
            else if (row == 2)
            {
                return m[0, 0] * m[1, 1] - m[1, 0] * m[0, 1];
            }
            else
                for (n = 0; n < row; n++)
                {
                    if ((n & 1) == 0)
                        sign = 1;
                    else
                        sign = -1;
                    det += sign * m[0, n] * Det(m.Menor(0, n));
                }

            return det;
        }

        public static double[,] MatrixAD(this double[,] m)
        {
            int row = m.GetLength(0);
            double[,] mad = new double[row, row];

            for (int i = 0; i < row; i++)
                for (int j = 0; j < row; j++)
                {
                    mad[i, j] = m.Menor(i, j).Det();
                }

            return mad;
        }

        public static double[,] ConnectedMatrix(this double[,] m)
        {
            int row = m.GetLength(0);
            double[,] con = new double[row, row];

            for (int i = 0; i < row; i++)
                for (int j = 0; j < row; j++)
                    con[i, j] = m[j, i];

            return con;
        }

        public static double[,] InverseMatrix(this double[,] m, double det)
        {
            double[,] mad = m.MatrixAD();
            double[,] con = mad.ConnectedMatrix();


            int row = m.GetLength(0);
            double[,] inv = new double[row, row];

            for (int i = 0; i < row; i++)
                for (int j = 0; j < row; j++)
                    inv[i, j] = con[i, j] / det;

            return inv;
        }
    }
}
