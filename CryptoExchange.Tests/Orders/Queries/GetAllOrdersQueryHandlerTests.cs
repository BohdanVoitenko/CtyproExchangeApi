using System;
using AutoMapper;
using CryptoExchange.Application.Orders.Queries.GetAllOrdersQuery;
using CryptoExchange.Persistence;
using CryptoExchange.Tests.Common;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Orders.Queries
{
    [Collection("QueryCollection")]
    public class GetAllOrdersQueryHandlerTests
    {
        private readonly CryptoExchangeDbContext Context;
        private readonly IMapper Mapper;

        public GetAllOrdersQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetAllOrdersQueryHandlerTests_Succes()
        {
            //Arrange
            var handler = new GetAllOrdersQueryHandler(Context, Mapper);

            //Act
            var result = await handler.Handle(new GetAllOrdersQuery(), CancellationToken.None);

            //Assert
            result.ShouldBeOfType<AllOrdersVm>();
            result.Orders.Count.ShouldBe(9);
        }
    }
}

