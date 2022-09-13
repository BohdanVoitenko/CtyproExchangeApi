using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersQuery
{
	public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, AllOrdersVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		private readonly IMapper _mapper;

		public GetAllOrdersQueryHandler(ICryptoExchangeDbContext dbContext, IMapper mapper)
		=> (_dbContext, _mapper) = (dbContext, mapper);

		public async Task<AllOrdersVm> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
			var ordersQuery = await _dbContext.Orders.Where(order => order.Id != Guid.Empty).ProjectTo<AllOrdersDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new AllOrdersVm { Orders = ordersQuery };
        }
	}
}

