using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2_Sports
{
    
    public abstract class Athlete
    {
        
        private string _name;
        private int _age;
        private string _country;

        public string Name => _name;
        public int Age => _age;
        public string Country => _country;

        protected Athlete(string name, int age, string country)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя не может быть пустым", nameof(name));
            if (age < 0 || age > 100) throw new ArgumentOutOfRangeException(nameof(age), "Возраст должен быть в пределах 0-100");

            _name = name;
            _age = age;
            _country = country;
        }

        
        public abstract double GetResult();

        
        public override string ToString()
        {
            return $"{GetType().Name}: {Name} ({Age} лет, {Country}). Результат: {GetResult():F2}";
        }
    }

    
    public class Runner : Athlete
    {
        public double DistanceMeters { get; }
        public double TimeSeconds { get; }

        public Runner(string name, int age, string country, double distance, double time)
            : base(name, age, country)
        {
            if (distance <= 0) throw new ArgumentOutOfRangeException(nameof(distance));
            if (time <= 0) throw new ArgumentOutOfRangeException(nameof(time));
            DistanceMeters = distance;
            TimeSeconds = time;
        }

       
        public override double GetResult() => TimeSeconds; 

        public override string ToString()
        {
            return base.ToString() + $" (Дистанция: {DistanceMeters}м, Время: {TimeSeconds}с)";
        }
    }

    
    public class Swimmer : Athlete
    {
        public string Style { get; }
        public double TimeSeconds { get; }

        public Swimmer(string name, int age, string country, string style, double time)
            : base(name, age, country)
        {
            if (time <= 0) throw new ArgumentOutOfRangeException(nameof(time));
            Style = style;
            TimeSeconds = time;
        }

        public override double GetResult() => TimeSeconds;

        public override string ToString()
        {
            return base.ToString() + $" (Стиль: {Style}, Время: {TimeSeconds}с)";
        }
    }

    
    public class AllRounder : Athlete
    {
        public double TotalPoints { get; }

        public AllRounder(string name, int age, string country, double points)
            : base(name, age, country)
        {
            if (points < 0) throw new ArgumentOutOfRangeException(nameof(points));
            TotalPoints = points;
        }

        public override double GetResult() => TotalPoints; 

        public override string ToString()
        {
            return base.ToString() + $" (Сумма очков: {TotalPoints})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Athlete> athletes = new List<Athlete>
            {
                new Runner("Усэйн Болт", 35, "Ямайка", 100, 9.58),
                new Swimmer("Майкл Фелпс", 38, "США", "Баттерфляй", 50.58),
                new AllRounder("Иван Иванов", 25, "Россия", 8500),
                new Runner("Карл Льюис", 60, "США", 200, 19.75),
                new Swimmer("Кэти Ледеки", 26, "США", "Кроль", 15.20)
            };

            Console.WriteLine("=== Список спортсменов ===");
            foreach (var athlete in athletes)
            {
               
                Console.WriteLine(athlete);
            }

            Console.WriteLine("\n=== Сортировка по результату (возрастание) ===");
            
            var sorted = athletes.OrderBy(a => a.GetResult()).ToList();
            foreach (var a in sorted)
            {
                Console.WriteLine($"{a.Name} - {a.GetResult()}");
            }
        }
    }
}