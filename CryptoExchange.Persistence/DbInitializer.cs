using System;
namespace CryptoExchange.Persistence
{
	public static class DbInitializer
	{
		public static void Initialize(CryptoExchangeDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        }
	}
}

