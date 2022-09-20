using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace CryptoExchange.Application.Exchangers.Commands.DeleteAllOrders
{
    public class DeleteAllOrdersHandler : IRequestHandler<DeleteAllOrdersCommand, DeleteAllOrdersResultVm>
    {
        private readonly ICryptoExchangeDbContext _dbContext;

        public DeleteAllOrdersHandler(ICryptoExchangeDbContext dbContext)
         => _dbContext = dbContext;

        public async Task<DeleteAllOrdersResultVm> Handle(DeleteAllOrdersCommand request, CancellationToken cancellationToken)
        {
            var exchanger = await _dbContext.Exchangers.Include(x => x.Orders).FirstOrDefaultAsync(ex => ex.Id == request.ExchangerId);
            if (exchanger == null) throw new NotFoundException(nameof(Exchanger), request.ExchangerId);

            _dbContext.Orders.RemoveRange(exchanger.Orders);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception exception)
            {
                return new DeleteAllOrdersResultVm
                {
                    Success = false,
                    Error = exception.Message
                };
            }

            return new DeleteAllOrdersResultVm { Success = true };
        }
    }
}

