using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Orders.Commands.DeleteOrder;
using CryptoExchange.Tests.Common;

namespace CryptoExchange.Tests.Orders.Commands
{
    public class DeleteOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteOrderCommandHandler_Success()
        {
            //Arrange
            var handler = new DeleteOrderCommandHandler(Context);

            //Act
            await handler.Handle(new DeleteOrderCommand {
                OrderId = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39"),
                ExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314")
            },
            CancellationToken.None);
            //Assers
            Assert.Null(Context.Orders.SingleOrDefault(order => order.Id == Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39")
                && order.ExchangerId == Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9")));
        }

        [Fact]
        public async Task DeleteOrderCommandHandler_FailedOnWrongId()
        {
            //Arrange
            var handler = new DeleteOrderCommandHandler(Context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new DeleteOrderCommand
            {
                OrderId = Guid.Parse("5af45918-c47c-47d8-ac64-b0207051bca0"),
                ExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9")
            },
            CancellationToken.None));

        }

        [Fact]
        public async Task DeleteOrderCommandHandler_FailedOnWrongExhcangerId()
        {
            //Arrange
            var handler = new DeleteOrderCommandHandler(Context);

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new DeleteOrderCommand
            {
                OrderId = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39"),
                ExchangerId = Guid.Parse("5af45918-c47c-47d8-ac64-b0207051bca0")
            },
            CancellationToken.None));

        }
    }
}

