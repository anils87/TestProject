using Moq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sparky
{
    
    public class BankAccountXUnitTests
    {        
        [Fact]
        public void BankDepositLogFakker_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            BankAccount bankAccount = new BankAccount(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.True(result);
            Assert.Equal(100,bankAccount.GetBalance());
        }

        [Fact]
        public void BankDeposit_Add100_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.Message(""));
            BankAccount bankAccount = new BankAccount(logMock.Object);
            var result = bankAccount.Deposit(100);
            Assert.True(result);            
            Assert.Equal<int>(100, bankAccount.GetBalance());
        }
        [Theory]
        [InlineData(200, 100)]
        [InlineData(200, 150)]
        public void BankWithdraw_Withdraw100With200Balance_ReturnTrue(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);

            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.True(result);
        }

        [Theory]
        [InlineData(200, 300)]
        public void BankWithdraw_Withdraw300With200Balance_ReturnFalse(int balance, int withdraw)
        {
            var logMock = new Mock<ILogBook>();
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x > 0))).Returns(true);
            //logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.Is<int>(x => x <= 0))).Returns(false);
            logMock.Setup(x => x.LogBalanceAfterWithdrawal(It.IsInRange(int.MinValue, -1, Moq.Range.Inclusive))).Returns(false);
            BankAccount bankAccount = new BankAccount(logMock.Object);
            bankAccount.Deposit(balance);
            var result = bankAccount.Withdraw(withdraw);
            Assert.False(result);
        }

        [Fact]

        public void BankLogDummy_LogMockString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";
            logMock.Setup(x => x.MessageWithReturnStr(It.IsAny<string>())).Returns((string str) => str.ToLower());
            Assert.Equal(desiredOutput,logMock.Object.MessageWithReturnStr("HELLo"));
        }

        [Fact]

        public void BankLogDummy_LogMockOutputString_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            string desiredOutput = "hello";
            logMock.Setup(x => x.LogWithOutputResult(It.IsAny<string>(), out desiredOutput)).Returns(true);
            string result = "";
            Assert.True(logMock.Object.LogWithOutputResult("Anil", out result));
            Assert.Equal(desiredOutput,result);
        }

        [Fact]

        public void BankLogDummy_LogRefChecker_ReturnTrue()
        {
            var logMock = new Mock<ILogBook>();
            Customer customer = new();
            Customer customerNotUsed = new();
            logMock.Setup(x => x.LogWithRefObj(ref customer)).Returns(true);
            Assert.True(logMock.Object.LogWithRefObj(ref customer));
            Assert.False(logMock.Object.LogWithRefObj(ref customerNotUsed));
        }
        [Fact]

        public void BankLogDummy_SetAndGetLogTypeAndSeverityMock_MockTest()
        {
            var logMock = new Mock<ILogBook>();
            logMock.SetupAllProperties();
            logMock.Setup(x => x.LogSeverity).Returns(10);
            logMock.Setup(x => x.LogType).Returns("warning");
            //logMock.Object.LogSeverity = 100;
            Assert.Equal(10,logMock.Object.LogSeverity);
            Assert.Equal("warning",logMock.Object.LogType);

            //Callback with string
            string logTemp = "Hello, ";
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true)
                .Callback((string str) => logTemp += str);
            logMock.Object.LogToDB("Anil");

            Assert.Equal("Hello, Anil",logTemp);


            //Callback with integer
            int counter = 5;
            logMock.Setup(x => x.LogToDB(It.IsAny<string>())).Returns(true)
                .Callback(() => counter++);
            logMock.Object.LogToDB("Anil");
            Assert.Equal(6,counter);

        }

        [Fact]
        public void BankLogDummy_VerifyExample()
        {
            var logMock = new Mock<ILogBook>();
            BankAccount bankAccount = new(logMock.Object);

            bankAccount.Deposit(100);

            // Veification
            logMock.Verify(u => u.Message(It.IsAny<string>()), Times.Exactly(2));
            logMock.Verify(u => u.Message("Test"), Times.AtLeastOnce);
            logMock.VerifySet(u => u.LogSeverity = 101, Times.Once);
            logMock.VerifyGet(u => u.LogSeverity, Times.Once);
        }
    }
}

