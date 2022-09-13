using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommandHandler : IRequestHandler<CreateExchangerCommand, Guid>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		private readonly UserManager<AppUser> _userManager;
		private readonly ILogger<CreateExchangerCommandHandler> _logger;

		public CreateExchangerCommandHandler(ICryptoExchangeDbContext dbContext, UserManager<AppUser> userManager,
			ILogger<CreateExchangerCommandHandler> logger)
			=> (_dbContext, _userManager, _logger) = (dbContext, userManager, logger);

		public async Task<Guid> Handle(CreateExchangerCommand request, CancellationToken cancellationToken)
        {

			var user = await _userManager.FindByIdAsync(request.UserId);

			if (user == null) throw new Exception($"Cannot find with id: {request.UserId}");

			var exchanger = new Exchanger
			{
				Id = new Guid(),
				Name = request.Name,
				User = user,
				WebResuorceUrl = request.WebResourceUrl
			};
			user.Exchanger = exchanger;
			//exchanger.UserId = user.Id;
			_dbContext.Users.Attach(user);
			_dbContext.Exchangers.Add(exchanger);
			//user.Exchanger = exchanger;
			//_dbContext.Users.Append(user);
			await _dbContext.SaveChangesAsync(cancellationToken);

			return exchanger.Id;
        }
	}
}

