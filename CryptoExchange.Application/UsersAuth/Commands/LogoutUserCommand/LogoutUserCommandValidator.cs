using System;
using FluentValidation;

namespace CryptoExchange.Application.UsersAuth.Commands.LogoutUserCommand
{
	public class LogoutUserCommandValidator : AbstractValidator<LogoutUserCommand>
	{
		public LogoutUserCommandValidator()
		{
			RuleFor(logoutUserCommand => logoutUserCommand.Id).NotNull().NotEqual(string.Empty);
		}
	}
}

