using System;
using CryptoExchange.Application.Common.Exceptions;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommandHandler : IRequestHandler<CreateExchangerCommand, Guid>
	{
		private readonly ICryptoExchangeDbContext _dbContext;
		//private readonly UserManager<AppUser> _userManager;


		public CreateExchangerCommandHandler(ICryptoExchangeDbContext dbContext)
			=> _dbContext = dbContext;

		public async Task<Guid> Handle(CreateExchangerCommand request, CancellationToken cancellationToken)
        {

			//var user = await _userManager.FindByIdAsync(request.UserId);
			var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == request.UserId);

			if (user == null) throw new NotFoundException(nameof(AppUser), request.UserId);

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

