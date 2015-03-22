using System;
using System.Collections.Generic;
using System.Text;
using ELW.Library.Math;
using ELW.Library.Math.Exceptions;
using ELW.Library.Math.Expressions;
using ELW.Library.Math.Tools;
using System.Windows.Forms;

namespace chm_lab_1
{
    class MyMath
    {
        public static double Epsilon
        {
            get
            {
                return 1E-7;
            }
        }

        public static double F(double x, string expression)
        {
            try
            {
                expression = expression.Replace(" ", "").Replace("=0", "");
                // Compiling an expression
                PreparedExpression preparedExpression = ToolsHelper.Parser.Parse(expression);
                CompiledExpression compiledExpression = ToolsHelper.Compiler.Compile(preparedExpression);
                // Optimizing an expression
                CompiledExpression optimizedExpression = ToolsHelper.Optimizer.Optimize(compiledExpression);
                // Creating list of variables specified
                List<VariableValue> variables = new List<VariableValue>();
                variables.Add(new VariableValue(x, "x"));

                // Do it !
                return ToolsHelper.Calculator.Calculate(compiledExpression, variables);
            }
            catch (CompilerSyntaxException ex)
            {
                MessageBox.Show(String.Format("Compiler syntax error: {0}", ex.Message));
            }
            catch (MathProcessorException ex)
            {
                MessageBox.Show(String.Format("Error: {0}", ex.Message));
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Error in input data.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected exception." + ex.Message);
                throw;
            }
            return 0;
        }

        public static double Derivative(double x, string expression)
        {
            return (F(x + Epsilon, expression) - F(x, expression)) / Epsilon;
        }

        public static double Sqr(double d)
        {
            return d * d;
        }

        public static void FindRange(string expression, DataGridView dgv)
        {
            double a = -10;
            double b = 10;
            double step = 0.01;

            int count = 0;
            double left = double.NaN, right;

            while (double.IsNaN(left) || double.IsInfinity(left))
            {
                left = MyMath.F(a, expression);
                a += step;
            }
            bool sign = left > 0;

            for (; a < b; a += step)
            {
                a = Math.Round(a, 4);
                double cur = MyMath.F(a, expression);
                if (double.IsNaN(cur) || double.IsInfinity(cur))
                    continue;

                if (sign == (cur > 0))
                {
                    left = a;
                }
                else
                {
                    sign = cur > 0;
                    right = a;
                    if (MyMath.F(left, expression) * cur < 0)
                    {
                        count++;
                        dgv.Rows.Add(new object[] { count, left, right });
                    }
                    left = a + step;
                }
            }
        }
    }
}
