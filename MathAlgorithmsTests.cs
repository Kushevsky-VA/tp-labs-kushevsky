using System;
using System.Linq;
using Xunit;
using Lab4.Core;

namespace Lab4.Tests
{
    public class MathAlgorithmsTests
    {
        
        [Fact]
        public void Factorial_Zero_ReturnsOne()
        {
            
            long result = MathAlgorithms.Factorial(0);
            
            Assert.Equal(1, result);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 120)]
        [InlineData(10, 3628800)]
        [InlineData(20, 2432902008176640000)] 
        public void Factorial_ValidNumbers_ReturnsExpected(int n, long expected)
        {
            
            long result = MathAlgorithms.Factorial(n);
            
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(21)] 
        public void Factorial_OutOfRange_ThrowsArgumentOutOfRangeException(int n)
        {
            
            Assert.Throws<ArgumentOutOfRangeException>(() => MathAlgorithms.Factorial(n));
        }

        

        [Fact]
        public void Fibonacci_ZeroCount_ReturnsEmptyList()
        {
            // Act
            var result = MathAlgorithms.Fibonacci(0);
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Fibonacci_First6_ReturnsCorrectSequence()
        {
            
            var expected = new long[] { 0, 1, 1, 2, 3, 5 };
         
            var result = MathAlgorithms.Fibonacci(6);
           
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Fibonacci_NegativeCount_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => MathAlgorithms.Fibonacci(-5));
        }



        [Theory]
        [InlineData(0)]
        [InlineData(0.5)]
        [InlineData(Math.PI / 2)]
        [InlineData(-1.2)]
        public void SinTaylor_MatchesMathSin(double x)
        {
            
            double result = MathAlgorithms.SinTaylor(x);
            double expected = Math.Sin(x);
           
            Assert.Equal(expected, result, 5);
        }
    }
}