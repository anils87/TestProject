using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CalculatorNUnitTests
    {
        [Test]
        public void AddNumbers_InputTwoInt_GetCorrectAddition()
        {
            //Arrange
            Calculator calc = new();

            //Act
            var result = calc.AddNumbers(10, 20);

            //Assert
            Assert.AreEqual(30, result);

        }

        [Test]
        public void IsOddChecker_InputEvenNumber_ReturnFalse()
        {
            Calculator calc = new();

            bool isOdd = calc.IsOddNumber(10);

            Assert.That(isOdd, Is.EqualTo(false));
            //Another way of asserting using helper
            Assert.IsFalse(isOdd);
            
        }
        [Test]
        public void IsOddChecker_InputOddNumber_ReturnTrue()
        {
            Calculator calc = new();

            bool isOdd = calc.IsOddNumber(11);

            Assert.That(isOdd, Is.EqualTo(true));
            //Another way of asserting using helper
            Assert.IsTrue(isOdd);

        }

        // Multiple Test Cases for same test methods
        [Test]
        [TestCase(11)]
        [TestCase(13)]
        public void IsOddChecker_InputOddNumberParam_ReturnTrue(int a)
        {
            Calculator calc = new();

            bool isOdd = calc.IsOddNumber(a);

            Assert.That(isOdd, Is.EqualTo(true));
            //Another way of asserting using helper
            Assert.IsTrue(isOdd);

        }

        [Test]
        [TestCase(10,ExpectedResult = false)]
        [TestCase(11, ExpectedResult = true)]
        public bool IsOddChecker_InputNumber_ReturnTrueIfOdd(int a)
        {
            Calculator calc = new();
            return calc.IsOddNumber(a);
        }

        [Test]
        [TestCase(5.4,10.5)] // 15.9
        [TestCase(5.43,10.53)] //15.96
        [TestCase(5.49, 10.59)] // 16.08
        public void AddNumbersDouble_InputTwoDouble_GetCorrectAddition(double a,double b)
        {
            //Arrange
            Calculator calc = new();

            //Act
            double result = calc.AddNumbersDouble(a, b);

            //Assert
            Assert.AreEqual(15.9, result,.2);
        }

        [Test]
        public void GetNumberRange_InputMinAndMaxRange_ReturnValidOddNumberRange()
        {
            //Arrange
            Calculator calc = new();
            List<int> expectedResult = new() { 5, 7, 9 };

            //Act
            List<int> result = calc.GetOddRange(5, 10);

            //Assert
            Assert.That(result, Is.EquivalentTo(expectedResult));
            //Assert.AreEqual(expectedResult, result);
            //Assert.Contains(7, result);
            Assert.That(result, Does.Contain(7));
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.Count, Is.EqualTo(3));
            Assert.That(result, Has.No.Member(6));
            Assert.That(result, Is.Ordered);
            Assert.That(result, Is.Unique);
        }
    }
}
