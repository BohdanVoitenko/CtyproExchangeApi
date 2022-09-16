using System;
using MediatR;

namespace CryptoExchange.Application.Exchangers.Queries
{
    public class GetExchangerInfoQuery : IRequest<ExchangerInfoVm>
    {
        public string ExchangerName { get; set; }
    }
}

