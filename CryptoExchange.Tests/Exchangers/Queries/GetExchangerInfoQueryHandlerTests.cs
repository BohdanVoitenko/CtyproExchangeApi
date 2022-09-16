using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Exchangers.Queries;
using CryptoExchange.Persistence;
using CryptoExchange.Tests.Common;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Exchangers.Queries
{
    [Collection("QueryCollection")]
    public class GetExchangerInfoQueryHandlerTests
    {
        private readonly CryptoExchangeDbContext Context;
        private readonly IMapper Mapper;

        public GetExchangerInfoQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetExchangerInfoQueryHandlerTests_Succes()
        {
            //Arrange
            var handler = new GetExchangerInfoQueryHandler(Context, Mapper);
            var rightExchangerName = "STExchange";


            //Act
            var result = await handler.Handle(new GetExchangerInfoQuery
            {
                ExchangerName = rightExchangerName
            },
            CancellationToken.None);


            //Assert
            result.ShouldBeOfType<ExchangerInfoVm>();
            Assert.Equal<string>(result.ExchangerName, rightExchangerName);
        }

        [Fact]
        public async Task GetExchangerInfoQueryHandlerTests_FailureOnWrongName()
        {
            //Arrange
            var handler = new GetExchangerInfoQueryHandler(Context, Mapper);
            var notExistingName = "sdfsdf";

            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await handler.Handle(new GetExchangerInfoQuery
            {
                ExchangerName = notExistingName
            },
            CancellationToken.None));
        }
    }
}

