using System;
namespace CryptoExchange.Application.Common.Caching
{
	public class RedisCacheSettings
	{
		public bool Enabled { get; set; }
		public string ConnectionString { get; set; }
	}
}

