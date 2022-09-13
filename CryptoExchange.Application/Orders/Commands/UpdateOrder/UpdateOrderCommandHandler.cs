using System;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Commands.UpdateOrder
{
	public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
	{
		private readonly ICryptoExchangeDbContext _dbContext;

		public UpdateOrderCommandHandler(ICryptoExchangeDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
			var entity =
				await _dbContext.Orders.FirstOrDefaultAsync(order =>
				order.Id == request.OrderId, cancellationToken);

			if(entity == null || entity.ExchangerId != request.ExchangerId)
            {
				throw new NotFoundException(nameof(Order), request.OrderId);
            }

			entity.ExchangeFrom = request.From;
			entity.ExchangeTo = request.To;
			entity.Amount = request.Amount;
			entity.OutcomeSum = request.Out;
			entity.IncomeSum = request.In;
			entity.MinAmount = request.MinAmount;
			entity.MaxAmount = request.MaxAmount;
			entity.EditTime = DateTime.Now;

			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
        }
	}
}

