using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chm_lab_1.Methods
{
    class Combined
    {
        string expression;

        public Combined(string expression)
        {
            this.expression = expression;
        }

        public double Roots(double a, double b)
        {
            double c;
            double fa, fb;
            
            int iter = 1;
            
            Log.WriteLn("Метод хорд-косательных.");
            if (MyMath.F(a, expression) * MyMath.F(b, expression) >= 0)
            {
                Log.WriteLn("F(a)F(b) > 0 \t не правельный интервал [a;b]");
            }
            do
            {
                Log.WriteLn("Итерация №" + iter + ":");
                Log.WriteLn("a = " + a + ";\tb = " + b + ";");
                c = (a + b) / 2;
                if (Math.Abs(a - b) < MyMath.Epsilon)
                {
                    Log.WriteLn("|a-b| < e \t--> найден корень достаточной точности");
                    Log.WriteLn("x = " + c.ToString());

                    return c;
                }

                if (MyMath.Derivative(c, expression) * MyMath.Derivative(MyMath.Derivative(c, expression), expression) <= 0)
                {
                    Log.WriteLn("F'(x)F''(x) <= 0  \t a=b, b=a");
                    double z = a;
                    a = b;
                    b = z;
                }
                            
                fa = MyMath.F(a, expression);
                fb = MyMath.F(b, expression);

                a = a - (b - a) * fa / (fb - fa);
                b = b - fb / MyMath.Derivative(b, expression);

                if (double.IsNaN(a) || double.IsNaN(b))
                {
                    return double.NaN;
                }

                Log.WriteLn("a = " + a + ";\tb = " + b + ";");

                iter++;
            } while (true);
        }
    
    }
}