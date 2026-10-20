using System;
using System.Collections.Generic;

namespace Lab4.Core
{
    public static class MathAlgorithms
    {
        /// <summary>
        /// Вычисляет факториал числа n.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Если n < 0 или n > 20 (переполнение long).</exception>
        public static long Factorial(int n)
        {
            if (n < 0) throw new ArgumentOutOfRangeException(nameof(n), "Число не может быть отрицательным");
            if (n > 20) throw new ArgumentOutOfRangeException(nameof(n), "Переполнение типа long");

            long result = 1;
            for (int i = 2; i <= n; i++) result *= i;
            return result;
        }

        /// <summary>
        /// Возвращает список первых count чисел Фибоначчи.
        /// </summary>
        public static IReadOnlyList<long> Fibonacci(int count)
        {
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Количество не может быть отрицательным");

            var result = new List<long>();
            long a = 0, b = 1;
            for (int i = 0; i < count; i++)
            {
                result.Add(a);
                (a, b) = (b, a + b);
            }
            return result;
        }

        /// <summary>
        /// Вычисляет sin(x) с помощью ряда Тейлора с точностью eps.
        /// </summary>
        public static double SinTaylor(double x, double eps = 1e-6)
        {
            double term = x, sum = x;
            for (int n = 1; Math.Abs(term) > eps; n++)
            {
                term *= -x * x / ((2 * n) * (2 * n + 1));
                sum += term;
            }
            return sum;
        }
    }
}