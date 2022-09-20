using System;
namespace CryptoExchange.Application.Interfaces
{
    public interface IUserService
    {
        public string GetUserName();

        public string GetTraceIdentifier();
    }
}

