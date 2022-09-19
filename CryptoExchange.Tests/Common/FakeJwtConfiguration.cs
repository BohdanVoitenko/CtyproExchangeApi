using System;
using Castle.Core.Configuration;

namespace CryptoExchange.Tests.Common
{
    public class FakeJwtConfiguration : IConfiguration
    {
        public string Key { get; set; }
        public string Issuer { get; set; }

        public string Name => throw new NotImplementedException();

        public string Value => throw new NotImplementedException();

        public ConfigurationCollection Children => throw new NotImplementedException();

        public ConfigurationAttributeCollection Attributes => throw new NotImplementedException();

        public object GetValue(Type type, object defaultValue)
        {
            throw new NotImplementedException();
        }
    }
}

