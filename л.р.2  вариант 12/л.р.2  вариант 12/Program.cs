using System;
using System.Collections.Generic;

namespace Lab2_Sports
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Athlete> athletes = new List<Athlete>
            {
                new Runner("Усэйн Болт", 35, "Ямайка", 100, 9.58, 95.5),
                new Swimmer("Майкл Фелпс", 38, "США", "Баттерфляй", 50.58, 98.0),
                new AllRounder("Иван Иванов", 25, "Россия", "Десятиборье", 8500, 85.0),
                new Runner("Карл Льюис", 60, "США", 200, 19.75, 92.0),
                new Swimmer("Кэти Ледеки", 26, "США", "Кроль", 15.20, 96.5)
            };

            Console.WriteLine("Список спортсменов ");

            foreach (var athlete in athletes)
            {
                Console.WriteLine(athlete);
            }

            Console.WriteLine("\n Таблица лидеров");

            athletes.Sort();

            int place = 1;
            foreach (var athlete in athletes)
            {
                Console.WriteLine($"{place} место. {athlete.Name} - {athlete.Score} баллов ({athlete.GetType().Name})");
                place++;
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}