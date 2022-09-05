using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
	{
        private readonly ICryptoExchangeDbContext _dbContext;

        public CreateOrderCommandHandler(ICryptoExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                UserId = request.UserId,
                ExchangeTo = request.To,
                ExchangeFrom = request.From,
                MaxAmount = request.MaxAmount,
                MinAmount = request.MinAmount,
                Amount = request.Amount,
                IncomeSum = request.In,
                OutcomeSum = request.Out,
                CreationTime = DateTime.Now,
                Id = new Guid(),
                EditTime = null
            };

            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}

