using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Commands.CreateFromXmlFile
{
    public class CreateOrderLisstCommandValidator : AbstractValidator<CreateOrderListCommand>
    {
        public CreateOrderLisstCommandValidator()
        {
            RuleFor(command => command.ExchangerId).NotEqual(Guid.Empty);
            RuleFor(command => command.FilePath).NotNull().NotEqual(string.Empty).MinimumLength(6).MaximumLength(50);
        }
    }
}

