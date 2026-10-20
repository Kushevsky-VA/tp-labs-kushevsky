using System;

namespace Lab1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Задание 1: Вычисление факториала ");
            Console.Write("Введите целое число n (0 <= n <= 20): ");

           
            if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 20)
            {
                Console.WriteLine("Ошибка: нужно ввести целое число от 0 до 20.");
                return; 
            }

           
            long result = CalculateFactorial(n);

           
            Console.WriteLine($"{n}! = {result}");
        }

        
        static long CalculateFactorial(int n)
        {
            long result = 1;

  
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }

            return result;
        }
    }
}