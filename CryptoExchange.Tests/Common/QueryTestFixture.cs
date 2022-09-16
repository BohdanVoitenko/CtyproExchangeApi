using System;
using System.Reflection;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Persistence;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CryptoExchange.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public CryptoExchangeDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = CryptoExchangeContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(typeof(ICryptoExchangeDbContext).Assembly));
                cfg.AddProfile(new AssemblyMappingProfile(typeof(Program).Assembly));
                cfg.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
            });
            //Mapper = configurationProvider.CreateMapper();
            Mapper = new Mapper(configurationProvider);
        }

        public void Dispose()
        {
            CryptoExchangeContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}

