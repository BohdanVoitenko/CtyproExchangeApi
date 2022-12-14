using System;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommand : IRequest<UpdateOrderResultVm>
	{
		public Guid ExchangerId { get; set; }
		public Guid OrderId { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public double In { get; set; }
		public double Out { get; set; }
		public double Amount { get; set; }
		public double MinAmount { get; set; }
		public double MaxAmount { get; set; }
	}
}

