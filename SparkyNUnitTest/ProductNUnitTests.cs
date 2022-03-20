using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class ProductNUnitTests
    {
        [Test]
        public void GetProductPrice_PlatinumCustomer_ReturnPriceWith20PercentDiscount()
        {
            //Arrange
            Product product = new Product() { Price = 50 };

            //Act
            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            //Assert
            Assert.That(result, Is.EqualTo(40));
        }
    }
}
