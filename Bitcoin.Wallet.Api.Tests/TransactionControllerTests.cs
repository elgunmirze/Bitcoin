using System.Threading.Tasks;
using System.Web.Http;
using Bitcoin.Wallet.Api.Controllers;
using Bitcoin.Wallet.Api.Interfaces;
using Bitcoin.Wallet.Api.Models.Request;
using Bitcoin.Wallet.Api.Respository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Bitcoin.Wallet.Api.Tests
{
    [TestClass]
    public class TransactionControllerTests
    {
        [TestMethod]
        public async Task Given_Address_Amount_When_Sending_Bitcoin_Then_Getting_Transaction_IdAsync()
        {
            //Arrange
            var request = new BitcoinRequest
            {
                Amount = 1,
                Address = "b21j21h3bu212j2jjj1jjjaaa"
            };

            var executor = new Mock<DbExecutor>().Object;

            var transactionController = new TransactionController(executor);

            

            //Act
            var result = await transactionController.SendBtc(request);

            //Assert
            Assert.IsNotNull(result);

        }
    }
}
