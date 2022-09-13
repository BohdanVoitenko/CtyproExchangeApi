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
            var exchanger = _dbContext.Exchangers.FirstOrDefault(e => e.Id == request.ExchangerId);

            if (exchanger == null) throw new Exception($"Cannot find exchanger with id: {request.ExchangerId}");

            var order = new Order
            {
                ExchangerId = request.ExchangerId,
                ExchangerName = exchanger.Name,
                ExchangeTo = request.To.ToUpper(),
                ExchangeFrom = request.From.ToUpper(),
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

            exchanger.Orders.Add(order);

            _dbContext.Exchangers.Attach(exchanger);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return order.Id;
        }
    }
}

