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
			var connectionString = configuration["DbConnection"];
			services.AddDbContext<CryptoExchangeDbContext>(options =>
			{
				options.UseSqlite(connectionString);
			});
            services.AddScoped<ICryptoExchangeDbContext>(provider =>
                provider.GetService<CryptoExchangeDbContext>());
            //services.AddScoped<UserManager<AppUser>>();
            //services.AddScoped<IUserManager<IdentityUser>>(provider =>
            //    provider.GetService<ApplicationUserManager<AppUser>>());
            //services.AddScoped<ISignInManager<IdentityUser>>(provider =>
            //    provider.GetService<ApplicationSignInManager<AppUser>>());

            return services;

		}
	}
}

