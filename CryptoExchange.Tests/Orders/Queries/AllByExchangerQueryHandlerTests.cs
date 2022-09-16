using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery;
using CryptoExchange.Persistence;
using CryptoExchange.Tests.Common;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Orders.Queries
{
    [Collection("QueryCollection")]
    public class AllByExchangerQueryHandlerTests
    {
        private readonly CryptoExchangeDbContext Context;
        private readonly IMapper Mapper;

        public AllByExchangerQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }


        [Fact]
        public async Task AllByExchangerQueryHandlerTests_Success()
        {
            //Arrange
            var handler = new AllByExchangerQueryHandler(Context, Mapper);
            var rightExchangerId = Guid.Parse("629fd77f-7569-4daf-b800-98b1c5ffb5c9");

            //Act
            var result = await handler.Handle(new AllByExchangerQuery
            {
                ExchangerId = rightExchangerId
            },
            CancellationToken.None);

            //Assers
            result.ShouldBeOfType<AllByExchangerVm>();
            result.Orders.Count.ShouldBe(3);
        }

        [Fact]
        public async Task AllByExchangerQueryHandlerTests_FailedOnWrongId()
        {
            //Arrange
            var handler = new AllByExchangerQueryHandler(Context, Mapper);
            var wrongExchangerId = Guid.Parse("4593a508-9156-4c2f-a2b0-b4483e8833b7");

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new AllByExchangerQuery
            {
                ExchangerId = wrongExchangerId
            },
            CancellationToken.None));
        }
    }
}

