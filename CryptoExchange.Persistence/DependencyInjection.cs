using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using CryptoExchange.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoExchange.Persistence
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddPersistence(this IServiceCollection services,
			IConfiguration configuration)
		{
			var server = configuration["DatabaseServer"] ?? "";
			var port = configuration["DatabasePort"] ?? "";
			var user = configuration["DatabaseUser"] ?? "";
			var password = configuration["DatabasePassword"] ?? "";
			var database = configuration["Database"] ?? "";

			var connectionString = $"Server={server}, {port}; Initial Catalog={database}; User ID={user}; Password={password}";

            services.AddDbContext<CryptoExchangeDbContext>(options =>
            {
                options.UseSqlServer(connectionString);

            });

            services.AddScoped<ICryptoExchangeDbContext>(provider =>
                provider.GetService<CryptoExchangeDbContext>());

            return services;

		}
	}
}

