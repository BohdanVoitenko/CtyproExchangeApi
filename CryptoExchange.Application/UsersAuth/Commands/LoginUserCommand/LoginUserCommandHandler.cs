using System;
using Microsoft.AspNetCore.Identity;
using MediatR;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using Microsoft.Extensions.Logging;
using CryptoExchange.Application.Common.JwtAuthentication;

namespace CryptoExchange.Application.UsersAuth.Commands.LoginUserCommand
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, AuthResult>
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IAuthService _authService;

		public LoginUserCommandHandler(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IAuthService authService)
			=> (_signInManager, _userManager, _authService) = (signInManager, userManager, authService);

		public async Task<AuthResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByEmailAsync(request.Email);

			if (user == null) throw new Exception($"Cannot find user with email = {request.Email}");

			var checkPasswordResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);


			if (checkPasswordResult.Succeeded)
			{
				var token = _authService.GenerateJwtToken(user);

				return new AuthResult
				{
					Success = true,
					Token = token,
					UserId = user.Id
				};
			}

			return new AuthResult
			{
				Success = false,
				Error = "Invalid login data"
			};
		}
	}
}

