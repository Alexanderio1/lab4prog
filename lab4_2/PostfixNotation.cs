using System;
using System.Collections.Generic;
using System.Text;

namespace Lab4_2
{
    public static class Poliz
    {
        private static readonly HashSet<char> Operators = new() { '+', '-', '*', '/', '^' };
        private const char LeftParen = '(';
        private const char RightParen = ')'; 

        public static string ConvertToPolishNotation(string expression)
        {
            var output = new List<string>();
            var stack = new Stack<char>();

            foreach (var token in Tokenize(expression))
            {
                if (char.IsLetterOrDigit(token[0]))
                {
                    output.Add(token);
                }
                else if (token.Length == 1 && Operators.Contains(token[0]))
                {
                    char op = token[0];
                    bool isRightAssoc = op == '^';

                    while (stack.Count > 0)
                    {
                        char top = stack.Peek();
                        if (!Operators.Contains(top))
                            break;

                        int precTop = Precedence(top);
                        int precOp = Precedence(op);

                        if ((isRightAssoc && precTop > precOp) ||
                            (!isRightAssoc && precTop >= precOp))
                        {
                            output.Add(stack.Pop().ToString());
                        }
                        else
                        {
                            break;
                        }
                    }

                    stack.Push(op);
                }
                else if (token == LeftParen.ToString())
                {
                    stack.Push(LeftParen);
                }
                else if (token == RightParen.ToString())
                {
                    while (stack.Count > 0 && stack.Peek() != LeftParen)
                        output.Add(stack.Pop().ToString());

                    if (stack.Count == 0 || stack.Pop() != LeftParen)
                        throw new InvalidOperationException("Несбалансированные скобки");
                }
                else
                {
                    throw new ArgumentException($"Недопустимый токен: '{token}'");
                }
            }

            while (stack.Count > 0)
            {
                char c = stack.Pop();
                if (c == LeftParen || c == RightParen)
                    throw new InvalidOperationException("Несбалансированные скобки");
                output.Add(c.ToString());
            }

            return string.Join(' ', output);
        }

        private static int Precedence(char op) => op switch
        {
            '+' or '-' => 1,
            '*' or '/' => 2,
            '^' => 3,
            _ => 0
        };

        private static IEnumerable<string> Tokenize(string expr)
        {
            int i = 0, n = expr.Length;
            while (i < n)
            {
                char c = expr[i];
                if (char.IsWhiteSpace(c)) { i++; continue; }

                if (char.IsLetterOrDigit(c))
                {
                    var sb = new StringBuilder();
                    while (i < n && char.IsLetterOrDigit(expr[i]))
                        sb.Append(expr[i++]);
                    yield return sb.ToString();
                }
                else if (Operators.Contains(c) || c == LeftParen || c == RightParen)
                {
                    yield return c.ToString();
                    i++;
                }
                else
                {
                    throw new ArgumentException($"Недопустимый символ: '{c}'");
                }
            }
        }
    }
}
