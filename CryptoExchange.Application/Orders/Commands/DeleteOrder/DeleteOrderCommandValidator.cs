using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
	{
		public DeleteOrderCommandValidator()
		{
			RuleFor(deleteOrderCommand =>
				deleteOrderCommand.UserId).NotEqual(Guid.Empty);
			RuleFor(deleteOrderCommand =>
				deleteOrderCommand.Id).NotEqual(Guid.Empty);
		}
	}
}

