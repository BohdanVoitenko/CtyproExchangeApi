using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommand : IRequest
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
	}
}

