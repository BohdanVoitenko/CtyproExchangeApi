using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using FluentValidation.TestHelper;
using MediatR;

namespace CryptoExchange.Application.Orders.Commands.CreateFromXmlFile
{
	public class CreateOrderListCommandHandler : IRequestHandler<CreateOrderListCommand, OrderListFromXmlVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		private readonly IMapper _mapper;

		public CreateOrderListCommandHandler(ICryptoExchangeDbContext dbContext, IMapper mapper)
		=> (_dbContext, _mapper) = (dbContext, mapper);

		public async Task<OrderListFromXmlVm> Handle(CreateOrderListCommand request, CancellationToken cancellationToken)
        {
            List<Order> orders = new List<Order>();
            var list = new List<OrderDto>();

            XDocument xdoc1 = XDocument.Load(request.FilePath);

            orders =
               (from _order in xdoc1.Element("orders").Elements("order")
                select new Order
                {
                    ExchangeFrom = _order.Element("from").Value,
                    ExchangeTo = _order.Element("to").Value,
                    IncomeSum = ((double)_order.Element("in")),
                    OutcomeSum = ((double)_order.Element("out")),
                    Amount = ((double)_order.Element("amount")),
                    MinAmount = ((double)_order.Element("minamount")),
                    MaxAmount = ((double)_order.Element("maxamount"))
                }).ToList();

            var exchanger = _dbContext.Exchangers.FirstOrDefault(ex => ex.Id == request.ExchangerId);

            foreach (var order in orders)
            {
                order.Id = new Guid();
                order.Exchanger = exchanger;
                order.ExchangerId = exchanger.Id;
                order.ExchangerName = exchanger.Name;
                list.Add(_mapper.Map<OrderDto>(order));
            }

            _dbContext.Orders.AddRange(orders);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new OrderListFromXmlVm { Orders = list};
        }
	}
}

