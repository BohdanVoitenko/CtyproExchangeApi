using System;
using CryptoExchange.Application.Common.JwtAuthentication;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthResult>
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IAuthService _authService;

		public CreateUserCommandHandler(UserManager<AppUser> userManager, IAuthService authService)
			=> (_userManager, _authService) = (userManager, authService);

		public async Task<AuthResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);
            var userByName = await _userManager.FindByNameAsync(request.UserName);

			if(userByEmail is not null || userByName is not null)
            {
				throw new Exception($"User with email {request.Email} or name {request.UserName} is already exists");
            }

            var user = new AppUser
			{
				UserName = request.UserName,
				Email = request.Email
			};

            var token = _authService.GenerateJwtToken(user);

            await _userManager.CreateAsync(user, request.Password);

            if (string.IsNullOrEmpty(user.Id))
            {
                return new AuthResult
                {
                    Success = false,
                    Error = "Server error occured"
                };
            }

            //        if (!result.Succeeded)
            //        {
            //return new AuthResult
            //{
            //	Success = false,
            //	Error = "Server error occured"
            //};
            //        }

            return new AuthResult
            {
				Success = true,
				Token = token,
				UserId = user.Id
            };
        }
	}
}

