using System;
using MediatR;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommand : IRequest<Guid>
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string WebResourceUrl { get; set; }
	}
}

