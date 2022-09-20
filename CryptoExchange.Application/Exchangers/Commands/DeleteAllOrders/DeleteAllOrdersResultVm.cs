using System;
namespace CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders
{
    public class DeleteAllOrdersResultVm
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
    }
}

