using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders;
using CryptoExchange.Tests.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Exchangers.Commands
{
    public class DeleteAllOrdersHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteAllOrdersHandlerTests_Success()
        {
            //Arrange
            var handler = new DeleteAllOrdersHandler(Context);
            var rightExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314");

            //Act
            var result = await handler.Handle(new DeleteAllOrdersCommand
            {
                ExchangerId = rightExchangerId
            },
            CancellationToken.None);


            //Assert
            result.ShouldBeOfType<Unit>();
        }

        [Fact]
        public async Task DeleteAllOrdersHandlerTests_FailureOnWrongId()
        {
            //Arrange
            var handler = new DeleteAllOrdersHandler(Context);
            var wrongExchangerId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new DeleteAllOrdersCommand
            {
                ExchangerId = wrongExchangerId
            },
            CancellationToken.None));
        }
    }
}

