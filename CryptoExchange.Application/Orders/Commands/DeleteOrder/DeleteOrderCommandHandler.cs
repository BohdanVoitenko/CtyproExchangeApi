using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Domain;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.DeleteOrder
{
	public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
	{
        private readonly ICryptoExchangeDbContext _dbContext;

        public DeleteOrderCommandHandler(ICryptoExchangeDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var entity =
                await _dbContext.Orders.FindAsync(new object[] { request.Id }, cancellationToken);

            if(entity == null || entity.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            _dbContext.Orders.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}

