using System;
using FluentValidation;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommandValidator : AbstractValidator<CreateExchangerCommand>
	{
		public CreateExchangerCommandValidator()
		{
			RuleFor(createExchangerCommand => createExchangerCommand.UserId).NotNull().NotEqual(string.Empty);
			RuleFor(createExchangerCommand => createExchangerCommand.Name).NotNull().NotEqual(string.Empty)
				.MinimumLength(6).MaximumLength(24);
			RuleFor(createExchangerCommand => createExchangerCommand.WebResourceUrl).NotNull().NotEqual(string.Empty)
				.MinimumLength(6);
		}
	}
}

