using System;
namespace CryptoExchange.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderResultVm
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}

