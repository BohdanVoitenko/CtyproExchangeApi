using System;
using AutoMapper;
using CryptoExchange.Application.Common.Mappings;
using CryptoExchange.Domain;

namespace CryptoExchange.Application.Orders.Queries.GetAllOrdersByExchangerQuery
{
	public class AllByExchangerDto : IMapWith<Order>
	{
		public string Id { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public double IncomeSum { get; set; }
        public double OutcomeSum { get; set; }
		public double Amount { get; set; }
		public double MinAmount { get; set; }
		public double MaxAmount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Order, AllByExchangerDto>()
                .ForMember(listDto => listDto.From,
                opt => opt.MapFrom(order => order.ExchangeFrom))
                .ForMember(listDto => listDto.To,
                opt => opt.MapFrom(order => order.ExchangeTo))
                .ForMember(listDto => listDto.IncomeSum,
                opt => opt.MapFrom(order => order.IncomeSum))
                .ForMember(listDto => listDto.Amount,
                opt => opt.MapFrom(order => order.Amount))
                .ForMember(listDto => listDto.MinAmount,
                opt => opt.MapFrom(order => order.MinAmount))
                .ForMember(listDto => listDto.MaxAmount,
                opt => opt.MapFrom(order => order.MaxAmount))
                .ForMember(listDto => listDto.Id,
                opt => opt.MapFrom(order => order.Id));
        }
    }
}

