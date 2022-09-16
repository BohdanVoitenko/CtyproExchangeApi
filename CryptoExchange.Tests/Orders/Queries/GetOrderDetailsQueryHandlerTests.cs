using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Orders.Queries.GetOrderDetails;
using CryptoExchange.Domain;
using CryptoExchange.Persistence;
using CryptoExchange.Tests.Common;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Orders.Queries
{
    [Collection("QueryCollection")]
    public class GetOrderDetailsQueryHandlerTests
    {
        private readonly CryptoExchangeDbContext Context;
        private readonly IMapper Mapper;

        public GetOrderDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetOrderDatailsQueryHandlerTests_Success()
        {
            //Arrange
            var handler = new GetOrderDetailsQueryHandler(Context, Mapper);
            var rightOrderId = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39");
            var rightExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314");

            //Act
            var result = await handler.Handle(new GetOrderDetailsQuery
            {
                Id = rightOrderId,
                ExchangerId = rightExchangerId
            },
            CancellationToken.None);

            //Assert

            result.ShouldBeOfType<OrderDetailsVm>();
            result.ExchangeFrom.ShouldBe("LINK");
            result.ShouldBeEquivalentTo(new OrderDetailsVm
            {
                Id = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39"),
                Exchanger = "UaExchange",
                ExchangeFrom = "LINK",
                ExchangeTo = "NEAR",
                IncomeSum = 1,
                OutcomeSum = 23.4319,
                Amount = 233.7492,
                MinAmount = 0.23,
                MaxAmount = 7
            });

        }


        [Fact]
        public async Task GetOrderDetailsQueryHandlerTests_FailedOnWrongOrderId()
        {
            //Arrange
            var handler = new GetOrderDetailsQueryHandler(Context, Mapper);
            var wrongOrderId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");
            var rightExchangerId = Guid.Parse("d0bd707a-3a92-4ac7-b7ce-682d0f1fb314");

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new GetOrderDetailsQuery
            {
                ExchangerId = rightExchangerId,
                Id = wrongOrderId
            },
            CancellationToken.None));
        }


        [Fact]
        public async Task GetOrderDetailsQueryHandlerTests_FailedOnWrongExchangerId()
        {
            //Arrange
            var handler = new GetOrderDetailsQueryHandler(Context, Mapper);
            var rightOrderId = Guid.Parse("c05ec9b3-ac9b-4273-aa89-15ca2e88db39");
            var wrongExchangerId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new GetOrderDetailsQuery
            {
                ExchangerId = wrongExchangerId,
                Id = rightOrderId
            },
            CancellationToken.None));
        }
    }
}

