using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery
{
	public class AllByExchangerQueryHandler : IRequestHandler<AllByExchangerQuery, AllByExchangerVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		private readonly IMapper _mapper;

		public AllByExchangerQueryHandler(ICryptoExchangeDbContext dbContext, IMapper mapper)
		=> (_dbContext, _mapper) = (dbContext, mapper);

		public async Task<AllByExchangerVm> Handle(AllByExchangerQuery request, CancellationToken cancellationToken)
        {
			var requestQuery = await _dbContext.Orders
				.Where(order => order.ExchangerId == request.ExchangerId)
				.ProjectTo<AllByExchangerDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new AllByExchangerVm { Orders = requestQuery };
        }
	}
}

