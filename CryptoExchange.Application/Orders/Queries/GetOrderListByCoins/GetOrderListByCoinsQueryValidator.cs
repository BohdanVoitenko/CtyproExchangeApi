using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Queries.GetOrderListByCoins
{
    public class GetOrderListByCoinsQueryValidator : AbstractValidator<GetOrderListByCoinsQuery>
    {
        public GetOrderListByCoinsQueryValidator()
        {
            RuleFor(command => command.From).NotEmpty().MaximumLength(10);
            RuleFor(command => command.To).NotEmpty().MaximumLength(10);
        }
    }
}

