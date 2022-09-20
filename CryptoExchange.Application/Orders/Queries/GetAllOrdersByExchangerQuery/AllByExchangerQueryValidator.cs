using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery
{
    public class AllByExchangerQueryValidator : AbstractValidator<AllByExchangerQuery>
    {
        public AllByExchangerQueryValidator()
        {
            RuleFor(command => command.ExchangerId).NotEqual(Guid.Empty);
        }
    }
}

