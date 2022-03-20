using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparky
{
    [TestFixture]
    public class BankAccountNUnitTests
    {
       // private BankAccount bankAccount;
        [SetUp]
        public void Setup()
        {
            //bankAccount = new BankAccount(new LogFakker());
        }
        //[Test]
        //public void BankDepositLogFakker_Add100_ReturnTrue()
        //{
        //    BankAccount bankAccount = new BankAccount(new LogFakker());
        //    var result = bankAccount.Deposit(100);
        //    Assert.IsTrue(result);
        //    Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        //}

        [Test]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            BankAccount bankAccount = new BankAccount(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.IsTrue(result);
            Assert.That(bankAccount.GetBalance, Is.EqualTo(100));
        }
        [Test]
        [TestCase(200,100)]
        [TestCase(200,150)]
        public void BankWithdraw_Withdraw100With200Balance_ReturnTrue(int balance,int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x=> x > 0))).Returns(true);

            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsTrue(result);            
        }

        [Test]       
        [TestCase(200, 300)]
        public void BankWithdraw_Withdraw300With200Balance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();            
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x=>x > 0))).Returns(true);
            //logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x <= 0))).Returns(false);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange(int.MinValue,-1,Moq.Range.Inclusive))).Returns(false);
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.IsFalse(result);
        }

        [Test]
        
        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";
            logMock.Setup(x => x.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());
            Assert.That(logMock.Object.MessageWithReturnStr("HELLo"), Is.EqualTo(desiredOutput));
        }

        [Test]

        public void BankLogDummy_LogMockOutputString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput="hello";
            logMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(),out desiredOutput)).Returns(true);
            string result = "";
            Assert.IsTrue(logMock.Object.LogWithOutputResult("Anil", out result));
            Assert.That(result, Is.EqualTo(desiredOutput));
        }

        [Test]

        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();
            logMock.Setup(x => x.LogWithRefObj(ref customer)).Returns(true);           
            Assert.IsTrue(logMock.Object.LogWithRefObj(ref customer));
            Assert.IsFalse(logMock.Object.LogWithRefObj(ref customerNotUsed));
        }
        [Test]

        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("warning");            
            //logMock.Object.LogSeverity = 100;
            Assert.That(logMock.Object.LogSeverity, Is.EqualTo(10));
            Assert.That(logMock.Object.LogType, Is.EqualTo("warning"));

            //Callback with string
            string logTemp = "Hello, ";
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true)
                .Callback((string str) => logTemp += str);
            logMock.Object.LogToDB("Anil");

            Assert.That(logTemp, Is.EqualTo("Hello, Anil"));


            //Callback with integer
            int counter = 5;
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true)
                .Callback(() => counter++);
            logMock.Object.LogToDB("Anil");
            Assert.That(counter, Is.EqualTo(6));

        }

        [Test]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);

            bankAccount.Deposit(100);

            // Veification
            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
            logMock.VerifySet(u => u.LogSeverity=101, Times.Once);
            logMock.VerifyGet(u => u.LogSeverity, Times.Once);
        }
    }
}

