using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    
    public class ProductXUnitTests
    {
        [Fact]
        public void GetProductPrice_PlatinumCustomer_ReturnPriceWith20PercentDiscount()
        {
            //Arrange
            Product product = new Product() { Price = 50 };

            //Act
            var result = product.GetPrice(new Customer() { IsPlatinum = true });

            //Assert
            Assert.Equal(40,result);
        }
    }
}
