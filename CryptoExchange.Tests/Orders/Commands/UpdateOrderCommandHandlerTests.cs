using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;
using CryptoExchange.Application.Orders.Commands.UpdateOrder;
using CryptoExchange.Domain;
using CryptoExchange.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CryptoExchange.Tests.Orders.Commands
{
    public class UpdateOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateOrderCommandHandler_Success()
        {
            //Arrange
            var handler = new UpdateOrderCommandHandler(Context);
            var rightExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9");
            var rightOrderId = Guid.Parse("c76e7da0-ff36-45c5-a572-154f07573801");
            var newFrom = "XRP";
            var newTo = "APE";
            var newIn = 2;
            var newOut = 11.01;
            var newAmount = 200.342;
            var newMinAmount = 0.10;
            var newMaxAmount = 6;

            //Act
            var result = handler.Handle(new UpdateOrderCommand
            {
                OrderId = rightOrderId,
                ExchangerId = rightExchangerId,
                From = newFrom,
                To = newTo,
                In = newIn,
                Out = newOut,
                Amount = newAmount,
                MinAmount = newMinAmount,
                MaxAmount = newMaxAmount
            },
            CancellationToken.None);

            //Assert
            Assert.NotNull(await Context.Orders.FirstOrDefaultAsync(order => order.Id == rightOrderId && order.ExchangeFrom == newFrom
            && order.ExchangeTo == newTo && order.IncomeSum == newIn && order.OutcomeSum == newOut
            && order.Amount == newAmount && order.MinAmount == newMinAmount && order.MaxAmount == newMaxAmount));
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_FailOnWrongOrderId()
        {
            //Arrange
            var handler = new UpdateOrderCommandHandler(Context);
            var rightExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9");
            var wrongOrderId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");
            var newFrom = "XRP";
            var newTo = "APE";
            var newIn = 2;
            var newOut = 11.01;
            var newAmount = 200.342;
            var newMinAmount = 0.10;
            var newMaxAmount = 6;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateOrderCommand
            {
                OrderId = wrongOrderId,
                ExchangerId = rightExchangerId,
                From = newFrom,
                To = newTo,
                In = newIn,
                Out = newOut,
                Amount = newAmount,
                MinAmount = newMinAmount,
                MaxAmount = newMaxAmount
            },
            CancellationToken.None));
        }

        [Fact]
        public async Task UpdateOrderCommandHandler_FailOnWrongExchangerId()
        {
            //Arrange
            var handler = new UpdateOrderCommandHandler(Context);
            var wrongExchangerId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");
            var rightOrderId = Guid.Parse("c76e7da0-ff36-45c5-a572-154f07573801");
            var newFrom = "XRP";
            var newTo = "APE";
            var newIn = 2;
            var newOut = 11.01;
            var newAmount = 200.342;
            var newMinAmount = 0.10;
            var newMaxAmount = 6;

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new UpdateOrderCommand
            {
                OrderId = rightOrderId,
                ExchangerId = wrongExchangerId,
                From = newFrom,
                To = newTo,
                In = newIn,
                Out = newOut,
                Amount = newAmount,
                MinAmount = newMinAmount,
                MaxAmount = newMaxAmount
            },
            CancellationToken.None));
        }
    }
}

