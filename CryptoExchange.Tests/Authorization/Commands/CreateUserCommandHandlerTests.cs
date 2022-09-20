using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CryptoExchange.Application.Common.JwtAuthentication;
using CryptoExchange.Application.Exchangers.Commands;
using CryptoExchange.Application.Interfaces;
using CryptoExchange.Application.UsersAuth.Commands.CreateUserCommand;
using CryptoExchange.Domain;
using CryptoExchange.Tests.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Shouldly;
using Xunit;

namespace CryptoExchange.Tests.Authorization.Commands
{
    public class CreateUserCommandHandlerTests : TestCommandBase
    {
        private readonly FakeJwtConfiguration _configuration;

        public CreateUserCommandHandlerTests()
        {
            _configuration = new FakeJwtConfiguration
            {
                Key = "W2DykK0cEkPAa8jqOkTamsvVIRRjbJmAM3ZbcgR5",
                Issuer = "https://localhost:7083"
            };
        }

        [Fact]
        public async Task CreateUserCommandHandlerTests_Succes()
        {
            //Arrange
            var userEmail = "sometest@gmail.com";
            var userName = "Thomas";
            var password = "Thomas123.";

            var userManager = MockHelper.TestUserManager<AppUser>();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var mockAuthService = new Mock<IAuthService>();
            mockAuthService.Setup(m => m.GenerateJwtToken(It.IsAny<AppUser>())).
                Returns(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken(_configuration.Issuer,
                _configuration.Issuer,
                new List<Claim>
                {
                    new (ClaimTypes.Name, userName),
                    new (ClaimTypes.Email, userEmail),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                },
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials)));

            var handler = new CreateUserCommandHandler(userManager, mockAuthService.Object);

            //Act
            var result = await handler.Handle(new CreateUserCommand
            {
                Email = userEmail,
                UserName = userName,
                Password = password
            },
            CancellationToken.None);

            //Assers
            result.ShouldBeOfType<AuthResult>();
            result.Success.ShouldBe(true);
            result.Token.ShouldNotBeEmpty();
            result.UserId.ShouldNotBeNullOrEmpty();

        }
    }
}

