using System;
using CryptoExchange.Persistence;

namespace CryptoExchange.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly CryptoExchangeDbContext Context;

        public TestCommandBase()
        {
            Context = CryptoExchangeContextFactory.Create();
        }

        public void Dispose()
        {
            CryptoExchangeContextFactory.Destroy(Context);
        }
    }
}

