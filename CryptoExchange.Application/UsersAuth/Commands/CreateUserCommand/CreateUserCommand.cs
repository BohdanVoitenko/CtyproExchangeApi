using System;
using CryptoExchange.Application.Common.JwtAuthentication;
using MediatR;

namespace CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand
{
	public class CreateUserCommand : IRequest<AuthResult>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}

