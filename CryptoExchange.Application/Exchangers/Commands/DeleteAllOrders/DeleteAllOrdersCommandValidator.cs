using System;
using FluentValidation;

namespace CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders
{
    public class DeleteAllOrdersCommandValidator : AbstractValidator<DeleteAllOrdersCommand>
    {
        public DeleteAllOrdersCommandValidator()
        {
            RuleFor(command => command.ExchangerId).NotNull().NotEqual(Guid.Empty);
        }
    }
}

