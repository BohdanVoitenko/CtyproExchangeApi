using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Domain;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, DeleteOrderResultVm>
	{
        private readonly ICryptoExchangeDbContext _dbContext;

        public DeleteOrderCommandHandler(ICryptoExchangeDbContext dbContext) => _dbContext = dbContext;

        public async Task<DeleteOrderResultVm> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Orders.FindAsync(new object[] { request.OrderId }, cancellationToken);

            if(entity == null || entity.ExchangerId != request.ExchangerId)
            {
                throw new NotFoundException(nameof(Order), request.OrderId);
            }

            _dbContext.Orders.Remove(entity);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception exception)
            {
                return new DeleteOrderResultVm
                {
                    Success = false,
                    Error = exception.Message
                };
            }

            return new DeleteOrderResultVm { Success = true };
        }
    }
}

