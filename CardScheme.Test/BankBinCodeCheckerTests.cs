using System;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using CardScheme.Domain.Interfaces;
using CardScheme.Domain.Services;
using Moq;
using Xunit;

namespace CardScheme.Test
{
    public class BankBinCodeCheckerTests
    {
        private readonly IBinCodeCheckerService _bankBinCodeChecker;

        public BankBinCodeCheckerTests()
        {
            var client = new HttpClient();
            var httpClientFactoryMock = new Mock<IHttpClientFactory>();
            httpClientFactoryMock.Setup(x => x.CreateClient("")).Returns(client);
            _bankBinCodeChecker = new BinCodeCheckerService();
        }

        /*[Theory]
        [InlineData("484845")]
        public async Task CheckBankBinCode_Should_Return_Valid_Result(string bankBinCode)
        {
            //Arrange

            //Act
            var result = await _bankBinCodeChecker.CheckBinDetails(bankBinCode);

            //Assert
            result.Should().NotBeNull();
            result.Data.Scheme.Should().Be("visa");
            result.Data.Type.Should().Be("debit");
            result.Data.Bank.Should().NotBeNull();
        }

        
        [Theory]
        [InlineData("0")]
        public async Task CheckBankBinCode_Should_be_empty_When_Invalid_Bin_Code(string bankBinCode)
        {
            //Arrange

            //Act
            var result = await _bankBinCodeChecker.CheckBinDetails(bankBinCode);

            //Assert
            result.Should().BeNull();
        }*/
    }
}
