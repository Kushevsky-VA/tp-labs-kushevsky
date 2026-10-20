using System;

namespace Lab2_Sports
{
    public class AllRounder : Athlete
    {
        public string Discipline { get; }
        public double TotalPoints { get; } 

   
        public AllRounder(string name, int age, string country, string discipline, double totalPoints, double score)
            : base(name, age, country, score)
        {
            if (string.IsNullOrWhiteSpace(discipline))
                throw new ArgumentException("Дисциплина не может быть пустой", nameof(discipline));
            if (totalPoints < 0)
                throw new ArgumentOutOfRangeException(nameof(totalPoints), "Очки не могут быть отрицательными");

            Discipline = discipline;
            TotalPoints = totalPoints;
        }

        public override string GetSportInfo()
        {
            return $"Многоборье: {Discipline}, Очки за вид: {TotalPoints}";
        }

        public override string ToString()
        {
            return base.ToString() + $" | {GetSportInfo()}";
        }
    }
}