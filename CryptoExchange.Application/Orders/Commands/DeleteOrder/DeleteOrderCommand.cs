using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest<DeleteOrderResultVm>
	{
		public Guid OrderId { get; set; }
		public Guid ExchangerId { get; set; }
	}
}

