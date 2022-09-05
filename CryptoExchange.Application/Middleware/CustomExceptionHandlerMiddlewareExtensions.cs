using System;
using Microsoft.AspNetCore.Builder;

namespace CryptoExchange.Application.Middleware
{
	public static class CustomExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
			return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
	}
}

