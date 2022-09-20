using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
            RuleFor(deleteOrderCommand => deleteOrderCommand.ExchangerId).NotEqual(Guid.Empty);
			RuleFor(deleteOrderCommand => deleteOrderCommand.OrderId).NotEqual(Guid.Empty);
        }
	}
}

