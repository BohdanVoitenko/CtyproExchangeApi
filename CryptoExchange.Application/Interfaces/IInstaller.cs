using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoExchange.Application.Interfaces
{
	public interface IInstaller
	{
		void InstallServices(IServiceCollection services, IConfiguration configuration);
	}
}

