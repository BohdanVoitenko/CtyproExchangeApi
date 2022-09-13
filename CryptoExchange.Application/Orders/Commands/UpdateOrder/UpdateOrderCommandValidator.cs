using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
	{
		public UpdateOrderCommandValidator()
		{
            RuleFor(updateOrderCommand =>
                updateOrderCommand.ExchangerId).NotEqual(Guid.Empty);
            RuleFor(updateOrderCommand =>
                updateOrderCommand.From).NotEmpty().MaximumLength(10);
            RuleFor(updateOrderCommand =>
                updateOrderCommand.To).NotEmpty().MaximumLength(10);
            RuleFor(updateOrderCommand =>
                updateOrderCommand.OrderId).NotEqual(Guid.Empty);
        }
	}
}

