
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
 
    public class GradingCalculatorXUnitTests
    {
        private GradingCalculator gradingCalculator;
        
        public GradingCalculatorXUnitTests()
        {
            gradingCalculator = new GradingCalculator();
        }

        [Fact]
        public void GetGrade_WithScore95AndAttendancePercentage90_ReturnGradeA()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.Equal("A", result);
        }
        [Fact]
        public void GetGrade_WithScore85AndAttendancePercentage90_ReturnGradeB()
        {
            gradingCalculator.Score = 85;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.Equal("B",result);
        }

        [Fact]
        public void GetGrade_WithScore65AndAttendancePercentage90_ReturnGradeC()
        {
            gradingCalculator.Score = 65;
            gradingCalculator.AttendancePercentage = 90;
            var result = gradingCalculator.GetGrade();
            Assert.Equal("C",result);
        }

        [Fact]
        public void GetGrade_WithScore95AndAttendancePercentage65_ReturnGradeB()
        {
            gradingCalculator.Score = 95;
            gradingCalculator.AttendancePercentage = 65;
            var result = gradingCalculator.GetGrade();
            Assert.Equal("B",result);
        }

        [Theory]
        [InlineData(95, 55,"F")]
        [InlineData(65, 55, "F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_WithNotMatchScoreAndAttendancePercentage_ReturnGradeF(int a, int b, string expectedResult)
        {
            gradingCalculator.Score = a;
            gradingCalculator.AttendancePercentage = b;
            var result = gradingCalculator.GetGrade();
            Assert.Equal(expectedResult, result);            
        }

        [Theory]
        [InlineData(95, 90, "A")]
        [InlineData(85, 90, "B")]
        [InlineData(65, 90, "C")]
        [InlineData(95, 65, "B")]
        [InlineData(95, 55, "F")]
        [InlineData(65, 55,"F")]
        [InlineData(50, 90, "F")]
        public void GetGrade_WithAllLogicalScenario_ReturnGrade(int a, int b, string expectedResult)
        {
            gradingCalculator.Score = a;
            gradingCalculator.AttendancePercentage = b;
            var result= gradingCalculator.GetGrade();
            Assert.Equal(expectedResult, result);            
        }
    }
}
