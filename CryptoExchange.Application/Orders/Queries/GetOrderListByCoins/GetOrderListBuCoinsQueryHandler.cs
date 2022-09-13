using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CryptoExchange.Application.Orders.Queries.GetOrderListByCoins
{
	public class GetOrderListBuCoinsQueryHandler : IRequestHandler<GetOrderListByCoinsQuery, OrderListByCoinsVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetOrderListBuCoinsQueryHandler(ICryptoExchangeDbContext dbContext, IMapper mapper)
			=> (_dbContext, _mapper) = (dbContext, mapper);

		public async Task<OrderListByCoinsVm> Handle(GetOrderListByCoinsQuery request, CancellationToken cancellationToken)
        {
			var ordersQuery = await _dbContext.Orders
				.Where(order => order.ExchangeFrom == request.From && order.ExchangeTo == request.To)
				.ProjectTo<OrderListByCoinsDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);


			return new OrderListByCoinsVm { Orders = ordersQuery };
        }
	}
}

