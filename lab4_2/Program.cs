using System;

namespace Lab4_2
{
    internal static class Program
    {
        static void Main()
        {
            Console.Write("Введите выражение: ");
            string input = Console.ReadLine() ?? "";
            try
            {
                string postfix = Poliz.ConvertToPolishNotation(input);
                Console.WriteLine($"Постфиксная запись: {postfix}");
            }
            catch (Exception ex) when
                (ex is ArgumentException || ex is InvalidOperationException)
            {
                Console.WriteLine($"Ошибка преобразования: {ex.Message}");
            }
        }
    }
}
