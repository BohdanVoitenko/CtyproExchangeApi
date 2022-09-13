using System;
using Microsoft.AspNetCore.Identity;
using MediatR;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly ILogger<LoginUserCommandHandler> _logger;

		public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager,
			ILogger<LoginUserCommandHandler> logger)
			=> (_signInManager, _userManager, _logger) = (signInManager, userManager, logger);

		public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
			var user = await _userManager.FindByIdAsync(request.Id);

			if (user == null) throw new Exception($"Cannot find user with id = {request.Id}");

			var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);

            if (!result.Succeeded)
            {
				_logger.LogError(result.ToString());
				throw new Exception("Invalid sign in attempt");
            }

			return user.UserName;
        }
	}
}

