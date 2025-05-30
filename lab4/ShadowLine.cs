using System;

namespace Lab4
{
    /// <summary>
    /// Вычисляет суммарную длину объединения отрезков на оси OX.
    /// </summary>
    public class ShadowLine
    {
        private readonly (int L, int R)[] _segments;

        public ShadowLine(int[,] coordinates)
        {
            if (coordinates.GetLength(1) != 2)
                throw new ArgumentException("Каждый отрезок должен задаваться парой (L, R).");

            int n = coordinates.GetLength(0);
            _segments = new (int, int)[n];

            for (int i = 0; i < n; i++)
            {
                int left = coordinates[i, 0];
                int right = coordinates[i, 1];

                if (left > right)
                    throw new ArgumentException($"Левая граница > правой в строке {i}: ({left}, {right}).");

                _segments[i] = (left, right);
            }

            SortByLeft(_segments); // собственная сортировка
        }

        /// <summary>
        /// Суммарная длина без двойного учёта перекрытий.
        /// </summary>
        public long CalculateSum()
        {
            if (_segments.Length == 0) return 0;

            long sum = 0;
            int currL = _segments[0].L;
            int currR = _segments[0].R;

            for (int i = 1; i < _segments.Length; i++)
            {
                var (L, R) = _segments[i];

                if (L > currR)                 // разрыв
                {
                    sum += (long)currR - currL;
                    currL = L;
                    currR = R;
                }
                else if (R > currR)            // расширяем вправо
                {
                    currR = R;
                }
                // иначе сегмент целиком внутри – пропускаем
            }

            sum += (long)currR - currL;
            return sum;
        }


        private static void SortByLeft((int L, int R)[] a)
        {
            for (int i = 1; i < a.Length; i++)
            {
                var key = a[i];
                int j = i - 1;

                // сдвигаем все элементы, большие key.L, вправо
                while (j >= 0 && a[j].L > key.L)
                {
                    a[j + 1] = a[j];
                    j--;
                }
                a[j + 1] = key;
            }
        }
    }
}
