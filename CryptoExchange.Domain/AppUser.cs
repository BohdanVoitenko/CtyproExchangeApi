using System;
using Microsoft.AspNetCore.Identity;

namespace CryptoExchange.Domain
{
	public class AppUser : IdentityUser
	{
        public string Password { get; set; }
        public Exchanger? Exchanger { get; set; }
        public Guid? ExchangerId { get; set; }

    }
}

