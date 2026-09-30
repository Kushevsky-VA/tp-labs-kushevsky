using ExpenseTracker.Core;
using ConsoleTables;


IStorage storage = new FileStorage("expenses.txt");
var manager = new ExpenseManager(storage);

while (true)
{
    Console.WriteLine("\n--- УЧЕТ РАСХОДОВ (Вариант 12) ---");
    Console.WriteLine("1. Добавить расход");
    Console.WriteLine("2. Показать все расходы");
    Console.WriteLine("3. Итоги за период");
    Console.WriteLine("4. Топ категорий");
    Console.WriteLine("0. Выход");
    Console.Write("Выбор: ");

    switch (Console.ReadLine())
    {
        case "1":
            Console.Write("Дата (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var date))
            {
                Console.WriteLine("Неверная дата");
                break;
            }

            Console.Write("Категория: ");
            string category = Console.ReadLine() ?? "Прочее";

            Console.Write("Сумма: ");
            if (!decimal.TryParse(Console.ReadLine(), out var amount))
            {
                Console.WriteLine("Неверная сумма");
                break;
            }

            manager.AddExpense(date, category, amount);
            Console.WriteLine("Расход добавлен!");
            break;

        case "2":
            var table = new ConsoleTable("Дата", "Категория", "Сумма");
            foreach (var e in manager.GetAll())
            {
                table.AddRow(e.Date.ToShortDateString(), e.Category, e.Amount);
            }
            table.Write();
            break;

        case "3":
            Console.Write("Начало периода (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var start)) break;

            Console.Write("Конец периода (гггг-мм-дд): ");
            if (!DateTime.TryParse(Console.ReadLine(), out var end)) break;

            decimal total = manager.GetTotalForPeriod(start, end);
            Console.WriteLine($"Итого за период: {total:C}");
            break;

        case "4":
            Console.Write("Сколько топ-категорий вывести? ");
            if (!int.TryParse(Console.ReadLine(), out int count)) break;

            var top = manager.GetTopCategories(count);

            var topTable = new ConsoleTable("Категория", "Сумма");
            foreach (var group in top)
            {
                topTable.AddRow(group.Key, group.Sum(e => e.Amount));
            }
            topTable.Write();
            break;

        case "0": return;
        default: Console.WriteLine("Неверный пункт меню"); break;
    }
}