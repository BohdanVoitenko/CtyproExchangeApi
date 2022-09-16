using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Orders.Queries.GetOrderListByCoins;
using CryptoExchange.Persistence;
using CryptoExchange.Tests.Common;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Orders.Queries
{
    [Collection("QueryCollection")]
    public class GetOrderListByCoinsQueryHandlerTests
    {
        private readonly CryptoExchangeDbContext Context;
        private readonly IMapper Mapper;

        public GetOrderListByCoinsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetOrderListBuCoinsQueryHandlerTests_Succes()
        {
            //Arrange
            var handler = new GetOrderListByCoinsQueryHandler(Context, Mapper);
            var coinFromThatExists = "BTC";
            var coinToThatExists = "XRP";

            //Act
            var result = await handler.Handle(new GetOrderListByCoinsQuery
            {
                From = coinFromThatExists,
                To = coinToThatExists
            },
            CancellationToken.None);

            //Assert
            result.ShouldBeOfType<OrderListByCoinsVm>();
            result.Orders.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetOrderListBuCoinsQueryHandlerTests_FailedByWrongCoinPair()
        {
            //Arrange
            var handler = new GetOrderListByCoinsQueryHandler(Context, Mapper);
            var wrongFromCoin = "SUV";
            var wrongToCoin = "BBB";

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new GetOrderListByCoinsQuery
            {
                From = wrongFromCoin,
                To = wrongToCoin
            },
            CancellationToken.None));
        }

    }
}

