using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chm_lab_1.Methods
{
    class Lobachevskogo
    {
        double[] input_koef;
        double[] root_sign;

        string expression;

        int koef_count;
        int root_count;

        string koef2str(double[] koef)
        {
            string str = "";

            for (int k = 0; k < koef_count; k++)
            {
                str += "a" + k + " = " + koef[k].ToString();
                str += Environment.NewLine;
            }
            str += Environment.NewLine;
            return str;
        }

        string root2str(double[] root)
        {
            string str = "";

            for (int k = 1; k <= root_count; k++)
            {
                str += "a" + k + " = " + root[k - 1].ToString();
                str += Environment.NewLine;
            }
            str += Environment.NewLine;
            return str;
        }


        public Lobachevskogo(Dictionary<int, double> KoefDictionary, string expression)
        {
            this.expression = expression;

            input_koef = new double[KoefDictionary.Count];
            foreach (KeyValuePair<int, double> kp in KoefDictionary)
                this.input_koef[kp.Key] = kp.Value;

            koef_count = input_koef.Length;
            root_count = koef_count - 1;

            root_sign = new double[root_count];
            for (int k = 0; k < root_count; k++)
            {
                root_sign[k] = Math.Sign(-input_koef[k] / input_koef[k + 1]);
                if (root_sign[k] == 0)
                    root_sign[k] = 1;
            }
        }

        public Dictionary<int, double> Roots(double accuracy = 1)
        {

            Log.WriteLn("Метод Лобачевского:");
            Log.WriteLn("Коэфициенты начального уровнения:");
            Log.WriteLn(koef2str(input_koef));
            Log.WriteLn("Знаки корней:");
            Log.WriteLn(root2str(root_sign));
            Log.WriteLn("Точность: " + accuracy);

            #region Квадрування

            double[] current_koef = new double[koef_count];
            input_koef.CopyTo(current_koef, 0);
            double[] new_koef = new double[koef_count];

            bool IsKoefEqual = false;
            bool error = false;
            Log.WriteLn("Квадрированине:");

            int p = 1;

            for (int iter = 1; !IsKoefEqual; iter++)
            {
                Log.WriteLn("-----===" + iter + "===-----");

                IsKoefEqual = true;

                for (int k = 0; k < koef_count; k++)
                {
                    new_koef[k] = current_koef[k] * current_koef[k];
                                                           // 0,1,2,3 //4,5,6,7
                    for (int n = 1; (k < koef_count / 2) ? (n <= k) : (n < koef_count - k); n++)
                    {
                        if (n % 2 == 1)
                            new_koef[k] -= 2 * current_koef[k + n] * current_koef[k - n];
                        else
                            new_koef[k] += 2 * current_koef[k + n] * current_koef[k - n];
                    }

                    if (double.IsInfinity(new_koef[k]) || double.IsNaN(new_koef[k]))
                    {
                        error = true;
                        Log.WriteLn("Аварийное завершение квадрирования. Значение а" + k + " = " + new_koef[k]);
                        break;
                    }

                    if (IsKoefEqual)
                        if (Math.Abs(MyMath.Sqr(current_koef[k]) - new_koef[k]) > accuracy)
                            IsKoefEqual = false;
                }

                if (error)
                    break;

                p *= 2; 
                new_koef.CopyTo(current_koef, 0);
                Log.WriteLn(koef2str(new_koef));
            }
            Log.WriteLn("Квадрированине завершено.");

            #endregion


            Log.WriteLn("Корни:");

            double[] x = new double[root_count];
            Dictionary<int, double> RootDictionary = new Dictionary<int, double>();

            for (int k = 0; k < root_count; k++)
            {
                x[k] = Math.Abs(current_koef[k] / current_koef[k + 1]);
                x[k] = root_sign[k] *  Math.Pow(x[k], (double) 1 / p);
                RootDictionary.Add(k, x[k]);
            }

            Log.WriteLn(root2str(x));
            
            DiscoverLimits();
            return RootDictionary;
        }

        private void DiscoverLimits()
        {
            Log.WriteLn("Грань корней: ");
            double R = 1 + Math.Abs(input_koef.Max()) / Math.Abs(input_koef.Last()); 

            double r = 1 / (1 + Math.Abs(input_koef.Max()) / Math.Abs(input_koef.First()));

            Log.WriteLn(r + " < |Xi| < " + R);

        }
    }
}
