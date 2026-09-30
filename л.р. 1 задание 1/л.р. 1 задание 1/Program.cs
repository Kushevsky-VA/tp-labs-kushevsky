Console.WriteLine("=== Задание 1: Факториал ===");
Console.Write("Введите целое число n (0 <= n <= 20): ");


if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 20)
{
    Console.WriteLine("Ошибка: нужно целое число от 0 до 20.");
    return;
}


long result = Factorial(n);
Console.WriteLine($"{n}! = {result}");


static long Factorial(int n)
{
    long result = 1;
    
    for (int i = 2; i <= n; i++)
    {
        result *= i;
    }
    return result;
}
