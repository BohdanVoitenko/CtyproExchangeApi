using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Queries.GetOrderList
{
	public class GetOrderListQuery : IRequest<OrderListVm>
	{
		public Guid ExchangerId { get; set; }
	}
}

