using System;
using CryptoExchange.Application.Exchangers.Commands.CreateExchanger;
using MediatR;

namespace CryptoExchange.Application.Exchangers.Commands
{
	public class CreateExchangerCommand : IRequest<CreateExchangerResultVm>
	{
		public string UserId { get; set; }
		public string Name { get; set; }
		public string WebResourceUrl { get; set; }
	}
}

