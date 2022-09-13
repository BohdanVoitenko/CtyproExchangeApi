using System;
using CryptoExchange.Application.Interfaces;
using MediatR;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Queries.GetOrderDetails
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsVm>
	{
        private readonly ICryptoExchangeDbContext _dbContext;

        private readonly IMapper _mapper;

        public GetOrderDetailsQueryHandler(ICryptoExchangeDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<OrderDetailsVm> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Orders.FirstOrDefaultAsync(order => order.Id == request.Id, cancellationToken);

            if(entity == null || entity.ExchangerId != request.ExchangerId)
            {
                throw new NotFoundException(nameof(Order), request.Id);
            }

            return _mapper.Map<OrderDetailsVm>(entity);
        }


    }
}

