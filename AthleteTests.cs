using System;
using Xunit;
using Lab4.Core;

namespace Lab4.Tests
{
    public class AthleteTests
    {


        [Fact]
        public void Athlete_EmptyName_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Runner("", 25, "Россия", 100, 10));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(101)]
        public void Athlete_InvalidAge_ThrowsArgumentOutOfRangeException(int invalidAge)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Runner("Иван", invalidAge, "Россия", 100, 10));
        }


        [Fact]
        public void Runner_CorrectInitialization_SetsProperties()
        {
            var runner = new Runner("Усэйн Болт", 35, "Ямайка", 100, 9.58);

            Assert.Equal("Усэйн Болт", runner.Name);
            Assert.Equal(35, runner.Age);
            Assert.Equal(100, runner.DistanceMeters);
            Assert.Equal(9.58, runner.TimeSeconds);
        }

        [Fact]
        public void Runner_GetResult_ReturnsTime()
        {
            var runner = new Runner("Тест", 20, "РФ", 100, 12.5);
            Assert.Equal(12.5, runner.GetResult());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void Runner_InvalidDistance_ThrowsException(double distance)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Runner("Тест", 20, "РФ", distance, 10));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1.5)]
        public void Runner_InvalidTime_ThrowsException(double time)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Runner("Тест", 20, "РФ", 100, time));
        }

     

        [Fact]
        public void Swimmer_CorrectInitialization_SetsProperties()
        {
            var swimmer = new Swimmer("Майкл Фелпс", 38, "США", "Баттерфляй", 50.58);

            Assert.Equal("Баттерфляй", swimmer.Style);
            Assert.Equal(50.58, swimmer.TimeSeconds);
        }

        [Fact]
        public void Swimmer_GetResult_ReturnsTime()
        {
            var swimmer = new Swimmer("Тест", 20, "РФ", "Кроль", 30.0);
            Assert.Equal(30.0, swimmer.GetResult());
        }

        [Fact]
        public void Swimmer_InvalidTime_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Swimmer("Тест", 20, "РФ", "Кроль", -10));
        }

      

        [Fact]
        public void AllRounder_CorrectInitialization_SetsPoints()
        {
            var allRounder = new AllRounder("Иван", 25, "Россия", 8500);
            Assert.Equal(8500, allRounder.TotalPoints);
        }

        [Fact]
        public void AllRounder_GetResult_ReturnsPoints()
        {
            var allRounder = new AllRounder("Иван", 25, "Россия", 8500);
            Assert.Equal(8500, allRounder.GetResult());
        }

        [Fact]
        public void AllRounder_NegativePoints_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new AllRounder("Иван", 25, "Россия", -100));
        }

      
        [Fact]
        public void Athlete_AgeZero_IsValid()
        {
            var runner = new Runner("Малыш", 0, "РФ", 10, 5);
            Assert.Equal(0, runner.Age);
        }

        [Fact]
        public void Athlete_AgeHundred_IsValid()
        {
            var runner = new Runner("Долгожитель", 100, "РФ", 10, 5);
            Assert.Equal(100, runner.Age);
        }

        [Fact]
        public void Runner_MinimalPositiveValues_AreValid()
        {
            var runner = new Runner("Тест", 20, "РФ", 0.1, 0.1);
            Assert.Equal(0.1, runner.DistanceMeters);
            Assert.Equal(0.1, runner.TimeSeconds);
        }
    }
}