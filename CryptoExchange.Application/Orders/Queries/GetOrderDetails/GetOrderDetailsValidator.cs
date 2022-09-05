using System;
using FluentValidation;

namespace CryptoExchange.Application.Orders.Queries.GetOrderDetails
{
	public class GetOrderDetailsValidator : AbstractValidator<GetOrderDetailsQuery>
	{
		public GetOrderDetailsValidator()
		{
			RuleFor(order => order.Id).NotEqual(Guid.Empty);
			RuleFor(order => order.UserId).NotEqual(Guid.Empty);
		}
	}
}

