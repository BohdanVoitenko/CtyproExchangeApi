using System;
using MediatR;

namespace CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand
{
	public class CreateUserCommand : IRequest<string>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
	}
}

