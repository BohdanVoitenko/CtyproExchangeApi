using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Queries.GetOrderDetails
{
	public class GetOrderDetailsQuery : IRequest<OrderDetailsVm>
	{
		public Guid Id { get; set; }
		public Guid ExchangerId { get; set; }
	}
}

