using System;
using FluentValidation;

namespace CryptoExchange.Application.Exchangers.Queries
{
    public class GetExchangerInfoQueryValidator : AbstractValidator<GetExchangerInfoQuery>
    {
        public GetExchangerInfoQueryValidator()
        {
            RuleFor(getExchangerInfoQuery => getExchangerInfoQuery.ExchangerName).NotNull().NotEqual(string.Empty)
                .MinimumLength(6).MaximumLength(24);
        }
    }
}

