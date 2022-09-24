using System;
using System.Xml.Linq;
using System.Xml.Serialization;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

            try
            {
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
            }
            catch(Exception exception)
            {
                return new OrderListFromXmlVm
                {
                    Success = false,
                    Error = exception.Message
                };
            }

            var exchanger = _dbContext.Exchangers.FirstOrDefault(ex => ex.Id == request.ExchangerId);

            if(exchanger == null)
            {
                throw new NotFoundException(nameof(Exchanger), request.ExchangerId);
            }

            foreach (var order in orders)
            {
                order.Id = Guid.NewGuid();
                order.Exchanger = exchanger;
                order.ExchangerId = exchanger.Id;
                order.ExchangerName = exchanger.Name;
                order.CreationTime = DateTime.UtcNow;
                list.Add(_mapper.Map<OrderDto>(order));
            }

            _dbContext.Orders.AddRange(orders);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new OrderListFromXmlVm { Orders = list, Success = true };
        }
	}
}

