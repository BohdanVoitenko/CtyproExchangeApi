﻿using System;
using System.Reflection;
using CryptoExchange.Application.Common.Behaviour;
using CryptoExchange.Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoExchange.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
        {

			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
			services.AddTransient(typeof(IPipelineBehavior<,>),
				typeof(ValidationBehavior<,>));
			return services;
        }
	}
}

