using System;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CryptoExchange.Api
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public abstract class BaseController : ControllerBase
	{
		private IMediator _mediator;
		protected IMediator Mediator =>
			_mediator ??= HttpContext.RequestServices.GetService<IMediator>();

		internal string UserId => !User.Identity.IsAuthenticated
			? string.Empty
			: User.FindFirst(ClaimTypes.NameIdentifier).Value;
	}
}

