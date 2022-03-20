using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class FiboNUnitTests
    {
        private Fibo fibo;
        [SetUp]
        public void Setup()
        {
            fibo = new Fibo();
        }
        [Test]
        public void GetFiboSeries_InputRange1_ReturnFiboSeries()
        {
            fibo.Range = 1;
            var result = fibo.GetFiboSeries();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Ordered);
            Assert.That(result, Does.Contain(0));
        }

        [Test]
        public void GetFiboSeries_InputRange6_ReturnFiboSeries()
        {
            //Arrange
            List<int> expectedSeries = new() { 0, 1, 1, 2, 3, 5 };
            fibo.Range = 6;

            //Act
            var result = fibo.GetFiboSeries();

            //Assert
            Assert.That(result, Does.Contain(3));
            Assert.That(result.Count, Is.EqualTo(6));
            Assert.That(result, Does.Not.Contain(4));
            Assert.That(result, Is.EquivalentTo(expectedSeries));

        }
    }
}
