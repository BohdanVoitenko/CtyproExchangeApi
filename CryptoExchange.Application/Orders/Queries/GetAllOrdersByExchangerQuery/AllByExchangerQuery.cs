using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery
{
	public class AllByExchangerQuery : IRequest<AllByExchangerVm>
	{
		public Guid ExchangerId { get; set; }

	}
}

