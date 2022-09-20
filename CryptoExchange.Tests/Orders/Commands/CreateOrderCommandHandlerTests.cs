using System;
using CryptoExchange.Application.Orders.Commands.CreateOrder;
using CryptoExchange.Domain;
using CryptoExchange.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace CryptoExchange.Tests.Orders.Commands
{
    public class CreateOrderCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateOrderCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateOrderCommandHandler(Context);

            var exchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9");
            var from = "BTC";
            var to = "XRP";
            var income = 1;
            var outcome = 12.13;
            var amount = 390.23;
            var minamount = 0.12;
            var maxamount = 4;

            //Act
            var result = await handler.Handle(
                    new CreateOrderCommand
                    {
                        ExchangerId = exchangerId,
                        From = from,
                        To = to,
                        In = income,
                        Out = outcome,
                        Amount = amount,
                        MinAmount = minamount,
                        MaxAmount = maxamount,
                        OrderId = Guid.Parse("c718d094-6727-4775-9c54-3783d354e845")
                    },
                    CancellationToken.None
            );


            //Assert
            //Assert.NotNull(
            //    await Context.Orders.SingleOrDefaultAsync(order => order.Id == orderId));
            result.ShouldBeOfType<CreateOrderResultVm>();
            result.OrderId.ShouldNotBe(Guid.Empty);


        }
    }
}

