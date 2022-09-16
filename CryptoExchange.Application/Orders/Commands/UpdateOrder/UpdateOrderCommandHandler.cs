using System;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Domain;
using System.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

			var changedEntity = _dbContext.Orders.Attach(entity);
			changedEntity.State = EntityState.Modified;
			
			await _dbContext.SaveChangesAsync(cancellationToken);

			return Unit.Value;
        }
	}
}

