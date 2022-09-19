using System;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Commands.CreateFromXmlFile
{
	public class OrderListFromXmlVm
	{
		public bool Success { get; set; }
		public string? Error { get; set; }
		public List<OrderDto> Orders { get; set; }

	}
}

