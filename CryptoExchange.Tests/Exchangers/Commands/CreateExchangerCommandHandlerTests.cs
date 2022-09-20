using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Exchangers.Commands;
using CryptoExchange.Tests.Common;
using Xunit;

namespace CryptoExchange.Tests.Exchangers.Commands
{
    public class CreateExchangerCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateExchangerCommandHandlerTest_Succes()
        {
            //Arrange
            var handler = new CreateExchangerCommandHandler(Context);
            var exchangerName = "TestExch";
            var exchangeWebResource = "https://example.com";
            var rightUserId = "86a9d249-fdd4-4b5e-8ccc-b86d1abb764f";

            //Act
            var result = await handler.Handle(new CreateExchangerCommand
            {
                Name = exchangerName,
                WebResourceUrl = exchangeWebResource,
                UserId = rightUserId
            },
            CancellationToken.None);

            //Assert
            Assert.NotNull(Context.Exchangers.SingleOrDefault(ex => ex.Id == result.ExchangerId));
        }

        [Fact]
        public async Task CreateExchangerCommandHandlerTests_FailedOnWrongId()
        {
            //Arrange
            var handler = new CreateExchangerCommandHandler(Context);
            var wrongUserId = "fdd4-4b5e-8ccc-b86d1abb764f-86a9d249";
            var exchangerName = "TestExch";
            var exchangeWebResource = "https://example.com";

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new CreateExchangerCommand
            {
                UserId = wrongUserId,
                Name = exchangerName,
                WebResourceUrl = exchangeWebResource
            },
            CancellationToken.None));
        }
    }
}

