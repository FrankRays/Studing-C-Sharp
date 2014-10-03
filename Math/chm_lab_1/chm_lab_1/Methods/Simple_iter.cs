using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chm_lab_1.Methods
{
    class Simple_iter
    {
        string expression;

        public Simple_iter(string expression)
        {
            this.expression = expression;
        }

        double f(double d, double lambda)
        {
            return d - MyMath.F(d, expression) * lambda;
        }

        public double Roots(double a, double b)
        {
            double x = (a+b)/2;
            double fx;
            
            int iter = 1;
            
            Log.WriteLn("Метод простых итераций.");
            Log.WriteLn("a = " + a + ";\tb = " + b + ";");
            if (MyMath.F(a, expression) * MyMath.F(b, expression) >= 0)
            {
                Log.WriteLn("F(a)F(b) > 0 \t не правельный интервал [a;b]");
            }

            double l = 2 / (MyMath.Derivative(a, expression) + MyMath.Derivative(b, expression));
            Log.WriteLn("L=" + l);
            double q = (MyMath.Derivative(b, expression) - MyMath.Derivative(a, expression)) / (MyMath.Derivative(a, expression) + MyMath.Derivative(b, expression));
            do
            {
                Log.WriteLn("Итерация №" + iter + ":");
                Log.WriteLn("x = " + x);
                fx = f(x, l);

                if (fx < a || fx > b || double.IsNaN(fx))
                {
                    Log.WriteLn("Error: x=" + fx);
                    return double.NaN;
                }
                             
                if (Math.Abs(x - fx) < Math.Abs((1-q)/q*MyMath.Epsilon))
                {
                    x = fx;
                    Log.WriteLn("|x - F(x)|<e  \t-->  найден корень достаточной точности");
                    Log.WriteLn("x = " + x);
                    return x;
                }
                else
                {
                    Log.WriteLn("x = F(x) = " + fx);
                    x = fx;
                }

                iter++;
            } while (true);
        }
    
    }
}
