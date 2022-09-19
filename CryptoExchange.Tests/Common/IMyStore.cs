using System;
using Microsoft.AspNetCore.Identity;

namespace CryptoExchange.Tests.Common
{
    public interface IMyStore<TUser> : IDisposable,
        IUserStore<TUser>,
        IUserEmailStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserPasswordStore<TUser> where TUser : class
    {
    }
}

