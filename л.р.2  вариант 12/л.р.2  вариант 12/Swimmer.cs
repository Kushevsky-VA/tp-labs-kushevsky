using System;

namespace Lab2_Sports
{
    public class Swimmer : Athlete
    {
        public string Style { get; }
        public double TimeSeconds { get; }

        public Swimmer(string name, int age, string country, string style, double time, double score)
            : base(name, age, country, score)
        {
            if (string.IsNullOrWhiteSpace(style)) throw new ArgumentException("Стиль не может быть пустым", nameof(style));
            if (time <= 0) throw new ArgumentOutOfRangeException(nameof(time));
            Style = style;
            TimeSeconds = time;
        }

        public override string GetSportInfo()
        {
            return $"Стиль: {Style}, Время: {TimeSeconds}с";
        }

        public override string ToString()
        {
            return base.ToString() + $" | {GetSportInfo()}";
        }
    }
}