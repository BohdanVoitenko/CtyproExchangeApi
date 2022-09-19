using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Orders.Commands.CreateFromXmlFile;
using CryptoExchange.Domain;
using CryptoExchange.Tests.Common;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Orders.Commands
{
    [Collection("QueryCollection")]
    public class CreateOrderListCommandHandlerTests : TestCommandBase
    {
        private readonly IMapper mapper;

        public CreateOrderListCommandHandlerTests(QueryTestFixture fixture)
        {
            mapper = fixture.Mapper;
        }

        [Fact]
        public async Task CreateOrderListCommandHandlerTests_Success()
        {
            //Arrange
            var handler = new CreateOrderListCommandHandler(Context, mapper);
            var exchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314");
            var filePath = "/Users/Ishootdice/Projects/CryptoExchange/CryptoExchange.Api/Test.xml";

            //Act
            var result = await handler.Handle(new CreateOrderListCommand
            {
                ExchangerId = exchangerId,
                FilePath = filePath
            },
            CancellationToken.None);

            //Assert
            result.ShouldBeOfType<OrderListFromXmlVm>();
            result.Orders.Count.ShouldBe(7);
        }

        [Fact]
        public async Task CreateOrderListCommandHandlerTests_FailureOnWrongExchangerId()
        {
            //Arrange
            var handler = new CreateOrderListCommandHandler(Context, mapper);
            var wrongExchangerId = Guid.Parse("c76e7da0-ff36-45c5-a572-154f07573801");
            var filePath = "/Users/Ishootdice/Projects/CryptoExchange/CryptoExchange.Api/Test.xml";
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new CreateOrderListCommand
            {
                ExchangerId = wrongExchangerId,
                FilePath = filePath
            },
            CancellationToken.None));

        }

        [Fact]
        public async Task CreateOrderListCommandHandlerTests_FailureOnWrongFilePath()
        {
            //Arrange
            var handler = new CreateOrderListCommandHandler(Context, mapper);
            var exchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314");
            var wrongFilePath = "WrongPath/CryptoExchange.Api/Test.xml";
            //Act
            //Assert
            var requestResult = await handler.Handle(new CreateOrderListCommand
            {
                ExchangerId = exchangerId,
                FilePath = wrongFilePath
            },
            CancellationToken.None);

            //Assert
            requestResult.ShouldBeOfType<OrderListFromXmlVm>();
            requestResult.Orders.ShouldBeNull();
            requestResult.Success.ShouldBe(false);
        }
    }
}

