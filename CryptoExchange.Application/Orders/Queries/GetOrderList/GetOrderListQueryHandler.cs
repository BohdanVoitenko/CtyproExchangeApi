using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Application.Orders.Queries.GetOrderList
{
	public class GetOrderListQueryHandler : IRequestHandler<GetOrderListQuery, OrderListVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;

		private readonly IMapper _mapper;

		public GetOrderListQueryHandler(ICryptoExchangeDbContext dbContext,
			IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

		public async Task<OrderListVm> Handle(GetOrderListQuery request, CancellationToken cancellationToken)
        {
			var ordersQuery = await _dbContext.Orders
				.Where(order => order.UserId == request.UserId)
				.ProjectTo<OrderListDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new OrderListVm { Orders = ordersQuery };
        }
	}
}

