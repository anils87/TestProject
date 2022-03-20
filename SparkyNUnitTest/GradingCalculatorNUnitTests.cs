using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
   public class GradingCalculatorNUnitTests
    {
        private GradingCalculator gradingCalculator;
       
        [SetUp]
        public void Setup()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Test]
        public void GetGrade_WithScore95AndAttendancePercentage90_ReturnGradeA()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("A"));
        }
        [Test]
        public void GetGrade_WithScore85AndAttendancePercentage90_ReturnGradeB()
        {
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void GetGrade_WithScore65AndAttendancePercentage90_ReturnGradeC()
        {
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("C"));
        }

        [Test]
        public void GetGrade_WithScore95AndAttendancePercentage65_ReturnGradeB()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;
            var result = gradingCalculator.GetGrade();
            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        [TestCase(95,55,ExpectedResult ="F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_WithNotMatchScoreAndAttendancePercentage_ReturnGradeF(int a,int b)
        {
            gradingCalculator.Score = a;
            gradingCalculator.AttendancePercentage = b;
            return gradingCalculator.GetGrade();
            //return Assert.That(result, Is.EqualTo("F"));
        }

        [Test]
        [TestCase(95, 90, ExpectedResult = "A")]
        [TestCase(85, 90, ExpectedResult = "B")]
        [TestCase(65, 90, ExpectedResult = "C")]
        [TestCase(95, 65, ExpectedResult = "B")]
        [TestCase(95, 55, ExpectedResult = "F")]
        [TestCase(65, 55, ExpectedResult = "F")]
        [TestCase(50, 90, ExpectedResult = "F")]
        public string GetGrade_WithAllLogicalScenario_ReturnGrade(int a, int b)
        {
            gradingCalculator.Score = a;
            gradingCalculator.AttendancePercentage = b;
            return gradingCalculator.GetGrade();
            //return Assert.That(result, Is.EqualTo("F"));
        }
    }
}
