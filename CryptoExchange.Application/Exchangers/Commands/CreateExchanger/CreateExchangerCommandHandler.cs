using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Exchangers.Commands.CreateExchanger;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommandHandler : IRequestHandler<CreateExchangerCommand, CreateExchangerResultVm>
	{
		private readonly ICryptoExchangeDbContext _dbContext;

		public CreateExchangerCommandHandler(ICryptoExchangeDbContext dbContext)
			=> _dbContext = dbContext;

		public async Task<CreateExchangerResultVm> Handle(CreateExchangerCommand request, CancellationToken cancellationToken)
        {

			var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

			if (user == null) throw new NotFoundException(nameof(AppUser), request.UserId);

			if(user.ExchangerId is not null)
            {
                return new CreateExchangerResultVm
                {
                    Success = false,
                    Error = "You already have created exchanger. There is no option to create couple exchangers for one user."
                };
            }

			var exchanger = new Exchanger
			{
				Id = Guid.NewGuid(),
				Name = request.Name,
				User = user,
				WebResuorceUrl = request.WebResourceUrl
			};
			user.Exchanger = exchanger;

			_dbContext.Users.Attach(user);
			_dbContext.Exchangers.Add(exchanger);

            try
            {
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch(Exception exception)
            {
				return new CreateExchangerResultVm
				{
					Success = false,
					Error = exception.Message
				};
            }

			return new CreateExchangerResultVm { Success = true, ExchangerId = exchanger.Id };
        }
	}
}

