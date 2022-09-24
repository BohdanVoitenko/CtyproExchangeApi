using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.CreateOrder
{
	public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResultVm>
	{
        private readonly ICryptoExchangeDbContext _dbContext;

        public CreateOrderCommandHandler(ICryptoExchangeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CreateOrderResultVm> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
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
                CreationTime = DateTime.UtcNow,
                Id = Guid.NewGuid(),
                EditTime = null
            };

            _dbContext.Orders.Add(order);
            exchanger.Orders.Add(order);

            _dbContext.Exchangers.Attach(exchanger);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception exception)
            {
                return new CreateOrderResultVm
                {
                    Succes = false,
                    Error = exception.Message
                };
            }

            return new CreateOrderResultVm
            {
                Succes = true,
                OrderId = order.Id
            };
        }
    }
}

