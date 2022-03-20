using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{    
    public class FiboXUnitTests
    {
        private Fibo fibo;
        
        public FiboXUnitTests()
        {
            fibo = new Fibo();
        }
        [Fact]
        public void GetFiboSeries_InputRange1_ReturnFiboSeries()
        {
            fibo.Range = 1;
            var result = fibo.GetFiboSeries();

            Assert.NotNull(result);
            Assert.Equal(result.OrderBy(m=>m), result);
            Assert.Contains(0,result);
        }

        [Fact]
        public void GetFiboSeries_InputRange6_ReturnFiboSeries()
        {
            //Arrange
            List<int> expectedSeries = new() { 0, 1, 1, 2, 3, 5 };
            fibo.Range = 6;

            //Act
            var result = fibo.GetFiboSeries();

            //Assert
            Assert.Contains(3,result);
            Assert.Equal(6,result.Count);
            Assert.DoesNotContain(4,result);
            Assert.Equal(expectedSeries,result);

        }
    }
}
