using System;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand
{
	public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
	{
		private readonly UserManager<AppUser> _userManager;
		//private readonly ILogger<CreateUserCommandHandler> _logger;

		public CreateUserCommandHandler(UserManager<AppUser> userManager)
			=> _userManager = userManager;

		public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
			var user = new AppUser
			{
				UserName = request.UserName,
				Email = request.Email
			};

			var result = await _userManager.CreateAsync(user, request.Password);

    //        if (!result.Succeeded)
    //        {
				//foreach(var error in result.Errors)
    //            {
				//	_logger.LogError(error.Description);
    //            }
				//throw new Exception("Invalid user creation attempt");
    //        }


			return user.Id;
        }
	}
}

