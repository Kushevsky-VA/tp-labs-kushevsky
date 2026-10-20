using System;

namespace Lab2_Sports
{
    public class Runner : Athlete
    {
        public double DistanceMeters { get; }
        public double TimeSeconds { get; }

        // Конструктор: передаем базовые данные + специфичные для бегуна
        public Runner(string name, int age, string country, double distance, double time, double score)
            : base(name, age, country, score)
        {
            if (distance <= 0) throw new ArgumentOutOfRangeException(nameof(distance));
            if (time <= 0) throw new ArgumentOutOfRangeException(nameof(time));
            DistanceMeters = distance;
            TimeSeconds = time;
        }

        public override string GetSportInfo()
        {
            return $"Дистанция: {DistanceMeters}м, Время: {TimeSeconds}с";
        }

        public override string ToString()
        {
            return base.ToString() + $" | {GetSportInfo()}";
        }
    }
}