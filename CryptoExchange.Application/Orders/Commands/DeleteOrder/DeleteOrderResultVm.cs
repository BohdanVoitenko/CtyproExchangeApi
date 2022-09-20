using System;
namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderResultVm
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}

