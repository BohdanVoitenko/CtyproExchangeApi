using System;
using MediatR;

namespace CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders
{
    public class DeleteAllOrdersCommand : IRequest
    {
        public Guid ExchangerId { get; set; }
    }
}

