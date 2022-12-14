using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Queries.GetOrderList
{
	public class GetOrderListQueryValidator : AbstractValidator<GetOrderListQuery>
	{
		public GetOrderListQueryValidator()
		{
            RuleFor(order => order.ExchangerId).NotEqual(Guid.Empty);
        }
	}
}

