using System;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.UsersAuth.Commands.LogoutUserCommand
{
	public class LogoutUserCommandHandler : IRequestHandler<LogoutUserCommand, string>
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly ILogger<LogoutUserCommandHandler> _logger;
		private readonly UserManager<AppUser> _userManager;

		public LogoutUserCommandHandler(SignInManager<AppUser> signInManager, ILogger<LogoutUserCommandHandler> logger,
			UserManager<AppUser> userManager)
		=> (_signInManager, _logger, _userManager) = (signInManager, logger, userManager);

		public async Task<string> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
			var user = await _userManager.FindByIdAsync(request.Id);

			if (user == null) throw new Exception($"Cannot find user with id {request.Id}");

			await _signInManager.SignOutAsync();
			return user.UserName;
        }
	}
}

