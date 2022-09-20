using System;
using CryptoExchange.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CryptoExchange.Application.Common.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
            => _httpContextAccessor = httpContextAccessor;

        public string GetTraceIdentifier()
        {
            return _httpContextAccessor.HttpContext.TraceIdentifier;
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.User.Identity.Name;
        }
    }
}

