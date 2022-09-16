using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
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
			var exhcanger = _dbContext.Exchangers.Where(exhcanger => exhcanger.Id == request.ExchangerId).SingleOrDefault();
			if (exhcanger == null) throw new NotFoundException(nameof(Exchanger), request.ExchangerId);

			var requestQuery = await _dbContext.Orders
				.Where(order => order.ExchangerId == request.ExchangerId)
				.ProjectTo<AllByExchangerDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);

			return new AllByExchangerVm { Orders = requestQuery };
        }
	}
}

