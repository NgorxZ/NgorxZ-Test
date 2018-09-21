using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formular
{
    class Program
    {
        private static string[] _operators = { "+", "-", "/", "*", "^" };
        private static Func<double, double, double>[] _operations = {
            (a1, a2) => a1 + a2,
            (a1, a2) => a1 - a2,
            (a1, a2) => a1 / a2,
            (a1, a2) => a1 * a2,
            (a1, a2) => Math.Pow(a1, a2)
        };


        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            while ("" != input)
            {
                Console.WriteLine(GetFormular(input).ToString());
                input = Console.ReadLine();
            }
        }

        static double GetFormular(string input)
        {
            return Eval(input);
        }

        static double Eval(string expression)
        {
            List<string> tokens = GetTokens(expression);
            Stack<double> operandStack = new Stack<double>();
            Stack<string> operatorStack = new Stack<string>();
            int tokenIndex = 0;

            if (Array.IndexOf(tokens.ToArray(), "(") < 0)
            {
                if (tokens.ToArray<string>().Intersect<string>(_operators).Count<string>() > 1)
                {
                    if (Array.IndexOf(tokens.ToArray(), "+") >= 0)
                    {
                        tokens.Insert(Array.IndexOf(tokens.ToArray(), "+") - 1, "(");
                        tokens.Insert(Array.IndexOf(tokens.ToArray(), "+") + 2, ")");
                    }
                    else if (Array.IndexOf(tokens.ToArray(), "-") >= 0)
                    {
                        tokens.Insert(Array.IndexOf(tokens.ToArray(), "-") - 1, "(");
                        tokens.Insert(Array.IndexOf(tokens.ToArray(), "-") + 2, ")");
                    }
                }
            }


            while (tokenIndex < tokens.Count)
            {
                string token = tokens[tokenIndex];
                if (token == "(")
                {
                    string subExpr = GetSubExpression(tokens, ref tokenIndex);
                    operandStack.Push(Eval(subExpr));
                    continue;
                }




                if (token == ")")
                {
                    throw new ArgumentException("Mis-matched parentheses in expression");
                }

                //If this is an operator  
                if (Array.IndexOf(_operators, token) >= 0)
                {
                    while (operatorStack.Count > 0 &&
                        Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
                    {
                        string op = operatorStack.Pop();
                        double arg2 = operandStack.Pop();
                        double arg1 = operandStack.Pop();
                        operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
                    }
                    operatorStack.Push(token);
                }

                else
                {
                    operandStack.Push(double.Parse(token));
                }
                tokenIndex += 1;
            }

            while (operatorStack.Count > 0)
            {
                string op = operatorStack.Pop();
                double arg2 = operandStack.Pop();
                double arg1 = operandStack.Pop();
                operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg2));
            }
            return operandStack.Pop();
        }

        static string GetSubExpression(List<string> tokens, ref int index)
        {
            StringBuilder subExpr = new StringBuilder();
            int parenlevels = 1;
            index += 1;
            while (index < tokens.Count && parenlevels > 0)
            {
                string token = tokens[index];
                if (tokens[index] == "(")
                {
                    parenlevels += 1;
                }

                if (tokens[index] == ")")
                {
                    parenlevels -= 1;
                }

                if (parenlevels > 0)
                {
                    subExpr.Append(token);
                }

                index += 1;
            }

            if ((parenlevels > 0))
            {
                throw new ArgumentException("Mis-matched parentheses in expression");
            }
            return subExpr.ToString();
        }




        static List<string> GetTokens(string expression)
        {
            string operators = "()+-^*/";
            List<string> tokens = new List<string>();
            StringBuilder sb = new StringBuilder();

            //ลบช่องว่างออกใน input เพื่อนำ Char แต่ละตัวมาใส่ยัง List<String>()
            foreach (char c in expression.Replace(" ", string.Empty))
            {

                if (operators.IndexOf(c) >= 0) //ถ้าพบว่า charector เป็น Operator
                {
                    if ((sb.Length > 0)) //พบว่า sb มีความยาวมากกว่า 0 
                    {
                        tokens.Add(sb.ToString());
                        sb.Length = 0;
                    }
                    tokens.Add(c.ToString());
                }
                else
                    sb.Append(c);
            }

            if ((sb.Length > 0))
                tokens.Add(sb.ToString());

            return tokens;
        }
    }
}
