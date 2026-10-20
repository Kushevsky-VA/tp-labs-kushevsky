using System;

namespace Lab2_Sports
{
    public abstract class Athlete : IComparable<Athlete>
    {
        private string _name;
        private int _age;
        private string _country;
        private double _score; 


        public string Name => _name;
        public int Age => _age;
        public string Country => _country;
        public double Score => _score;

        protected Athlete(string name, int age, string country, double score)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Имя не может быть пустым", nameof(name));
            if (age < 0 || age > 100)
                throw new ArgumentOutOfRangeException(nameof(age), "Возраст должен быть в пределах 0-100");
            if (score < 0)
                throw new ArgumentOutOfRangeException(nameof(score), "Баллы не могут быть отрицательными");

            _name = name;
            _age = age;
            _country = country;
            _score = score;
        }

        public abstract string GetSportInfo();


        public int CompareTo(Athlete other)
        {
            if (other == null) return 1;

            return other.Score.CompareTo(this.Score);
        }

        public override string ToString()
        {
            return $"{GetType().Name}: {Name} ({Age} лет, {Country}) | Баллы: {Score:F1}";
        }
    }
}