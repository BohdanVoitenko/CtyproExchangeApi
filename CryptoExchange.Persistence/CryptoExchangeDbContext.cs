using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using CryptoExchange.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Persistence
{
	public class CryptoExchangeDbContext : DbContext, ICryptoExchangeDbContext
	{
		public CryptoExchangeDbContext(DbContextOptions<CryptoExchangeDbContext> options)
			:base(options){ }

        public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.ApplyConfiguration(new OrderConfiguration());
			base.OnModelCreating(builder);
        }
    }
}

