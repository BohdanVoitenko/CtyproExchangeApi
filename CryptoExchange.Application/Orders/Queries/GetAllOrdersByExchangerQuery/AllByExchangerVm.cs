using System;
namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery
{
	public class AllByExchangerVm
	{
		public bool Success { get; set; }
		public string? Error { get; set; }
		public List<AllByExchangerDto>? Orders { get; set; }
	}
}

