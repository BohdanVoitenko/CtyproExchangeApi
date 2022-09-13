using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using CryptoExchange.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Persistence
{
	public class CryptoExchangeDbContext : IdentityDbContext<IdentityUser>, ICryptoExchangeDbContext
	{
		public CryptoExchangeDbContext(DbContextOptions<CryptoExchangeDbContext> options)
			:base(options){ }

        public DbSet<Order> Orders { get; set; }
		public DbSet<AppUser> Users { get; set; }
		public DbSet<Exchanger> Exchangers { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
        {
			builder.ApplyConfiguration(new OrderConfiguration());
			builder.ApplyConfiguration(new AppUserConfiguration());
			builder.ApplyConfiguration(new ExchangerConfiguration());
			base.OnModelCreating(builder);
        }
    }
}

