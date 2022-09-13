using System;
using MediatR;

namespace CryptoExchange.Application.UsersAuth.Commands.LogoutUserCommand
{
	public class LogoutUserCommand : IRequest<string>
	{
		public string Id { get; set; }
	}
}

