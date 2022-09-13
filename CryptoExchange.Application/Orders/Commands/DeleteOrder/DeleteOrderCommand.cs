using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest
	{
		public Guid OrderId { get; set; }
		public Guid ExchangerId { get; set; }
	}
}

