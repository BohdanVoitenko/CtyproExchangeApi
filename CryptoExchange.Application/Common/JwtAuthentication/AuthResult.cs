using System;
namespace CryptoExchange.Application.Common.JwtAuthentication
{
    public class AuthResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Error { get; set; }
        public string? UserId { get; set; }
    }
}

