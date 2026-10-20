using System;

namespace Lab4.Core
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
}