using System;
using MediatR;

namespace CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders
{
    public class DeleteAllOrdersCommand : IRequest<DeleteAllOrdersResultVm>
    {
        public Guid ExchangerId { get; set; }
    }
}

