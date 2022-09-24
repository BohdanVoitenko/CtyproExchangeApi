using System;
using System.Data.Common;
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
            //var connectionString = configuration["DbConnection"];

            services.AddDbContext<CryptoExchangeDbContext>(options =>
            {
                //var host = configuration["DB_HOST"] ?? configuration["DbConnection:DefaultHost"];
                //            var port = configuration["DB_PORT"] ?? configuration["DbConnection:DefaultPort"];
                //            var user = configuration["DB_USER"] ?? configuration["DbConnection:DefaultUser"];
                //            var password = configuration["DB_PASSWORD"] ?? configuration["DbConnection:DefaultPass"];
                //            var dbName = configuration["DB_NAME"] ?? configuration["DbConnection:DefaultDb"];

                //            var connectionString = $"host={host};port={port};database={dbName};username={user};password={password}";

                var host = Environment.GetEnvironmentVariable("DB_HOST");
                var port = Environment.GetEnvironmentVariable("DB_PORT");
                var user = Environment.GetEnvironmentVariable("DB_USER");
                var pass = Environment.GetEnvironmentVariable("DB_PASSWORD");
                var db = Environment.GetEnvironmentVariable("DB_NAME");
                var connectionString = $"host={host};port={port};database={db};username={user};password={pass}";

                options.UseNpgsql(connectionString);

            });

            services.AddScoped<ICryptoExchangeDbContext>(provider =>
                provider.GetService<CryptoExchangeDbContext>());

            return services;

		}
	}
}

