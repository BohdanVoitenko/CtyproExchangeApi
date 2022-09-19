using System;
namespace CryptoExchange.Application
{
    public class RequestResult
    {
        public bool Succes { get; set; }
        public string? Message { get; set; }
        public List<Object> Result { get; set; }
    }
}

