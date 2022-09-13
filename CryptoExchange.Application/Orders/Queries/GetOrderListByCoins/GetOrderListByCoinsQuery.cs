using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Queries.GetOrderListByCoins
{
	public class GetOrderListByCoinsQuery : IRequest<OrderListByCoinsVm>
	{
		public string From { get; set; }
		public string To { get; set; }
	}
}

