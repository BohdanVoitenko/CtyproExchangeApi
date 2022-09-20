using System;
namespace CryptoExchange.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderResultVm
    {
        public bool Succes { get; set; }
        public Guid OrderId { get; set; }
        public string? Error { get; set; }
    }
}

