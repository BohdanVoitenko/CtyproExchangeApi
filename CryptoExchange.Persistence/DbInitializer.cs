using System;
using Microsoft.Extensions.Configuration;

namespace CryptoExchange.Persistence
{
	public static class DbInitializer
	{
		public static void Initialize(CryptoExchangeDbContext context)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
	}
}

