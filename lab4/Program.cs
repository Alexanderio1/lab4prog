using System;

namespace Lab4
{
    internal static class Program
    {
        static void Main()
        {
            // Пример: один «правильный» и один «неправильный» вызов
            try
            {
                var ok = new ShadowLine(new int[,] { { -2, 5 }, { 1, 3 }, { 7, 10 } });
                Console.WriteLine($"Общая длина = {ok.CalculateSum()}");  // -> 10
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }

            try
            {
                // Левая граница > правой – должно бросить исключение
                var bad = new ShadowLine(new int[,] { { -2, -3 } });
                Console.WriteLine(bad.CalculateSum());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка (как и ожидалось): {ex.Message}");
            }
        }
    }
}
