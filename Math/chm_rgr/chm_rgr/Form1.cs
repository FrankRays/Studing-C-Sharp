using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace chm_rgr
{
    public partial class Form1 : Form
    {
        int N = 1;

        public Form1()
        {
            InitializeComponent();
        }

        void Write(string str)
        {
            textBox1.Text += str + Environment.NewLine;
        }

        private void SelectSlar(object sender, EventArgs e)
        {
            N = int.Parse(((RadioButton)sender).Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        double[,] SolveTriangular(double[,] m)
        {
            int row_n = m.GetLength(0);
            int col_n = m.GetLength(1);

            double[] src = new double[col_n];
            double koef;

            for (int i = row_n - 1; i >= 0; i--)
            {
                src = m.GetRow(i);
                src = src.Select(e => e / src[i]).ToArray();

                m.InsertRow(src, i);

                for (int j = i - 1; j >= 0; j--)
                {
                    koef = m[j, i];
                    m[j, i] -= src[i] * koef;
                    m[j, row_n] -= src[row_n] * koef;
                }
            }
            return m;
        }

        double[,] SolveTTriangular(double[,] m)
        {
            int row_n = m.GetLength(0);
            int col_n = m.GetLength(1);

            double[] src = new double[col_n];
            double koef;

            for (int i = 0; i < row_n; i++)
            {
                src = m.GetRow(i);
                src = src.Select(e => e / src[i]).ToArray();

                m.InsertRow(src, i);

                for (int j = i+1; j < row_n; j++)
                {
                    koef = m[j, i];
                    m[j, i] -= src[i] * koef;
                    m[j, row_n] -= src[row_n] * koef;
                }
            }
            return m;
        }

        //Класичний метод Гаусса
        private void button1_Click(object sender, EventArgs arg)
        {
            double[,] slar = InputData.GetSlar(N);
            Write("-------------------Initial slar---------------------------");
            Write(slar.MatrixToString());

            int row_n = slar.GetLength(0);
            int col_n = slar.GetLength(1);

            double[] src = new double[col_n];

            double koef;

            for (int i = 0; i < row_n; i++)
            {
                src = slar.GetRow(i);
                src = src.Select(e => e / src[i]).ToArray();
                
                for (int j = i+1; j < row_n; j++)
                {
                    koef = slar[j, i];
                    
                    for (int y = i; y < col_n; y++)
                        slar[j, y] -= src[y] * koef;
                }
            }
            Write("-------------------Triangular feed---------------------------");
            double det = 1;
            for (int i = 0; i < row_n; i++)
                det *= slar[i, i];

            Write(slar.MatrixToString());

            slar = SolveTriangular(slar);
            Write("-------------------Answer-------------------------------------");
            Write(slar.MatrixToString());
            Write("-------------------Det----------------------------------------");
            Write(det.ToString());
            Write("-------------------A^(-1)----------------------------------------");
            Write(InputData.GetSlar(N).InverseMatrix(det).MatrixToString());
        }

        double GetC(double a, double b)
        {
            return a / Math.Sqrt(a * a + b * b);
        }

        double GetS(double a, double b)
        {
            return b / Math.Sqrt(a * a + b * b);
        }

        //LT-метод обертань
        private void button3_Click(object sender, EventArgs arg)
        {
            double[,] slar = InputData.GetSlar(N);
            Write("-------------------Initial slar---------------------------");
            Write(slar.MatrixToString());

            int row_n = slar.GetLength(0);
            int col_n = slar.GetLength(1);

            double[] src = new double[col_n];
            double[] next = new double[col_n];

            double c, s;

            double[] a, b;

            for (int j = 0; j < row_n; j++)
            {
                for (int i = j+1; i < row_n; i++)
                {
                    src = slar.GetRowFrom(j, j);
                    next = slar.GetRowFrom(i, j);

                    c = GetC(src[0], next[0]);
                    s = GetS(src[0], next[0]);

                    a = src.Select(e => e * c).ToArray();
                    b = next.Select(e => e * s).ToArray();
                    slar.InsertRowAt(a.Plus(b), j, j);

                    a = src.Select(e => e * -s).ToArray();
                    b = next.Select(e => e * c).ToArray();
                    slar.InsertRowAt(a.Plus(b), i, j);
                }
            }

            Write("-------------------Triangular feed---------------------------");
            double det = 1;
            for (int i = 0; i < row_n; i++)
                det *= slar[i, i];
            Write(slar.MatrixToString());
            slar = SolveTriangular(slar);
            Write("-------------------Answer---------------------------");
            Write(slar.MatrixToString());
            Write("-------------------Det----------------------------------------");
            Write(det.ToString());
            Write("-------------------A^(-1)----------------------------------------");
            Write(InputData.GetSlar(N).InverseMatrix(det).MatrixToString());
        }

        double Sum(double[,] U, int i, int j)
        {
            int row_n = U.GetLength(0);
            int col_n = U.GetLength(1);
            double s = 0;

            for (int k = 0; k < i; k++)
            {
                s += U[k, i] * U[k, j];
            }

            return s;
        }

        //Метод квадратних коренів
        private void button4_Click(object sender, EventArgs e)
        {
            double[,] A = InputData.GetA(N);
            Write("-------------------Initial slar---------------------------");
            Write(A.MatrixToString());

            int row_n = A.GetLength(0);
            int col_n = A.GetLength(1);

            double[,] U = new double[col_n, row_n];
            double[] Y = new double[col_n];

            double det = 1;

            for (int i = 0; i < row_n; i++)
            {
                U[i, i] = Math.Sqrt(A[i, i] - Y[i]);
                det *= U[i, i] * U[i, i];
                Y[i] += U[i, i] * U[i, i];
                for (int j = i + 1; j < col_n; j++)
                {
                    U[i, j] = (A[i, j] - Sum(U,i,j)) / U[i, i];
                    Y[j] += U[i, j] * U[i, j];
                }
            }
            Write("-------------------U--------------------------------------");
            Write(U.MatrixToString());

            double[,] Ut = U.ConnectedMatrix();
            Write("-------------------Ut--------------------------------------");
            Write(Ut.MatrixToString());

            double[] B = InputData.GetB(N);

            Ut = Ut.AddColumn(B);
            Ut = SolveTTriangular(Ut);

            Write("-------------------Ut solved--------------------------------------");
            Write(Ut.MatrixToString());

            U = U.AddColumn(Ut.GetColumn(col_n));
            U = SolveTriangular(U);
            Write("-------------------U solved--------------------------------------");
            Write(U.MatrixToString());
            Write("-------------------Det----------------------------------------");
            Write(det.ToString());
            Write("-------------------A^(-1)----------------------------------------");
            Write(InputData.GetSlar(N).InverseMatrix(det).MatrixToString());
        }

        //Метод прогонки
        private void button5_Click(object sender, EventArgs e)
        {
            double[,] A = InputData.GetSlar(N);
            Write("-------------------Initial slar---------------------------");
            Write(A.MatrixToString());

            int row_n = A.GetLength(0);
            int col_n = A.GetLength(1);

            double[] P, Q, X;
            P = new double[row_n];
            Q = new double[row_n];
            X = new double[row_n];
            row_n--;

            double det;

            int i = 0;
            P[i] = - A[i, 1] / A[i, i];
            Q[i] = A[i, col_n - 1] / A[i, i];
            det = A[i, i];

            for (i = 1; i < row_n; i++)
            {
                P[i] = A[i, i + 1] / (-A[i, i] - P[i - 1] * A[i, i - 1]);
                Q[i] = (A[i, i - 1] * Q[i - 1] - A[i, col_n - 1]) / (-A[i, i] - P[i - 1] * A[i, i - 1]);
                det *= (-A[i, i] - P[i - 1] * A[i, i - 1]);
            }
            X[row_n] = Q[row_n] = (A[i, i - 1] * Q[i - 1] - A[i, col_n - 1]) / (-A[i, i] - P[i - 1] * A[i, i - 1]);
            det *= (-A[i, i] - P[i - 1] * A[i, i - 1]);
            det = -det;
            for (i-- ; i >= 0; i--)
            {
                X[i] = P[i] * X[i + 1] + Q[i];
            }
            Write("-------------------P---------------------------");
            Write(P.ArrayToString());
            Write("-------------------Q---------------------------");
            Write(Q.ArrayToString());
            Write("-------------------X---------------------------");
            Write(X.ArrayToString());
            Write("-------------------Det----------------------------------------");
            Write(det.ToString());
            Write("-------------------A^(-1)----------------------------------------");
            Write(InputData.GetSlar(N).InverseMatrix(det).MatrixToString());
        }

        //Метод Шульца другого порядку
        private void button6_Click_1(object sender, EventArgs e)
        {

        }



        
    
        
        
    
    }
}
