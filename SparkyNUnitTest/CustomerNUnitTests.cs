using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class CustomerNUnitTests
    {
        private Customer customer;
        [SetUp]
        public void Setup()
        {
            customer = new Customer();
        }
        [Test]
        public void GreetAndCombineName_InputFistNameAndLastName_GetFullName()
        {
            //Arrange
            
            //Act
            var fullName = customer.GreetAndCombineNames("Anil", "Yadav");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.AreEqual("Hello, Anil Yadav", fullName);
                Assert.That(fullName, Is.EqualTo("Hello, Anil Yadav"));
                Assert.That(fullName, Does.Contain("anil Yadav").IgnoreCase);
                Assert.That(fullName, Does.StartWith("Hello,"));
                Assert.That(fullName, Does.EndWith("Yadav"));
                Assert.That(fullName, Does.Match("Hello, [A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+"));
            });
           
        }

        [Test]
        public void GreetMessage_NotGreeted_ReturnNull()
        {
            //Arrange 
           
            //Act

            //Assert
            Assert.IsNull(customer.GreetMessage);
        }

        [Test]
        public void CheckDiscount_DefaultCustomer_ReturnDiscountInRange()
        {
            var result = customer.Discount;
            Assert.That(result, Is.InRange(10,25));
        }
        [Test]
        public void GreetMessage_GreetedWithoutLastName_ReturnNotNull()
        {
            customer.GreetAndCombineNames("anil", "");
            Assert.IsNotNull(customer.GreetMessage);

            Assert.IsFalse(string.IsNullOrEmpty(customer.GreetMessage));
        }

        [Test]
        public void GreetChecker_EmptyFirstName_ThrowException()
        {
            // Type 1
            var exceptionDetails = Assert.Throws<ArgumentException>(() =>
            {
                customer.GreetAndCombineNames("", "Yadav");
            });
            Assert.AreEqual("Empty First Name", exceptionDetails.Message);

            //Type 2
            Assert.That(() => customer.GreetAndCombineNames("", "Yadav"), 
                Throws.ArgumentException.With.Message.EqualTo("Empty First Name"));

            // Type 3
            Assert.Throws<ArgumentException>(() => customer.GreetAndCombineNames("", "Yadav"), 
                Throws.ArgumentException.With.Message.EqualTo("Empty First Name").ToString());

            //Type 4
            Assert.That(() => customer.GreetAndCombineNames("", "Yadav"),
                Throws.ArgumentException);

        }

        [Test]
        public void CustomerType_CreateCustomerWithLessThan100Order_ReturnBasicCustomer()
        {
            customer.OrderTotal = 10;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<BasicCustomer>());
        }

        [Test]
        public void CustomerType_CreateCustomerWithMoreThan100Order_ReturnPlatinumCustomer()
        {
            customer.OrderTotal = 110;
            var result = customer.GetCustomerDetails();
            Assert.That(result, Is.TypeOf<PlatinumCustomer>());
        }
    }
}
