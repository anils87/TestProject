using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    
    public class CustomerXUnitTests
    {
        private Customer customer;
      
        public CustomerXUnitTests()
        {
            customer = new Customer();
        }
        [Fact]
        public void GreetAndCombineName_InputFistNameAndLastName_GetFullName()
        {
            //Arrange

            //Act
            var fullName = customer.GreetAndCombineNames("Anil", "Yadav");

            //Assert
           
            Assert.Equal("Hello, Anil Yadav",fullName);
            
            Assert.Contains(("anil Yadav").ToLower(), fullName.ToLower());
            Assert.StartsWith("Hello,", fullName);
            Assert.EndsWith("Yadav",fullName);
            Assert.Matches(("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"), fullName);
            

        }

        [Fact]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            //Arrange 

            //Act

            //Assert
            Assert.Null(customer.GreetMessage);
        }

        [Fact]
        public void CheckDiscount_DefaultCustomer_ReturnDiscountInRange()
        {
            var result = customer.Discount;
            Assert.InRange(result,10, 25);
        }

        [Fact]
        public void GreetMessage_GreetedWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("anil", "");
            Assert.NotNull(customer.GreetMessage);

            Assert.False(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Fact]
        public void GreetChecker_EmptyFirstName_ThrowException()
        {
            // Type 1
            var exceptionDetails = Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Yadav");
            });
            Assert.Equal("Empty First Name", exceptionDetails.Message);

            // Type 3
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Yadav"));

           

        }

        [Fact]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.IsType< BasicCustomer>(result);
        }

        [Fact]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.IsType< PlatinumCustomer>(result);
        }
    }
}
