using System;
namespace CryptoExchange.Application.Exchangers.Commands.CreateExchanger
{
    public class CreateExchangerResultVm
    {
        public bool Success { get; set; }
        public string? Error { get; set; }
        public Guid ExchangerId { get; set; }
    }
}

