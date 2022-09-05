using System;
using CryptoExchange.Domain;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Application.Interfaces
{
	public interface ICryptoExchangeDbContext
	{
		DbSet<Order> Orders { get; set; }

		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
	}
}

