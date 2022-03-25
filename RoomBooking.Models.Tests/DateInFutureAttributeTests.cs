using NUnit.Framework;
using RoomBooking.Models.ModelValidations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Models
{
    [TestFixture]
   public class DateInFutureAttributeTests
    {
        [Test]
        [TestCase(100,ExpectedResult =true)]
        [TestCase(-100, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        public bool DateValidator_InputDateRange_DateValidaty(int addTime)
        {
            DateInFutureAttribute dateInFutureAttribute = new(() => DateTime.Now);
            return dateInFutureAttribute.IsValid(DateTime.Now.AddMinutes(addTime));            
        }

        [Test]
        public void DateValidator_NotValidDate_ReturnExceptionMessage()
        {
            var result = new DateInFutureAttribute();
            Assert.AreEqual("Date must be in the future", result.ErrorMessage);
        }
    }
}
