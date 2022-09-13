using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
	{
		public CreateOrderCommandValidator()
		{
            RuleFor(createOrderCommand =>
                createOrderCommand.ExchangerId).NotEqual(Guid.Empty);
            RuleFor(createOrderCommand =>
                createOrderCommand.From).NotEmpty().MaximumLength(10);
            RuleFor(createOrderCommand =>
                createOrderCommand.To).NotEmpty().MaximumLength(10);
        }
	}
}

