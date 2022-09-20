using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderCommandValidator()
		{
            RuleFor(command => command.OrderId).NotEqual(Guid.Empty);
            RuleFor(command => command.ExchangerId).NotEqual(Guid.Empty);
            RuleFor(command => command.From).NotEmpty().MaximumLength(10);
            RuleFor(command => command.To).NotEmpty().MaximumLength(10);
            RuleFor(command => command.Amount).NotEmpty();
            RuleFor(command => command.MaxAmount).NotEmpty();
            RuleFor(command => command.MinAmount).NotEmpty();
            RuleFor(command => command.In).NotEmpty();
            RuleFor(command => command.Out).NotEmpty();
        }
	}
}

