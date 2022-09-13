using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.CreateFromXmlFile
{
	public class CreateOrderListCommand : IRequest<OrderListFromXmlVm>
	{
		public Guid ExchangerId { get; set; }
		public string FilePath { get; set; }
	}
}

