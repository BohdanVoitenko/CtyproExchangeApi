using System;
using FluentValidation;

namespace CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand
{
	public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
	{
		public CreateUserCommandValidator()
		{
			RuleFor(createUserCommand => createUserCommand.UserName).MaximumLength(20).NotNull().NotEqual(string.Empty);
			RuleFor(createUserCommand => createUserCommand.Email).NotNull().NotEqual(string.Empty).EmailAddress()
				.MinimumLength(6).MaximumLength(24);
			RuleFor(createUserCommand => createUserCommand.Password).NotNull()
				.NotEqual(string.Empty).MinimumLength(6);
		}

	}
}

