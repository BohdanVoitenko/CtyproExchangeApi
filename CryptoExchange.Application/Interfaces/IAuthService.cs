using System;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Interfaces
{
    public interface IAuthService
    {
        string GenerateJwtToken(AppUser user);

    }
}

