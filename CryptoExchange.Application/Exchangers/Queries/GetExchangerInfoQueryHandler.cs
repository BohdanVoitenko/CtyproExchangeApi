using System;
using AutoMapper;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoExchange.Application.Exchangers.Queries
{
    public class GetExchangerInfoQueryHandler : IRequestHandler<GetExchangerInfoQuery, ExchangerInfoVm>
    {
        private readonly ICryptoExchangeDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetExchangerInfoQueryHandler(ICryptoExchangeDbContext dbContext, IMapper mapper)
        => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ExchangerInfoVm> Handle(GetExchangerInfoQuery request, CancellationToken cancellationToken)
        {
            var exchanger = await _dbContext.Exchangers.FirstOrDefaultAsync(e => e.Name == request.ExchangerName);

            if (exchanger == null)
            {
                throw new NotFoundException(nameof(exchanger), request.ExchangerName);
            }

            var vm = _mapper.Map<ExchangerInfoVm>(exchanger);

            return vm;
        }
    }
}

