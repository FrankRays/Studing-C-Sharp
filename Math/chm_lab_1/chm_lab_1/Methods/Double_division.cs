using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chm_lab_1.Methods
{
    class Double_division
    {
        string expression;

        public Double_division(string expression)
        {
            this.expression = expression;
        }

        public double Roots(double a, double b)
        {
            double c;
            double fa, fb, fc;
            
            int iter = 1;
            
            Log.WriteLn("Метод деления пополам.");
            if (MyMath.F(a, expression) * MyMath.F(b, expression) >= 0)
            {
                Log.WriteLn("F(a)F(b) > 0 \t не правельный интервал [a;b]");
            }
            do
            {
                Log.WriteLn("Итерация №" + iter + ":");
                Log.WriteLn("a = " + a + ";\tb = " + b + ";");
                if (Math.Abs(a - b) < MyMath.Epsilon)
                {
                    Log.WriteLn("|a-b| < e \t--> найден корень достаточной точности");
                    Log.WriteLn("x = " + ((a + b) / 2).ToString());

                    return (a + b) / 2;
                }
                fa = MyMath.F(a, expression);
                fb = MyMath.F(b, expression);
                Log.WriteLn("F(a) = " + fa + ";\tF(b) = " + fb + ";");

                c = (a + b) / 2;
                fc = MyMath.F(c, expression);
                Log.WriteLn("с = " + c + ";\tF(с) = " + fc + ";");
                if (fc == 0)
                {
                    Log.WriteLn("F(c) = 0  \t-->  c = " + c + " - точный корень");
                    return c;
                }
                if (fa * fc < 0)
                {
                    Log.WriteLn("F(a)F(c) < 0  \t-->  b = c = " + c);
                    b = c;
                }
                else if (fb * fc < 0)
                {
                    Log.WriteLn("F(b)F(c) < 0  \t-->  a = c = " + c);
                    a = c;
                }
                else
                {
                    Log.WriteLn("F(a)F(c) >  0 && F(b)F(c) > 0  \t-->  [a,b]");
                    return c;
                }
                iter++;
            } while (true);
        }
    
    }
}
