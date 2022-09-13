using System;
using MediatR;

namespace CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand
{
	public class LoginUserCommand : IRequest<string>
	{
		public string Id { get; set; }
		public string Password { get; set; }
	}
}

