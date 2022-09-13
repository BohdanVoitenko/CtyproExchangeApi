using System;
using FluentValidation;

namespace CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand
{
	public class LoginUserCommangValidator : AbstractValidator<LoginUserCommand>
	{
		public LoginUserCommangValidator()
		{
			RuleFor(loginUserCommand => loginUserCommand.Id).NotEqual(string.Empty).NotNull();
			RuleFor(loginUserCommand => loginUserCommand.Password).MinimumLength(6).NotEqual(string.Empty).NotNull();
		}
	}
}

